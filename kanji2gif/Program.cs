using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using SharpVectors.Dom.Svg;
using SharpVectors.Renderers.Forms;

namespace Kanji2GIF
{
	public static class Program
	{
		private const string KANJIVG_XPATH = "/kanjis/kanji[@midashi='{0}']//stroke";
		private const int WIDTH = 109, HEIGHT = 109;

		private static string APP_DIR;

		private static Random m_Random;
		private static XmlDocument m_kanjiVG;
		private static XslCompiledTransform m_SmilTransform;

		private static bool m_IsGuiMode;
		private static string m_GuiWordListFile;

		[STAThread]
		public static void Main(string[] args)
		{
			PrintBanner();

			#region Program initalization
			Console.OutputEncoding = Encoding.UTF8;

			Application.ThreadException += new
				ThreadExceptionEventHandler(Application_ThreadException);

			APP_DIR = Path.GetDirectoryName(Application.ExecutablePath);

			m_kanjiVG = new XmlDocument();
			m_Random = new Random();

			m_kanjiVG.Load(string.Format(CultureInfo.InvariantCulture,
				"{0}{1}kanjivg.xml", APP_DIR, Path.DirectorySeparatorChar));

			m_SmilTransform = new XslCompiledTransform();
			using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Kanji2GIF.smil.xslt"))
				m_SmilTransform.Load(new XmlTextReader(s));
			#endregion

			string wordListFile, outputDir;
			bool useColors;
			double strokeDelay, finalDelay;

			#region Load GUI if no arguments given
			if (args.Length == 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Warning: No command line arguments given. Opening a GUI.");
				Console.ResetColor();

				m_IsGuiMode = true;

				Application.EnableVisualStyles();

				IWin32Window consoleWindow = new
					WindowHandleWrapper(Process.GetCurrentProcess().MainWindowHandle);

				using (MainForm mainForm = new MainForm())
				{
					if (mainForm.ShowDialog(consoleWindow) != DialogResult.OK)
						Environment.Exit(0);

					m_GuiWordListFile = Path.GetTempFileName();
					File.WriteAllText(m_GuiWordListFile,
						mainForm.wordlistTextBox.Text, Encoding.UTF8);

					List<string> newArgs = new List<string>();
					newArgs.Add(m_GuiWordListFile);
					newArgs.Add(mainForm.outDirTextBox.Text);

					if (mainForm.colorCheckBox.Checked)
						newArgs.Add("/c");

					newArgs.Add(string.Format(CultureInfo.CurrentCulture, "/s:{0}",
						mainForm.strokeDelayUpDown.Value));

					newArgs.Add(string.Format(CultureInfo.CurrentCulture, "/w:{0}",
						mainForm.loopDelayUpDown.Value));

					args = newArgs.ToArray();
				}
			}
			#endregion

			ParseArguments(ref args, out wordListFile, out outputDir, out useColors,
				out strokeDelay, out finalDelay);

			using (StreamReader sr = File.OpenText(wordListFile))
				while (!sr.EndOfStream)
				{
					string word = sr.ReadLine();

					if (string.IsNullOrEmpty(word.Trim()))
						continue;

					string outFile = string.Format(CultureInfo.InvariantCulture,
						"{0}{1}{2}.GIF", Path.GetFullPath(outputDir),
						Path.DirectorySeparatorChar, word);

					Console.WriteLine("Processing \"{0}\":", word);

					Convert(word, outFile, useColors, strokeDelay, finalDelay);

					Console.WriteLine();
				}

			Console.WriteLine("Finished.");

			if (m_IsGuiMode)
			{
				try { File.Delete(m_GuiWordListFile); }
				catch (Exception) { }

				// Clear key input buffer
				while (Console.KeyAvailable)
					Console.ReadKey(false);

				Console.WriteLine("Press any key to exit.");
				Console.ReadKey(false);
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			ShowError(e.Exception.Message);
		}

		private static void Convert(string text, string outputFile, bool colorful,
			double strokeDelay, double finalDelay)
		{
			string tempDir = Path.GetTempPath() + Path.DirectorySeparatorChar +
				Path.GetRandomFileName();

			Directory.CreateDirectory(tempDir);

			try
			{
				#region Build SVG
				Console.WriteLine("Building SMIL animation...");

				StringBuilder sb = new StringBuilder();

				sb.AppendFormat(SVG.Header, WIDTH * text.Length);

				int charIndex = 0;
				double animTimeline = 0;

				foreach (char c in text)
				{
					XmlNodeList nodes = m_kanjiVG.SelectNodes(string.Format(
						CultureInfo.InvariantCulture, KANJIVG_XPATH, c));

					if (nodes.Count == 0)
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
							"No stroke information found for \"{0}\".", c), "text");

					sb.AppendFormat(CultureInfo.InvariantCulture, SVG.TransformHeader,
						WIDTH * charIndex);

					sb.Append(SVG.Lines);

					foreach (XmlNode node in nodes)
					{
						string pathData = node.Attributes["path"].Value;
						double pathLength = GetPathLength(pathData);
						double animLength = pathLength / 75D; // Speed based on stroke length

						string color;

						if (colorful)
							color = RandomColor();
						else
							color = "#000000";

						sb.AppendFormat(CultureInfo.InvariantCulture, SVG.Stroke, color,
							pathLength, pathData, animTimeline, animLength);

						animTimeline += animLength + strokeDelay;
					}

					sb.Append(SVG.TransformFooter);
					charIndex++;
				}

				sb.Append(SVG.Footer);
				#endregion

				#region Build Individual Frames
				Console.Write("Building individual frames...");

				{
					XsltArgumentList argList = new XsltArgumentList();
					XPathDocument xpathDoc = new XPathDocument(new StringReader(sb.ToString()));

					double ctr = 0;
					int frame = 0;

					while (ctr < animTimeline)
					{
						ctr += (1D / 15D);

						argList.Clear();
						argList.AddParam("currentTime", "", ctr);

						string path = string.Format(CultureInfo.InvariantCulture,
							@"{0}{1}{2:00000}.SVG", tempDir, Path.DirectorySeparatorChar,
							frame);

						using (FileStream fs = File.OpenWrite(path))
							m_SmilTransform.Transform(xpathDoc, argList, fs);

						frame++;
					}

					Console.WriteLine(" {0:n0} frames created.", frame);
				}
				#endregion

				#region Create GIF
				{
					ProcessStartInfo pi = new ProcessStartInfo();
					pi.WindowStyle = ProcessWindowStyle.Hidden;
					pi.CreateNoWindow = false;
					pi.FileName = string.Format(CultureInfo.InvariantCulture,
						"{0}{1}convert.exe", APP_DIR, Path.DirectorySeparatorChar);

					string animationGif = string.Format(CultureInfo.InvariantCulture,
						"{0}{1}animation.gif", tempDir, Path.DirectorySeparatorChar);

					Console.WriteLine("Creating initial GIF image...");
					pi.Arguments = string.Format(CultureInfo.InvariantCulture,
						@"""{0}{1}00000.SVG"" ""{2}""", tempDir, Path.DirectorySeparatorChar,
						animationGif);

					Process.Start(pi).WaitForExit();

					Console.WriteLine("Merging frames into animation... (may take a long time)");
					pi.Arguments = string.Format(CultureInfo.InvariantCulture,
						@"-dispose previous -delay 10 -size {0}x109 -page +0+0 ""{1}{2}*.SVG"" ""{3}""",
						text.Length * WIDTH, tempDir, Path.DirectorySeparatorChar, animationGif);

					Process.Start(pi).WaitForExit();

					Console.WriteLine("Delaying final frame...");
					pi.Arguments = string.Format(CultureInfo.InvariantCulture,
						@"""{0}"" ( +clone -set delay {1} ) +swap +delete ""{0}""", animationGif,
						finalDelay * 100);

					Process.Start(pi).WaitForExit();

					Console.WriteLine("Optimizing animation...");
					pi.Arguments = string.Format(CultureInfo.InvariantCulture,
						@"""{0}"" -layers optimize -layers remove-dups ""{0}""", animationGif);

					Process.Start(pi).WaitForExit();

					File.Copy(animationGif, outputFile, true);
					File.Delete(animationGif);
				}
				#endregion
			}
			finally
			{
				Directory.Delete(tempDir, true);
			}
		}

		#region Utility Functions
		private static string RandomColor()
		{
			string ret = "#";

			for (int i = 0; i < 3; i++)
				ret += string.Format(CultureInfo.InvariantCulture, "{0:x}",
					(int)(m_Random.NextDouble() * 13));

			return ret;
		}

		private static double GetPathLength(string p)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(SVG.Header);
			sb.AppendFormat(CultureInfo.InvariantCulture, SVG.StrokeBasic, p);
			sb.AppendFormat(CultureInfo.InvariantCulture, SVG.Footer);

			SvgPictureBoxWindow w = new SvgPictureBoxWindow(WIDTH, HEIGHT, null);
			SharpVectors.Dom.Svg.SvgDocument d = new SharpVectors.Dom.Svg.SvgDocument(w);
			d.LoadXml(sb.ToString());

			return ((SvgPathElement)d.ChildNodes[0].ChildNodes[0]).GetTotalLength();
		}
		#endregion

		#region Command Line Functions
		private static void PrintBanner()
		{
			Console.Title = string.Format(CultureInfo.CurrentCulture, "{0} {1}",
				AssemblyAttributes.AssemblyTitle, AssemblyAttributes.AssemblyVersion);

			Console.WriteLine("{0} Version {1}", AssemblyAttributes.AssemblyTitle,
				AssemblyAttributes.AssemblyVersion);
			Console.WriteLine("Written by {0}", AssemblyAttributes.AssemblyCompany);

			Console.WriteLine("Uses ImageMagick by ImageMagick Studio LLC");
			Console.WriteLine("Uses KanjiVG data created by Ulrich Apel");
			Console.WriteLine("Uses SharpVectors by Paul Selormey");
			Console.WriteLine("Uses SMIL XSLT created by Holger Will");
			Console.WriteLine();
		}

		private static void PrintUsage()
		{
			Console.WriteLine("Usage: {0} [Wordlist] [OutDir] </c> </s:N> </w:N>",
				Path.GetFileName(Application.ExecutablePath));
			Console.WriteLine();

			Console.WriteLine("Wordlist - A UTF-8-encoded plain text file with one word per line.");
			Console.WriteLine("OutDir   - The directory to place created GIF images into.");
			Console.WriteLine("/c       - Makes every drawn stroke use a different color.");
			Console.WriteLine("/s:N     - Wait this many seconds between strokes (default: 0.5).");
			Console.WriteLine("/w:N     - Wait this many seconds before looping animation (default: 5).");

			Console.WriteLine();
		}

		private static void ParseArguments(ref string[] args, out string wordListFile, out string outputDir,
			out bool useColors, out double strokeDelay, out double finalDelay)
		{
			useColors = false;
			strokeDelay = 0.5D;
			finalDelay = 5D;

			if (args.Length < 2)
			{
				PrintUsage();
				ShowError("Missing wordlist or output directory.");
			}

			if (!File.Exists(args[0]))
				ShowError("Wordlist file not found at \"{0}\"", args[0]);

			wordListFile = args[0];

			if (!Directory.Exists(args[1]))
				ShowError("Output directory \"{0}\" does not exist", args[1]);

			outputDir = args[1];

			for (int i = 2; i < args.Length; i++)
			{
				string arg = args[i].ToUpperInvariant();

				if (arg == "/C")
					useColors = true;
				else if (arg.StartsWith("/S:"))
					strokeDelay = double.Parse(arg.Substring(3), CultureInfo.CurrentCulture);
				else if (arg.StartsWith("/W:"))
					finalDelay = double.Parse(arg.Substring(3), CultureInfo.CurrentCulture);
				else
				{
					PrintUsage();
					ShowError("Unknown argument \"{0}\"", arg);
				}
			}
		}

		private static void ShowError(string message, params object[] args)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Error.WriteLine("ERROR: " + message, args);
			Console.ResetColor();

			if (m_IsGuiMode)
			{
				try { File.Delete(m_GuiWordListFile); }
				catch (Exception) { }

				// Clear key input buffer
				while (Console.KeyAvailable)
					Console.ReadKey(false);

				Console.Write("Press any key to exit.");
				Console.ReadKey(false);
			}

			Environment.Exit(255);
		}
		#endregion
	}
}

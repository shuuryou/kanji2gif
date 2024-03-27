using ImageMagick;
using SharpVectors.Dom.Svg;
using SharpVectors.Renderers.Forms;
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

namespace Kanji2GIF
{
    public static class Program
	{
		private const string KANJIVG_XPATH = "/kanjivg/kanji[@id='kvg:kanji_{0:x5}']//path";

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
			int imageWidth, imageHeight;

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

                    newArgs.Add(string.Format(CultureInfo.InvariantCulture, "/imgw:{0}",
                        mainForm.widthHeightUpDown.Value));

                    newArgs.Add(string.Format(CultureInfo.InvariantCulture, "/imgh:{0}",
                        mainForm.widthHeightUpDown.Value));

                    args = newArgs.ToArray();
				}
			}
			#endregion

			ParseArguments(ref args, out wordListFile, out outputDir, out useColors,
				out strokeDelay, out finalDelay, out imageWidth, out imageHeight);

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

					Convert(word, outFile, useColors, strokeDelay, finalDelay, imageWidth, imageHeight);

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
			double strokeDelay, double finalDelay, int imageWidth, int imageHeight)
		{
			string tempDir = Path.GetTempPath() + Path.DirectorySeparatorChar +
				Path.GetRandomFileName();

			Directory.CreateDirectory(tempDir);

			try
			{
				#region Build SVG
				Console.WriteLine("Building SMIL animation...");

				StringBuilder sb = new StringBuilder();

				sb.AppendFormat(SVG.Header, imageWidth * text.Length, imageHeight);

				int charIndex = 0;
				double animTimeline = 0;

				foreach (char c in text)
				{
					XmlNodeList nodes = m_kanjiVG.SelectNodes(string.Format(
						CultureInfo.InvariantCulture, KANJIVG_XPATH, (int)c));

					if (nodes.Count == 0)
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
							"No stroke information found for \"{0}\".", c), "text");

					{
						double scalex = imageWidth / 109D;
                        double scaley = imageHeight / 109D;
						sb.AppendFormat(CultureInfo.InvariantCulture, SVG.TransformHeader,
							imageWidth * charIndex, scalex , scaley);
					}

                    sb.AppendFormat(SVG.Lines);

					foreach (XmlNode node in nodes)
					{
						string pathData = node.Attributes["d"].Value;
						double pathLength = GetPathLength(pathData, imageWidth, imageHeight);
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

				int total_frames = 0;

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
					total_frames = frame;
				}
				#endregion

				#region Create GIF
				{
					string animationGif = string.Format(CultureInfo.InvariantCulture,
						"{0}{1}animation.gif", tempDir, Path.DirectorySeparatorChar);

					// This was rewritten in 2022 (11 years after I made this!) to use Magick.NET
					// instead of launching convert.exe three times. Not that anyone cares...

					using (MagickImageCollection collection = new MagickImageCollection())
					{
						Console.WriteLine("Composing GIF animation from frames.");

						for (int i = 0; i < total_frames; ++i)
						{
							collection.Add(string.Format(CultureInfo.InvariantCulture, @"{0}{1}{2:00000}.SVG", tempDir, Path.DirectorySeparatorChar, i));
							collection[i].AnimationDelay = ((i == total_frames - 1 ) ? (int)(finalDelay * 100) : 10);
							collection[i].GifDisposeMethod = GifDisposeMethod.Previous;
							collection[i].Page.X = collection[i].Page.Y = 0;
							collection[i].Resize(text.Length * imageWidth, imageHeight);
						}

						Console.WriteLine("Quantizing GIF. Takes a long time.");
						QuantizeSettings settings = new QuantizeSettings();
						settings.Colors = 256;
						collection.Quantize(settings);

						Console.WriteLine("Optimizing GIF. Takes a long time.");
						collection.OptimizePlus();

						Console.WriteLine("Writing GIF file to disk.");
						collection.Write(animationGif, MagickFormat.Gif);
					}

					Console.WriteLine("Moving GIF to destination.");
					File.Copy(animationGif, outputFile, true);
					File.Delete(animationGif);

					Console.WriteLine("Complete!");
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

		private static double GetPathLength(string p, int imageWidth, int imageHeight)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(SVG.Header);
			sb.AppendFormat(CultureInfo.InvariantCulture, SVG.StrokeBasic, p);
			sb.AppendFormat(CultureInfo.InvariantCulture, SVG.Footer);

			SvgPictureBoxWindow w = new SvgPictureBoxWindow(imageWidth, imageWidth, null);
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

			Console.WriteLine();
			Console.WriteLine("Attribution statements:");
			Console.WriteLine("KanjiVG copyright (c) Ulrich Apel. Licensed under the Creative Commons Attribution-Share Alike 3.0 license.");
			Console.WriteLine("SharpVectors library copyright (c) Elinam LLC. All rights reserved. Licensed under BSD 3-Clause License.");
			Console.WriteLine("Magick.NET library copyright (c) Dirk Lemstra. Licensed under the Apache License, Version 2.0.");
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
			Console.WriteLine("/imgw:N  - Width of each character in pixels (default: 109).");
			Console.WriteLine("/imgh:N  - Height of each character in pixels (default: 109).");

			Console.WriteLine();
		}

		private static void ParseArguments(ref string[] args, out string wordListFile, out string outputDir,
			out bool useColors, out double strokeDelay, out double finalDelay, out int imageWidth, out int imageHeight)
		{
			useColors = false;
			strokeDelay = 0.5D;
			finalDelay = 5D;
			imageWidth = 109;
			imageHeight = 109;

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
				else if (arg.StartsWith("/IMGW:"))
					imageWidth = int.Parse(arg.Substring(6), NumberStyles.None, CultureInfo.InvariantCulture);
				else if (arg.StartsWith("/IMGH:"))
					imageHeight = int.Parse(arg.Substring(6), NumberStyles.None, CultureInfo.InvariantCulture);
				else
				{
					PrintUsage();
					ShowError("Unknown argument \"{0}\"", arg);
				}
			}

			if (imageWidth != imageHeight)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Error.WriteLine("WARNING You've specified image dimensions that aren't square.");
				Console.Error.WriteLine("I won't stop you, but the output will probably be weird.");
				Console.ResetColor();
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

using System.IO;
using System.Reflection;

namespace Kanji2GIF
{
	public static class AssemblyAttributes
	{
		public static string AssemblyTitle
		{
			get
			{
				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyTitleAttribute), false);

				if (attributes.Length > 0)
				{
					AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];

					if (!string.IsNullOrEmpty(titleAttribute.Title))
						return titleAttribute.Title;
				}

				return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
			}
		}

		public static string AssemblyVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		public static string AssemblyCompany
		{
			get
			{

				object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(
					typeof(AssemblyCompanyAttribute), false);

				if (attributes.Length == 0)
					return string.Empty;

				return ((AssemblyCompanyAttribute)attributes[0]).Company;
			}
		}
	}
}
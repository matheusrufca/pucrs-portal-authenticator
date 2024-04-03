using System.Text;
using System.Xml;

namespace Utilities.Xml
{
	public class XmlDefaultSettings
	{
		public static Encoding GetDefaultEncoding()
		{
			return new UTF8Encoding(false);
		}

		public static XmlWriterSettings GetDefaultXmlSettings(Encoding encoding = null, bool indent = true, string indentChars = null, string newLineChars = null, NewLineHandling? newLineHandling = null)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();

			xmlWriterSettings.Encoding = encoding ?? GetDefaultEncoding();
			xmlWriterSettings.Indent = indent;
			xmlWriterSettings.IndentChars = indentChars ?? "\t";
			xmlWriterSettings.NewLineChars = newLineChars ?? "\r\n";
			xmlWriterSettings.NewLineHandling = newLineHandling ?? NewLineHandling.Replace;
			//xmlWriterSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;

			return xmlWriterSettings;
		}
	}
}
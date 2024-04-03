using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities.Xml
{
	public static class XmlParser
	{
		public static T DeserializeXml<T>(string xmlString, Encoding encoding = null)
		{
			XmlSerializer xmlSerializer = null;
			XmlReader xmlReader = null;
			T outputObject = default(T);

			encoding = encoding ?? XmlDefaultSettings.GetDefaultEncoding();

			if (Strings.HasValue(xmlString))
			{
				// Create an instance of the XmlSerializer specifying type and namespace.
				xmlSerializer = new XmlSerializer(typeof(T));

				// A Memory is needed to read the XML document.
				using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(xmlString)))
				{
					try
					{
						xmlReader = XmlReader.Create(memoryStream);
						outputObject = (T)xmlSerializer.Deserialize(xmlReader);
					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
			}
			return outputObject;
		}
	}
}
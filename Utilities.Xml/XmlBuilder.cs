using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities.Xml
{
	public class XmlBuilder
	{
		public string BuildDocument<T>(T inputObject)
		{
			string xmlDocument = null;

			//TODO: Consistências criação xml

			xmlDocument = this.SerializeObject(inputObject);

			//TODO: Validar xml

			return xmlDocument;
		}

		//Create XML from object
		public string SerializeObject<T>(T inputObject, Encoding encoding = null)
		{
			#region Variáveis

			string result = null;
			XmlWriter xmlWriter = null;
			XmlWriterSettings xmlWriterSettings = null;
			XmlSerializer xmlSerializer = null;

			#endregion

			encoding = encoding ?? XmlDefaultSettings.GetDefaultEncoding();
			
			using (var memoryStream = new MemoryStream())
			{
				xmlWriterSettings = XmlDefaultSettings.GetDefaultXmlSettings(encoding);
				
				try
				{
					//Informa o tipo de objeto
					xmlSerializer = new XmlSerializer(typeof(T));

					xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
					xmlSerializer.Serialize(xmlWriter, inputObject);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					if (xmlWriter != null)
					{
						xmlWriter.Flush();
						xmlWriter.Dispose();
					}
					result = encoding.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
				}
			}
			return result;
		}
	}
}
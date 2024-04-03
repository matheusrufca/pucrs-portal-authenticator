using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Utilities.Web
{
	public static class Utils
	{
		public static Encoding GetDefaultEncoding()
		{
			return new UTF8Encoding(false);
		}

		public static string GetStringFromStream(Stream stream, Encoding encoding = null)
		{
			string result = null;

			encoding = encoding ?? new UTF8Encoding(false);

			if (stream != null)
			{
				using (var streamReader = new StreamReader(stream, encoding))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		public static Dictionary<string, string> ParseQueryString(string parameters)
		{
			Dictionary<string, string> result = new Dictionary<string, string>(0);

			if (!string.IsNullOrWhiteSpace(parameters))
				result = HttpUtility.ParseQueryString(parameters);

			return result;
		}

		public static string ParseQueryString(Dictionary<string, string> parameters)
		{
			string param = string.Empty;

			if (parameters != null && parameters.Count > 0)
			{
				var queryItens = parameters.Keys.Select(x => string.Concat(x, "=", parameters[x]));

				param = string.Join("&", queryItens);
			}

			return param;
		}


		public static byte[] QueryStringToByteArray(string parameters, Encoding encoding = null)
		{
			byte[] result = new byte[0];

			encoding = encoding ?? new UTF8Encoding(false);

			if (!string.IsNullOrWhiteSpace(parameters))
				result = encoding.GetBytes(parameters);

			return result;
		}

		public static byte[] QueryStringToByteArray(Dictionary<string, string> parameters, Encoding encoding = null)
		{
			byte[] result = new byte[0];
			string parametersString = ParseQueryString(parameters);

			return QueryStringToByteArray(parametersString, encoding);
		}
	}
}
using System;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Utilities
{
	public static class Strings
	{
		public static bool HasValue(string input)
		{
			bool HasValue = false;

			if (input == null)
				return false;
			if (input.Length <= 0)
				return false;

			for (int i = 0; i < input.Length; i++)
			{
				if (!char.IsWhiteSpace(input, i))
				{
					HasValue = true;
					break;
				}
			}
			return HasValue;

		}

		public static string GenerateNumericCode(int length = 6)
		{
			StringBuilder output = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < length; i++)
			{
				output.Append(random.Next(0, 9));
			}
			return output.ToString();
		}

		public static bool IsNumeric(string input, bool parseValue =false)
		{
			bool resp = true;

			if (parseValue)
			{
				decimal value;
				resp = decimal.TryParse(input, out value);
			}
			else
			{
				input = input.Replace(".", "");
				input = input.Replace(",", "");

				foreach (char c in input)
				{
					if (!char.IsNumber(c))
						return false;
				}
			}

			return resp;
		}

		public static string Trim(string input, bool returnEmpty = false)
		{
			if (input != null)
				return input.Trim();

			if (returnEmpty)
				return string.Empty;

			return input;
		}

		public static string ToString(object input, bool returnEmpty = false)
		{
			if (input != null)
				return input.ToString();

			if (returnEmpty)
				return string.Empty;

			return null;
		}

		public static string Substring(object input, int startIndex, int length)
		{
			string Output = null;

			if (input != null)
			{
				Output = input.ToString();

				if (startIndex >= Output.Length)
					return null;

				if ((length - startIndex) >= (Output.Length - startIndex))
					length = Output.Length - startIndex - 1;

				return Output.Substring(startIndex, length);
			}
			return null;
		}
	}
}
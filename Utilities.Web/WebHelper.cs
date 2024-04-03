using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Utilities.Web
{
	public static class WebHelper
	{
		public static WebResponse SendPOSTRequest(string url)
		{
			#region Variáveis

			WebResponse result = null;

			#endregion

			result = SendPOSTRequest(url, (NameValueCollection)null, Utils.GetDefaultEncoding());

			return result;
		}

		public static WebResponse SendPOSTRequest(string url, string parameters, Encoding encoding = null)
		{
			#region Variáveis

			NameValueCollection param = Utils.ParseQueryString(parameters);
			WebResponse result = null;

			#endregion

			result = SendPOSTRequest(url, param, encoding);

			return result;
		}

		public static WebResponse SendPOSTRequest(string url, NameValueCollection parameters, Encoding encoding = null)
		{
			#region Variáveis

			WebResponse result = new WebResponse();
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse httpWebResponse = null;
			string responseText = null;

			#endregion

			encoding = encoding ?? Utils.GetDefaultEncoding();

			#region Request_Response

			byte[] postData = Utils.QueryStringToByteArray(parameters, encoding);

			try
			{
				#region Request

				httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = RequestType.POST;
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				httpWebRequest.ContentLength = postData.Length;

				/* Send data. */
				using (Stream stream = httpWebRequest.GetRequestStream())
				{
					stream.Write(postData, 0, postData.Length);
				}

				#endregion

				#region Response

				/* Do request to get response */
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				result.Response = httpWebResponse;
				result.HttpStatusCode = httpWebResponse.StatusCode;
				result.HttpStatusDescription = httpWebResponse.StatusDescription;

				try
				{
					responseText = Utils.GetStringFromStream(httpWebResponse.GetResponseStream(), encoding);
				}
				catch (WebException ex)
				{
					if (ex.Response != null)
					{
						responseText = Utils.GetStringFromStream(ex.Response.GetResponseStream(), encoding);
					}
					throw ex;
				}
				finally
				{
					result.ResponseText = responseText;
				}

				#endregion
			}
			catch (Exception ex)
			{
				result.ErrorMessage = ex.Message;
			}
			finally
			{
				if (httpWebResponse != null)
					httpWebResponse.Dispose();
			}

			#endregion

			return result;
		}


		public static WebResponse SendGETRequest(string url)
		{
			#region Variáveis

			WebResponse result = null;

			#endregion

			result = WebHelper.SendGETRequest(url, (NameValueCollection)null, Utils.GetDefaultEncoding());

			return result;
		}

		public static WebResponse SendGETRequest(string url, string parameters, Encoding encoding = null)
		{
			#region Variáveis

			NameValueCollection param = Utils.ParseQueryString(parameters);
			WebResponse result = null;

			#endregion

			result = WebHelper.SendGETRequest(url, param, encoding);

			return result;
		}
		
		public static WebResponse SendGETRequest(string url, NameValueCollection parameters, Encoding encoding = null)
		{
			#region Variáveis

			WebResponse result = new WebResponse();
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse httpWebResponse = null;
			string responseText = null;
			string queryString = Utils.ParseQueryString(parameters);

			#endregion

			encoding = encoding ?? Utils.GetDefaultEncoding();

			if (!string.IsNullOrWhiteSpace(queryString))
				url = string.Concat(url, "?", queryString);

			#region Request_Response

			try
			{
				#region Request

				httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = RequestType.GET;

				#endregion

				#region Response

				/* Do request to get response */
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				result.Response = httpWebResponse;
				result.HttpStatusCode = httpWebResponse.StatusCode;
				result.HttpStatusDescription = httpWebResponse.StatusDescription;

				try
				{
					responseText = Utils.GetStringFromStream(httpWebResponse.GetResponseStream(), encoding);
				}
				catch (WebException ex)
				{
					responseText = Utils.GetStringFromStream(ex.Response.GetResponseStream(), encoding);

					throw ex;
				}
				finally
				{
					result.ResponseText = responseText;
				}

				#endregion
			}
			catch (Exception ex)
			{
				result.ErrorMessage = ex.Message;
			}
			finally
			{
				if (httpWebResponse != null)
					httpWebResponse.Dispose();
			}
			#endregion

			return result;
		}


		public static bool CheckInternetConnection()
		{
			bool resp = false;

			resp = NetworkInterface.GetIsNetworkAvailable();
			
			return resp;
		}
		
		public static string IncludeHttpInUrlString(string url)
		{
			string result = null;
			if (Strings.HasValue(url))
			{

				if (url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
					result = url;
				else
					result = "http://" + url;
			}
			return result;
		}
	}
}
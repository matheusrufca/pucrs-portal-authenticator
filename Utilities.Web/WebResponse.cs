using System.Net;

namespace Utilities.Web
{
	public class WebResponse
	{
		public HttpStatusCode HttpStatusCode { get; set; }
		public HttpWebResponse Response { get; set; }

		public string HttpStatusDescription { get; set; }
		public string ResponseText { get; set; }
		public string ErrorMessage { get; set; }
	}
}
using PUCRS_Logger.Messages;
using System;
using System.Collections.Specialized;
using Utilities;
using Utilities.Web;


namespace PUCRS_Logger
{
	public partial class Logger
	{

		public bool IsUserLogged { get; private set; }
		private readonly string UrlLogin;
		private readonly string UrlLogout;
		private readonly string UrlTest;


		public Logger()
		{
			this.UrlLogin = "https://wifiportal.pucrs.br/login.html";
			this.UrlLogout = "https://wifiportal.pucrs.br/logout.html";
			this.UrlTest = "http://www.apple.com/library/test/success.html";
			this.IsUserLogged = false;
		}


		public void Login(string username, string password)
		{
			bool couldLogin = false;
			string errorMessage = null;

			var parameters = new NameValueCollection(){
					{"username", username},
					{"password", password},
					{"redirect_url", null},
					{"buttonClicked", "4"},
					{"err_flag", "0"}
				};

			if (this.IsLoginNeeded())
			{
				var response = WebHelper.SendPOSTRequest(this.UrlLogin, parameters);

				couldLogin = this.CouldLogin(response.ResponseText, out errorMessage);

				this.IsUserLogged = couldLogin;
			}
			else
			{
				errorMessage = "Você já estava logado, babaca";
			}


			if(Strings.HasValue(errorMessage))
				throw new Exception(errorMessage);
		}

		public void Logout()
		{
			bool couldLogoff = false;
			string errorMessage = null;

			var parameters = new NameValueCollection(){
					{"userStatus", "1"},
					{"err_flag", "0"},
					{"err_msg", null}
				};

			if (!this.IsLoginNeeded())
			{
				var response = WebHelper.SendPOSTRequest(this.UrlLogout, parameters);

				couldLogoff = this.CouldLogout(response.ResponseText, out errorMessage);

				if (!couldLogoff)
					errorMessage = ErrorMessages.GenericMessage;

				this.IsUserLogged = !couldLogoff;
			}
			else
			{
				errorMessage = ErrorMessages.AlreadyLoggedOut;
			}
			
			if (Strings.HasValue(errorMessage))
				throw new Exception(errorMessage);
		}

		public bool CouldLogin(string htmlPage, out string errorMessage)
		{
			bool resp = false;
			errorMessage = null;

			if (Strings.HasValue(htmlPage))
			{
				if (htmlPage.Contains("Login Successful"))
				{
					resp = true;
				}
				else if (htmlPage.Contains("<h1>Login</h1>"))
				{
					resp = false;
					errorMessage = ErrorMessages.InvalidUsernameOrPassword;
				}
			}

			return resp;
		}

		public bool CouldLogout(string htmlPage, out string errorMessage)
		{
			bool resp = false;
			errorMessage = null;

			if (Strings.HasValue(htmlPage))
			{
				if (htmlPage.Contains("To complete the log off"))
				{
					resp = true;
				}
				else
				{
					resp = false;
					errorMessage = ErrorMessages.CouldNotLogout;
				}
			}

			return resp;
		}

		public bool IsLoginNeeded()
		{
			bool resp = false;
			var response = WebHelper.SendGETRequest(this.UrlTest);

			/* Verifica está conectado à internet */
			if (WebHelper.CheckInternetConnection())
			{
				/* Verifica se necessita fazer login */
				if (response.ResponseText != null && !response.ResponseText.Contains("Success"))
				{
					resp = true;
					this.IsUserLogged = false;
				}
				else { this.IsUserLogged = true; }
			}
			else
			{
				this.IsUserLogged = false;
				throw new Exception(ErrorMessages.NotConnected);
			}
			return resp;
		}
	}	
}

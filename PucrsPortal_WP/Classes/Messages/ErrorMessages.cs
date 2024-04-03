using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCRS_Logger.Messages
{
	public struct ErrorMessages
	{
		public static string NotConnected = "Véi, você nem ao menos tá conectado na rede.";
		public static string AlreadyLoggedIn = "Você já estava logado, babaca.";
		public static string AlreadyLoggedOut = "Cara, você nem tava logado.";
		public static string InvalidUsernameOrPassword = "Usuário ou senha incorretos.";
		public static string CouldNotLogout = "Não foi possível efetuar o logout.";
		public static string GenericMessage = "Acho que não rolou.";
	}
}

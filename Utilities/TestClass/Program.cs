using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Web;

namespace TestClass
{
	class Program
	{
		static void Main(string[] args)
		{
			//Network network = new Network();
			bool teste = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
			var response = WebHelper.SendGETRequest("http://www.google.com");

			;
		}
	}
}

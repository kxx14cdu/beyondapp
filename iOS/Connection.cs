using System;
using Xamarin.Forms;
using bfbnet;
using SystemConfiguration;
using System.Net;

using bfbnet.iOS:

[assembly: Xamarin.Forms.Dependency (typeof (Connection))]
namespace bfbnet.iOS
{
	public class Connection : IConnection
	{
		public static string HostName = "www.beyondfleshandbloodgame.com";

		public Connection ()
		{
			NetworkReachability = new NetworkReachability (new IPAddress (0));

		}
	}
}


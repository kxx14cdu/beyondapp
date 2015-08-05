using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace bfbnet.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			UIApplication.CheckForEventAndDelegateMismatches = false;
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}


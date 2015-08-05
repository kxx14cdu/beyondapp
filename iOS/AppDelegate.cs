using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms.Labs.Services;
using Xamarin.Forms.Labs;
using Xamarin.Forms;

namespace bfbnet.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			LoadApplication (new App ());

			UIApplication.SharedApplication.SetStatusBarHidden (false, false);

			UITextAttributes attr = UINavigationBar.Appearance.GetTitleTextAttributes();
			UITextAttributes attr2 = UIBarButtonItem.Appearance.GetTitleTextAttributes(UIControlState.Normal);

			attr.Font = UIFont.FromName ("Orbitron", 18);
			attr2.Font = UIFont.FromName ("Orbitron", 18);

			UIBarButtonItem.Appearance.SetTitleTextAttributes(attr2, UIControlState.Normal);
			UINavigationBar.Appearance.SetTitleTextAttributes(attr);

			return base.FinishedLaunching (app, options);
		}
	}
}


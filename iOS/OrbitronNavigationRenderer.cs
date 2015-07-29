using System;
using Xamarin.Forms;
using bfbnet;
using bfbnet.iOS;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(OrbitronNavigationRenderer))]

namespace bfbnet.iOS
{
	public class OrbitronNavigationRenderer : NavigationRenderer
	{
		public override void ViewDidLoad() {
			base.ViewDidLoad ();
			UIStringAttributes myTextAttrib = new UIStringAttributes ();

			myTextAttrib.Font = UIFont.FromName ("Orbitron", 18);
			myTextAttrib.ForegroundColor = UIColor.White;

			this.NavigationItem.BackBarButtonItem = new UIBarButtonItem ("Back", 
				UIBarButtonItemStyle.Plain, null);

			this.NavigationBar.TitleTextAttributes = myTextAttrib;
			UITextAttributes myTextAttrib2 = new UITextAttributes ();
			myTextAttrib2.Font = UIFont.FromName ("Orbitron", 18);
		}
	}
}


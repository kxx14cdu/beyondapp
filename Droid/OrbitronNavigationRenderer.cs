using System;
using bfbnet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;
using bfbnet.Droid;
using Android.App;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(OrbitronNavigationRenderer))]


namespace bfbnet.Droid
{
	public class OrbitronNavigationRenderer : NavigationRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
		{
			base.OnElementChanged (e);

			RemoveAppIconFromActionBar ();
		}

		void RemoveAppIconFromActionBar()
		{
			var actionBar = ((Activity)Context).ActionBar;
			actionBar.SetIcon (new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()));
		}
	}
}


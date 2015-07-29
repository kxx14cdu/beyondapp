using System;
using Xamarin.Forms;
using Xamarin.Forms.Labs;

namespace bfbnet
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage (new LoadingPage ()) {
				BarBackgroundColor = Color.FromHex("007DA1"),
				BarTextColor = Color.White
			};
		}

		protected override void OnStart ()
		{
		}

		protected override void OnSleep ()
		{
		}

		protected override void OnResume ()
		{
		}

	}
}


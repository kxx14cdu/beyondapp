using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage (new LoadingPage ()) {
				BarBackgroundColor = Color.FromHex("0A4852"),
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


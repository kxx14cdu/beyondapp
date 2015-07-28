using System;
using Xamarin.Forms;

namespace bfb.net
{
	public class App : Application
	{
		public App ()
		{
			LoadingPage mainapplication = new LoadingPage ();
			MainPage = mainapplication;
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

s
using System;
using Xamarin.Forms;
using Foundation;
using bfbnet;
using bfbnet.iOS;

[assembly: Dependency (typeof (BaseUrl_iOS))]
namespace bfbnet.iOS {
	public class BaseUrl_iOS : IBaseUrl {
		public string Get () {
			return NSBundle.MainBundle.BundlePath;
		}
	}
}
using Xamarin.Forms;
using bfbnet;

[assembly: Dependency (typeof (WorkingWithWebview.Android.BaseUrl_Android))]
namespace WorkingWithWebview.Android {
	public class BaseUrl_Android : IBaseUrl {
		public string Get () {
			return "file:///android_asset/";
		}
	}
}
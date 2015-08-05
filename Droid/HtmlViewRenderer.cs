using System;
using System.ComponentModel;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Telephony;
using Android.Views;
using Android.Webkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Uri = Android.Net.Uri;
using WebView = Android.Webkit.WebView;
using bfbnet;
using bfbnet.Droid;
using Android.Text;
using Android.Widget;
using Android.Text.Util;
using System.Text.RegularExpressions;

[assembly: ExportRenderer(typeof(HtmlView), typeof(HtmlViewRenderer))]
namespace bfbnet.Droid
{
	public class HtmlViewRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var element = (HtmlView)e.NewElement;
				this.Control.AutoLinkMask = MatchOptions.WebUrls;
				this.Control.LinksClickable = true;

				String text = null;
				if (element.Text.Contains ("table")) {
					text = Regex.Replace (element.Text, "</tr>", System.Environment.NewLine);
					text = Regex.Replace (text, "</td>", System.Environment.NewLine);
					text = Regex.Replace (Regex.Replace (text, "&[^\\s]*;", String.Empty), @"<.*?>", String.Empty);
				} else {
					text = Regex.Replace (Regex.Replace (element.Text, "&[^\\s]*;", String.Empty), @"<.*?>", String.Empty);
				}
				this.Control.SetText(text,null);
			}
		}
	}
}
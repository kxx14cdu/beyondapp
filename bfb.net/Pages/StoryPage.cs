using System;
using Xamarin.Forms;
using System.IO;
using System.Net;

namespace bfbnet
{
	public class StoryPage : ContentPage
	{
		public StoryPage (BeyondRootModel Page)
		{
			StackLayout pageContent = new StackLayout ();

			ScrollView scrollContent = new ScrollView {
				Content = pageContent
			};

			Image storyImage = new Image ();

			Image backgroundImage = new Image () {
				Source = "2.jpg"
			};

			this.Title = Page.pageName;

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.slideRightImage);

			storyImage.Source = ImageSource.FromStream (() => new MemoryStream (ImageBase64));

			var browser = new TransparentWebView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var htmlSource = new HtmlWebViewSource ();

			htmlSource.Html = @"<link href='http://fonts.googleapis.com/css?family=Orbitron:400,700' rel='stylesheet' type='text/css'><style>body{font-family: 'Orbitron', sans-serif;}</style>" + Page.slideContent;

			browser.Source = htmlSource;

			this.Content = pageLayout;

			pageContent.Children.Add (storyImage);
			pageContent.Children.Add (browser); 
		}
	}
}


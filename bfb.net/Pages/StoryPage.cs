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

			Image storyImage = new Image () {
				Aspect = Aspect.AspectFit
			};

			Image backgroundImage = new Image () {
				Source = "2.jpg",
				Aspect = Aspect.AspectFill
			};

			this.Title = Page.pageName;

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.slideRightImage);

			storyImage.Source = ImageSource.FromStream (() => new MemoryStream (ImageBase64));

			ContentView webviewholder = new ContentView () {
				BackgroundColor = Color.FromRgba(0,74,97,0.9),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			ContentView imageholder = new ContentView () {
				BackgroundColor = Color.FromRgba(0,31,49,0.9),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var browser = new TransparentWebView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent
			};

			var htmlSource = new HtmlWebViewSource ();

			htmlSource.Html = @"<link href='http://fonts.googleapis.com/css?family=Orbitron:400,700' rel='stylesheet' type='text/css'><style>body{font-family: 'Orbitron', sans-serif;color:#fff;overflow-x:hidden;}</style>" + Page.slideContent;

			browser.Source = htmlSource;

			webviewholder.Content = browser;
			imageholder.Content = storyImage;

			RelativeLayout layout = new RelativeLayout ();

			layout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
				return parent.Width;
			}), 
				Constraint.RelativeToParent ((parent) => {
				return parent.Height;
			}));

			layout.Children.Add (scrollContent, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			pageContent.Children.Add (imageholder);
			pageContent.Children.Add (webviewholder); 

			this.Content = layout;
		}
	}
}


using System;
using Xamarin.Forms;
using System.IO;
using System.Net;
using Xamarin.Forms.Labs;
using Acr.DeviceInfo;

namespace bfbnet
{
	public class StoryPage : ContentPage
	{
		public StoryPage (BeyondRootModel Page)
		{

			

			StackLayout pageContent = new StackLayout ();

			ScrollView scrollContent = new ScrollView {
				Content = pageContent,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill
			};

			Image storyImage = new Image () {
				Scale = 1.2
			};

			Image backgroundImage = new Image () {
				Source = "2.jpg",
				Aspect = Aspect.AspectFill
			};

			this.Title = Page.pageName;

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.slideRightImage);

			storyImage.Source = ImageSource.FromStream (() => new MemoryStream (ImageBase64));

			Color background = new Color (0, 0, 0, 0.5);
			Color background_alt = new Color (0, 0, 0, 0.5);

			ContentView webviewholder = new ContentView () {
				BackgroundColor = background_alt,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				WidthRequest = DeviceInfo.Instance.ScreenWidth
			};

			ContentView imageholder = new ContentView () {
				BackgroundColor = background,
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var browser = new TransparentWebView () {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
				WidthRequest = DeviceInfo.Instance.ScreenWidth
			};

			var htmlSource = new HtmlWebViewSource ();

			htmlSource.Html = @"<meta name=""viewport"" content=""width=device-width"" /><link href='http://fonts.googleapis.com/css?family=Orbitron:400,700' rel='stylesheet' type='text/css'><style>body{font-family: 'Orbitron', sans-serif;color:#fff;overflow-x:hidden;}</style>" + Page.slideContent;

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


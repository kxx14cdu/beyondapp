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

			Grid pageContent = new Grid {
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			ScrollView scrollContent = new ScrollView {
				Content = pageContent,
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
				VerticalOptions = LayoutOptions.FillAndExpand,
				WidthRequest = DeviceInfo.Instance.ScreenWidth
			};

			ContentView imageholder = new ContentView () {
				BackgroundColor = background,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var browser = new TransparentWebView () {
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent
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
					
			pageContent.Children.Add (imageholder, 0, 0);
			pageContent.Children.Add (webviewholder, 0, 1); 

			this.Content = layout;
		}
	}
}


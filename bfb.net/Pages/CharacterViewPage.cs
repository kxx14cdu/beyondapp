using System;
using Xamarin.Forms;
using System.IO;
using System.Net;

namespace bfbnet
{
	public class CharacterViewPage : ContentPage
	{
		public CharacterViewPage (BeyondCharacterModel Page)
		{
			StackLayout pageContent = new StackLayout ();

			NavigationPage.SetBackButtonTitle (this, "Back");

			ScrollView scrollContent = new ScrollView {
				Content = pageContent
			};

			Image storyImage = new Image () {
				Scale = 0.5
			};

			Image backgroundImage = new Image () {
				Source = "4.jpg",
				Aspect = Aspect.AspectFill
			};

			this.Title = Page.pageName;

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.characterRightHandSideImage);

			storyImage.Source = ImageSource.FromStream (() => new MemoryStream (ImageBase64));

			Color background = new Color (0, 0, 0, 0.5);
			Color background_alt = new Color (0, 0, 0, 0.5);

			ContentView webviewholder = new ContentView () {
				BackgroundColor = background_alt
			};

			ContentView statsviewholder = new ContentView () {
				BackgroundColor = background_alt
			};

			ContentView imageholder = new ContentView () {
				BackgroundColor = background
			};

			var browser = new TransparentWebView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent
			};

			var htmlSource = new HtmlWebViewSource ();

			htmlSource.Html = @"<link href='http://fonts.googleapis.com/css?family=Orbitron:400,700' rel='stylesheet' type='text/css'><style>body{font-family: 'Orbitron', sans-serif;font-size:1.5em;color:#fff;overflow-x:hidden;}</style>" + Page.characterDescription;

			browser.Source = htmlSource;

			var statsbrowser = new TransparentWebView () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent
			};

			var statshtmlSource = new HtmlWebViewSource ();

			statshtmlSource.Html = @"<link href='http://fonts.googleapis.com/css?family=Orbitron:400,700' rel='stylesheet' type='text/css'><style>body{font-family: 'Orbitron', sans-serif;font-size:1.5em;color:#fff;overflow-x:hidden;}</style>" + Page.characterStats;

			statsbrowser.Source = statshtmlSource;

			webviewholder.Content = browser;
			statsviewholder.Content = statsbrowser;
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
			pageContent.Children.Add (statsviewholder);

			this.Content = layout;
		}
	}
}


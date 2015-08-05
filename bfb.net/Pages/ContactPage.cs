using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class ContactPage : ContentPage
	{
		public ContactPage (BeyondRootModel Page)
		{
			NavigationPage.SetBackButtonTitle (this, "");
			this.Title = Page.pageName;

			RelativeLayout pageLayout = new RelativeLayout ();

			StackLayout pageView = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

			StackLayout ButtonSection = new StackLayout {				Spacing = 15,
				Padding = new Thickness(15,15,15,0), VerticalOptions = LayoutOptions.FillAndExpand };
			
			ScrollView scrollContent = new ScrollView { Content = pageView, VerticalOptions = LayoutOptions.FillAndExpand };

			HtmlView test = new HtmlView ();

			Label ContactInfo = new Label () {
				FontFamily = Device.OnPlatform("Orbitron", null, null),
				TextColor = Color.White,
				XAlign = TextAlignment.Center
			};

			ContentView slideContentHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(10,10,10,0) };
			ContentView slideContentHolder2 = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(10,10,10,0) };

			Image backgroundImage = new Image () { Source = "bg4.jpg", Aspect = Aspect.AspectFill };

			test.Text = Page.copyrightInformation;
			ContactInfo.Text = "Version 1.0 \n\n Contact app@pixelbombgames.com for any queries or information about this application \n\n Thankyou for downloading our App! \n";

			slideContentHolder2.Content = test;
			slideContentHolder.Content = ContactInfo;

			pageLayout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

			pageLayout.Children.Add (scrollContent, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

			pageView.Children.Add (slideContentHolder); 
			pageView.Children.Add (slideContentHolder2); 
			pageView.Children.Add (ButtonSection); 

			ButtonSection.Children.Add (new BeyondButton {
				Text = "Wiki",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("http://www.indiedb.com/games/beyond-flesh-and-blood"));
				})
			});

			ButtonSection.Children.Add (new BeyondButton {
				Text = "Forum",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("http://beyondfleshandbloodgame.com/forum"));
				})
			});

			ButtonSection.Children.Add (new BeyondButton {
				Text = "Blog",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("http://beyondfleshandbloodgame.com/d/blog/"));
				})
			});

			this.Content = pageLayout;
		}
	}
}


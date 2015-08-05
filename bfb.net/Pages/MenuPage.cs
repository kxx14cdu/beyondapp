using System;
using Xamarin.Forms;
using System.Threading;

namespace bfbnet
{
	public class MenuPage : ContentPage
	{
		public MenuPage (BeyondRootModel[] Pages)
		{
			StackLayout buttonList = new StackLayout {
				Spacing = 15,
				Padding = new Thickness(15,15,15,0),
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			ScrollView buttonScroll = new ScrollView {
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			NavigationPage.SetBackButtonTitle (this, "");

			Image backgroundImage = new Image () {
				Source = "bg1.jpg",
				Aspect = Aspect.AspectFill
			}; 

			Image logo = new Image () {
				Source = "logo.png"
			};

			RelativeLayout layout = new RelativeLayout ();

			layout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			layout.Children.Add (buttonScroll, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			buttonScroll.Content = buttonList;

			this.Title = "Main Menu";

			buttonList.Children.Add (logo);

			foreach (var Page in Pages) {
				switch(Page.pageType) {
				case "text":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new StoryPage (Page),false);
						})
					});
					break;
				case "character_list":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new CharacterPage (Page));
						})
					});
					break;		
				case "screenshots":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new ScreenshotsPage (Page));
						})
					});
					break;	
				case "conceptart":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new ConceptartPage (Page));
						})
					});
					buttonList.Children.Add (new BeyondButton {
						Text = "YouTube",
						Command = new Command (async c => {
							Device.OpenUri(new Uri("http://www.youtube.com/beyondfleshandbloodgame"));
						})
					});
					buttonList.Children.Add (new BeyondButton {
						Text = "Social Media",
						Command = new Command (async c => {
							await Navigation.PushAsync (new SocialPage ());
						})
					});
					break;	
				case "contact":
					buttonList.Children.Add (new BeyondButton {
						Text = "About",
						Command = new Command (async c => {
							await Navigation.PushAsync (new ContactPage (Page));
						})
					});
					break;
				default:
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new TextPage (Page));
						})
					});
					break;
				}
			}

			this.Content = layout;

		}
	}
}


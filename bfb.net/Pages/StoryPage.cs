using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class StoryPage : ContentPage
	{
		public StoryPage (BeyondRootModel Page)
		{
			NavigationPage.SetBackButtonTitle (this, "");
			this.Title = Page.pageName;

			RelativeLayout pageLayout = new RelativeLayout ();

			StackLayout pageView = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };
			 
			ScrollView scrollContent = new ScrollView { Content = pageView, VerticalOptions = LayoutOptions.FillAndExpand };

			ContentView slideContentHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(10,10,10,0) };
			HtmlView test = new HtmlView ();
			test.Text = Page.slideContent;
			slideContentHolder.Content = test;

			ContentView imageHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(0,15,0,15) };

			Image storyImage = new Image () { HeightRequest = 450, Aspect = Aspect.AspectFit };

			Image backgroundImage = new Image () { Source = "bg2.jpg", Aspect = Aspect.AspectFill };

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.slideRightImage);

			storyImage.Source = ImageSource.FromStream (() => new System.IO.MemoryStream (ImageBase64));

			imageHolder.Content = storyImage;

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
					
			pageView.Children.Add (imageHolder);
			pageView.Children.Add (slideContentHolder); 

			this.Content = pageLayout;
		}
	}
}


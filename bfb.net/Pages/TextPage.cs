using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class TextPage : ContentPage
	{
		public TextPage (BeyondRootModel Page)
		{
			NavigationPage.SetBackButtonTitle (this, "");
			this.Title = Page.pageName;

			RelativeLayout pageLayout = new RelativeLayout ();

			StackLayout pageView = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };
			 
			ScrollView scrollContent = new ScrollView { Content = pageView, VerticalOptions = LayoutOptions.FillAndExpand };

			HtmlView test = new HtmlView ();

			ContentView slideContentHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(10,10,10,0) };

			Image backgroundImage = new Image () { Source = "bg4.jpg", Aspect = Aspect.AspectFill };


			slideContentHolder.Content = test;

			test.Text = Page.slideContent;

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

			this.Content = pageLayout;
		}
	}
}


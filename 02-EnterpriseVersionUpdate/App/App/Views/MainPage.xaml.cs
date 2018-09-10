using App.ViewModels;
using Xamarin.Forms;

namespace App.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BindingContext = new MainPageViewModel();
		}
	}
}

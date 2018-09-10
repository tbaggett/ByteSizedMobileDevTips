using App.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.Views
{
	public partial class UpdateAvailable : ContentPage
	{
        public static readonly BindableProperty PageAppearingCommandProperty =
            BindableProperty.Create(nameof(PageAppearingCommand),
                                    typeof(ICommand),
                                    typeof(UpdateAvailable));

        public ICommand PageAppearingCommand
        {
            get => (ICommand)GetValue(PageAppearingCommandProperty);
            set => SetValue(PageAppearingCommandProperty, value);
        }

        public bool IsUpdateRequired { get; set; }

		public UpdateAvailable()
		{
			InitializeComponent();
            BindingContext = new UpdateAvailableViewModel();
            Appearing += OnAppearing;
		}

        private void OnAppearing(object sender, System.EventArgs e)
        {
            if (PageAppearingCommand?.CanExecute(IsUpdateRequired) == true)
            {
                PageAppearingCommand.Execute(IsUpdateRequired);
            }
        }
    }
}
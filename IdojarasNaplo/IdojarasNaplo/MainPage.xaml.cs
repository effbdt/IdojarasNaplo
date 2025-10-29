using CommunityToolkit.Mvvm.Messaging;

namespace IdojarasNaplo
{
	public partial class MainPage : ContentPage
	{

		public MainPage(MainPageViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
			WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
			{
				await DisplayAlert("Warning", m, "OK");
			});
		}


	}
}

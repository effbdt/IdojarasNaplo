using CommunityToolkit.Mvvm.Messaging;

namespace IdojarasNaplo
{
	public partial class MainPage : ContentPage
	{
		private MainPageViewModel viewModel;

		public MainPage(MainPageViewModel viewModel)
		{
			InitializeComponent();
			this.viewModel = viewModel;
			BindingContext = viewModel;
			WeakReferenceMessenger.Default.Register<string>(this, async (r, m) =>
			{
				await DisplayAlert("Warning", m, "OK");
			});
		}
	}
}

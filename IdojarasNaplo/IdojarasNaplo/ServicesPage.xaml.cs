namespace IdojarasNaplo;

public partial class ServicesPage : ContentPage
{
	ServicesViewModel viewModel;

	public ServicesPage(ServicesViewModel vm)
	{
		InitializeComponent();
		this.viewModel = vm;
	}
}
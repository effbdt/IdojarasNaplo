namespace IdojarasNaplo;

public partial class ServicesPage : ContentPage
{
	private ServicesViewModel viewModel;

	public ServicesPage(ServicesViewModel vm)
	{
		InitializeComponent();
		this.viewModel = vm;
		BindingContext = vm;
	}
}
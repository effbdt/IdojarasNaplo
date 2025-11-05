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

	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);

		if (BindingContext is ServicesViewModel vm)
		{
			vm.ResetServiceValues();
		}
	}

	protected async override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		if (BindingContext is ServicesViewModel vm)
		{
			await vm.LoadDiaries();
		}
	}
}
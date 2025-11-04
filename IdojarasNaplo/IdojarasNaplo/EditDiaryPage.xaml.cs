
using System.Globalization;

namespace IdojarasNaplo;

public partial class EditDiaryPage : ContentPage
{
	private EditDiaryEntryViewModel viewModel;
	public EditDiaryPage(EditDiaryEntryViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		viewModel.InitDraft();
	}
}
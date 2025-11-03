namespace IdojarasNaplo;

[QueryProperty(nameof(Diary), "DiaryEntry")]
public partial class DiaryDetailsPage : ContentPage
{
	Diary diary;

	public Diary Diary
	{
		get => diary;
		set
		{
			diary = value;
			OnPropertyChanged();
		}

	}


	public DiaryDetailsPage()
	{
		InitializeComponent();
		BindingContext = this;
	}
}
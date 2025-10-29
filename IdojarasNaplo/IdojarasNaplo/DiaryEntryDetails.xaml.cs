namespace IdojarasNaplo;

[QueryProperty(nameof(Diary), "Diary")]
public partial class DiaryEntryDetails : ContentPage
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

	public DiaryEntryDetails()
	{
		InitializeComponent();
		BindingContext = this;
	}
}
namespace IdojarasNaplo
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute("editDiary", typeof(EditDiaryPage));
			Routing.RegisterRoute("diarydetails", typeof(DiaryDetailsPage));
			Routing.RegisterRoute("entryDetails", typeof(DiaryDetailsPage));
		}
	}
}

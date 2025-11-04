namespace IdojarasNaplo
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute("editDiary", typeof(EditDiaryPage));
			Routing.RegisterRoute("diaryDetails", typeof(DiaryDetailsPage));
			Routing.RegisterRoute("servicePage", typeof(ServicesPage));
		}
	}
}

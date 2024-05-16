namespace BookstoreApp.MVVM.Views.ApplicationPages;

[QueryProperty("query", "query")]
public partial class ExplorePage : ContentPage
{
	public ExplorePage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        //query = Uri.EscapeDataString(query);
    }
}
namespace BookstoreApp.MVVM.Views.ApplicationPages;

[QueryProperty(nameof(Query), "query")]
public partial class ExplorePage : ContentPage
{
    public string Query { get; set; }
	public ExplorePage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        TestLabel.Text = Query;
    }
}
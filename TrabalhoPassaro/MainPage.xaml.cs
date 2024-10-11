namespace TrabalhoPassaro;

public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
	}

	private async void BotaoIniciarFoiClicado(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new JogoPage());
	}
}


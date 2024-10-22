namespace TrabalhoPassaro;

public partial class JogoPage : ContentPage
{
   const int gravidade=30;
   const int tempoEntreFrames=25;
   bool estaMorto=false;
   double larguraJanela=0;
   double alturaJanela=0;
   int velocidade=20;
    const int maxTempoPulando=3;
   int tempoPulando=0;
   bool estaPulando=false;
   const int forcaPulo=60;
   	const int aberturaMinima = 200;
	int score=0;
	



		public JogoPage()
	{
		InitializeComponent();
	}
	void AplicaPulo()
	{
      imgpassaro.TranslationY-=forcaPulo;
	  tempoPulando++;
	  if(tempoPulando>=maxTempoPulando)
	  {
		estaPulando=false;
		tempoPulando=0;
	  }

	}
	void AplicaGravidade()
	{
		imgpassaro.TranslationY+=gravidade;
	}

     async Task Desenhar()
	  {
		while (!estaMorto)
       {   if (estaPulando)
		        AplicaPulo();
		else
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
			GerenciaCanos();
		if (VerificaColisao())
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
	 		await Task.Delay(tempoEntreFrames);
	    }
	 }
	 
   

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela=w;
		alturaJanela=h;
	}

      void GerenciaCanos()
	{
		CanoDeCima.TranslationX-=velocidade;
		CanoDeBaixo.TranslationX-=velocidade;
		if(CanoDeBaixo.TranslationX<=-larguraJanela)
	 {
		CanoDeBaixo.TranslationX=4;
		CanoDeCima.TranslationX=4;
		var alturaMax = -100;
			var alturaMin = -CanoDeBaixo.HeightRequest;
			CanoDeCima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			CanoDeBaixo.TranslationY = CanoDeCima.TranslationY + aberturaMinima + CanoDeBaixo.HeightRequest;
			score ++;
			LabelScore.Text="Canos:" + score.ToString("D3");

     }
  
	}
	void OnGameOverClicked(object s, TappedEventArgs a)
    {
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto=false;
	    imgpassaro.TranslationY=0;
	}
	bool VerificaColisao()
	{
			if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			VerificaColisaoChao() || 
			VerificaColisaoCanoCima() ||
			VerificaColisaoCanoBaixo())
			{
				return true;
			}

		}
		return false;
	}
	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (imgpassaro.TranslationY <= minY)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - FundoImg.HeightRequest;
		if (imgpassaro.TranslationY >= maxY)
			return true;
		else
			return false;

	}
	void OnGridClicked(object s, TappedEventArgs a)
		{ 
			estaPulando=true;
		}

		bool VerificaColisaoCanoCima()
	{
		var posHPassaro = (larguraJanela / 2) - (imgpassaro.WidthRequest / 2);
		var posVPassaro = (alturaJanela / 2) - (imgpassaro.HeightRequest / 2) + imgpassaro.TranslationY;
		if (posHPassaro >= Math.Abs(CanoDeCima.TranslationX) - CanoDeCima.WidthRequest &&
		 posHPassaro <= Math.Abs(CanoDeCima.TranslationX) + CanoDeCima.WidthRequest &&
		 posVPassaro <= CanoDeCima.HeightRequest + CanoDeCima.TranslationY)
		 {
			return true;
		 }
		 else 
		 {
			return false;
		 }
	}
	bool VerificaColisaoCanoBaixo()
	{
		var posHPassaro = (larguraJanela / 2) - (imgpassaro.WidthRequest / 2);
		var posVPassaro = (alturaJanela / 2) - (imgpassaro.HeightRequest / 2) + imgpassaro.TranslationY;
		if (posHPassaro >= Math.Abs(CanoDeBaixo.TranslationX) - CanoDeBaixo.WidthRequest &&
		 posHPassaro <= Math.Abs(CanoDeBaixo.TranslationX) + CanoDeBaixo.WidthRequest &&
		 posVPassaro <= CanoDeBaixo.HeightRequest + CanoDeBaixo.TranslationY)
		 {
			return true;
		 }
		 else 
		 {
			return false;
		 }
	}

}
using ViewModels;

namespace LolApp;

public partial class ChampionPage : ContentPage
{
	public ChampionVM Champion { get; }

	public ChampionPage(ChampionVM cvm)
	{
		InitializeComponent();

		BindingContext = Champion = cvm;
	}
}

using LolApp.ViewModels;
using ViewModels;

namespace LolApp;

public partial class ChampionsPage : ContentPage
{
	public ApplicationVM VM { get; }
	public ChampionsPage(ApplicationVM vm)
	{
		InitializeComponent();
		BindingContext = VM = vm;
	}
}

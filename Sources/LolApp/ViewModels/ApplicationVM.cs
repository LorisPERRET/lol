using System;
using CommunityToolkit.Mvvm.Input;
using Model;
using ViewModels;

namespace LolApp.ViewModels
{
	public class ApplicationVM
	{
		internal IDataManager DataMgr { get; set; }

		public ChampionsMgrVM ChampionsMgrVM { get; set; } 

		public ApplicationVM(IDataManager dataManager)
		{
			DataMgr = dataManager;
			ChampionsMgrVM = new ChampionsMgrVM(DataMgr);
		}

		public IRelayCommand<ChampionVM> NavigateToChampionDetailsPageCommand { get; }
			= new RelayCommand<ChampionVM>(async cvm => await App.Current.MainPage.Navigation.PushAsync(new ChampionPage(cvm)));
	}
}


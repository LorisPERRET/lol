using System.Threading.Tasks;
using Model;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Data.SqlTypes;

namespace ViewModels;

public partial class ChampionsMgrVM : ObservableObject
{
    internal IDataManager DataMgr { get; set; }

    public ChampionsMgrVM(IDataManager dataManager)
    {
        DataMgr = dataManager;
        LoadChampionsCommand = new RelayCommand(async () => await LoadChampions());
        LoadChampionsByNameCommand = new RelayCommand<string>(
                async (substring) => await LoadChampionsByName(substring),
                substring => !string.IsNullOrWhiteSpace(substring));
        LoadChampionsBySkillCommand = new RelayCommand<string>(
                async (substring) => await LoadChampionsBySkill(substring),
                substring => !string.IsNullOrWhiteSpace(substring));
        PreviousPageCommand = new RelayCommand(async() => await PreviousPage(), () => Index > 0);
        NextPageCommand = new RelayCommand(async () => await NextPage(), () => Index < NbPages-1);
        DeleteChampionCommand = new RelayCommand<object>(async (cvm) => { if (cvm is ChampionVM) await DeleteChampion(cvm as ChampionVM); }, (cvm) => cvm!= null && cvm is ChampionVM && Champions.Contains(cvm));
        PropertyChanged += ChampionsMgrVM_PropertyChanged;

        loadingMethods = new Dictionary<LoadingCriterium, Func<object, Task>>()
        {
            [LoadingCriterium.None] = async (o) => await LoadChampions(), 
            [LoadingCriterium.ByName] = async (o) =>
            {
                string substring = o as string;
                if(substring == null) return;
                await LoadChampionsByName(substring);
            },
            [LoadingCriterium.BySkill] = async (o) =>
            {
                string skillString = o as string;
                if(skillString == null) return;
                await LoadChampionsBySkill(skillString);
            },
            [LoadingCriterium.ByCharacteristic] = async (o) =>
            {
                string characString = o as string;
                if(characString == null) return;
                await LoadChampionsByCharacteristic(characString);
            },
        };
    }

    private async Task LoadChampionsFunc(Func<Task<IEnumerable<Champion?>>> loader,
                                         Func<Task<int>> nbReader,
                                         LoadingCriterium criterium,
                                         object parameter = null)
    {
        Champions.Clear();
        var someChampions = (await loader()).Select(c => new ChampionVM(c)).ToList();
        foreach (var cvm in someChampions)
        {
            Champions.Add(cvm);
        }
        NbChampions = await nbReader();
        currentLoadingCriterium = criterium;
        currentLoadingParameter = parameter;
    }

    public IRelayCommand LoadChampionsCommand { get; }

    public async Task LoadChampions()
    {
        await LoadChampionsFunc(async () => await DataMgr.ChampionsMgr.GetItems(index, count),
                          async () => await DataMgr.ChampionsMgr.GetNbItems(),
                          LoadingCriterium.None);
    }

    public IRelayCommand<string> LoadChampionsByNameCommand { get; }

    public async Task LoadChampionsByName(string substring)
    {
        await LoadChampionsFunc(async () => await DataMgr.ChampionsMgr.GetItemsByName(substring, index, count),
                                async () => await DataMgr.ChampionsMgr.GetNbItemsByName(substring),
                                LoadingCriterium.ByName,
                                substring);
    }

    public async Task LoadChampionsBySkill(string skill)
    {
        await LoadChampionsFunc(
            async () => await DataMgr.ChampionsMgr.GetItemsBySkill(skill, index, count),
            async () => await DataMgr.ChampionsMgr.GetNbItemsBySkill(skill),
            LoadingCriterium.BySkill,
            skill);
    }

    [RelayCommand(CanExecute = nameof(CanLoadChampionsByCharacteristic))]
    public async Task LoadChampionsByCharacteristic(string characteristic)
    {
        await LoadChampionsFunc(
            async () => await DataMgr.ChampionsMgr.GetItemsByCharacteristic(characteristic, index, count),
            async () => await DataMgr.ChampionsMgr.GetNbItemsByCharacteristic(characteristic),
            LoadingCriterium.ByCharacteristic,
            characteristic);
    }

    private bool CanLoadChampionsByCharacteristic(string characteristic)
        => !string.IsNullOrWhiteSpace(characteristic);

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NextPageCommand))]
    [NotifyCanExecuteChangedFor(nameof(PreviousPageCommand))]
    private int index = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NbPages))]
    [NotifyCanExecuteChangedFor(nameof(NextPageCommand))]
    [NotifyCanExecuteChangedFor(nameof(PreviousPageCommand))]
    private int count = 5;

    public int NbPages
    {
        get
        { 
            if(Count == 0 || NbChampions == 0)
            {
                return 0;
            }
            return (NbChampions-1) / Count + 1;
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NbPages))]
    [NotifyCanExecuteChangedFor(nameof(NextPageCommand))]
    [NotifyCanExecuteChangedFor(nameof(PreviousPageCommand))]
    private int nbChampions = 0;

    [ObservableProperty]
    private ObservableCollection<ChampionVM> champions = new ObservableCollection<ChampionVM>();

    public IRelayCommand PreviousPageCommand {get; }

    async Task PreviousPage()
    {
        if(Index > 0)
        {
            Index--;
            await loadingMethods[currentLoadingCriterium](currentLoadingParameter);
        }
    }

    public IRelayCommand NextPageCommand { get; } 

    async Task NextPage()
    {
        if(Index < NbPages-1)
        {
           Index++;
           await loadingMethods[currentLoadingCriterium](currentLoadingParameter);
        }
    }

    private static string[] searchedStrings = { nameof(SearchedName), nameof(SearchedSkill), nameof(SearchedCharacteristic) };

    private async void ChampionsMgrVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(searchedStrings.Any(s => e.PropertyName == s))
        {
            if(!string.IsNullOrWhiteSpace(typeof(ChampionsMgrVM).GetProperty(e.PropertyName).GetValue(this) as string))
            {
                foreach(string s in searchedStrings.Except(new string[]{e.PropertyName }))
                {
                    typeof(ChampionsMgrVM).GetProperty(s).SetValue(this, "");
                }
                return;
            }
            Index=0;
            if(searchedStrings.All(s => string.IsNullOrWhiteSpace(typeof(ChampionsMgrVM).GetProperty(s).GetValue(this) as string)))
            {
                await LoadChampions();
            }
        }
    }

    
    

    public IRelayCommand<ChampionVM> DeleteChampionCommand { get; }

    public async Task<bool> DeleteChampion(ChampionVM cvm)
    {
        if(cvm == null || !Champions.Contains(cvm)) return false;

        bool result = await DataMgr.ChampionsMgr.DeleteItem(cvm.Model);
        Champions.Remove(cvm);
        await LoadChampions();
        return result;
    }

    

    [ObservableProperty]
    private ChampionVM selectedChampion;

    [ObservableProperty]
    private string searchedName;

    

    [ObservableProperty]
    private string searchedSkill;

    public IRelayCommand<string> LoadChampionsBySkillCommand { get; }

    [ObservableProperty]
    private string searchedCharacteristic;



    enum LoadingCriterium
    {
        None,
        ByName,
        BySkill,
        ByCharacteristic
    }

    private LoadingCriterium currentLoadingCriterium = LoadingCriterium.None;
    private object currentLoadingParameter = null;

    private Dictionary<LoadingCriterium, Func<object, Task>> loadingMethods;
}


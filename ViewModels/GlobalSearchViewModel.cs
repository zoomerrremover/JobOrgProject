
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels
{
    public partial class GlobalSearchViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<string> typePickerItems = new();

        string searchPromt = "";
        public string SearchPromt
        {
            get => searchPromt;
            set
            {
                searchPromt = value;
                LoadModels();
            }
        }


        [ObservableProperty]

        string selectedModel = typeof(Item).Name;

        GlobalSettings settings;
        IDataStorage dataStorage { get; }

        ObservableCollection<Thing> Models { get; set; } = new();

        public ObservableCollection<Thing> ObsModels { get; set; } = new();

        partial void OnSelectedModelChanging(string oldValue, string newValue)
        {
            Models = dataStorage.GetItems<Thing>(newValue);
            LoadModels();
        }
        void LoadModels()
        {
            ObsModels.Clear();
            foreach (var model in Models)
            {
                if (model.Name.Contains(SearchPromt))
                {
                    ObsModels.Add(model);
                }
            }
        }

        public GlobalSearchViewModel(GlobalSettings settings,IDataStorage data)
        {
            dataStorage = data;
            this.settings = settings;
            InitiateModelChoice();
            OnSelectedModelChanging("", selectedModel);
        }

        void InitiateModelChoice()
        {
            foreach (var type in settings.Models)
            {
                var attribute = (Model)Attribute.GetCustomAttribute(type, typeof(Model));

                // If IsActive is true, add the type to the list
                if (attribute.DisplayableInTheGlobalSearch)
                {
                    TypePickerItems.Add(type.Name);
                }
            }
        }
    }
}

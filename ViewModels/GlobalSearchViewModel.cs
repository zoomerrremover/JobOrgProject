
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

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

        [ObservableProperty]

        Thing selectedObject; 
        IDataStorage dataStorage { get; }

        ObservableCollection<Thing> Models { get; set; } = new();

        public ObservableCollection<Thing> ObsModels { get; set; } = new();

        [ObservableProperty]
        DataTemplate currentTemplate; 

        GSSelector Selector;

        partial void OnSelectedModelChanging(string oldValue, string newValue)
        {
            Models = dataStorage.GetItems<Thing>(newValue);
            CurrentTemplate = Selector.TemplatesAndTypes[newValue];
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
        PageFactory factory;
        public GlobalSearchViewModel(PageFactory Factory,GlobalSettings settings,IDataStorage data,GSSelector selector)
        {
            factory = Factory;
            dataStorage = data;
            this.settings = settings;
            Selector = selector;

            InitiateModelChoice();
            OnSelectedModelChanging("", selectedModel);
        }
        [RelayCommand]
        void GoToDetails()
        {
            if(SelectedObject is null)
            {
                return;
            }
            var PageToLoad = factory.MakeADetailsPage(SelectedObject);
            Shell.Current.Navigation.PushAsync(PageToLoad);
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

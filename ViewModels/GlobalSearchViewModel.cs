
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
        DetailsPageFactory factory;
        public GlobalSearchViewModel(DetailsPageFactory Factory,GlobalSettings settings,IDataStorage data,GSSelector selector)
        {
            factory = Factory;
            dataStorage = data;
            this.settings = settings;
            Selector = selector;

            InitiateModelChoice();
            OnSelectedModelChanging("", selectedModel);
        }
        public void GoToDetails(object sender, SelectionChangedEventArgs e)
        {
            Thing selectedObject = (Thing)e.CurrentSelection;
            var PageToLoad = factory.MakeADetailsPage(selectedObject);
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

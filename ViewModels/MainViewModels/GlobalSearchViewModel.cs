
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.MainViewModels
{
    public partial class GlobalSearchViewModel : BaseViewModel
    {
        [ObservableProperty]
        List<string> displayedPickerItems = new();
        List<Type> typePickerItems = new();

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
        [ObservableProperty]
        bool isLoading = false;

        IReflectionContent ReflectionContent;

        [ObservableProperty]

        Thing selectedObject; 
        IDataStorage dataStorage { get; }

        ObservableCollection<Thing> Models { get; set; } = new();

        public ObservableCollection<Thing> ObsModels { get; set; } = new();

        [ObservableProperty]
        DataTemplate currentTemplate; 

        IConverter Converter;

        partial void OnSelectedModelChanged(string value)
        {
            var localType = typePickerItems.Find(w => w.Name == value);
            Models = dataStorage.GetItems<Thing>(localType);
            Models.CollectionChanged += LoadModels;
            CurrentTemplate = Converter.ConvertToDataTemplate(localType);
            LoadModels();
        }

        private void LoadModels(object sender, NotifyCollectionChangedEventArgs e)
        {
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
        public GlobalSearchViewModel(PageFactory Factory,IReflectionContent ReflectionContent,IDataStorage DataBase,IConverter Converter)
        {
            factory = Factory;
            dataStorage = DataBase;
            this.ReflectionContent = ReflectionContent;
            this.Converter = Converter;
            InitiateModelChoice();
            OnSelectedModelChanging(null, selectedModel);
        }
        PageFactory factory;
        [RelayCommand]
        async void GoToDetails(Thing SelectedObject)
        {
            IsLoading = true;
            if (SelectedObject is null)
            {
                return;
            }
            var pageToLoad = factory.MakeADetailsPage(SelectedObject);
            await Shell.Current.Navigation.PushAsync(pageToLoad);
            IsLoading = false;
        }

        void InitiateModelChoice()
        {
            foreach (var type in ReflectionContent.Models)
            {
                var attribute = (ModelAttribute)Attribute.GetCustomAttribute(type, typeof(ModelAttribute));

                // If IsActive is true, add the type to the list
                if (attribute.DisplayableInTheGlobalSearch)
                {
                    typePickerItems.Add(type);
                    DisplayedPickerItems.Add(type.Name);
                }
                
            }
        }
    }
}


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
        #region Services
        IReflectionContent ReflectionContent;
        IDataStorage dataStorage { get; init; }
        IErrorService errorService { get; init; }
        IConverter Converter { get; init; }
        PageFactory factory;
        #endregion
        #region Type Picker Mechanism
        public List<string> DisplayedPickerItems { get; init; }
        List<Type> typePickerItems { get; init; }
        [ObservableProperty]
        string selectedModel = typeof(Item).Name;
        [ObservableProperty]
        Thing selectedObject;
        [ObservableProperty]
        DataTemplate currentTemplate;
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
        partial void OnSelectedModelChanging(string oldValue, string newValue)
        {
            if (IsLoading)
            {
                newValue = oldValue;
                return;
            }
            IsLoading = true;
            try
            {
                var localType = typePickerItems.Find(w => w.Name == newValue);
                Models = dataStorage.GetItems<Thing>(localType);
                Models.CollectionChanged += LoadModels;
                CurrentTemplate = Converter.ConvertToDataTemplate(localType);
                IsLoading = false;
                LoadModels();
            }
            catch (Exception e)
            {
                errorService.CallError("There was problem displaying this type.");
                OnSelectedModelChanging(oldValue, oldValue);

            }
        }
        #endregion
        #region Ctors and others
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
        bool isLoading = false;

        ObservableCollection<Thing> Models { get; set; } = new();

        public ObservableCollection<Thing> ObsModels { get; set; } = new();


        void LoadModels(object sender = null, NotifyCollectionChangedEventArgs e = null)
        {
                ObsModels.Clear();
                foreach (var model in Models)
                {
                    if (model.Name.ToLower().Contains(SearchPromt.ToLower()))
                    {
                        ObsModels.Add(model);
                    }
                }
        }
        public void LoadContent()
        {
            OnSelectedModelChanging(null, selectedModel);
        }
        public GlobalSearchViewModel(IErrorService ErrorService,PageFactory Factory,IReflectionContent ReflectionContent,IDataStorage DataBase,IConverter Converter)
        {
            DisplayedPickerItems = new();
            typePickerItems = new();
            factory = Factory;
            dataStorage = DataBase;
            this.ReflectionContent = ReflectionContent;
            errorService = ErrorService;
            this.Converter = Converter;
            InitiateModelChoice();
        }
        [RelayCommand]
        async Task GoToDetails(Thing SelectedObject)
        {
            try
            {
                IsLoading = true;
                if (SelectedObject is null)
                {
                    return;
                }
                var pageToLoad = factory.MakeADetailsPage(SelectedObject);
                await Shell.Current.Navigation.PushAsync(pageToLoad);
                return;
            }
            catch (Exception ex)
            {
                errorService.CallError("There was a problem opening this item");
            }
            finally
            {
                IsLoading = false;
            }
        }

        #endregion
    }
}

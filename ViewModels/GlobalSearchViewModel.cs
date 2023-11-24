
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

        [ObservableProperty]

        string searchPromt = "";


        [ObservableProperty]

        string selectedModel = "Items";


        IDataStorage dataStorage { get; }

        List<Thing> Models { get; set; } = new();

        public ObservableCollection<Thing> ObsModels { get; set; } = new();

        partial void OnSelectedModelChanging(string oldValue, string newValue)
        {
            Models = dataStorage[newValue];
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

        public GlobalSearchViewModel(ILoadableContent loadableContent,IDataStorage data)
        {
            dataStorage = data;
            foreach(var item in loadableContent.InteractableModels)
            {
                TypePickerItems.Add(item);
            }
        }
    }
}

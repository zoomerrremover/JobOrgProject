
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(Assignment))]
    public partial class AssignmentProxy:ThingProxy
    {
        new public Assignment BindingObject { get; set; }
        // CTORS
        //------------------------------------------------------------------------------------------------
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is Assignment)
            {
                var wm = new AssignmentProxy(model as Assignment);
                return wm;
            }
            else return null;
        }
        public AssignmentProxy(Assignment item) : base(item)
        {
            BindingObject = item;
            workers = queryService.GetItems<Worker>();
        }
        //------------------------------------------------------------------------------------------------
        [ObservableProperty]
        ObservableCollection<PickableWorker> displayableWorkers = new();
        ObservableCollection<Worker> workers = new ObservableCollection<Worker>();
        string searchEntryText = "";
        public string SearchEntryText
        {
            get
            {
                return searchEntryText;
            }
            set
            {
                searchEntryText = value;
                ApplySearchString();
            }
        }
        [RelayCommand]
        void EditWorker(PickableWorker obj)
        {
            obj.data = !obj.data;
            if(obj.data)
            {
                BindingObject.Workers.Add(obj.model);
            }
            else
            {
                BindingObject.Workers.Remove(obj.model);
            }
            ApplySearchString();
        }
        void ApplySearchString()
            {
            var wholeWorkers = new ObservableCollection<Worker>();
            var promptLocal = searchEntryText.ToLower();
            foreach (var worker in workers)
            {
                var NameLocal = worker.Name.ToLower();
                if (NameLocal.Contains(promptLocal))
                {
                    wholeWorkers.Add(worker);
                }
            }
            LoadWorkers(wholeWorkers);
            }
        void LoadWorkers(ObservableCollection<Worker> source)
        {
            DisplayableWorkers.Clear();
            source.OrderByDescending(BindingObject.Workers.Contains)
                .ToList()
                     .ForEach((w) =>
                                {
                                    bool boolValue = BindingObject.Workers.Contains(w) ? true : false;
                                    DisplayableWorkers.Add(new PickableWorker(w, boolValue));

                                });
        }


        


    }
}

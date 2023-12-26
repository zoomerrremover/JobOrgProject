
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public partial class Assignment:Thing,ITimeBased
    {
        public List<Worker> Workers;
        public List<Color> WorkerColours { get => Workers.Select(x => x.Color).ToList(); }
        [ObservableProperty]
        public DateTime startTime;
        [ObservableProperty]
        public DateTime finishTime;
        public Place Place { get ; set ; }
        
    }
} 
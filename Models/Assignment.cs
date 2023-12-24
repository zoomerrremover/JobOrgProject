﻿
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Assignment:Thing,ITimeBased
    {
        public List<Worker> Workers;
        public List<Color> WorkerColours { get => Workers.Select(x => x.Color).ToList(); }
        public DateTime StartTime { get ; set ; }
        public DateTime FinishTime { get ; set ; }
        public Place Place { get ; set ; }
    }
}

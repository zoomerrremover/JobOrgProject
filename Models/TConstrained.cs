﻿
namespace TheJobOrganizationApp.Models
{
    public interface TConstrained
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Place Place { get; set; }
    }
}

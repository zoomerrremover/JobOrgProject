using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services.HighLevelServices
{
    public class Settings : ISettings
    {
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool NotificationPermission { get; set; }
        public bool GeoLocationPermission { get; set; }
        public int LoadPeriodFuture { get; set; }
        public int LoadPeriodPast { get; set; }

        public async Task LoadFromFile()
        {
        }

        public async Task SaveToFile()
        {
        }
    }
}

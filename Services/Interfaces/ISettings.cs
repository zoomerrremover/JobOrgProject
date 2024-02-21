﻿
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface ISettings
    {
        public void LoadFromFile();
        public void SaveToFile();
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool NotificationPermission {  get; set; }
        public bool GeoLocationPermission { get; set; }
        public int LoadPeriodFuture { get; set; }
        public int LoadPeriodPast { get; set; }
    }
}

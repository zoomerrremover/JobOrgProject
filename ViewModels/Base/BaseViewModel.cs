﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace TheJobOrganizationApp.ViewModels.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isLoading = false;
    }
}

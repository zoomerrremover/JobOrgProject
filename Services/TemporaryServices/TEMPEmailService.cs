
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services.TemporaryServices;

public class TEMPEmailService : IEmainAndMobileHandler
{
    public async Task<bool> ChangeEmail(string email)
    {
        return true;
    }

    public async Task<bool> SendEmail(IHasContacts contact)
    {
        return true; 
    }

    public async Task<bool> SendSms(IHasContacts contact)
    {
        return true;
    }
}

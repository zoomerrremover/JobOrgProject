using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IEmainAndMobileHandler
    {
        public Task<bool> SendEmail(IHasContacts contact);
        public Task<bool> SendSms(IHasContacts contact);
        public Task<bool> ChangeEmail(string email);


    }
}

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
        public bool SendEmail(IHasContacts contact);
        public bool SendSms(IHasContacts contact);
        public bool ChangeEmail(string email);


    }
}

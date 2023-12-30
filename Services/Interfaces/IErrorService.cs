
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IErrorService
    {
        public void CallException(string message,params Type[] args);
        public void CallError(string message,Action ErrorHandlingMethod = null);
    }
}

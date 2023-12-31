
using System.Text;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services.LowLeveLServices;

public class ErrorService : IErrorService
{
    public void CallError(string message, Action ErrorHandlingMethod = null)
    {
        Console.WriteLine(message);
    }

    public void CallException(string message, params Type[] args)
    {
        StringBuilder ErrorMess = new StringBuilder();
        foreach (Type type in args)
        {
            ErrorMess.Append($"{type.Name}/");
        }
        ErrorMess.Append(message);
        Console.WriteLine(ErrorMess.ToString());
    }
}


using System.Reflection;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.ModelsProxies;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class ModelToVMAdaptor
{
    Dictionary<Type, Type> modelsToViewModel = new Dictionary<Type, Type>();

    public ModelToVMAdaptor(GlobalSettings gs)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types)
        {
            if (type.IsClass && type.BaseType == typeof(ModelView))
            {
                var newName = type.Name.Replace("Proxy", "");
                var Originaltype = gs.Models.Where(t => t.Name == newName).First();
                if (Originaltype is not null)
                {
                    modelsToViewModel[Originaltype] = type;
                }
            }
        }
    }
    public BaseViewModel ConvertToViewModel(Thing model)
    {
        var type = modelsToViewModel.Where(m => m.Key == model.GetType()).First().Value;
        var methodToInvoke = type.GetMethod("CreateFromTheModel");
        return methodToInvoke.Invoke(null, new object[] { model }) as BaseViewModel;
    }

}

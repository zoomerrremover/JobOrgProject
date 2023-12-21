
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
            if (type.IsClass & type.
                GetCustomAttribute(typeof(Proxy)) is not null)
            {
                var attribute = (Proxy)Attribute.GetCustomAttribute(type, typeof(Proxy));
                var LinkedClass = attribute.ClassLinked;
                if (LinkedClass is not null)
                {
                    modelsToViewModel[LinkedClass] = type;
                }
            }
        }
    }
    public BaseViewModel ConvertToViewModel(Thing model,Type asociatedType = null)
    {
        Type type;
        if( asociatedType is not null && modelsToViewModel.ContainsKey(asociatedType))
        {
            type = modelsToViewModel[asociatedType];
            
        }
        else
        {
            type = modelsToViewModel
                .Where(m => m.Key == model.GetType())
                        .First().Value;
        }
       
        var methodToInvoke = type
            .GetMethod("CreateFromTheModel");
        return methodToInvoke.Invoke(null, new object[] { model }) as BaseViewModel;
    }

}

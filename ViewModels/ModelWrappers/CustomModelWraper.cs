

using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels.ModelsProxies;

public class CustomModelWraper
    <Model,CustomData> where Model : Thing
{
    public Model model { get; set; }

    public CustomData data { get; set; }

    public CustomModelWraper(Model model,CustomData data)
    {
        this.model = model;
        this.data = data;

    }
}
public class PickableWorker : CustomModelWraper<Worker, bool>
{
public PickableWorker(Worker model, bool data) : base(model, data)
{
}
}

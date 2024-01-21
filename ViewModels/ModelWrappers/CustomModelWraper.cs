

using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels.ModelWrappers;

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
public partial class PickableWorker : CustomModelWraper<Worker, bool>
{
    public static Action<PickableWorker> ActivatedAction {  get; set; } 

    [RelayCommand]
    void OnPressed(PickableWorker worker) => ActivatedAction(worker);
public PickableWorker(Worker model, bool data) : base(model, data)
{
}
}


namespace TheJobOrganizationApp.Models.ModelsProxies
{
    public partial class AssignmentProxy:ThingProxy
    {
        new public Assignment BindingObject { get; set; }


        // CTORS
        //------------------------------------------------------------------------------------------------
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is Assignment)
            {
                var wm = new ItemProxy(model as Assignment);
                return wm;
            }
            else return null;
        }
        public AssignmentProxy(Assignment item) : base(item)
        {
            BindingObject = item;
        }
        public AssignmentProxy(Thing BindingObject) : base(BindingObject)
        {
        }
        //------------------------------------------------------------------------------------------------
    
    }
}

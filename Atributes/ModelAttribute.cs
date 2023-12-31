namespace TheJobOrganizationApp.Atributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModelAttribute
        : Attribute
    {
        public bool DisplayableInTheGlobalSearch { get; set; }
    }
}

namespace TheJobOrganizationApp.Atributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DetailsViewModel
        : Attribute
    {
        public Type ClassLinked { get; set; }
    }
}

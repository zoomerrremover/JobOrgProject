namespace TheJobOrganizationApp.Atributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DetailsViewModelAttribute
        : Attribute
    {
        public Type ClassLinked { get; set; }
    }
}

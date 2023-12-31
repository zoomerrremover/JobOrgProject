
using System.Reflection;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.Services.HighLevelServices
{
    public class RuntimeContent : IReflectionContent, IXAMLContent, IConverter
    {
        public List<Type> Models { get; private set; }
        public Dictionary<Type,Type> DetailsViewModels { get; private set; }
        public Dictionary<Type,Type> DataTemplates { get; private set; }
        public Dictionary<Type,Type> ContentPages { get; private set; }
        public Type DataTemplatesDefaultValue { get; private set; }
        public Type ContentPageDefaultValue { get; private set; }

        IErrorService ErrorService { get; set; }

        public RuntimeContent(IErrorService errorService)
        {
            ErrorService = errorService;
            Initialize();
        }
        void Initialize()
        {
            InitializeLists();
            InitializeContent();
        }
        void InitializeLists()
        {
            Models = new List<Type>();
            DetailsViewModels = new Dictionary<Type, Type>();
            DataTemplates = new();
            ContentPages = new();
            ContentPageDefaultValue = null;
            DataTemplatesDefaultValue = typeof(Thing);
        }

        void InitializeContent()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass){
                    switch (type)
                    {
                        case Type t when Attribute.IsDefined(t, typeof(ModelAttribute)):
                            Models.Add(type);
                            break;
                        case Type t when Attribute.IsDefined(t, typeof(DetailsViewModelAttribute)):
                            var viewModelAtribute = (DetailsViewModelAttribute)Attribute.GetCustomAttribute(type, typeof(DetailsViewModelAttribute));
                            var viewModelLinkedClass = viewModelAtribute.ClassLinked;
                            if (viewModelLinkedClass is not null)
                            {
                                DetailsViewModels[viewModelLinkedClass] = type;
                            }
                            break;
                        case Type t when t.BaseType == typeof(ContentPage) & Attribute.IsDefined(t, typeof(DetailsPageAttribute)):
                            var pageAttribute = (DetailsPageAttribute)Attribute.GetCustomAttribute(type, typeof(DetailsPageAttribute));
                            var pageLinkedClass = pageAttribute.ClassLinked;
                            if (pageLinkedClass is not null)
                            {
                                ContentPages[pageLinkedClass] = type;
                            }
                            break;
                        case Type t when t.BaseType == typeof(DataTemplate) & Attribute.IsDefined(t, typeof(GlobalSearchDataTemplateAttribute)):
                            var dataTemplateAttribute = (GlobalSearchDataTemplateAttribute)Attribute.GetCustomAttribute(type, typeof(GlobalSearchDataTemplateAttribute));
                            var dataTemplateAttributeLinkedClass = dataTemplateAttribute.ClassLinked;
                            if (dataTemplateAttributeLinkedClass is not null)
                            {
                                DataTemplates[dataTemplateAttributeLinkedClass] = type ;
                            }
                            break;
                        default:
                            
                            break;
                    }

                }

            }
            ChackData();
        }

        void ChackData()
        {
            var ThisType = GetType();
            var reflType = typeof(IReflectionContent);
            if (Models.Count > 0) ErrorService.CallException("No models were found in assembly", ThisType, reflType);
            if (DetailsViewModels.Count > 0) ErrorService.CallException("No ViewModels were found in assembly", ThisType, reflType);
        }


//Converter Part-----------------------------------------------------------------------------------
        public BaseViewModel ConvertToViewModel(Thing model)
        {
            Type modelType = model.GetType();
            var ViewModels = DetailsViewModels;
            Type viewModeType = ViewModels.ContainsKey(modelType) ? ViewModels[modelType] : null;
            var methodToInvoke = viewModeType
                .GetMethod("CreateFromTheModel");
            return methodToInvoke.Invoke(null, new object[] { model }) as BaseViewModel;
        }
        public DataTemplate ConvertToDataTemplate(Thing model)
        {
            Type modelType = model.GetType();
            var Templatetype = DataTemplates.ContainsKey(modelType) ?
            DataTemplates[modelType] : DataTemplates[DataTemplatesDefaultValue];
            return (DataTemplate)Activator.CreateInstance(Templatetype);
        }
        public ContentPage ConvertToContentPage(Thing model)
        {
            Type modelType = model.GetType();
            var PageType = ContentPages.ContainsKey(modelType) ?
            ContentPages[modelType] : ContentPages[ContentPageDefaultValue];
            return (ContentPage)Activator.CreateInstance(PageType);
        }


        public DataTemplate ConvertToDataTemplate(Type type)
        {
            var Templatetype = DataTemplates.ContainsKey(type) ?
            DataTemplates[type] : DataTemplates[DataTemplatesDefaultValue];
            return (DataTemplate)Activator.CreateInstance(Templatetype);
        }

    }
}

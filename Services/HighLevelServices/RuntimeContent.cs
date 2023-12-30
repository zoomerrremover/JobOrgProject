
using System.Reflection;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services.HighLevelServices
{
    public class RuntimeContent:IReflectionContent,IXAMLContent,IConverter
    {
        public List<Type> Models { get; private set; }
        public Dictionary<Type, Type> DetailsViewModels { get; private set; }
        public Dictionary<Type, DataTemplate> DataTemplates {get;private set; }
        public Dictionary<Type, ContentPage> ContentPages {get;private set; }
        public Type DataTemplatesDefaultValue {get;private set;}
        public Type ContentPageDefaultValue {get;private set;}

        IErrorService ErrorService { get; set;}

        public RuntimeContent(IErrorService errorService)
        {
            ErrorService = errorService;
            Initialize();
        }
        void Initialize()
        {
            Models = new List<Type>();
            DetailsViewModels = new Dictionary<Type, Type>();
            DataTemplates = new();
            ContentPages = new();
            ContentPageDefaultValue = null;
            DataTemplatesDefaultValue = typeof(Thing);


        }
        void InitializeReflection()
        {
            Models = new List<Type>();
            DetailsViewModels = new Dictionary<Type, Type>();
            InitializeReflectionContent();
        }
        void InitializeReflectionContent()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                ModelCheck(type);
                DetailsViewModelCheck(type);

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

        void ModelCheck(Type type)
        {
            if (type.IsClass && Attribute.IsDefined(type, typeof(Model)))
            {

                Models.Add(type);
            }
        }
        private void DetailsViewModelCheck(Type type)
        {
            if (type.IsClass & type.GetCustomAttribute(typeof(DetailsViewModel)) is not null)
            {
                var attribute = (DetailsViewModel)Attribute.GetCustomAttribute(type, typeof(DetailsViewModel));
                var LinkedClass = attribute.ClassLinked;
                if (LinkedClass is not null)
                {
                    DetailsViewModels[LinkedClass] = type;
                }
            }
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
            return DataTemplates.ContainsKey(modelType) ?
            DataTemplates[modelType] : DataTemplates[DataTemplatesDefaultValue];
        }
        public ContentPage ConvertToContentPage(Thing model)
        {
            Type modelType = model.GetType();
            return ContentPages.ContainsKey(modelType) ?
            ContentPages[modelType] : ContentPages[ContentPageDefaultValue];
        }
    }
}

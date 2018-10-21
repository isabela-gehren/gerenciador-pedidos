using AutoMapper;

namespace GEPED.Web
{
    public class AutoMapperConfig 
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMapping>();
                x.AddProfile<ViewModelToDomainMapping>();
            });

        }
    }
}
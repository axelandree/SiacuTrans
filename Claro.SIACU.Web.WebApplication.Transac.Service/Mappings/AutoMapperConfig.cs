using AutoMapper;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Mappings
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
            {
                ServiceModelToViewModelMappings.Configure(config);
                ViewModelToServiceModelMappings.Configure(config);
                ViewModelToViewModelMappings.Configure(config);
            });
        }
    }
}
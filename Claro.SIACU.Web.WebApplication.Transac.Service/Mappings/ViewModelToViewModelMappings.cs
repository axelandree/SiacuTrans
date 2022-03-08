using AutoMapper;
using FolderModel = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Mappings
{
    public class ViewModelToViewModelMappings
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<FolderModel.ProgramTaskModelExcel, FolderModel.ScheduledTransactionModel>()
                .ForMember(s => s.CO_ID, o => o.MapFrom(d => d.Contract))
                .ForMember(s => s.CUSTOMER_ID, o => o.MapFrom(d => d.CustomerId))
                .ForMember(s => s.SERVD_DATEPROG, o => o.MapFrom(d => d.ProgramationDate))
                .ForMember(s => s.SERVD_DATE_REG, o => o.MapFrom(d => d.RegisterDate))
                .ForMember(s => s.SERVD_DATE_EJEC, o => o.MapFrom(d => d.EjectDate))
                .ForMember(s => s.DESC_STATE, o => o.MapFrom(d => d.State))
                .ForMember(s => s.DESC_SERVICE, o => o.MapFrom(d => d.ServiceDescription))
                .ForMember(s => s.SERVC_NUMBERACCOUNT, o => o.MapFrom(d => d.Account))
                .ForMember(s => s.SERVC_TYPE_SERV, o => o.MapFrom(d => d.ServiceType));

            config.CreateMap<FolderModel.CallDetails.BilledCallsDetailHfcModel, FolderModel.CustomersDataModel>()
                .ForMember(s => s.StrCustomerId, o => o.MapFrom(d => d.CustomerId))
                .ForMember(s => s.StrPhone, o => o.MapFrom(d => d.Telephone))
                .ForMember(s => s.StrUser, o => o.MapFrom(d => d.CurrentUser))
                .ForMember(s => s.StrName, o => o.MapFrom(d => d.NameComplet))
                .ForMember(s => s.StrLastName, o => o.MapFrom(d => d.LastName))
                .ForMember(s => s.StrBusinessName, o => o.MapFrom(d => d.RazonSocial))
                .ForMember(s => s.StrDocumentType, o => o.MapFrom(d => d.TypeDoc))
                .ForMember(s => s.StrDocumentNumber, o => o.MapFrom(d => d.NroDoc))
                .ForMember(s => s.StrAccount, o => o.MapFrom(d => d.Cuenta))
                .ForMember(s => s.StrDistrict, o => o.MapFrom(d => d.StrDistrict))
                .ForMember(s => s.StrDepartament, o => o.MapFrom(d => d.StrDepartament))
                .ForMember(s => s.StrProvince, o => o.MapFrom(d => d.StrProvince))
                .ForMember(s => s.StrModality, o => o.MapFrom(d => d.StrModality));
        }
    }
}
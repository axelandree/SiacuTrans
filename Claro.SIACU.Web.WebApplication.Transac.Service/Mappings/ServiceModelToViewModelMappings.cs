using AutoMapper;
using CallDetailHelpers = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails;
using FixedSeviceModels = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using CommonSeviceModels = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Mappings
{
    public class ServiceModelToViewModelMappings
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<FixedSeviceModels.PhoneCall, CallDetailHelpers.UnBilledCallsDetail>();

            config.CreateMap<CommonSeviceModels.Typification, Model.TypificationModel>()
                .ForMember(s => s.Type, o => o.MapFrom(d => d.TIPO))
                .ForMember(s => s.Class, o => o.MapFrom(d => d.CLASE))
                .ForMember(s => s.SubClass, o => o.MapFrom(d => d.SUBCLASE))
                .ForMember(s => s.InteractionCode, o => o.MapFrom(d => d.INTERACCION_CODE))
                .ForMember(s => s.TypeCode, o => o.MapFrom(d => d.TIPO_CODE))
                .ForMember(s => s.ClassCode, o => o.MapFrom(d => d.CLASE_CODE))
                .ForMember(s => s.SubClassCode, o => o.MapFrom(d => d.SUBCLASE_CODE));


            config.CreateMap<CommonSeviceModels.InteractionTemplate, Model.TemplateInteractionModel>();

            config.CreateMap<CommonSeviceModels.ConsultSecurity, Model.SecurityModel>();


            config.CreateMap<FixedSeviceModels.BilledCallsDetailHFC, CallDetailHelpers.BilledCallsDetail>()
                .ForMember(s => s.CargOriginal, o => o.MapFrom(d => d.CargOrig))
                .ForMember(s => s.CurrentNumber, o => o.MapFrom(d => d.NroRegistre))
                .ForMember(s => s.DestinationPhone, o => o.MapFrom(d => d.TelephoneDest))
                .ForMember(s => s.TypeCalls, o => o.MapFrom(d => d.TypeCalls))
                .ForMember(s => s.Destination, o => o.MapFrom(d => d.Destination))
                .ForMember(s => s.Operator, o => o.MapFrom(d => d.Operator))
                .ForMember(s => s.StrDate, o => o.MapFrom(d => d.StrDate))
                .ForMember(s => s.StrHour, o => o.MapFrom(d => d.StrHour))
                .ForMember(d => d.NroCustomer, o => o.MapFrom(s => s.NroCustomer))
                .ForMember(d => d.Type, o => o.MapFrom(d => d.TypeCalls))
                .ForMember(d => d.TelephoneDestExport, o => o.MapFrom(d => d.TelephoneDestExport));

            config.CreateMap<CommonSeviceModels.Iteraction, Model.InteractionModel>()
                .ForMember(s => s.ObjidContacto, o => o.MapFrom(d => d.OBJID_CONTACTO))
                .ForMember(s => s.ObjidSite, o => o.MapFrom(d => d.OBJID_SITE))
                .ForMember(s => s.Cuenta, o => o.MapFrom(d => d.CUENTA))
                .ForMember(s => s.IdInteractio, o => o.MapFrom(d => d.ID_INTERACCION))
                .ForMember(s => s.DateCreaction, o => o.MapFrom(d => d.FECHA_CREACION))
                .ForMember(s => s.Telephone, o => o.MapFrom(d => d.TELEFONO))
                .ForMember(s => s.Type, o => o.MapFrom(d => d.TIPO))
                .ForMember(s => s.Class, o => o.MapFrom(d => d.CLASE))
                .ForMember(s => s.SubClass, o => o.MapFrom(d => d.SUBCLASE))
                .ForMember(s => s.Tipification, o => o.MapFrom(d => d.TIPIFICACION))
                .ForMember(s => s.TypeCode, o => o.MapFrom(d => d.TIPO_CODIGO))
                .ForMember(s => s.ClassCode, o => o.MapFrom(d => d.CLASE_CODIGO))
                .ForMember(s => s.SubClassCode, o => o.MapFrom(d => d.SUBCLASE_CODIGO))
                .ForMember(s => s.InsertPor, o => o.MapFrom(d => d.INSERTADO_POR))
                .ForMember(s => s.TypeInter, o => o.MapFrom(d => d.TIPO_INTER))
                .ForMember(s => s.Method, o => o.MapFrom(d => d.METODO))
                .ForMember(s => s.Result, o => o.MapFrom(d => d.RESULTADO))
                .ForMember(s => s.MadeOne, o => o.MapFrom(d => d.HECHO_EN_UNO))
                .ForMember(s => s.Agenth, o => o.MapFrom(d => d.AGENTE))
                .ForMember(s => s.NameAgenth, o => o.MapFrom(d => d.NOMBRE_AGENTE))
                .ForMember(s => s.ApellidoAgenth, o => o.MapFrom(d => d.APELLIDO_AGENTE))
                .ForMember(s => s.IdCase, o => o.MapFrom(d => d.ID_CASO))
                .ForMember(s => s.Note, o => o.MapFrom(d => d.NOTAS))
                .ForMember(s => s.FlagCase, o => o.MapFrom(d => d.FLAG_CASO))
                .ForMember(s => s.UserProces, o => o.MapFrom(d => d.USUARIO_PROCESO))
                .ForMember(s => s.Service, o => o.MapFrom(d => d.SERVICIO))
                .ForMember(s => s.Inconveniente, o => o.MapFrom(d => d.INCONVENIENTE))
                .ForMember(s => s.ServiceCode, o => o.MapFrom(d => d.SERVICIO));

            config.CreateMap<FixedSeviceModels.BEDeco, Model.LTE.CheckDevice>();
            config.CreateMap<FixedSeviceModels.BEDeco, Model.HFC.CheckDeviceHFC>();
            config.CreateMap<FixedSeviceModels.ListScheduledTransactionsResponse, Model.ScheduledTransactionModel>();
            config.CreateMap<FixedSeviceModels.TransactionScheduled, Model.ScheduledTransactionHfcModel>();
        }
    }
}
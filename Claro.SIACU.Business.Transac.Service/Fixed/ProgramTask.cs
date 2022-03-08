using System.Collections.Generic;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class ProgramTask
    { 
        public static EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse GetListScheduledTransactions(EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsRequest request)
        {
            bool correctProcess = false;

            List<EntitiesFixed.ScheduledTransaction> list = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ProgramTask.GetListScheduledTransactions(request.Audit.Session, request.Audit.Transaction,
                        request.IdTransaction, request.ApplicationCode, request.ApplicationName, request.UserApp, request.ServiCoId, request.StrStartDate, request.StrEndDate ,
                        request.ServiceState, request.Advisor, request.Account, request.TransactionType, request.CodeInteraction, request.NameCACDAC , out correctProcess);
                });

            EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse objResponse = new EntitiesFixed.GetListScheduledTransactions.ListScheduledTransactionsResponse() { 
                LstTransactions = list ,
                CorrectProcess = correctProcess          
            }; 

            return objResponse; 
        }

        public static EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse DeleteProgramTask(EntitiesFixed.DeleteProgramTask.DeleteProgramTaskRequest request)
        {
            EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse objResponse = new EntitiesFixed.DeleteProgramTask.DeleteProgramTaskResponse();

            objResponse.ResponseStatus = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.ProgramTask.DeleteProgramTask(request);
            });

            return objResponse;
        }

        public static EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse DeleteProgramTaskHfc(EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcRequest request)
        {
            EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse objResponse = new EntitiesFixed.DeleteProgramTaskHfc.DeleteProgTaskHfcResponse();

            objResponse.ResultStatus = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.ProgramTask.DeleteProgramTaskHfc(request.StrIdSession, request.StrTransaction, request.VstrServCod, request.VstrCodId, request.VstrServCEstado);
            });

            return objResponse;
        }

        public static EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse UpdateProgTaskLte(EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteRequest request)
        {
            EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse objResponse = new EntitiesFixed.PostUpdateProgTaskLte.UpdateProgTaskLteResponse();

            objResponse.ResultStatus = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.ProgramTask.UpdateProgramTaskLte(request.StrIdSession, request.StrTransaction, request.CodigoAplicacion, request.NombreAplicacion, request.UsuarioApp, request.ServiCod, request.ConId, request.ServiEstado, request.FechaProg);
            });

            return objResponse;
        }
    }
}

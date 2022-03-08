using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegisterInteractionAdjustRequest : Claro.Entity.Request
    {
        #region "Constructor"
        public RegisterInteractionAdjustRequest()
        {
            RegistrarInteraccion = new RegistrarInteraccion();
            RegistrarDetalleInteraccion = new RegistrarDetalleInteraccion();
            RegistrarCabeceraDoc = new RegistrarCabeceraDoc();
            RegistrarDetalleDoc = new RegistrarDetalleDoc();
            RegistrarAreaImputable = new RegistrarAreaImputable();
            AprobarDocumento = new AprobarDocumento();
            RegistrarAjusteOAC = new RegistrarAjusteOAC();
            RegistrarExportarSap = new RegistrarExportarSap();
            ActualizarCamposSap = new ActualizarCamposSap();
            listaRequestOpcional = new List<ObjetoOpcional>();
        }
        #endregion
        

        [DataMember]
        public string ContingClafifyAct { get; set; }
        [DataMember]
        public string pLTE { get; set; }
        [DataMember]
        public RegistrarInteraccion RegistrarInteraccion { get; set; }
        [DataMember]
        public RegistrarDetalleInteraccion RegistrarDetalleInteraccion { get; set; }
        [DataMember]
        public RegistrarCabeceraDoc RegistrarCabeceraDoc { get; set; }
        [DataMember]
        public RegistrarDetalleDoc RegistrarDetalleDoc { get; set; } 
        [DataMember]
        public RegistrarAreaImputable RegistrarAreaImputable { get; set; } 
        [DataMember]
        public AprobarDocumento AprobarDocumento { get; set; }
        [DataMember]
        public RegistrarAjusteOAC RegistrarAjusteOAC { get; set; }
        [DataMember]
        public RegistrarExportarSap RegistrarExportarSap { get; set; }
        [DataMember]
        public ActualizarCamposSap ActualizarCamposSap { get; set; }
        [DataMember]
        public List<ObjetoOpcional> listaRequestOpcional { get; set; }

    }
}

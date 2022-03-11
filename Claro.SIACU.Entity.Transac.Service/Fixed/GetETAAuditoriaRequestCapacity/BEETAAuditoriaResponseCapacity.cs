using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity
{
   [DataContract(Name = "BEETAAuditoriaResponseCapacityHFC")]
   public class BEETAAuditoriaResponseCapacity : Claro.Entity.Request
    {
        public BEETAAuditoriaResponseCapacity()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }
        #region "Atributos"

        private string _idTransaccion;

        private string _codigoRespuesta;

        private string _mensajeRespuesta;

        private string _duraActivity;

        private string _tiempoViajeActivity;


        private BEETAEntidadcapacidadType[] _ObjetoCapacity;

        private BEETAListaEntidadparametrosResponse[] _ObjetoResponseReq;
        #endregion"Atributos"

        #region "Propiedades"




        [DataMember]
        public string IdTransaccion
        {
            get
            {
                return this._idTransaccion;
            }
            set
            {
                this._idTransaccion = value;
            }
        }

        [DataMember]
        public string CodigoRespuesta
        {
            get
            {
                return this._codigoRespuesta;
            }
            set
            {
                this._codigoRespuesta = value;
            }
        }
        [DataMember]
        public string MensajeRespuesta
        {
            get
            {
                return this._mensajeRespuesta;
            }
            set
            {
                this._mensajeRespuesta = value;
            }
        }

        [DataMember]
        public string DuraActivity
        {
            get
            {
                return this._duraActivity;
            }
            set
            {
                this._duraActivity = value;
            }
        }
        [DataMember]
        public string TiempoViajeActivity
        {
            get
            {
                return this._tiempoViajeActivity;
            }
            set
            {
                this._tiempoViajeActivity = value;
            }
        }
        [DataMember]
        public BEETAEntidadcapacidadType[] ObjetoCapacity
        {
            get
            {
                return this._ObjetoCapacity;
            }
            set
            {
                this._ObjetoCapacity = value;
            }
        }
        [DataMember]
        public BEETAListaEntidadparametrosResponse[] ObjetoResponseReq
        {
            get
            {
                return this._ObjetoResponseReq;
            }
            set
            {
                this._ObjetoResponseReq = value;
            }
        }

        #endregion "Propiedades"

    }
}

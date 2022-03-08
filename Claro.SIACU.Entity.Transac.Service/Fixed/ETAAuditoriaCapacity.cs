using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class ETAAuditoriaCapacity
    {
        public ETAAuditoriaCapacity()
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

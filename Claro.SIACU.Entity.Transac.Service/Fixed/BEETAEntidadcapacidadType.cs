using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
	/// <summary>
	/// Descripción breve de AreaBE.
	/// </summary>
    public class BEETAEntidadcapacidadType
	{
        public BEETAEntidadcapacidadType()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		#region "Atributos"



        private string _ubicacion;
        private DateTime _fecha;
        private string _espacioTiempo;
        private string _habilidadTrabajo;
        private long _cuota;
        private long _disponible;

		#endregion"Atributos"

		#region "Propiedades"

        public string Ubicacion
		{
			get
			{
                return this._ubicacion;
			}
			set
			{
                this._ubicacion = value;
			}
		}
        public DateTime Fecha
		{
			get
			{
                return this._fecha;
			}
			set
			{
                this._fecha = value;
			}
		}
        public string EspacioTiempo
        {
            get
            {
                return this._espacioTiempo;
            }
            set
            {
                this._espacioTiempo = value;
            }
        }
        public string HabilidadTrabajo
        {
            get
            {
                return this._habilidadTrabajo;
            }
            set
            {
                this._habilidadTrabajo = value;
            }
        }
        public long Cuota
        {
            get
            {
                return this._cuota;
            }
            set
            {
                this._cuota = value;
            }
        }
        public long Disponible
        {
            get
            {
                return this._disponible;
            }
            set
            {
                this._disponible = value;
            }
        }

		#endregion "Propiedades"

	}
}

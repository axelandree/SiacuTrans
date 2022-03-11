using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "BEETACampoActivityHfc")]
	/// <summary>
	/// Descripción breve de AreaBE.
	/// </summary>
    public class BEETACampoActivity 
	{
        public BEETACampoActivity()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		#region "Atributos"


		private string _Nombre;
		private string _Valor;


		#endregion"Atributos"

		#region "Propiedades"

	


        [DataMember]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				this._Nombre=value;
			}
		}
        [DataMember]
		public string Valor
		{
			get
			{
				return this._Valor;
			}
			set
			{
				this._Valor=value;
			}
		}


		#endregion "Propiedades"

	}
}

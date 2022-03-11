using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name="BEETAParamRequestCapacityHfc")]
	/// <summary>
	/// Descripción breve.
	/// </summary>
    public class BEETAParamRequestCapacity
	{
        public BEETAParamRequestCapacity()
		{
            Valor = string.Empty;
            Campo = string.Empty;
		}
       
		#region "Atributos"


		private string _Campo;
		private string _Valor;


		#endregion"Atributos"

		#region "Propiedades"

	


        [DataMember]
		public string Campo
		{
			get
			{
				return this._Campo;
			}
			set
			{
                this._Campo = value;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan
{
	/// <summary>
	/// Descripción breve.
	/// </summary>
    public class BEETAParamRequestCapacity
	{
        public BEETAParamRequestCapacity()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		#region "Atributos"


		private string _Campo;
		private string _Valor;


		#endregion"Atributos"

		#region "Propiedades"

	



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

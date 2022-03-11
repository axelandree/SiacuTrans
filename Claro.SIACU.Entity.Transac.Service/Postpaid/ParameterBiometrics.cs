using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ParameterBiometrics")]
    public class ParameterBiometrics
    {
        public ParameterBiometrics()
		{}

        public ParameterBiometrics(Int64 vIdParametro, string vValor, string vDescripcion, string vValor1, string vValor2)
		{			
			_ID_PARAMETRO = vIdParametro;
			_VALOR = vValor;
			_DESCRIPCION = vDescripcion;			
            VALOR2 = vValor2;
		}

        public ParameterBiometrics(Int64 vIdParametro, string vValor, string vDescripcion, string vValor1)
        {
            _ID_PARAMETRO = vIdParametro;
            _VALOR = vValor;
            _DESCRIPCION = vDescripcion;
            _VALOR1 = vValor1;
        }
	
		private Int64 _ID_PARAMETRO;
		private string _VALOR;
        private string _VALOR1;
		private string _DESCRIPCION;
			
		public Int64 ID_PARAMETRO
		{
			set{_ID_PARAMETRO=value;}
			get{return _ID_PARAMETRO;}
		}

		public string VALOR
		{
			set{_VALOR=value;}
			get{return _VALOR;}
		}

        public string VALOR1
        {
        set { _VALOR1 = value; }
        get { return _VALOR1; }
        } 

        public string VALOR2 { get; set; }

		public string DESCRIPCION
		{
			set{_DESCRIPCION=value;}
			get{return _DESCRIPCION;}
		}
    }
}

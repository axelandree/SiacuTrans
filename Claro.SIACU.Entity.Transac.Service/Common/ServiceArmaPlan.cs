using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ServiceArmaPlan
    {
        public ServiceArmaPlan()
        { }

        private string _ID_INTERACCION;
        private string _COD_SERVICIO;
        private string _DES_SERVICIO;
        private string _MOTIVO_EXCLUYE;
        private decimal _CARGO_FIJO;
        private int _PERIODO;
        private string _USUARIO;
        private DateTime _FEC_CREACION;

        public string ID_INTERACCION
        {
            set { _ID_INTERACCION = value; }
            get { return _ID_INTERACCION; }
        }
        public string COD_SERVICIO
        {
            set { _COD_SERVICIO = value; }
            get { return _COD_SERVICIO; }
        }
        public string DES_SERVICIO
        {
            set { _DES_SERVICIO = value; }
            get { return _DES_SERVICIO; }
        }
        public string MOTIVO_EXCLUYE
        {
            set { _MOTIVO_EXCLUYE = value; }
            get { return _MOTIVO_EXCLUYE; }
        }
        public decimal CARGO_FIJO
        {
            set { _CARGO_FIJO = value; }
            get { return _CARGO_FIJO; }
        }
        public int PERIODO
        {
            set { _PERIODO = value; }
            get { return _PERIODO; }
        }
        public string USUARIO
        {
            set { _USUARIO = value; }
            get { return _USUARIO; }
        }
        public DateTime FEC_CREACION
        {
            set { _FEC_CREACION = value; }
            get { return _FEC_CREACION; }
        }
    }

}

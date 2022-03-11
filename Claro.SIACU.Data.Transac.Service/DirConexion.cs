using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.Transac.Service
{
    public abstract class DirConexion : Conexion
    {
        public DirConexion(string aplicacion)
            : base(aplicacion) { }

        public abstract IClaroBDConfiguracion Configuracion { get; }

        //public override DAABRequest CreaRequest()
        //{
        //    DAABRequest.TipoOrigenDatos obOrigen;
        //    if (Configuracion.Provider.IndexOf("ORA") > 0 || Configuracion.Provider == "")
        //    {
        //        obOrigen = DAABRequest.TipoOrigenDatos.ORACLE;
        //    }
        //    else
        //    {
        //        obOrigen = DAABRequest.TipoOrigenDatos.SQL;
        //    }
        //    return new DAABRequest(obOrigen, Configuracion.CadenaConexion);
        //}
    }
}

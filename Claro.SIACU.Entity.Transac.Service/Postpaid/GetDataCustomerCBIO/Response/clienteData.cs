using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response
{
    public class clienteData
    {
        public string apellidos { get; set; }
        public string asesor { get; set; }
        public string cargo { get; set; }
        public string codigoTipoCliente { get; set; }
        public string codPostalFac { get; set; }
        public string codPostalLeg { get; set; }
        public string coId { get; set; }
        public string contactoCliente { get; set; }
        public string creditScore { get; set; }
        public string cuenta { get; set; }
        public string customerId { get; set; }
        public string departamentoFac { get; set; }
        public string departamentoLeg { get; set; }
        public string direccionFac { get; set; }
        public string direccionLeg { get; set; }
        public string distritoFac { get; set; }
        public string distritoLeg { get; set; }
        public string email { get; set; }
        public string estadoCivil { get; set; }
        public string estadoCivilId { get; set; }
        public string fax { get; set; }
        public string fechaAct { get; set; }
        public string fechaNac { get; set; }
        public string formaPago { get; set; }
        public string lugNac { get; set; }
        public string nacionalidad { get; set; }
        public string nombComercial { get; set; }
        public string nombre { get; set; }
        public string numDoc { get; set; }
        public string paisFac { get; set; }
        public string paisLeg { get; set; }
        public string provinciaFac { get; set; }
        public string provinciaLeg { get; set; }
        public string razonSocial { get; set; }
        public string repLegal { get; set; }
        public string rucDni { get; set; }
        public string sexo { get; set; }
        public string telefContacto { get; set; }
        public string telefPrincipal { get; set; }
        public string titulo { get; set; }
        public string tipDoc { get; set; }
        public string tipDocId { get; set; }
        public string urbanizacionFac { get; set; }
        public string urbanizacionLeg { get; set; }
        public string csIdPub { get; set; }
        public string coIdPub { get; set; }
        public string flagConvivencia { get; set; }
        public string contactSeqno { get; set; }


        public string direccionLegal { get; set; }
        public string direccionReferenciaLegal { get; set; }
        public string paisLegal { get; set; }
        public string departamentoLegal { get; set; }
        public string provinciaLegal { get; set; }
        public string distritoLegal { get; set; }
        public string urbanizacionLegal { get; set; }
        public string codigoPostalLegal { get; set; }
        public string lugarNacimientoId { get; set; }
        public string dirReferenciaFac { get; set; }
        //apellidos separados
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string indicadorCambioNumero { get; set; }
        public string nacionalidadId { get; set; }
        public List<listaRepresentanteLegal> listaRepresentanteLegal { get; set; }
    }
}

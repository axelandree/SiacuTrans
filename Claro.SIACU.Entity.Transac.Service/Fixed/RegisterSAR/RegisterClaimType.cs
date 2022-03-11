using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterClaimType
    {
        [DataMember]
        public string piCodigoRecurso { get; set; }
        [DataMember]
        public string piFaseRecurso { get; set; }
        [DataMember]
        public string piFechaCreacionRecurso { get; set; }
        [DataMember]
        public string piHoraCreacionRecurso { get; set; }
        [DataMember]
        public string piEstadoRecurso { get; set; }
        [DataMember]
        public string piCondicionRecurso { get; set; }
        [DataMember]
        public string piResultadoRecurso { get; set; }
        [DataMember]
        public string piNombreAdjuntosRecurso { get; set; }
        [DataMember]
        public string piNotasRecurso { get; set; }
        [DataMember]
        public string piUsuarioReg { get; set; }
        [DataMember]
        public string piNombres { get; set; }
        [DataMember]
        public string piApellidos { get; set; }
        [DataMember]
        public string piDireccion { get; set; }
        [DataMember]
        public string piEmail { get; set; }
        [DataMember]
        public string piTelefonoRefencia { get; set; }
        [DataMember]
        public string piRelacionTitular { get; set; }
        [DataMember]
        public string piAprobadorReclamo { get; set; }
        [DataMember]
        public string piCentroAtencion { get; set; }
        [DataMember]
        public string piNumeroResolucion { get; set; }
        [DataMember]
        public string piFechaResolucion { get; set; }
        [DataMember]
        public string piFechaNotificacion { get; set; }
        [DataMember]
        public string piModoNotificacion { get; set; }
        [DataMember]
        public string piCantidadFolios { get; set; }
        [DataMember]
        public string piTipoReclamante { get; set; }
        [DataMember]
        public string piTipoDocumento { get; set; }
        [DataMember]
        public string piNumeroDocumento { get; set; }
        [DataMember]
        public string piPoderExpediente { get; set; }
        [DataMember]
        public string piCodFormaNotificacion { get; set; }
        [DataMember]
        public string piDescFormaNotificacion { get; set; }
        [DataMember]
        public string piPlazoAtencion { get; set; }
        [DataMember]
        public string piFechaAtencion { get; set; }
        [DataMember]
        public string piFechaAplazado { get; set; }
        [DataMember]
        public string piSolicitudRecurso { get; set; }
        [DataMember]
        public string piFaReMedioAceptacion { get; set; }
        [DataMember]
        public string piFaReSolucionAdecuada { get; set; }
        [DataMember]
        public string piFaReFechaAceptacion { get; set; }
        [DataMember]
        public string piFaReHoraAceptacion { get; set; }
        [DataMember]
        public string piFaReNroCartaDescargos { get; set; }
        [DataMember]
        public string piFaReFechaDescargos { get; set; }
        [DataMember]
        public string piFaReCliNombre { get; set; }
        [DataMember]
        public string piFaReCliApellido { get; set; }
        [DataMember]
        public string piFaReCliTipoDoc { get; set; }
        [DataMember]
        public string piFaReCliNumDoc { get; set; }
        [DataMember]
        public string piFaReCliRepresentante { get; set; }
        [DataMember]
        public string piDepartamento { get; set; }
        [DataMember]
        public string piProvincia { get; set; }
        [DataMember]
        public string piDistrito { get; set; }
    }
}

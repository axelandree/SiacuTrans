var HFCPOST_Session = {};

HFCPOST_Session.UsuarioAcceso = {};
HFCPOST_Session.DatosCliente = {};
HFCPOST_Session.DatosLinea = {};
HFCPOST_Session.Url = {};

HFCPOST_Session.UsuarioAcceso.CodigoUsuario = '1100';
HFCPOST_Session.UsuarioAcceso.Usuario = "";
HFCPOST_Session.UsuarioAcceso.Accesos = "";

HFCPOST_Session.DatosCliente.CONTRATO_ID = "5744793";
HFCPOST_Session.DatosCliente.UBIGEO_INST = "150103";
HFCPOST_Session.DatosCliente.NOMBRE_COMPLETO = "";
HFCPOST_Session.DatosCliente.EMAIL = "ventas@gmail.com";
HFCPOST_Session.DatosCliente.RAZON_SOCIAL = "IFU91BN9SD";
HFCPOST_Session.DatosCliente.FECHA_ACT = "3/28/2012 7:02:16 AM";
HFCPOST_Session.DatosCliente.REPRESENTANTE_LEGAL = "P86F68WBXB";
HFCPOST_Session.DatosCliente.DNI_RUC = "44434244";
HFCPOST_Session.DatosCliente.NRO_DOC = "44434244";
HFCPOST_Session.DatosCliente.CODIGO_PLANO_INST = "LMAT001";
HFCPOST_Session.DatosCliente.DIRECCION_DESPACHO = "TJQQ640HX1";
HFCPOST_Session.DatosCliente.URBANIZACION_LEGAL = "3YSS7GC9EM";
HFCPOST_Session.DatosCliente.DEPARTEMENTO_LEGAL = "HUANCAVELICA";
HFCPOST_Session.DatosCliente.DISTRITO_LEGAL = "ACRAQUIA";
HFCPOST_Session.DatosCliente.PAIS_LEGAL = "Peru";
HFCPOST_Session.DatosCliente.PROVINCIA_LEGAL = "S15KTC3GWK";
HFCPOST_Session.DatosCliente.CICLO_FACTURACION = "05";
HFCPOST_Session.DatosCliente.NOMBRES = "YOV3Q3S9T1";
HFCPOST_Session.DatosCliente.APELLIDOS = "EKM1XUSF5X";
HFCPOST_Session.DatosCliente.TIPO_CLIENTE = "Business";
HFCPOST_Session.DatosCliente.CONTACTO_CLIENTE = "YOV3Q3S9T1 EKM1XUSF5X";
HFCPOST_Session.DatosCliente.REFERENCIA = "8LGTKPDAX2";
HFCPOST_Session.DatosCliente.CUSTOMER_ID = "4681916";
HFCPOST_Session.DatosCliente.LIMITE_CREDITO = "422";
HFCPOST_Session.DatosCliente.DIRECCION_FAC = "";
HFCPOST_Session.DatosCliente.URBANIZACION_FAC = "";
HFCPOST_Session.DatosCliente.PAIS_FAC = "";
HFCPOST_Session.DatosCliente.DEPARTEMENTO_FAC = "";
HFCPOST_Session.DatosCliente.PROVINCIA_FAC = "";
HFCPOST_Session.DatosCliente.DISTRITO_FAC = "";

HFCPOST_Session.DatosLinea.cableTv = "T";
HFCPOST_Session.DatosLinea.StatusLinea = "Activo";
HFCPOST_Session.DatosLinea.Plan = "Plan 2 Play Cable -Telefono";
HFCPOST_Session.DatosLinea.FecActivacion = "2014-09-15 18:09:51";

//Constantes
HFCPOST_Session.strLetraF = "F";
HFCPOST_Session.Slash = "/";
HFCPOST_Session.strVariableEmpty = "";

HFCPOST_Session.intNumeroCero = 0;
HFCPOST_Session.intNumeroUno = 1;

HFCPOST_Session.strNumeroCeroDecimal = "0.00";
HFCPOST_Session.strNumeroMenosUno = "-1";
HFCPOST_Session.strNumeroCero = "0";
HFCPOST_Session.strNumeroUno = "1";

//Mensages
HFCPOST_Session.gConstMensajeNoTieneEquiposAdicionales = "";
HFCPOST_Session.MensajeProblemaLoad = "";
HFCPOST_Session.MensajeCantidadLimiteDeEquipos = "";
HFCPOST_Session.strMensajeValidaPlanComercial = ""; 
HFCPOST_Session.strTextoDecoNoTieneCable = "";
HFCPOST_Session.strTextoEstadoInactivo = "";
HFCPOST_Session.gConstKeyGenTipificacionDeco = "No se pudo generar la Tipificación, por favor vuelva a intentarlo mas tarde.";//
HFCPOST_Session.strMsgTituloDecoAdiCons = "INSTALACIÓN / DESINTALACIÓN DE DECODIFICADOR";//
HFCPOST_Session.gConstKeyAsuntoCorreoInstDeco = "Instalación/Desinstalación de Decodificador.";//
HFCPOST_Session.strMsgConsultaCustomerContratoVacio = "";

HFCPOST_Session.strDescActivo = "Activo";
HFCPOST_Session.DecoAdicionalAudit = "DecoAdicional";//

//Variables
HFCPOST_Session.rutaArchivo = "";
HFCPOST_Session.nombreArchivo = "";
HFCPOST_Session.SotDeBaja = "";
HFCPOST_Session.gAccesoInstalacionIDD = "";
HFCPOST_Session.gAccesoDesinstalacionIDD = "";
HFCPOST_Session.gAccesoFidelizaCostoIDD = "";
HFCPOST_Session.gAccesoGuardarIDD = "";
HFCPOST_Session.gSubTipoTrabajoDecoAdicional = "";
HFCPOST_Session.gSubTipoTrabajoBajaDeco = "";
HFCPOST_Session.idInteraccion = "";
HFCPOST_Session.strInteraccionId = "";
HFCPOST_Session.strCodError = "";
HFCPOST_Session.strMsgError = "";
HFCPOST_Session.InstDesins = "0"; //0 = Instalacion y 1 = Desinstalacion
HFCPOST_Session.igv = 0;
HFCPOST_Session.CantidadLimiteDeEquipos = 0;
HFCPOST_Session.MontoFidelizacionInstalacion = "";
HFCPOST_Session.MontoFidelizacionDesinstalacion = "";
HFCPOST_Session.intNroSec = 0;
HFCPOST_Session.intNroOST = 0;
HFCPOST_Session.intDesasociaDeco = 0;
HFCPOST_Session.resultAsociar = false;
HFCPOST_Session.resultDesasocia = false;
HFCPOST_Session.tblEquipos = {};
HFCPOST_Session.ListaEquiposAsociadosAlCliente = {};
HFCPOST_Session.ListaEquiposAdicionalesServer = {};
HFCPOST_Session.ListaEquiposBajaServer = {};

//Hidden
HFCPOST_Session.FechaActualServidor = "";
HFCPOST_Session.TipoTrabajo = "";
HFCPOST_Session.CodigoUbi = "";
HFCPOST_Session.FranjaHorariaFinal = "";
HFCPOST_Session.Validado = "";
HFCPOST_Session.CodEquipAlSelec = "";
HFCPOST_Session.CodigoPlan = "";
HFCPOST_Session.IdEliminacion = "";
HFCPOST_Session.IdComponenteEliminacion = "";
HFCPOST_Session.TipoEliminacion = "";
HFCPOST_Session.TipoServicioEliminar = "";
HFCPOST_Session.ErrValidarAge = "";
HFCPOST_Session.ContenidoEquipo = "";
HFCPOST_Session.ContenidoTablaCons = "";
HFCPOST_Session.CargoFijoTotal = "";
HFCPOST_Session.Cantidad = "";
HFCPOST_Session.CargoFijoTotalPlanCIGV = "";
HFCPOST_Session.CargoFijoTotalPlanSIGV = "";
HFCPOST_Session.HayServicioCoreCable = "";
HFCPOST_Session.CodigoSOT = "";
HFCPOST_Session.TipoTrabCU = "";
HFCPOST_Session.SubTipOrdCU = "";
HFCPOST_Session.IDPlano = "";
HFCPOST_Session.ValidaEta = "";
HFCPOST_Session.HistorialEta = "";
HFCPOST_Session.FranjaHorariaETA = "";
HFCPOST_Session.ParamOrigen = "";
HFCPOST_Session.ParamTipoServicio = "";
HFCPOST_Session.CodigoRequestAct = "";
HFCPOST_Session.CantidadListaEquipos = "";
HFCPOST_Session.ErrorProcesoEquiposAsociado = "";
HFCPOST_Session.MensajeEquiposAsociado = "";

HFCPOST_Session.AgregaAsociar = 0;
HFCPOST_Session.AgregaDesaso = 0;
HFCPOST_Session.Botonasociar = 0;
HFCPOST_Session.Cerrar = "";
HFCPOST_Session.CoID = "";
HFCPOST_Session.CodigoUbi = "";
HFCPOST_Session.TipoTrabajo = "";
HFCPOST_Session.ErrValidarAge = "";

HFCPOST_Session.Email = "";

HFCPOST_Session.IdEliminacion = "";
HFCPOST_Session.IdComponenteEliminacion = "";
HFCPOST_Session.TipoEliminacion = "";
HFCPOST_Session.TipoServicioEliminar = "";

HFCPOST_Session.MensajeConfirmacion = "";
HFCPOST_Session.Mensaje1 = "";
HFCPOST_Session.Mensaje2 = "";
HFCPOST_Session.Mensaje8 = "";
HFCPOST_Session.Mensaje10 = "";
HFCPOST_Session.MensajeErrorConsultaIGV = "";
HFCPOST_Session.MsgErrorTrasaccion = "";
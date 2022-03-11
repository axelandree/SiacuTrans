
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Common.GeneratePDF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GENERATEPDF = Claro.SIACU.ProxyService.Transac.Service.WSGeneratePDF;
namespace Claro.SIACU.Data.Transac.Service
{
    public static class GeneratePDF
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(GenerarPDF).Name);

        public static bool test(string a)
        {
            return true;
        }
        /// <summary>
        /// Método que genera Constancia PDF
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <param name="error"></param>
        /// <returns>bool</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        public static GeneratePDFDataResponse generateConstancePDF(GeneratePDFDataRequest oParametersGeneratePDF)
        {
            GeneratePDFDataResponse oResponse = new GeneratePDFDataResponse();
            string error = string.Empty;
            try
            {
                string xml = buildXML(oParametersGeneratePDF);
                //log.Info(string.Format("construyeXML: {0}", xml.ToString()));

                string strDateTransaction = oParametersGeneratePDF.StrFechaTransaccionProgram.Replace("/", "_");
                string strRutaPDF = string.Format("{0}{1}", oParametersGeneratePDF.StrCarpetaPDFs, oParametersGeneratePDF.StrCarpetaTransaccion);
                string strNombrePDF = string.Format("{0}_{1}_{2}.pdf", oParametersGeneratePDF.StrCasoInter, strDateTransaction, oParametersGeneratePDF.StrNombreArchivoTransaccion);
                Claro.Web.Logging.Info("123123123", "1", "oParametersGeneratePDF.StrServidorGenerarPDF: " + oParametersGeneratePDF.StrServidorGenerarPDF);
                oResponse = generatePDFPostSale(oParametersGeneratePDF.StrServidorGenerarPDF,
                    xml, strRutaPDF, strNombrePDF, ref error);
                
                return oResponse;
            }
            catch (Exception exd)
            {
                Claro.Web.Logging.Info("123123123", "1", "Error en el data exd" + exd.Message);
                if (exd.InnerException != null)
                {
                     Claro.Web.Logging.Info("123123123", "1", "Error en elexd.InnerException" + exd.InnerException);
                }
                error = exd.Message;
                //log.Info(string.Format("Error construyeXML: {0}", error));
                return oResponse;
            }


        }

        /// <summary>
        /// Método que construye XML
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        private static string buildXML(GeneratePDFDataRequest oParametersGeneratePDF)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + oParametersGeneratePDF.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<NRO_SERVICIO>" + oParametersGeneratePDF.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TITULAR_CLIENTE>" + oParametersGeneratePDF.StrTitularCliente + "</TITULAR_CLIENTE>";
            xml += "<CONTACTO_CLIENTE>" + oParametersGeneratePDF.StrContactoCliente + "</CONTACTO_CLIENTE>";
            xml += "<PLAN_ACTUAL>" + oParametersGeneratePDF.StrPlanActual + "</PLAN_ACTUAL>";
            xml += "<CENTRO_ATENCION_AREA>" + oParametersGeneratePDF.StrCentroAtencionArea + "</CENTRO_ATENCION_AREA>";
            xml += "<TIPO_DOC_IDENTIDAD>" + oParametersGeneratePDF.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + oParametersGeneratePDF.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<FECHA_TRANSACCION_PROGRAM>" + oParametersGeneratePDF.StrFechaTransaccionProgram + "</FECHA_TRANSACCION_PROGRAM>";
            xml += "<CASO_INTER>" + oParametersGeneratePDF.StrCasoInter + "</CASO_INTER>";
            xml += "<REPRES_LEGAL>" + oParametersGeneratePDF.StrRepresLegal + "</REPRES_LEGAL>";
            xml += "<NRO_DOC_REP_LEGAL>" + oParametersGeneratePDF.StrNroDocRepLegal + "</NRO_DOC_REP_LEGAL>";
            xml += "<CUSTOMER_ID>" + oParametersGeneratePDF.StrCustomerId + "</CUSTOMER_ID>";
            xml += "<CUENTA_POSTPAGO>" + oParametersGeneratePDF.StrCuentaPostpago + "</CUENTA_POSTPAGO>";
            xml += "<NOTAS>" + oParametersGeneratePDF.StrNotas + "</NOTAS>";
            xml += "<TELF_REFERENCIA>" + oParametersGeneratePDF.StrTelfReferencia + "</TELF_REFERENCIA>";
            xml += "<CICLO_FACTURACION>" + oParametersGeneratePDF.StrCicloFacturacion + "</CICLO_FACTURACION>";
            xml += "<FECHA_EJECUCION>" + oParametersGeneratePDF.StrFechaEjecucion + "</FECHA_EJECUCION>";
            xml += "<COD_USUARIO>" + oParametersGeneratePDF.StrCodUsuario + "</COD_USUARIO>";
            xml += "<NOMBRE_AGENTE_USUARIO>" + oParametersGeneratePDF.StrNombreAgenteUsuario + "</NOMBRE_AGENTE_USUARIO>";
            xml += "<APLICA_EMAIL>" + oParametersGeneratePDF.StrAplicaEmail + "</APLICA_EMAIL>";
            xml += "<EMAIL>" + oParametersGeneratePDF.StrEmail + "</EMAIL>";
            xml += "<APLICA_OTRO_CONTACTO>" + oParametersGeneratePDF.StrAplicaOtroContacto + "</APLICA_OTRO_CONTACTO>";
            xml += "<CONTACTO_OTRO>" + oParametersGeneratePDF.StrContactoOtro + "</CONTACTO_OTRO>";
            xml += "<NRO_DOC_CONTACTO_OTRO>" + oParametersGeneratePDF.StrNroDocContactoOtro + "</NRO_DOC_CONTACTO_OTRO>";
            xml += "<MOTIVO_PARIENTE>" + oParametersGeneratePDF.StrMotivoPariente + "</MOTIVO_PARIENTE>";
            xml += "<TELF_OTRO_CONTACTO>" + oParametersGeneratePDF.StrTelfOtroContacto + "</TELF_OTRO_CONTACTO>";
            xml += "<CANAL_ATENCION>" + oParametersGeneratePDF.StrCanalAtencion + "</CANAL_ATENCION>";
            xml += "<FLAG_PLANTILLA_PLAZO>" + oParametersGeneratePDF.StrFlagPlantillaPlazo + "</FLAG_PLANTILLA_PLAZO>";
            xml += "<ESCENARIO_SERVICIO_COM>" + oParametersGeneratePDF.StrEscenarioServicioCom + "</ESCENARIO_SERVICIO_COM>";
            xml += "<APLICA_PROGRAMACION>" + oParametersGeneratePDF.StrAplicaProgramacion + "</APLICA_PROGRAMACION>";
            xml += "<CF_SERVICIO_COM>" + oParametersGeneratePDF.StrCfServicioCom + "</CF_SERVICIO_COM>";
            xml += "<FECHA_PLAZO>" + oParametersGeneratePDF.StrFechaPlazo + "</FECHA_PLAZO>";
            xml += "<PLAZO>" + oParametersGeneratePDF.StrPlazo + "</PLAZO>";
            xml += "<CANAL_ATENCION>" + oParametersGeneratePDF.StrCanalAtencion + "</CANAL_ATENCION>";
            xml += buildXMLArray(oParametersGeneratePDF);
            xml += "<BOLSA_SOLES_ADICIONALES>" + oParametersGeneratePDF.StrBolsaSolesAdicionales + "</BOLSA_SOLES_ADICIONALES>";
            xml += "<IMEI>" + oParametersGeneratePDF.StrImei + "</IMEI>";
            xml += "<MARCA_EQUIPO>" + oParametersGeneratePDF.StrMarcaEquipo + "</MARCA_EQUIPO>";
            xml += "<MODELO_EQUIPO>" + oParametersGeneratePDF.StrModeloEquipo + "</MODELO_EQUIPO>";
            xml += "<TRANSACCION_BLOQUEO>" + oParametersGeneratePDF.StrTransaccionBloqueo + "</TRANSACCION_BLOQUEO>";
            xml += "<MOTIVO_TIPO_BLOQUEO>" + oParametersGeneratePDF.StrMotivoTipoBloqueo + "</MOTIVO_TIPO_BLOQUEO>";
            xml += "<TOPE_CONSUMO>" + oParametersGeneratePDF.StrTopeConsumo + "</TOPE_CONSUMO>";
            xml += "<FECHA_EJEC_TOPE_CONS>" + oParametersGeneratePDF.StrFechaEjecTopeCons + "</FECHA_EJEC_TOPE_CONS>";
            xml += "<FLAG_GRILA_ATP>" + oParametersGeneratePDF.StrFlagGrilaAtp + "</FLAG_GRILA_ATP>";
            xml += "<ESCENARIO_MIGRACION>" + oParametersGeneratePDF.StrEscenarioMigracion + "</ESCENARIO_MIGRACION>";
            xml += "<NUEVO_PLAN>" + oParametersGeneratePDF.StrNuevoPlan + "</NUEVO_PLAN>";
            xml += "<CF_TOTAL_NUEVO>" + oParametersGeneratePDF.StrCfTotalNuevo + "</CF_TOTAL_NUEVO>";
            xml += "<MONTO_APADECE>" + oParametersGeneratePDF.StrMontoApadece + "</MONTO_APADECE>";
            xml += "<MONTO_PCS>" + oParametersGeneratePDF.StrMontoPcs + "</MONTO_PCS>";
            xml += "<MOTIVO_CANCELACION>" + oParametersGeneratePDF.StrMotivoCancelacion + "</MOTIVO_CANCELACION>";
            xml += "<ESCENARIO_RETENCION>" + oParametersGeneratePDF.StrEscenarioRetencion + "</ESCENARIO_RETENCION>";
            xml += "<ACCION_RETENCION>" + oParametersGeneratePDF.StrAccionRetencion + "</ACCION_RETENCION>";
            xml += "<MODALIDAD>" + oParametersGeneratePDF.StrModalidad + "</MODALIDAD>";
            xml += "<PRODUCTOS>" + oParametersGeneratePDF.StrProductos + "</PRODUCTOS>";
            //xml += buildXMLArray2(oParametersGeneratePDF);
            xml += "<PUNTOS_CC_ANTES_TRANS>" + oParametersGeneratePDF.StrPuntosCcAntesTrans + "</PUNTOS_CC_ANTES_TRANS>";
            xml += "<CANTIDAD_TOTAL_CANJE_DEV>" + oParametersGeneratePDF.StrCantidadTotalCanjeDev + "</CANTIDAD_TOTAL_CANJE_DEV>";
            xml += "<TIPO_DOC_FACT>" + oParametersGeneratePDF.StrTipoDocFact + "</TIPO_DOC_FACT>";
            xml += "<NRO_DOC_FACT>" + oParametersGeneratePDF.StrNroDocFact + "</NRO_DOC_FACT>";
            xml += "<DIRECCION_POSTAL>" + oParametersGeneratePDF.StrDireccionPostal + "</DIRECCION_POSTAL>";
            xml += "<DISTRITO_POSTAL>" + oParametersGeneratePDF.StrDistritoPostal + "</DISTRITO_POSTAL>";
            xml += "<PROVINCIA_POSTAL>" + oParametersGeneratePDF.StrProvinciaPostal + "</PROVINCIA_POSTAL>";
            xml += "<DEPARTAMENTO_POSTAL>" + oParametersGeneratePDF.StrDepartamentoPostal + "</DEPARTAMENTO_POSTAL>";
            xml += "<FECHA_EMISION_DOC_FACT>" + oParametersGeneratePDF.StrFechaEmisionDocFact + "</FECHA_EMISION_DOC_FACT>";
            xml += "<FECHA_VENC_DOC_FACT>" + oParametersGeneratePDF.StrFechaVencDocFact + "</FECHA_VENC_DOC_FACT>";
            //xml += buildXMLArray3(oParametersGeneratePDF);
            //xml += buildXMLArray4(oParametersGeneratePDF);
            xml += "<IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>" + oParametersGeneratePDF.StrImporteConceptoAjusteSinIgv + "</IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>";
            xml += "<SUBTOTAL_AJUSTE_SIN_IGV>" + oParametersGeneratePDF.StrSubtotalAjusteSinIgv + "</SUBTOTAL_AJUSTE_SIN_IGV>";
            xml += "<IGV_TAX>" + oParametersGeneratePDF.StrIgvTax + "</IGV_TAX>";
            xml += "<TOTAL_CON_IGV>" + oParametersGeneratePDF.StrTotalConIgv + "</TOTAL_CON_IGV>";
            xml += "<TOTAL_AJUSTE>" + oParametersGeneratePDF.StrTotalAjuste + "</TOTAL_AJUSTE>";
            xml += "<MOTIVO_CAMBIO_SIM>" + oParametersGeneratePDF.StrMotivoCambioSim + "</MOTIVO_CAMBIO_SIM>";
            xml += "<NUEVO_SIM>" + oParametersGeneratePDF.StrNuevoSim + "</NUEVO_SIM>";
            xml += "<COSTO_TRANSACCION>" + oParametersGeneratePDF.StrCostoTransaccion + "</COSTO_TRANSACCION>";
            xml += "<FLAG_4G>" + oParametersGeneratePDF.StrFlag4G + "</FLAG_4G>";
            xml += "<SIM_4G_LTE>" + oParametersGeneratePDF.StrSim4GLte + "</SIM_4G_LTE>";
            xml += "<ESTADO_SERVICIO_4G>" + oParametersGeneratePDF.StrEstadoServicio4G + "</ESTADO_SERVICIO_4G>";
            xml += "<NRO_DOC_REF>" + oParametersGeneratePDF.StrNroDocIdentidadRef + "</NRO_DOC_REF>";
            xml += "<FECHA_EMISION_DOC_REF>" + oParametersGeneratePDF.StrFechaEmisionDocRef + "</FECHA_EMISION_DOC_REF>";
            xml += "<COD_DESBLOQ>" + oParametersGeneratePDF.StrCodDesbloqueo + "</COD_DESBLOQ>";
            xml += "<PAIS_POSTAL>" + oParametersGeneratePDF.StrPaisA + "</PAIS_POSTAL>";
            xml += "<CODIGO_POSTAL>" + oParametersGeneratePDF.StrCodigoLocalA + "</CODIGO_POSTAL>";
            xml += "<CENTRO_POBLADO_ACTUAL>" + oParametersGeneratePDF.StrCentroPobladoActual + "</CENTRO_POBLADO_ACTUAL>";
            xml += "<REFERENCIA>" + oParametersGeneratePDF.StrReferenciaActual + "</REFERENCIA>";
            xml += "<DIRECCION_DESTINO>" + oParametersGeneratePDF.StrDireccionPostalC + "</DIRECCION_DESTINO>";
            xml += "<DESCRIP_TRANSACCION>" + oParametersGeneratePDF.StrDescripTransaccion + "</DESCRIP_TRANSACCION>";
            xml += "<REFERENCIA_DESTINO>" + oParametersGeneratePDF.StrReferenciaDestino + "</REFERENCIA_DESTINO>";
            xml += "<DEPARTAMENTO_DESTINO>" + oParametersGeneratePDF.StrDepartamentoLocalB + "</DEPARTAMENTO_DESTINO>";
            xml += "<DISTRITO_DESTINO>" + oParametersGeneratePDF.StrDistrtitoLocalB + "</DISTRITO_DESTINO>";
            xml += "<CODIGO_POSTAL_DESTINO>" + oParametersGeneratePDF.StrCodigoLocalB + "</CODIGO_POSTAL_DESTINO>";
            xml += "<PAIS_DESTINO>" + oParametersGeneratePDF.StrPaisB + "</PAIS_DESTINO>";
            xml += "<PROVINCIA_DESTINO>" + oParametersGeneratePDF.StrProvinciaLocalB + "</PROVINCIA_DESTINO>";
            xml += "<CENTRO_POBLADO_DESTINO>" + oParametersGeneratePDF.StrCentroPobladoDestino + "</CENTRO_POBLADO_DESTINO>";
            xml += "<CORREO_SOLICITUD>" + oParametersGeneratePDF.StrEmail + "</CORREO_SOLICITUD>";
            xml += "<APLICA_CAMBIO_DIREC>" + oParametersGeneratePDF.StrAplicaCambioDireccion + "</APLICA_CAMBIO_DIREC>";
            xml += "<APLICA_CAMBIO_NOMBRE>" + oParametersGeneratePDF.StrAplicaCambioNombre + "</APLICA_CAMBIO_NOMBRE>";
            xml += "<FECHA_SUSP>" + oParametersGeneratePDF.StrFechaSuspension + "</FECHA_SUSP>";
            xml += "<FECHA_ACTIVACION>" + oParametersGeneratePDF.StrFechaActivacion + "</FECHA_ACTIVACION>";
            xml += "<COSTO_REACTIVACION>" + oParametersGeneratePDF.StrCostoReactivacion + "</COSTO_REACTIVACION>";
            xml += "<FLAG_TRASLADO>" + oParametersGeneratePDF.StrFlagExterInter + "</FLAG_TRASLADO>";
            xml += "<FECHA_AUTORIZACION>" + oParametersGeneratePDF.StrFechaTransaccionProgram + "</FECHA_AUTORIZACION>";
            xml += "</PLANTILLA>";

            return xml;
        }


        /// <summary>
        /// Método que construye XMLArray
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        private static string buildXMLArray(GeneratePDFDataRequest oParametersGeneratePDF)
        {
            try
            {
                string strTextoXML = "";
                ServiceArmaPlan oServicioArmaPlan;
                ArrayList arrList = null;

                if (oParametersGeneratePDF.ArrListaInfo != null)
                    arrList = new ArrayList(oParametersGeneratePDF.ArrListaInfo);

                if (arrList != null)
                {
                    if (arrList.Count > 0)
                    {
                        for (int i = 0; i < arrList.Count; i++)
                        {
                            oServicioArmaPlan = new ServiceArmaPlan();
                            oServicioArmaPlan = (ServiceArmaPlan)arrList[i];

                            strTextoXML = strTextoXML + "<CF_SERVICIO_MODIF>" + oServicioArmaPlan.CARGO_FIJO + "</CF_SERVICIO_MODIF>"
                                + "<DESCRIPCION_SERVICIO_COM>" + oServicioArmaPlan.DES_SERVICIO + "</DESCRIPCION_SERVICIO_COM>"
                                + "<PERIODO_CUOTA_SERVICIO>" + oServicioArmaPlan.PERIODO + "</PERIODO_CUOTA_SERVICIO>";
                        }
                    }
                    else
                    {
                        strTextoXML = "<CF_SERVICIO_MODIF>" + oParametersGeneratePDF.StrCfServicioModif + "</CF_SERVICIO_MODIF>"
                            + "<DESCRIPCION_SERVICIO_COM>" + oParametersGeneratePDF.StrDescripcionServicioCom + "</DESCRIPCION_SERVICIO_COM>"
                            + "<PERIODO_CUOTA_SERVICIO>" + oParametersGeneratePDF.StrPeriodoCuotaServicio + "</PERIODO_CUOTA_SERVICIO>";
                    }
                }
                else
                {
                    strTextoXML = "<CF_SERVICIO_MODIF>" + oParametersGeneratePDF.StrCfServicioModif + "</CF_SERVICIO_MODIF>"
                        + "<DESCRIPCION_SERVICIO_COM>" + oParametersGeneratePDF.StrDescripcionServicioCom + "</DESCRIPCION_SERVICIO_COM>"
                        + "<PERIODO_CUOTA_SERVICIO>" + oParametersGeneratePDF.StrPeriodoCuotaServicio + "</PERIODO_CUOTA_SERVICIO>";
                }
                return strTextoXML;
            }
            catch (Exception ex)
            {
                //log.Info(String.Format("Error al construirXMLArray: {0}", ex.Message));
                throw ex;
            }

        }

        /// <summary>
        /// Método que construye XMLArray2
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        //private string buildXMLArray2(ParametersGeneratePDF oParametersGeneratePDF)
        //{
        //    try
        //    {
        //        string strTextoXML = "";
        //        Producto oProducto;
        //        ArrayList arrList2 = null;

        //        if (oParametersGeneratePDF.ArrListaInfo2 != null)
        //            arrList2 = new ArrayList(oParametersGeneratePDF.ArrListaInfo2);

        //        if (arrList2 != null)
        //        {
        //            if (arrList2.Count > 0)
        //            {
        //                for (int i = 0; i < arrList2.Count; i++)
        //                {
        //                    oProducto = new Producto();
        //                    oProducto = (Producto)arrList2[i];

        //                    strTextoXML = strTextoXML + "<SECUENCIAL_ORDEN>" + (i + 1) + "</SECUENCIAL_ORDEN>"
        //                        + "<ID_PRODUCTO_CC>" + oProducto.Id + "</ID_PRODUCTO_CC>"
        //                        + "<DESC_PRODUCTO_CC>" + oProducto.Descripcion + "</DESC_PRODUCTO_CC>"
        //                        + "<PUNTOS_CC>" + oProducto.Puntos + "</PUNTOS_CC>"
        //                        + "<CANTIDAD_CC>" + oProducto.Cantidad + "</CANTIDAD_CC>"
        //                        + "<DESCUENTO_CC>" + oProducto.Pago + "</DESCUENTO_CC>"
        //                        + "<TIPO_PREMIO_CC>" + oProducto.TipoPremio + "</TIPO_PREMIO_CC>"
        //                        + "<MONTO_RECARGA_CC>" + oProducto.MontoRecarga + "</MONTO_RECARGA_CC>";
        //                }
        //            }
        //            else
        //            {
        //                strTextoXML = "<SECUENCIAL_ORDEN>" + oParametersGeneratePDF.StrSecuencialOrden + "</SECUENCIAL_ORDEN>"
        //                    + "<ID_PRODUCTO_CC>" + oParametersGeneratePDF.StrIdProductoCc + "</ID_PRODUCTO_CC>"
        //                    + "<DESC_PRODUCTO_CC>" + oParametersGeneratePDF.StrDescProductoCc + "</DESC_PRODUCTO_CC>"
        //                    + "<PUNTOS_CC>" + oParametersGeneratePDF.StrPuntosCc + "</PUNTOS_CC>"
        //                    + "<CANTIDAD_CC>" + oParametersGeneratePDF.StrCantidadCc + "</CANTIDAD_CC>"
        //                    + "<DESCUENTO_CC>" + oParametersGeneratePDF.StrDescuentoCc + "</DESCUENTO_CC>"
        //                    + "<TIPO_PREMIO_CC>" + oParametersGeneratePDF.StrTipoPremioCc + "</TIPO_PREMIO_CC>"
        //                    + "<MONTO_RECARGA_CC>" + oParametersGeneratePDF.StrMontoRecargaCc + "</MONTO_RECARGA_CC>";
        //            }
        //        }
        //        else
        //        {
        //            strTextoXML = "<SECUENCIAL_ORDEN>" + oParametersGeneratePDF.StrSecuencialOrden + "</SECUENCIAL_ORDEN>"
        //                + "<ID_PRODUCTO_CC>" + oParametersGeneratePDF.StrIdProductoCc + "</ID_PRODUCTO_CC>"
        //                + "<DESC_PRODUCTO_CC>" + oParametersGeneratePDF.StrDescProductoCc + "</DESC_PRODUCTO_CC>"
        //                + "<PUNTOS_CC>" + oParametersGeneratePDF.StrPuntosCc + "</PUNTOS_CC>"
        //                + "<CANTIDAD_CC>" + oParametersGeneratePDF.StrCantidadCc + "</CANTIDAD_CC>"
        //                + "<DESCUENTO_CC>" + oParametersGeneratePDF.StrDescuentoCc + "</DESCUENTO_CC>"
        //                + "<TIPO_PREMIO_CC>" + oParametersGeneratePDF.StrTipoPremioCc + "</TIPO_PREMIO_CC>"
        //                + "<MONTO_RECARGA_CC>" + oParametersGeneratePDF.StrMontoRecargaCc + "</MONTO_RECARGA_CC>";

        //        }
        //        return strTextoXML;
        //    }

        //    catch (Exception ex)
        //    {
        //        //log.Info(String.Format("Error al construirXMLArray2: {0}", ex.Message));
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// Método que construye XMLArray3
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        //private string buildXMLArray3(ParametersGeneratePDF oParametersGeneratePDF)
        //{
        //    try
        //    {
        //        string strTextoXML = "";
        //        ArrayList arrList3 = null;

        //        if (oParametersGeneratePDF.ArrListaInfo3 != null)
        //            arrList3 = new ArrayList(oParametersGeneratePDF.ArrListaInfo3);

        //        if (arrList3 != null)
        //        {
        //            if (arrList3.Count > 0)
        //            {
        //                strTextoXML = "<SUBCATEGORIA_FACT " + oParametersGeneratePDF.StrSubcategoriaFact + "</SUBCATEGORIA_FACT>";

        //            }
        //            else
        //            {
        //                strTextoXML = "<SUBCATEGORIA_FACT " + oParametersGeneratePDF.StrSubcategoriaFact + "</SUBCATEGORIA_FACT>";

        //            }
        //        }
        //        else
        //        {
        //            strTextoXML = "<SUBCATEGORIA_FACT>" + oParametersGeneratePDF.StrSubcategoriaFact + "</SUBCATEGORIA_FACT>";

        //        }
        //        return strTextoXML;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.Info(String.Format("Error al construirXMLArray3: {0}", ex.Message));
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// Método que construye XMLArray4
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns>string</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        //private string buildXMLArray4(ParametersGeneratePDF oParametersGeneratePDF)
        //{
        //    try
        //    {
        //        string strTextoXML = "";
        //        ArrayList arrList4 = null;

        //        if (oParametersGeneratePDF.ArrListaInfo4 != null)
        //            arrList4 = new ArrayList(oParametersGeneratePDF.ArrListaInfo4);

        //        if (arrList4 != null)
        //        {
        //            if (arrList4.Count > 0)
        //            {
        //                strTextoXML = "<SUBCATEGORIA_FACT_SINIGV " + oParametersGeneratePDF.StrSubcategoriaFactSinIgv + "</SUBCATEGORIA_FACT_SINIGV>";

        //            }
        //            else
        //            {
        //                strTextoXML = "<SUBCATEGORIA_FACT_SINIGV " + oParametersGeneratePDF.StrSubcategoriaFactSinIgv + "</SUBCATEGORIA_FACT_SINIGV>";

        //            }
        //        }
        //        else
        //        {
        //            strTextoXML = "<SUBCATEGORIA_FACT_SINIGV>" + oParametersGeneratePDF.StrSubcategoriaFactSinIgv + "</SUBCATEGORIA_FACT_SINIGV>";

        //        }
        //        return strTextoXML;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.Info(String.Format("Error al construirXMLArray4: {0}", ex.Message));
        //        throw ex;
        //    }

        //}

        /// <summary>
        /// Método que gernera PDF PostVenta
        /// </summary>
        /// <param name="strServerGeneratePDF"></param>
        /// <param name="xml"></param>
        /// <param name="rutaPDF"></param>
        /// <param name="nombrePDF"></param>
        /// <param name="error"></param>
        /// <returns>bool</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>06/10/2015.</FecActu></item>
        /// <item><Resp>COSAPISOFT-MHR.</Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        private static GeneratePDFDataResponse generatePDFPostSale(string strServerGeneratePDF, string xml, string rutaPDF, string nombrePDF, ref string error)
        {
            byte[] XMLCodificadoUTF8 = ASCIIEncoding.UTF8.GetBytes(xml);

            //Seccion de Soap
            string strCodigo = "";

            string strDriver = "Driver"
                , archivo_pub = "claro-postventa.pub";
            GeneratePDFDataResponse oResponse = new GeneratePDFDataResponse();
            GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();
            GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();

            //Instancia para poder asignar el arreglo de Bytes del PDF al objeto del WS
            GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();

            //Instancia para poder asignar la ruta y el nombre del archivo PDF
            GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();

            GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();

            //log.Info("URL Servicio: " + ConfigurationManager.AppSettings["strServidorGenerarPDF"]);
            //log.Info(string.Format("Valores para la Web Service= driver: {0}, fileName: {1}, fileReturnRegEx: {2}, includeHeader: {3}, includeMessageFile: {4}, directory: {5}, fileName: {6}, pubFile: {7}",
            //System.Convert.ToBase64String(XMLCodificadoUTF8, 0, XMLCodificadoUTF8.Length), strDriver, ".*.(pdf)", false, true, rutaPDF, nombrePDF, archivo_pub));

            try
            {
                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF").ToString();
                objGenerarPDF.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //byte[] PDF = ;
                //Asigno el PDF codificado en un array de Bytes
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;

                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;

                //Asigno ruta y nombre de archivo PDF
                objGenerarPDFRequestOutput.directory = rutaPDF;
                objGenerarPDFRequestOutput.fileName = nombrePDF;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;

                objGenerarPDFRequest.pubFile = archivo_pub;

                Claro.Web.Logging.Info("123123123", "1", "Antes de ejecutar metodo Compose- DATA");

                objGenerarPDFResponse = objGenerarPDF.Compose(objGenerarPDFRequest);

                if (objGenerarPDFResponse.statusMessage.Contains("12"))
                {
                    error = string.Format("{0}. No se pudo generar el archivo", strCodigo.Replace(".", ""));
                    // log.Info(string.Format("Error en ejecucion de WS {0}: {1}", ConfigurationManager.AppSettings["strServidorGenerarPDF"], error));
                    oResponse.Generated = false;
                    return oResponse;
                }
                else
                {
                    oResponse.Generated = true;
                    
                    if (objGenerarPDFResponse.files!=null)
                    {
                        oResponse.FilePath = objGenerarPDFResponse.files[0].fileName;    
                    }
                    else
                    {
                        oResponse.FilePath = objGenerarPDFResponse.files[0].fileName;
                    }
                    //log.Info("Se ejecutó correctamente el WS: " + ConfigurationManager.AppSettings["strServidorGenerarPDF"].ToString());
                    return oResponse;
                }
            }
            catch (Exception exd)
            {
                Claro.Web.Logging.Info("123123123", "1", "ex.mesage" + exd.Message);
                if (exd.InnerException != null)
                {
                     Claro.Web.Logging.Info("123123123", "1", "ex.InnerException" + exd.InnerException);
                }
                error = exd.Message;
                //log.Info(string.Format("Error WS {0}: {1}", ConfigurationManager.AppSettings["strServidorGenerarPDF"], error));
                oResponse.Generated = false;
                return oResponse;
            }
        }
    }

}

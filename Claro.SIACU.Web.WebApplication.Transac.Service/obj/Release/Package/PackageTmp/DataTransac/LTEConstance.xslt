<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="CENTRO_ATENCION_AREA"/>
  <xsl:param name="REPRES_LEGAL"/>
  <xsl:param name="TITULAR_CLIENTE"/>
  <xsl:param name="TIPO_DOC_IDENTIDAD"/>
  <xsl:param name="PLAN_ACTUAL"/>
  <xsl:param name="CICLO_FACTURACION"/>
  <xsl:param name="FECHA_TRANSACCION_PROGRAM"/>
  <xsl:param name="CASO_INTER"/>
  <xsl:param name="NRO_SERVICIO"/>
  <xsl:param name="NRO_DOC_IDENTIDAD"/>
  <xsl:param name="ID_CU_ID"/>
  <xsl:param name="ESCENARIO_MIGRACION"/>
  <xsl:param name="NUEVO_PLAN"/>
  <xsl:param name="CF_TOTAL_NUEVO"/>
  <xsl:param name="FECHA_EJECUCION"/>
  <xsl:param name="MONTO_REINTEGRO"/>
  <xsl:param name="MONTO_PCS"/>
  <xsl:param name="TOPE_CONSUMO"/>
  <xsl:param name="CF_SERVICIO_COM"/>
  <xsl:param name="FECHA_EJEC_TOPE_CONS"/>
  <xsl:param name="CONTENIDO_COMERCIAL"/>
  <xsl:param name="CONTENIDO_COMERCIAL2"/>

  <xsl:template match="/">
    <PLANTILLA>
      <FORMATO_TRANSACCION>MIGRACION_PLAN_FIJOS</FORMATO_TRANSACCION>
      <CENTRO_ATENCION_AREA>
        <xsl:value-of select="$CENTRO_ATENCION_AREA"/>
      </CENTRO_ATENCION_AREA>
      <REPRES_LEGAL>
        <xsl:value-of select="$REPRES_LEGAL"/>
      </REPRES_LEGAL>
      <TITULAR_CLIENTE>
        <xsl:value-of select="$TITULAR_CLIENTE"/>
      </TITULAR_CLIENTE>
      <TIPO_DOC_IDENTIDAD>
        <xsl:value-of select="$TIPO_DOC_IDENTIDAD"/>
      </TIPO_DOC_IDENTIDAD>
      <PLAN_ACTUAL>
        <xsl:value-of select="$PLAN_ACTUAL"/>
      </PLAN_ACTUAL>
      <CICLO_FACTURACION>
        <xsl:value-of select="$CICLO_FACTURACION"/>
      </CICLO_FACTURACION>
      <FECHA_TRANSACCION_PROGRAM>
        <xsl:value-of select="$FECHA_TRANSACCION_PROGRAM"/>
      </FECHA_TRANSACCION_PROGRAM>
      <CASO_INTER>
        <xsl:value-of select="$CASO_INTER"/>
      </CASO_INTER>
      <NRO_SERVICIO>
        <xsl:value-of select="$NRO_SERVICIO"/>
      </NRO_SERVICIO>
      <NRO_DOC_IDENTIDAD>
        <xsl:value-of select="$NRO_DOC_IDENTIDAD"/>
      </NRO_DOC_IDENTIDAD>
      <ID_CU_ID>
        <xsl:value-of select="$ID_CU_ID"/>
      </ID_CU_ID>
      <ESCENARIO_MIGRACION>
        <xsl:value-of select="$ESCENARIO_MIGRACION"/>
      </ESCENARIO_MIGRACION>
      <NUEVO_PLAN>
        <xsl:value-of select="$NUEVO_PLAN"/>
      </NUEVO_PLAN>
      <CF_TOTAL_NUEVO>
        <xsl:value-of select="$CF_TOTAL_NUEVO"/>
      </CF_TOTAL_NUEVO>
      <FECHA_EJECUCION>
        <xsl:value-of select="$FECHA_EJECUCION"/>
      </FECHA_EJECUCION>
      <MONTO_REINTEGRO>
        <xsl:value-of select="$MONTO_REINTEGRO"/>
      </MONTO_REINTEGRO>
      <MONTO_PCS>
        <xsl:value-of select="$MONTO_PCS"/>
      </MONTO_PCS>
      <TOPE_CONSUMO>
        <xsl:value-of select="$TOPE_CONSUMO"/>
      </TOPE_CONSUMO>
      <CF_SERVICIO_COM>
        <xsl:value-of select="$CF_SERVICIO_COM"/>
      </CF_SERVICIO_COM>
      <FECHA_EJEC_TOPE_CONS>
        <xsl:value-of select="$FECHA_EJEC_TOPE_CONS"/>
      </FECHA_EJEC_TOPE_CONS>
      <CONTENIDO_COMERCIAL>
        <xsl:value-of select="$CONTENIDO_COMERCIAL"/>
      </CONTENIDO_COMERCIAL>
      <CONTENIDO_COMERCIAL2>
        <xsl:value-of select="$CONTENIDO_COMERCIAL2"/>
      </CONTENIDO_COMERCIAL2>
    </PLANTILLA>
  </xsl:template>
</xsl:stylesheet>

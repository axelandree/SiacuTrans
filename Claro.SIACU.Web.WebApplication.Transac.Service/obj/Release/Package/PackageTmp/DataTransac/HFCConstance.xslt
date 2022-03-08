<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="CENTRO_ATENCION_AREA"/>
  <xsl:param name="REPRES_LEGAL"/>
  <xsl:param name="TITULAR_CLIENTE"/>
  <xsl:param name="TIPO_DOC_IDENTIDAD"/>
  <xsl:param name="PLAN_ACTUAL"/>
  <xsl:param name="CICLO_FACTURACION"/>
  <xsl:param name="FECHA_TRANSACCION_PROGRAM"/>
  <xsl:param name="CASO_INTER"/>
  <xsl:param name="NRO_CONTRATO"/>
  <xsl:param name="NRO_DOC_IDENTIDAD"/>
  <xsl:param name="NUEVO_PLAN"/>
  <xsl:param name="SOLUCION"/>
  <xsl:param name="CF_TOTAL_NUEVO"/>
  <xsl:param name="FECHA_VISITA"/>
  <xsl:param name="PENALIDAD"/>
  <xsl:param name="SOT"/>
  <xsl:param name="NOMBRE_SERVICIO"/>
  <xsl:param name="TIPO_SERVICIO"/>
  <xsl:param name="GRUPO_SERVICIO"/>
  <xsl:param name="CF_TOTAL_IGV"/>
  <xsl:param name="PRESUSCRITO"/>
  <xsl:param name="NRO_CARTA"/>
  <xsl:param name="NOM_OPERADOR"/>
  <xsl:param name="PUB_NT_PA"/>
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
      <NRO_CONTRATO>
        <xsl:value-of select="NRO_CONTRATO"/>
      </NRO_CONTRATO>
      <NRO_DOC_IDENTIDAD>
        <xsl:value-of select="$NRO_DOC_IDENTIDAD"/>
      </NRO_DOC_IDENTIDAD>
      <NUEVO_PLAN>
        <xsl:value-of select="$NUEVO_PLAN"/>
      </NUEVO_PLAN>
      <SOLUCION>
        <xsl:value-of select="$SOLUCION"/>
      </SOLUCION>
      <CF_TOTAL_NUEVO>
        <xsl:value-of select="$CF_TOTAL_NUEVO"/>
      </CF_TOTAL_NUEVO>
      <FECHA_VISITA>
        <xsl:value-of select="$FECHA_VISITA"/>
      </FECHA_VISITA>
      <PENALIDAD>
        <xsl:value-of select="$PENALIDAD"/>
      </PENALIDAD>
      <SOT>
        <xsl:value-of select="$SOT"/>
      </SOT>
      <NOMBRE_SERVICIO>
        <xsl:value-of select="$NOMBRE_SERVICIO"/>
      </NOMBRE_SERVICIO>
      <TIPO_SERVICIO>
        <xsl:value-of select="$TIPO_SERVICIO"/>
      </TIPO_SERVICIO>
      <GRUPO_SERVICIO>
        <xsl:value-of select="$GRUPO_SERVICIO"/>
      </GRUPO_SERVICIO>
      <CF_TOTAL_IGV>
        <xsl:value-of select="$CF_TOTAL_IGV"/>
      </CF_TOTAL_IGV>
      <PRESUSCRITO>
        <xsl:value-of select="$PRESUSCRITO"/>
      </PRESUSCRITO>
      <NRO_CARTA>
        <xsl:value-of select="$NRO_CARTA"/>
      </NRO_CARTA>
      <NOM_OPERADOR>
        <xsl:value-of select="$NOM_OPERADOR"/>
      </NOM_OPERADOR>
      <PUB_NT_PA>
        <xsl:value-of select="$PUB_NT_PA"/>
      </PUB_NT_PA>
      <CONTENIDO_COMERCIAL2>
        <xsl:value-of select="$CONTENIDO_COMERCIAL2"/>
      </CONTENIDO_COMERCIAL2>
    </PLANTILLA>
  </xsl:template>
</xsl:stylesheet>

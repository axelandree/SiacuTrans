<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="CENTRO_ATENCION_AREA"/>
  <xsl:param name="TITULAR_CLIENTE"/>
  <xsl:param name="REPRES_LEGAL"/>
  <xsl:param name="TIPO_DOC_IDENTIDAD"/>
  <xsl:param name="NRO_DOC_IDENTIDAD"/>
  <xsl:param name="FECHA_TRANSACCION_PROGRAM"/>
  <xsl:param name="CASO_INTER"/>
  <xsl:param name="CONTRATO"/>
  <xsl:param name="TRANSACCION_DESCRIPCION"/>
  <xsl:param name="COSTO_TRANSACCION"/>
  <xsl:param name="DIRECCION_CLIENTE_ACTUAL"/>
  <xsl:param name="REFERENCIA_TRANSACCION_ACTUAL"/>
  <xsl:param name="DEPARTAMENTO_CLIENTE_ACTUAL"/>
  <xsl:param name="DISTRITO_CLIENTE_ACTUAL"/>
  <xsl:param name="CODIGO_POSTAL"/>
  <xsl:param name="PAIS_CLIENTE_ACTUAL"/>
  <xsl:param name="PROVINCIA_CLIENTE_ACTUAL"/>
  <xsl:param name="CONTENIDO_COMERCIAL2"/>
  <xsl:param name="NRO_SOT"/>
  <xsl:template match="/">
    <PLANTILLA>
      <FORMATO_TRANSACCION>CONFIGURACION_IP</FORMATO_TRANSACCION>
      <CENTRO_ATENCION_AREA>
        <xsl:value-of select="$CENTRO_ATENCION_AREA"/>
      </CENTRO_ATENCION_AREA>
      <TITULAR_CLIENTE>
        <xsl:value-of select="$TITULAR_CLIENTE"/>
      </TITULAR_CLIENTE>
      <REPRES_LEGAL>
        <xsl:value-of select="$REPRES_LEGAL"/>
      </REPRES_LEGAL>
      <TIPO_DOC_IDENTIDAD>
        <xsl:value-of select="$TIPO_DOC_IDENTIDAD"/>
      </TIPO_DOC_IDENTIDAD>
      <NRO_DOC_IDENTIDAD>
        <xsl:value-of select="$NRO_DOC_IDENTIDAD"/>
      </NRO_DOC_IDENTIDAD>
      <FECHA_TRANSACCION_PROGRAM>
        <xsl:value-of select="$FECHA_TRANSACCION_PROGRAM"/>
      </FECHA_TRANSACCION_PROGRAM>
      <CASO_INTER>
        <xsl:value-of select="$CASO_INTER"/>
      </CASO_INTER>
      <CONTRATO>
        <xsl:value-of select="$CONTRATO"/>
      </CONTRATO>
      <TRANSACCION_DESCRIPCION>
        <xsl:value-of select="$TRANSACCION_DESCRIPCION"/>
      </TRANSACCION_DESCRIPCION>
      <COSTO_TRANSACCION>
        <xsl:value-of select="$COSTO_TRANSACCION"/>
      </COSTO_TRANSACCION>
      <DIRECCION_CLIENTE_ACTUAL>
        <xsl:value-of select="$DIRECCION_CLIENTE_ACTUAL"/>
      </DIRECCION_CLIENTE_ACTUAL>
      <REFERENCIA_TRANSACCION_ACTUAL>
        <xsl:value-of select="$REFERENCIA_TRANSACCION_ACTUAL"/>
      </REFERENCIA_TRANSACCION_ACTUAL>
      <DEPARTAMENTO_CLIENTE_ACTUAL>
        <xsl:value-of select="$DEPARTAMENTO_CLIENTE_ACTUAL"/>
      </DEPARTAMENTO_CLIENTE_ACTUAL>
      <DISTRITO_CLIENTE_ACTUAL>
        <xsl:value-of select="$DISTRITO_CLIENTE_ACTUAL"/>
      </DISTRITO_CLIENTE_ACTUAL>
      <CODIGO_POSTAL>
        <xsl:value-of select="$CODIGO_POSTAL"/>
      </CODIGO_POSTAL>
      <PAIS_CLIENTE_ACTUAL>
        <xsl:value-of select="$PAIS_CLIENTE_ACTUAL"/>
      </PAIS_CLIENTE_ACTUAL>
      <PROVINCIA_CLIENTE_ACTUAL>
        <xsl:value-of select="$PROVINCIA_CLIENTE_ACTUAL"/>
      </PROVINCIA_CLIENTE_ACTUAL>
      <CONTENIDO_COMERCIAL2>
        <xsl:value-of select="$CONTENIDO_COMERCIAL2"/>
      </CONTENIDO_COMERCIAL2>
      <NRO_SOT>
        <xsl:value-of select="$NRO_SOT"/>
      </NRO_SOT>
    </PLANTILLA>
  </xsl:template>
</xsl:stylesheet>

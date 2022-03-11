﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.Transac.Service
{
    public abstract class Conexion
    {
        private static System.Collections.Hashtable _arrConfigurraciones;
        private string _strAplicacion;

        public Conexion(string aplicacion)
        {
            _strAplicacion = aplicacion;
        }

        //internal static ClaroBDConfiguracion GeneraConfiguracion<T>(string aplicacion)
        //{
        //    return GeneraConfiguracion<T>(aplicacion, ConfigurationManager.AppSettings[aplicacion]);
        //}

        internal static ClaroBDConfiguracion GeneraConfiguracion<T>(string aplicacion, string archivoConfig)
        {

            if (_arrConfigurraciones == null)
            {
                _arrConfigurraciones = new System.Collections.Hashtable();
            }

            if (_arrConfigurraciones[aplicacion] == null)
            {
                if ((aplicacion == null || aplicacion == ""))
                {
                    throw new ApplicationException("No ha especificado el Key del Código de la Aplicacion");
                }

                if ((archivoConfig == null || archivoConfig == ""))
                {
                    throw new ApplicationException("No se encuentra el Key del Archivo de Configuracion");
                }

                IConfiguracion objType = (IConfiguracion)Activator.CreateInstance(typeof(T), new object[] { archivoConfig });

                //_arrConfigurraciones.Add(aplicacion, objType.Parametros);
            }

            return (ClaroBDConfiguracion)_arrConfigurraciones[aplicacion];
        }

        protected string Aplicacion
        {
            get
            {
                return this._strAplicacion;
            }
        }

        //public abstract DAABRequest CreaRequest();
    }
}

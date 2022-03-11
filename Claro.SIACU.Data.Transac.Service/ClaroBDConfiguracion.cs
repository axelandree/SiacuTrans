using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.Transac.Service
{
    public class ClaroBDConfiguracion : IClaroBDConfiguracion
    {
        string mServidor;
        string mBaseDatos;
        string mUsuario;
        string mPassWord;
        string mProvider;
        string mIdioma;
        string mSistema;
        string mMaxPoolSize;
        string mMinPoolSize;
        string mConnectionLifetime;
        string mPooling;

        public string Servidor
        {
            get
            {
                return mServidor;
            }
            set
            {
                mServidor = value;
            }
        }

        public string BaseDatos
        {
            get
            {
                return mBaseDatos;
            }
            set
            {
                mBaseDatos = value;
            }
        }

        public string Usuario
        {
            get
            {
                return mUsuario;
            }
            set
            {
                mUsuario = value;
            }
        }

        public string Password
        {
            get
            {
                return mPassWord;
            }
            set
            {
                mPassWord = value;
            }
        }

        public string Provider
        {
            get
            {
                return mProvider;
            }
            set
            {
                mProvider = value;
            }
        }

        public string CadenaConexion
        {
            get
            {
                string cadena = "";

                if (mProvider.IndexOf("ORA") > 0 || mProvider == "")
                {
                    cadena = "User Id=" + ((mUsuario == null) ? "" : mUsuario) + ";Data Source=" + ((mBaseDatos == null) ? "" : mBaseDatos) + ";password=" + ((mPassWord == null) ? "" : mPassWord);
                    cadena += ((mPooling == "") ? "" : string.Format(";Pooling='{0}';", mPooling));
                    cadena += ((mMaxPoolSize == "") ? "" : string.Format("Max Pool Size={0};", mMaxPoolSize));
                    cadena += ((mMinPoolSize == "") ? "" : string.Format("Min Pool Size={0};", mMinPoolSize));
                    cadena += ((mConnectionLifetime == "") ? "" : string.Format("Connection Lifetime={0};", mConnectionLifetime));
                    return cadena;
                }
                else
                {
                    cadena = "User Id=" + ((mUsuario == null) ? "" : mUsuario) + ";Data Source=" + ((mServidor == null) ? "" : mServidor) + ";password=" + ((mPassWord == null) ? "" : mPassWord) + ";Initial Catalog=" + ((mBaseDatos == null) ? "" : mBaseDatos);
                    return cadena;
                }
            }
        }

        public string Idioma
        {
            get
            {
                return mIdioma;
            }
            set
            {
                mIdioma = value;
            }
        }

        public string Sistema
        {
            get
            {
                return mSistema;
            }
            set
            {
                mSistema = value;
            }
        }

        public string MaxPoolSize
        {
            get
            {
                return mMaxPoolSize;
            }
            set
            {
                mMaxPoolSize = value;
            }
        }

        public string MinPoolSize
        {
            get
            {
                return mMinPoolSize;
            }
            set
            {
                mMinPoolSize = value;
            }
        }
        public string ConnectionLifetime
        {
            get
            {
                return mConnectionLifetime;
            }
            set
            {
                mConnectionLifetime = value;
            }
        }
        public string Pooling
        {
            get
            {
                return mPooling;
            }
            set
            {
                mPooling = value;
            }
        }
    }
}

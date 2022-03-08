using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ETAEntityCapacityType
    {
        public ETAEntityCapacityType()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }
        #region "Atributos"



        private string _strLocation;
        private DateTime _dtmDate;
        private string _strSpaceTime;
        private string _strSkillWork;
        private long _lngQuota;
        private long _lngAvailable;

        #endregion"Atributos"

        #region "Propiedades"

        public string strLocation
        {
            get
            {
                return this._strLocation;
            }
            set
            {
                this._strLocation = value;
            }
        }
        public DateTime dtmDate
        {
            get
            {
                return this._dtmDate;
            }
            set
            {
                this._dtmDate = value;
            }
        }
        public string strSpaceTime
        {
            get
            {
                return this._strSpaceTime;
            }
            set
            {
                this._strSpaceTime = value;
            }
        }
        public string strSkillWork
        {
            get
            {
                return this._strSkillWork;
            }
            set
            {
                this._strSkillWork = value;
            }
        }
        public long lngQuota
        {
            get
            {
                return this._lngQuota;
            }
            set
            {
                this._lngQuota = value;
            }
        }
        public long lngAvailable
        {
            get
            {
                return this._lngAvailable;
            }
            set
            {
                this._lngAvailable = value;
            }
        }

        #endregion "Propiedades"

    }
}

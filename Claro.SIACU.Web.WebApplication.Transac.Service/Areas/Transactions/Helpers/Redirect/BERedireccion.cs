using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
{
    public class BERedireccion
    {
        private BEUrlParams _UrlParams;
        private BESessionParams _SessionParams;

        public BERedireccion()
        {
            _SessionParams = new BESessionParams();
            _UrlParams = new BEUrlParams();
        }

        public BESessionParams SessionParams
        {
            get
            {
                return _SessionParams;
            }
            set
            {
                _SessionParams = value;
            }
        }
        public BEUrlParams UrlParams
        {
            get
            {
                _UrlParams.SessionParams = _SessionParams;
                return _UrlParams;
            }
            set
            {
                _UrlParams = value;
            }
        }
    }
}
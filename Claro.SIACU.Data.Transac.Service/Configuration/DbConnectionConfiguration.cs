using Claro.Data;

namespace Claro.SIACU.Data.Transac.Service.Configuration
{
    internal struct DbConnectionConfiguration : IDbConnectionConfiguration
    {
        public static readonly IDbConnectionConfiguration SIAC_POST_BSCS = Create("SIAC_POST_BSCS");
        public static readonly IDbConnectionConfiguration SIAC_POST_CLARIFY = Create("SIAC_POST_CLARIFY");
        public static readonly IDbConnectionConfiguration SIAC_POST_COBIL = Create("SIAC_POST_COBIL");
        public static readonly IDbConnectionConfiguration SIAC_POST_DB = Create("SIAC_POST_DB");
        public static readonly IDbConnectionConfiguration SIAC_POST_DBTO = Create("SIAC_POST_DBTO");
        public static readonly IDbConnectionConfiguration SIAC_POST_EAIAVM = Create("SIAC_POST_EAIAVM");
        public static readonly IDbConnectionConfiguration SIAC_POST_GWP = Create("SIAC_POST_GWP");
        public static readonly IDbConnectionConfiguration SIAC_POST_MGR = Create("SIAC_POST_MGR");
        public static readonly IDbConnectionConfiguration SIAC_POST_OAC = Create("SIAC_POST_OAC");
        public static readonly IDbConnectionConfiguration SIAC_POST_PVU = Create("SIAC_POST_PVU");
        public static readonly IDbConnectionConfiguration SIAC_POST_RICE = Create("SIAC_POST_RICE");
        public static readonly IDbConnectionConfiguration SIAC_POST_SACE = Create("SIAC_POST_SACE");
        public static readonly IDbConnectionConfiguration SIAC_POST_SGA = Create("SIAC_POST_SGA");
        public static readonly IDbConnectionConfiguration SIAC_POST_SIGA = Create("SIAC_POST_SIGA");
        public static readonly IDbConnectionConfiguration SIAC_POST_DWO = Create("SIAC_POST_DWO");
        public static readonly IDbConnectionConfiguration SIAC_ODSPRE_REC = Create("SIAC_ODSPRE_REC");


        public static readonly IDbConnectionConfiguration SIAC_CLAROCLUB = Create("SIAC_CLAROCLUB");
        public static readonly IDbConnectionConfiguration SIAC_SIXPROV = Create("SIAC_SIXPROV");

        public static readonly IDbConnectionConfiguration SIAC_POST_BD_MSSAP = Create("MSSAPDB");
        public static readonly IDbConnectionConfiguration TIMGLOBAL = Create("TIMGLOBAL");

        public static readonly IDbConnectionConfiguration SIAC_AUDIT = DbConnectionConfiguration.Create("SIAC_AUDIT");
        public static readonly IDbConnectionConfiguration SIAC_CAE = DbConnectionConfiguration.Create("SIAC_POST_CAE");

        public static readonly IDbConnectionConfiguration SIACU_DM = DbConnectionConfiguration.Create("SIACU_DM");

        public static readonly IDbConnectionConfiguration SIAC_POST_PDI = DbConnectionConfiguration.Create("SIAC_POST_PDI");

        public static readonly IDbConnectionConfiguration SIAC_POST_COBSDB = Create("SIAC_POST_COBSDB");

        public static readonly IDbConnectionConfiguration SIAC_POST_SANSDB = Create("SIAC_POST_SANSDB");

      

        #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbConnectionConfiguration Create(string name)
        {
            DbConnectionConfiguration helper = new DbConnectionConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}

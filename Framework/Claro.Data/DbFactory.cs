using Claro.Data.Configuration;
using Oracle.DataAccess.Client;
using System;
using System.Data;
using System.Text;

namespace Claro.Data
{

    public class DbFactory : IDisposable
    {
        private DbConnectionConfigurationElement m_databaseConfiguration;
        private System.Data.Common.DbProviderFactory m_factory;
        private IDbConnection m_innerConnection;
        private string m_IdSession;
        private string m_Transaction;

        #region [ Constructors ]

        protected DbFactory(IDbConnectionConfiguration config)
        {
            this.SetDatabaseConfiguration(Claro.Data.DataSettings.DataBases[config.Name]);

            this.SetFactory(CreateFactory(this.GetProvider()));
        }

        #endregion

        #region [ Private Properties ]

        #region HasInnerConnection

        private bool HasInnerConnection
        {
            get
            {
                return (this.m_innerConnection != null);
            }
        }

        #endregion

        #endregion

        #region [ Private Methods ]


        #region GetProvider

        private DbProvider GetProvider()
        {
            return this.m_databaseConfiguration.Provider;
        }

        #endregion

        #region SetDatabaseConfiguration

        private void SetDatabaseConfiguration(DbConnectionConfigurationElement value)
        {
            this.m_databaseConfiguration = value;
        }

        #endregion

        #region GetFactory

        private System.Data.Common.DbProviderFactory GetFactory()
        {
            return this.m_factory;
        }

        #endregion

        #region SetFactory

        private void SetFactory(System.Data.Common.DbProviderFactory value)
        {
            this.m_factory = value;
        }

        #endregion

        #region EnsureInnerConnection

        private void EnsureInnerConnection()
        {
            if (!this.HasInnerConnection)
            {
                this.m_innerConnection = this.GetFactory().CreateConnection();
                this.m_innerConnection.ConnectionString = this.m_databaseConfiguration.ConnectionString;
            }
        }

        #endregion

        #region OpenInnerConnection

        private void OpenInnerConnection(string strIdSession, string strTransaction)
        {
            try
            {
                m_IdSession = strIdSession;
                m_Transaction = strTransaction;
                this.m_innerConnection.Open();
                Claro.Web.Logging.Info(strIdSession, strTransaction, "Se abrió la conexión a la base de datos de forma satisfactoria: " + this.m_databaseConfiguration.Database.ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "No se pudo conectar a la base de datos " + this.m_databaseConfiguration.Database.ToString() + ", Error:" + ex.Message);
                throw;
            }
        }

        #endregion

        #endregion

        #region Provider

        public DbProvider Provider
        {
            get
            {
                return this.GetProvider();
            }
        }

        #endregion

        public void CreateParameters(IDbCommand command, DbParameter[] parameters)
        {
            foreach (DbParameter parameter in parameters)
            {
                CreateParameter(command, parameter);
            }
        }

        public void CreateParameter(IDbCommand command, DbParameter parameter)
        {
            System.Data.Common.DbParameter parameter1 = (System.Data.Common.DbParameter)command.CreateParameter();

            string paramterName = parameter.ParameterName;
            OracleDbType dbType;
            int size = parameter.Size;
            ParameterDirection direction = parameter.Direction;
            object value = parameter.Value;

            switch (this.Provider)
            {
                case DbProvider.Oracle:

                    switch (parameter.DbType)
                    {
                        case DbType.Currency:
                        case DbType.Guid:
                        case DbType.SByte:
                        case DbType.UInt16:
                        case DbType.UInt32:
                        case DbType.UInt64:
                        case DbType.VarNumeric:

                            throw new ArgumentException();
                    }

                    if ((parameter.DbType < DbType.AnsiString) || (parameter.DbType > DbType.StringFixedLength))
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    dbType = (OracleDbType)Database.OraDbTypeTable.dbTypeToOracleDbTypeMapping[(int)parameter.DbType];

                    parameter1.GetType().GetProperty("OracleDbType").SetValue(parameter1, dbType);

                    break;
                case DbProvider.MSSQL:

                    break;
                default:
                    throw new NotImplementedException();
            }

            parameter1.ParameterName = paramterName;

            parameter1.Direction = direction;
            if (size > 0)
            {
                parameter1.Size = size;
            }
            parameter1.Value = value;

            command.Parameters.Add(parameter1);
        }

        #region CreateFactory

        private static System.Data.Common.DbProviderFactory CreateFactory(DbProvider provider)
        {
            System.Data.Common.DbProviderFactory factory;

            switch (provider)
            {
                case DbProvider.MSSQL:
                    factory = System.Data.Common.DbProviderFactories.GetFactory("System.Data.SqlClient");
                    break;
                case DbProvider.Oracle:

                    factory = System.Data.Common.DbProviderFactories.GetFactory("Oracle.DataAccess.Client"); 
                    break;
                default:
                    throw new NotSupportedException(string.Format("Data Provider \"{0}\" not supported.", Enum.GetName(typeof(DbProvider), provider)));
            }

            return factory;
        }

        #endregion

        #region CreateCommand

        private IDbCommand CreateCommand(string strIdSession, string strTransaction, IDbCommandConfig config, params DbParameter[] parameters)
        {

            DbProcedureConfigurationElement configuration = DataSettings.Procedures[config.Name];

            IDbCommand command = this.GetFactory().CreateCommand();

            this.EnsureInnerConnection();
            Claro.Web.Logging.Info(strIdSession, strTransaction, "El procedimiento almacenado es: " + configuration.ProcedureName.ToString());

            StringBuilder strParameter = new StringBuilder();
            foreach (DbParameter item in parameters)
            {
                if (item.Value == null)
                {
                    item.Value = DBNull.Value;
                }
                strParameter.Append(item.ParameterName.ToString() + ": " + item.Value.ToString() + ", ");
            }
            string strParameterResponse = strParameter.ToString().Substring(0, strParameter.Length - 1).ToString();
            Claro.Web.Logging.Info(strIdSession, strTransaction, "Parámetros de entrada: " + strParameterResponse.Substring(0, strParameterResponse.Length - 1));

            command.Connection = this.m_innerConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = configuration.ProcedureName;
            command.CommandTimeout = configuration.CommandTimeout;

            this.CreateParameters(command, parameters);

            return command;
        }

        #endregion

        #region ExecuteScalar

        private object ExecuteScalar(string strIdSession, string strTransaction, string fieldName, IDbCommand command, DbParameter[] parameters)
        {
            object result = null;

            this.OpenInnerConnection(strIdSession, strTransaction);

            try {
                using (System.Data.Common.DbDataReader reader = (System.Data.Common.DbDataReader)command.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            int ordinal = (!string.IsNullOrEmpty(fieldName) ? reader.GetOrdinal(fieldName) : 0);

                            if (ordinal >= 0)
                            {
                                if (!reader.IsDBNull(ordinal))
                                {
                                    result = reader[ordinal];
                                }
                            }
                            else
                            {
                                throw new NotSupportedException(string.Format("Fieldname \"{0}\" not found.", fieldName));
                            }
                        }

                        if (parameters != null && parameters.Length > 0)
                        {
                            foreach (DbParameter parameter in parameters)
                            {
                                if (parameter.Direction != ParameterDirection.Input && parameter.DbType != DbType.Object)
                                {
                                    parameter.Value = ((System.Data.IDataParameter)command.Parameters[parameter.ParameterName]).Value;
                                }
                            }
                        }
                    }
                }
            
            }catch (Exception ex){
                Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString() , ex.Message));
                throw;
            }

            
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteReader

        private void ExecuteReader(string strIdSession, string strTransaction, IDbCommand command, DbParameter[] parameters, ExecuteReaderDelegate @delegate)
        {
            this.OpenInnerConnection(strIdSession, strTransaction);
            try
            {
                using (System.Data.Common.DbDataReader reader = (System.Data.Common.DbDataReader)command.ExecuteReader(CommandBehavior.Default))
                {
                    if (@delegate != null)
                    {
                        @delegate(reader);
                    }

                    if (parameters != null && parameters.Length > 0)
                    {
                        foreach (DbParameter parameter in parameters)
                        {
                            if (parameter.Direction != ParameterDirection.Input && parameter.DbType != DbType.Object)
                            {
                                parameter.Value = ((System.Data.IDataParameter)command.Parameters[parameter.ParameterName]).Value;
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString(), ex.Message));
                throw;

            }
        }

        private TResult ExecuteReader<TResult>(string strIdSession, string strTransaction, IDbCommand command, DbParameter[] parameters) where TResult : new()
        {
            TResult result = default(TResult);

            this.OpenInnerConnection(strIdSession, strTransaction);
            try {

                using (System.Data.Common.DbDataReader reader = (System.Data.Common.DbDataReader)command.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.HasRows)
                    {
                        result = Activator.CreateInstance<TResult>();

                        if (result is System.Collections.IEnumerable)
                        {
                            (result as System.Collections.IEnumerable).Add1(reader);
                        }
                        else
                        {
                            if (reader.Read())
                            {
                                result.Entity(reader);
                            }
                        }

                        if (parameters != null && parameters.Length > 0)
                        {
                            foreach (DbParameter parameter in parameters)
                            {
                                if (parameter.Direction != ParameterDirection.Input && parameter.DbType != DbType.Object)
                                {
                                    parameter.Value = ((System.Data.IDataParameter)command.Parameters[parameter.ParameterName]).Value;
                                }
                            }
                        }
                    }
                }
                
               
            }catch (Exception ex){
                Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString() , ex.Message));
                throw;
            
            }

            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteNonQuery

        private int ExecuteNonQuery(string strIdSession, string strTransaction, IDbCommand command, params DbParameter[] parameters)
        {
            int result;

            this.OpenInnerConnection(strIdSession, strTransaction);
            try {
                result = command.ExecuteNonQuery();

                if (parameters != null && parameters.Length > 0)
                {
                    foreach (DbParameter parameter in parameters)
                    {
                        if (parameter.Direction != ParameterDirection.Input && parameter.DbType != DbType.Object)
                        {
                            parameter.Value = ((System.Data.IDataParameter)command.Parameters[parameter.ParameterName]).Value;
                        }
                    }
                }
            
            }catch (Exception ex){
                Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString(), ex.Message));
                throw;
            
            }
         
            
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }


        #endregion

        #region [ Static Members ]

        #region [ Methods ]

        #region ExecuteScalarToInt

        public static int? ExecuteScalarToInt(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters, string fieldname)
        {
            int? result;
            
            object value = ExecuteScalar(strIdSession, strTransaction, connectionConfiguration, commandConfiguration, parameters, fieldname);

            if (value != null)
            {
                result = int.Parse(value.ToString());
            }
            else
            {
                result = null;
            }
            
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteScalarToString

        public static string ExecuteScalarToString(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters, string fieldname)
        {
            string result;
            
                object value = ExecuteScalar(strIdSession, strTransaction, connectionConfiguration, commandConfiguration, parameters, fieldname);

                if (value != null)
                {
                    result = value.ToString();
                }
                else
                {
                    result = null;
                }
         
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalar(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters)
        {
            object result;
            result = ExecuteScalar(strIdSession, strTransaction, connectionConfiguration, commandConfiguration, parameters, string.Empty);
            
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        public static object ExecuteScalar(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters, string fieldname)
        {
            object result = null;
           
            using (DbFactory procedure = new Data.DbFactory(connectionConfiguration))
            {

                using (IDbCommand command = procedure.CreateCommand(strIdSession, strTransaction, commandConfiguration, parameters))
                {
                        
                    try
                    {
                        result = procedure.ExecuteScalar(strIdSession, strTransaction, fieldname, command, parameters);
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString(), ex.Message));
                        throw;
                    }
                }
                }
            
            


            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteReader

        public static void ExecuteReader(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters, ExecuteReaderDelegate @delegate)
        {
           
                using (DbFactory procedure = new Data.DbFactory(connectionConfiguration))
                {
                    using (IDbCommand command = procedure.CreateCommand(strIdSession, strTransaction, commandConfiguration, parameters))
                    {
                        try
                        {
                            procedure.ExecuteReader(strIdSession, strTransaction, command, parameters, @delegate);
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString(), ex.Message));
                            throw;
                        }
                    }
                }        
              
        }

        public static TResult ExecuteReader<TResult>(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters) where TResult : new()
        {
            TResult result = default(TResult);
           

                using (DbFactory procedure = new Data.DbFactory(connectionConfiguration))
                {
                    using (IDbCommand command = procedure.CreateCommand(strIdSession, strTransaction, commandConfiguration, parameters))
                    {
                        
                        try
                        {
                            result = procedure.ExecuteReader<TResult>(strIdSession, strTransaction, command, parameters);
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString() , ex.Message));
                            throw;
                        }
                    }
                }    
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #region ExecuteNonQuery

        public static int ExecuteNonQuery(string strIdSession, string strTransaction, IDbConnectionConfiguration connectionConfiguration, IDbCommandConfig commandConfiguration, DbParameter[] parameters)
        {
            int result = -1;
         
                using (DbFactory procedure = new Data.DbFactory(connectionConfiguration))
                {
                    using (IDbCommand command = procedure.CreateCommand(strIdSession, strTransaction, commandConfiguration, parameters))
                    {   
                        try
                        {
                            result = procedure.ExecuteNonQuery(strIdSession, strTransaction, command, parameters);
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strTransaction, string.Format("Error al ejecutar el procedimiento {0}, Error:{1}", command.CommandText.ToString() , ex.Message));
                            throw;
                        }
                    }
                }         
            Claro.Web.Logging.WriteDataBase(strIdSession, strTransaction, result);
            return result;
        }

        #endregion

        #endregion

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this.m_innerConnection.Close();
                this.m_innerConnection.Dispose();
                Claro.Web.Logging.Info(m_IdSession, m_Transaction, "Se cerro la conexión.");
                disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

    public delegate void ExecuteReaderDelegate(IDataReader reader);

}

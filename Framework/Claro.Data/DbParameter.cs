using System;
using System.Data;

namespace Claro.Data
{
    public class DbParameter : System.Data.Common.DbParameter
    {
        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; }
        public override string ParameterName { get; set; }
        public override int Size { get; set; }
        public override string SourceColumn { get; set; }
        public override bool SourceColumnNullMapping { get; set; }
        public override object Value { get; set; }

        public DbParameter()
        {
            this.Direction = ParameterDirection.Input;
        }

        public DbParameter(string parameterName, DbType type) :
            this()
        {
            this.ParameterName = parameterName;
            this.DbType = type;
        }

        public DbParameter(string parameterName, DbType type, ParameterDirection direction)
        {
            this.ParameterName = parameterName;
            this.DbType = type;
            this.Direction = direction;
        }

        public DbParameter(string parameterName, DbType type, int size)
        {
            this.ParameterName = parameterName;
            this.DbType = type;
            this.Size = size;
            this.Direction = ParameterDirection.Input;
        }

        public DbParameter(string parameterName, DbType type, int size, ParameterDirection direction)
        {
            this.ParameterName = parameterName;
            this.DbType = type;
            this.Size = size;
            this.Direction = direction;
        }

        public DbParameter(string parameterName, DbType type, ParameterDirection direction, object value)
        {
            this.ParameterName = parameterName;
            this.DbType = type;
            this.Value = value;
            this.Direction = direction;
        }

        public DbParameter(string parameterName, DbType type, int size, ParameterDirection direction, object value)
        {
            this.ParameterName = parameterName;
            this.DbType = type;
            this.Size = size;
            this.Value = value;
            this.Direction = direction;
        }

        public override void ResetDbType()
        {
            throw new NotImplementedException();
        }
    }
}

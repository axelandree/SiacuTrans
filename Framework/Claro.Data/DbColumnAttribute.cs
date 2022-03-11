using System;

namespace Claro.Data
{
    public class DbColumnAttribute : Attribute
    {
        private string m_strName;

        public DbColumnAttribute(string name)
        {
            this.SetName(name);
        }

        public string Name
        {
            get
            {
                return this.GetName();
            }
        }

        private string GetName()
        {
            return this.m_strName;
        }

        private void SetName(string value)
        {
            this.m_strName = value;
        }
    }
}

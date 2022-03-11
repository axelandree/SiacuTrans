using System;

namespace Claro.Data
{
    public class DbTableAttribute : Attribute
    {
        private string m_strName;

        public DbTableAttribute(string name)
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

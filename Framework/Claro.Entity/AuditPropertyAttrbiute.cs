using System;

namespace Claro.Entity
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AuditPropertyAttribute : Attribute
    {
        private string m_name;

        public AuditPropertyAttribute(string name)
        {
            this.SetName(name);
        }

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        private void SetName(string value)
        {
            this.m_name = value;
        }
    }
}

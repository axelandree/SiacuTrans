
namespace Claro.Data
{
    public struct SuccessCode
    {
        public static readonly SuccessCode Default = Create("0");

        public static readonly SuccessCode Alternative = Create("1");

        public static readonly SuccessCode OK = Create("OK");

        private readonly string m_value;

        private SuccessCode(string value)
        {
            this.m_value = value;
        }

        private static SuccessCode Create(string value)
        {
            return new SuccessCode(value);
        }

        public string Value
        {
            get
            {
                return this.m_value;
            }
        }
    }
}

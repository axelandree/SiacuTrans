using System;

namespace Claro.Helpers
{
    public class HeaderAttribute : Attribute
    {
        public HeaderAttribute()
        {
            Order = -1;
        }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Group { get; set; }
    }
}

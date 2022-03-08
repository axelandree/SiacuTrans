using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "visualizations")]
    public class Visualization
    {
        [DataMember(Name = "item")]
        public List<VisualizationItem> item { get; set; }
    }
}

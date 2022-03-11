using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "visualizationsList")]
    public class VisualizationsList
    {
        [DataMember(Name = "visualization")]
        public List<Visualization> visualization { get; set; }
    }
}

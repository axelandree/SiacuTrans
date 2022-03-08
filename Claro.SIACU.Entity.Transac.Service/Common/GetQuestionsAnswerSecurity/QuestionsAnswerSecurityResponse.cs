using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetQuestionsAnswerSecurity
{
    [DataContract(Name = "QuestionsAnswerSecurityResponse")]
    public class QuestionsAnswerSecurityResponse : Claro.Entity.Request
    {
        [DataMember]
        public List<QuestionsSecurity> ListQuestionsSecurity { get; set; }

        [DataMember]
        public List<AnswerSecurity> ListAnswerSecurity { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Poll
    {
        public Poll()
        {
            OrderSets = new HashSet<OrderSets>();
            PoliticianSets = new HashSet<PoliticianSets>();
        }

        public decimal Id { get; set; }
        public decimal IdRespondent { get; set; }
        public DateTime? PollDate { get; set; }

        public Respondents IdRespondentNavigation { get; set; }
        public ICollection<OrderSets> OrderSets { get; set; }
        public ICollection<PoliticianSets> PoliticianSets { get; set; }
    }
}

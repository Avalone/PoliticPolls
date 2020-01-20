using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Poll
    {
        public Poll()
        {
            Orders = new HashSet<OrderSets>();
            Politicians = new HashSet<PoliticianSets>();
        }

        public int Id { get; set; }
        public int IdRespondent { get; set; }
        public DateTime? PollDate { get; set; }

        public Respondents Respondent { get; set; }
        public ICollection<OrderSets> Orders { get; set; }
        public ICollection<PoliticianSets> Politicians { get; set; }
    }
}

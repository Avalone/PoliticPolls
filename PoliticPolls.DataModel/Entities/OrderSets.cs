using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class OrderSets
    {
        public decimal IdPoll { get; set; }
        public decimal IdOrder { get; set; }

        public Orders IdOrderNavigation { get; set; }
        public Poll IdPollNavigation { get; set; }
    }
}

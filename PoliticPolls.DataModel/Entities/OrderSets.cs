using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class OrderSets
    {
        public int IdPoll { get; set; }
        public int IdOrder { get; set; }

        public Orders Order { get; set; }
        public Poll Poll { get; set; }
    }
}

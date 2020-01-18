using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class PoliticianSets
    {
        public decimal IdPoll { get; set; }
        public decimal IdPolitician { get; set; }
        public decimal? Rating { get; set; }

        public Politicians IdPoliticianNavigation { get; set; }
        public Poll IdPollNavigation { get; set; }
    }
}

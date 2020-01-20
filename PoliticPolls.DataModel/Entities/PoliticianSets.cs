using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class PoliticianSets
    {
        public int IdPoll { get; set; }
        public int IdPolitician { get; set; }
        public int? Rating { get; set; }

        public Politicians Politician { get; set; }
        public Poll Poll { get; set; }
    }
}

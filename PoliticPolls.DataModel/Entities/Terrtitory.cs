using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Terrtitory
    {
        public Terrtitory()
        {
            Politicians = new HashSet<Politicians>();
        }

        public int Id { get; set; }
        public string TerritoryName { get; set; }

        public ICollection<Politicians> Politicians { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Politicians
    {
        public Politicians()
        {
            Orders = new HashSet<Orders>();
            PoliticianSets = new HashSet<PoliticianSets>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patro { get; set; }
        public decimal IdTerritory { get; set; }

        public Terrtitory Terrtitory { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<PoliticianSets> PoliticianSets { get; set; }
    }
}

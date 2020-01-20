using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Orders
    {
        public Orders()
        {
            OrderSets = new HashSet<OrderSets>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public int? IdPolitician { get; set; }

        public Politicians Politician { get; set; }
        public ICollection<OrderSets> OrderSets { get; set; }
    }
}

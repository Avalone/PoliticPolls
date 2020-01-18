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

        public decimal Id { get; set; }
        public string Text { get; set; }
        public decimal? IdPolitician { get; set; }

        public Politicians IdPoliticianNavigation { get; set; }
        public ICollection<OrderSets> OrderSets { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class RespondentsLog
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public DateTime? ChangedDate { get; set; }
        public string ChangedAttr { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}

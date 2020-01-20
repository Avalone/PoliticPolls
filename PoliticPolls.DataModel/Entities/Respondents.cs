using System;
using System.Collections.Generic;

namespace PoliticPolls.DataModel
{
    public partial class Respondents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patro { get; set; }
        public DateTime? BirthDate { get; set; }

        public Poll Poll { get; set; }
    }
}

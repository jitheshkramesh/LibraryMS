using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMS.Models.patron
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronDetalModel> Patrons { get; set; }
    }
}

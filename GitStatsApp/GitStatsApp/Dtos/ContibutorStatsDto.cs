using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitStatsApp.Dtos
{
    public class ContibutorStatsDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Branches { get; set; }
        public int Commits { get; set; }
        public int LinesOfCode { get; set; }
        public double Contribution { get; set; }
    }
}

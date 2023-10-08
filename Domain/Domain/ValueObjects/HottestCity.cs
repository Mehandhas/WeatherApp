using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class HottestCity
    {
        public string CityName { get; set; } = null!;

        public int TotalDaysAbove30C { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class MoistestCity
    {
        public string CityName { get; set; } = null!;

        public decimal AverageHumidity { get; set; }
    }
}

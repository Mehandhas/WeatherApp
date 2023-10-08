using System;
using System.Collections.Generic;

namespace Domain.DomainModels;

public partial class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Variable> Variables { get; set; } = new List<Variable>();
}

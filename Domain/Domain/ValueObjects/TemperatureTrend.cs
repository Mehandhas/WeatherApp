
namespace Domain.ValueObjects
{
    public  class TemperatureTrend
    {
        public string VariableName { get; set; } = null!;

        public string variableUnit { get; set; } = null!;

        public decimal VariableValue { get; set; }

        public DateTimeOffset VariableTimestamp { get; set; }

        public string CityName { get; set; } = null!;

    }
}

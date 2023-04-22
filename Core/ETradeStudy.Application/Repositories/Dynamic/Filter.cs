namespace ETradeStudy.Application.Repositories.Dynamic
{
    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string? Value { get; set; }
        public string? Logic { get; set; }

        public Filter(string field, string @operator, string? value, string? logic)
        {
            Field = field;
            Operator = @operator;
            Value = value;
            Logic = logic;
        }
        public Filter()
        {

        }
    }
}

namespace Portfolio.Models.Model
{
    public class TokenModel
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public double PriceVariance { get; set; } = 0;
    }
}

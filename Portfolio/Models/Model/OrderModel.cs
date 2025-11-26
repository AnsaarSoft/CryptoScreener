using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Portfolio.Models.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Tokken name is mandatory.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(10, ErrorMessage = "Symbol is mandatory.")]
        public string Symbol { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity is mandatory.")]
        public double Quantity { get; set; } = 0;
        [Required]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Buy price is mandatory.")]
        public double BuyPrice { get; set; } = 0;
        public double CurrentPrice { get; set; } = 0;
        public double Profit => CurrentAmount > 0 && InvestedAmount > 0 ? (CurrentAmount - InvestedAmount) / InvestedAmount : 0;
        public double CurrentAmount => Quantity > 0 && CurrentPrice > 0 ? Quantity * CurrentPrice : 0;
        public double InvestedAmount
        {
            get => Quantity > 0 && BuyPrice > 0 ? Quantity * BuyPrice : 0;
            set { } 
        }

    }
}

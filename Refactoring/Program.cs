using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<string, List<Product>> order = new Tuple<string, List<Product>>("John Doe",
                new List<Product>
                {
                    new Product
                    {
                        ProductName = "Pulled Pork",
                        Price = 6.99m,
                        Weight = 0.5m,
                        PricingMethod = "PerPound",
                    },
                    new Product
                    {
                        ProductName = "Coke",
                        Price = 3m,
                        Quantity = 2,
                        PricingMethod = "PerItem"
                    }
                }
            );

            decimal price = 0m;
            StringBuilder orderSummary = new StringBuilder("ORDER SUMMARY FOR ");
            orderSummary.AppendLine(order.Item1 + ":");
            decimal productPrice = 0m;
            decimal coef = 0m;
            string formatString;
            foreach (Product orderProduct in order.Item2)
            {
                orderSummary.Append(orderProduct.ProductName);
                if (orderProduct.PricingMethod == "PerPound")
                {
                    coef = orderProduct.Weight.GetValueOrDefault();
                    formatString = $" ${{0}} ({{1}} pounds at ${orderProduct.Price} per pound)";
                }
                else // Per item
                {
                    coef = orderProduct.Quantity.GetValueOrDefault();
                    formatString = $" ${{0}} ({{1}} items at ${orderProduct.Price} per each)";
                }
                productPrice = coef * orderProduct.Price;
                orderSummary.AppendFormat(formatString, productPrice, coef).AppendLine();
                price += productPrice;
            }
            orderSummary.AppendLine($"\nTotal Price: ${price}");

            Console.WriteLine(orderSummary);

            Console.ReadKey();
        }
    }

    public class Product
    {
        public string ProductName;
        public decimal Price;
        public decimal? Weight;
        public int? Quantity;
        public string PricingMethod;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public int Price { get; private set; }
        public string Name { get; private set; }

        public void ProductIdEdit(int idOfProduct)
        {
            Id = idOfProduct;
        }

        public void ProductPriceEdit(int priceOfProduct)
        {
            Price = priceOfProduct;
        }

        public void ProductNameEdit(string nameOfProduct)
        {
            Name = nameOfProduct;
        }
    }

   
}

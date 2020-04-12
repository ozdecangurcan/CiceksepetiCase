using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Business.Service
{
    public static class ProductService
    {

        public class ProductModel
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public int MaxCartQuantity { get; set; }
        }

        /// <summary>
        /// Ürünleri dönen fake metot
        /// </summary>
        /// <returns></returns>
        public static List<ProductModel> GetProducts()
        {
            var productList = new List<ProductModel>();

            productList.Add(new ProductModel { ProductId = Guid.Parse("278df7ab-8c79-46be-9e84-d9956fccd3b3"), ProductName = "Bilgisayar", Quantity = 8, MaxCartQuantity = 1 });
            productList.Add(new ProductModel { ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"), ProductName = "Ayakkabı", Quantity = 12, MaxCartQuantity = 15 });
            productList.Add(new ProductModel { ProductId = Guid.Parse("4310ce2c-17a0-4117-8e33-27cc237e2770"), ProductName = "Tshirt", Quantity = 5, MaxCartQuantity = 6 });
            productList.Add(new ProductModel { ProductId = Guid.Parse("ed9829b9-684e-49a2-9287-332e6827076a"), ProductName = "Çiçek", Quantity = 10, MaxCartQuantity = 5 });
            productList.Add(new ProductModel { ProductId = Guid.Parse("b801ff1a-3117-4136-aff1-8dcb01b2fb05"), ProductName = "Kolonya", Quantity = 0, MaxCartQuantity = 1 });

            return productList;
        }
    }
}

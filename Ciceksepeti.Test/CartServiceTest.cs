
using Ciceksepeti.Business.Interface;
using Ciceksepeti.Business.Service;
using Ciceksepeti.DataAccess.Interface;
using Ciceksepeti.DataAccess.Service;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;
using Ciceksepeti.Test.Base;
using Ciceksepeti.Test.Database;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Ciceksepeti.Test
{
    [TestFixture]
    public class CartServiceTest : TestBase
    {
        private ICartRepository _cartRepository;

        private FakeDbContext _fakeDbContext;

        private CartService _cartService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _fakeDbContext = new FakeDbContext(FakeDbContextOptions);
            _cartRepository = new CartRepository(_fakeDbContext);
            _cartService = new CartService(_cartRepository);
        }

        [Test]
        public async Task Add_Item_To_Cart_Out_Of_Stock_Test()
        {
            var result = await _cartService.Add(new CartRequestDto
            {
                ProductId = Guid.Parse("b801ff1a-3117-4136-aff1-8dcb01b2fb05"),
                UserId = Guid.NewGuid(),
                Quantity = 1
            });

            Assert.False(result.IsSuccess);
            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Add_Item_To_Cart_Cart_Size_Over_Test()
        {
            for (int i = 0; i < 5; i++)
            {
                _fakeDbContext.Carts.Add(new Cart
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1,
                    UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78")
                });
            }

            _fakeDbContext.SaveChanges();

            var result = await _cartService.Add(new CartRequestDto
            {
                ProductId = Guid.NewGuid(),
                UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78"),
                Quantity = 1
            });

            Assert.False(result.IsSuccess);
            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.BadRequest);
        }
    }
}

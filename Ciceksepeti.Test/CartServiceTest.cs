using Ciceksepeti.Business.Service;
using Ciceksepeti.DataAccess.Interface;
using Ciceksepeti.DataAccess.Service;
using Ciceksepeti.Dto.Cart;
using Ciceksepeti.Entity;
using Ciceksepeti.Test.Base;
using Ciceksepeti.Test.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
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

        [Test]
        public async Task Add_Item_To_Cart_Max_Quantity_Over_Test()
        {
            var result = await _cartService.Add(new CartRequestDto
            {
                ProductId = Guid.Parse("ed9829b9-684e-49a2-9287-332e6827076a"),
                UserId = Guid.NewGuid(),
                Quantity = 6
            });

            var maxQuantity = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == Guid.Parse("ed9829b9-684e-49a2-9287-332e6827076a")).MaxCartQuantity;

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);

            var resultData = result.Data as CartResponseDto;
            Assert.AreEqual(resultData.Quantity, maxQuantity);
        }

        [Test]
        public async Task Add_Item_To_Cart_Stock_Quantity_Over_Test()
        {
            var result = await _cartService.Add(new CartRequestDto
            {
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                UserId = Guid.NewGuid(),
                Quantity = 16
            });

            var stockQuantity = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a")).Quantity;

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);

            var resultData = result.Data as CartResponseDto;
            Assert.AreEqual(resultData.Quantity, stockQuantity);
        }

        [Test]
        public async Task Add_Item_To_Cart_Passed_All_Validations()
        {
            var result = await _cartService.Add(new CartRequestDto
            {
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                UserId = Guid.NewGuid(),
                Quantity = 1
            });

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);
            Assert.NotNull(result.Data);
        }

        [Test]
        public async Task Get_Cart_List_Wrong_Guid()
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

            var result = await _cartService.GetAll(Guid.Empty);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Get_Cart_List_Success()
        {
            for (int i = 0; i < 5; i++)
            {
                await _fakeDbContext.Carts.AddAsync(new Cart
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1,
                    UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78")
                });
            }

            await _fakeDbContext.SaveChangesAsync();

            var result = await _cartService.GetAll(Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78"));

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);
        }

        [Test]
        public void Update_Cart_Item_Out_Of_Stock_Test()
        {
            var result = _cartService.Update(new CartUpdateRequestDto
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("b801ff1a-3117-4136-aff1-8dcb01b2fb05"),
                UserId = Guid.NewGuid(),
                Quantity = 1
            });

            Assert.False(result.IsSuccess);
            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.BadRequest);
        }


        [Test]
        public void Update_Cart_Item_Cart_Size_Over_Test()
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

            var result = _cartService.Update(new CartUpdateRequestDto
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.Parse("b801ff1a-3117-4136-aff1-8dcb01b2fb05"),
                UserId = Guid.NewGuid(),
                Quantity = 1
            });

            Assert.False(result.IsSuccess);
            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Update_Cart_Item_Max_Quantity_Over_Test()
        {
            var entity = new Cart
            {
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                Quantity = 1,
                UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78")
            };

            var addResult = _fakeDbContext.Carts.Add(entity);

            _fakeDbContext.SaveChanges();

            _fakeDbContext.Entry(entity).State = EntityState.Detached;

            var cartId = addResult.Entity.Id;

            var result = _cartService.Update(new CartUpdateRequestDto
            {
                Id = cartId,
                ProductId = Guid.Parse("ed9829b9-684e-49a2-9287-332e6827076a"),
                UserId = Guid.NewGuid(),
                Quantity = 6
            });

            var maxQuantity = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == Guid.Parse("ed9829b9-684e-49a2-9287-332e6827076a")).MaxCartQuantity;

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);

            var resultData = result.Data as CartResponseDto;
            Assert.AreEqual(resultData.Quantity, maxQuantity);
        }

        [Test]
        public void Update_Cart_Item_Stock_Quantity_Over_Test()
        {
            var entity = new Cart
            {
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                Quantity = 1,
                UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78")
            };

            var addResult = _fakeDbContext.Carts.Add(entity);

            _fakeDbContext.SaveChanges();

            _fakeDbContext.Entry(entity).State = EntityState.Detached;

            var cartId = addResult.Entity.Id;

            var result = _cartService.Update(new CartUpdateRequestDto
            {
                Id = cartId,
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78"),
                Quantity = 16
            });

            var stockQuantity = ProductService.GetProducts().FirstOrDefault(x => x.ProductId == Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a")).Quantity;

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);

            var resultData = result.Data as CartResponseDto;
            Assert.AreEqual(resultData.Quantity, stockQuantity);
        }

        [Test]
        public void Update_Cart_Success_Test()
        {
            var entity = new Cart
            {
                ProductId = Guid.Parse("43f18e01-28c5-468a-b688-c1a896a7291a"),
                Quantity = 1,
                UserId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78")
            };

            var addResult = _fakeDbContext.Carts.Add(entity);

            _fakeDbContext.SaveChanges();

            var result = _cartRepository.UpdateCartUser(new UpdateCartUserRequestDto
            {
                SessionId = Guid.Parse("05d2f53a-d949-4984-85ec-b8326375ee78"),
                UserId = Guid.NewGuid()
            });

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.OK);
            Assert.GreaterOrEqual((int)result.Data, 1);
        }

        [Test]
        public void Update_Cart_Not_Update_Test()
        {
            var entity = new Cart
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1,
                UserId = Guid.NewGuid()
            };

            var addResult = _fakeDbContext.Carts.Add(entity);

            _fakeDbContext.SaveChanges();

            var result = _cartRepository.UpdateCartUser(new UpdateCartUserRequestDto
            {
                SessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            });

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(result.ResultCode, HttpStatusCode.NoContent);
            Assert.AreEqual(result.Data, 0);
        }
    }
}

using Ciceksepeti.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Ciceksepeti.Test.Database
{
    public class FakeDbContext : CartContext
    {
        public FakeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

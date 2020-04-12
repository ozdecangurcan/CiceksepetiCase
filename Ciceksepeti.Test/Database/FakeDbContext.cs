using Ciceksepeti.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Test.Database
{
    public class FakeDbContext : CartContext
    {
        public FakeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

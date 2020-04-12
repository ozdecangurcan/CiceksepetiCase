using Ciceksepeti.DataAccess;
using Ciceksepeti.Test.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciceksepeti.Test.Base
{
    public class TestBase
    {
        protected DbContextOptions<CartContext> FakeDbContextOptions { get; }

        protected TestBase()
        {
            //Db Options
            FakeDbContextOptions = new DbContextOptionsBuilder<CartContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

    }
}

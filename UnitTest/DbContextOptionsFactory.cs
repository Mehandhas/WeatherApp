using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class DbContextOptionsFactory
    {
        public static DbContextOptions<TContext> Create<TContext>()
      where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder.UseInMemoryDatabase(databaseName: "TestWeatherDB");
           // builder.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            return builder.Options;
        }
    }
}

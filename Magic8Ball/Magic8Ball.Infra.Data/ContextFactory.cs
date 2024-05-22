using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Infra.Data
{
    public class ContextFactoryy : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        public string DbPath { get; }

        public ContextFactoryy()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "magic8ball.db");
        }

        public DefaultDbContext CreateDbContext(string[] args)
        {
            string connectionString
                = $"Data Source={DbPath}";
            var optionBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            optionBuilder.UseSqlite(connectionString, (opt) => { });

            return new DefaultDbContext(optionBuilder.Options);
        }
    }
}

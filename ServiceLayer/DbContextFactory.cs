using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder();

            optionBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MeetingDb;Data Source=.");

            return new DatabaseContext(optionBuilder.Options);
        }
    }
}

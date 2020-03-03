using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreOgrenci.Models
{
    public class PostgresContext : DbContext
    {
        public PostgresContext()
        {

        }
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {

        }

        public DbSet<Ogrenci> ogrenci { get; set; }
        public DbSet<OgrenciToken> ogrenciToken { get; set; }
        public DbSet<postgreLogs> postgreLogs { get; set; }

        //public IQueryable<Ogrenci> ogrenci { get; set; }

    }
}

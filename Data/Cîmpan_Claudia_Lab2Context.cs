using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cîmpan_Claudia_Lab2.Models;

namespace Cîmpan_Claudia_Lab2.Data
{
    public class Cîmpan_Claudia_Lab2Context : DbContext
    {
        public Cîmpan_Claudia_Lab2Context (DbContextOptions<Cîmpan_Claudia_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Cîmpan_Claudia_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Cîmpan_Claudia_Lab2.Models.Publisher> Publisher { get; set; }

        public DbSet<Cîmpan_Claudia_Lab2.Models.Author> Author { get; set; }

        public DbSet<Cîmpan_Claudia_Lab2.Models.Category> Category { get; set; }
    }
}

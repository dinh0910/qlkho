using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qlkho.Models;

namespace qlkho.Data
{
    public class qlkhoContext : DbContext
    {
        public qlkhoContext (DbContextOptions<qlkhoContext> options)
            : base(options)
        {
        }

        public DbSet<qlkho.Models.Role> Role { get; set; } = default!;

        public DbSet<qlkho.Models.User>? User { get; set; }

        public DbSet<qlkho.Models.MaterialTopic>? MaterialTopic { get; set; }

        public DbSet<qlkho.Models.MaterialType>? MaterialType { get; set; }

        public DbSet<qlkho.Models.MaterialName>? MaterialName { get; set; }

        public DbSet<qlkho.Models.Material>? Material { get; set; }

        public DbSet<qlkho.Models.Unit>? Unit { get; set; }

        public DbSet<qlkho.Models.Supplier>? Supplier { get; set; }

        public DbSet<qlkho.Models.Import>? Import { get; set; }

        public DbSet<qlkho.Models.ImportLog>? ImportLog { get; set; }

        public DbSet<qlkho.Models.Export>? Export { get; set; }

        public DbSet<qlkho.Models.ExportLog>? ExportLog { get; set; }
        
        public DbSet<qlkho.Models.Lend>? Lend { get; set; }

        public DbSet<qlkho.Models.LendLog>? LendLog { get; set; }
    }
}

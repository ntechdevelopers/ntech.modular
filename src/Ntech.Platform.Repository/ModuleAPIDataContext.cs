using Microsoft.EntityFrameworkCore;
using Ntech.Contract.Entity.SubDatabase;
using Ntech.Infrastructure;
using System;
using System.Collections.Generic;

namespace Ntech.Platform.Repository
{
    public class ModuleAPIDataContext : BaseDataContext
    {
        public virtual DbSet<Value> Values { get; set; }

        public ModuleAPIDataContext(DbContextOptions<ModuleAPIDataContext> options) : base(options)
        {
        }

        protected override void RegisterEntities(ModelBuilder modelBuilder, List<Type> typeToRegisters)
        {
            modelBuilder.RegisterEntities(typeToRegisters, typeof(Value));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

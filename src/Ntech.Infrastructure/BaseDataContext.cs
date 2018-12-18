using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ntech.Contract.Entity;
using Ntech.Contract.Entity.SubDatabase;
using Ntech.Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ntech.Infrastructure
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext(DbContextOptions<BaseDataContext> options) : base(options)
        {
        }

        public BaseDataContext(DbContextOptions options) : base(options)
        {
        }

        protected virtual void RegisterEntities(ModelBuilder modelBuilder, List<Type> typeToRegisters)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typeToRegisters = new List<Type>();
            foreach (var module in GlobalConfiguration.Modules)
            {
                typeToRegisters.AddRange(module.Assembly.DefinedTypes.Select(t => t.AsType()));
            }

            this.RegisterEntities(modelBuilder, typeToRegisters);

            modelBuilder.RegiserConvention();

            modelBuilder.RegisterCustomMappings(typeToRegisters);
        }
    }
}

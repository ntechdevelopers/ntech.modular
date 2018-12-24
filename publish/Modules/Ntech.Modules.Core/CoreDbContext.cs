using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ntech.Contract.Entity;
using Ntech.Core.Server;
using Ntech.Infrastructure;
using Ntech.Modules.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ntech.Modules.Core
{
    public class CoreDbContext : IdentityDbContext<Models.User, Models.Role, long>
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public CoreDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Type> typeToRegisters = new List<Type>();
            foreach (var module in GlobalConfiguration.Modules)
            {
                typeToRegisters.AddRange(module.Assembly.DefinedTypes.Select(t => t.AsType()));
            }

            modelBuilder.RegisterEntities(typeToRegisters, typeof(IdentityEntity));

            modelBuilder.RegiserConvention();

            base.OnModelCreating(modelBuilder);

            modelBuilder.RegisterCustomMappings(typeToRegisters);
        }

    }
}

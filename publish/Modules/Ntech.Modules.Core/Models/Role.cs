using Microsoft.AspNetCore.Identity;
using Ntech.Contract.Entity;
using Ntech.Core.Server;
using System;

namespace Ntech.Modules.Core.Models
{
    public class Role : IdentityRole<long>, IEntityWithTypedId<long>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}

using Ntech.Modules.Core;

namespace Ntech.Infrastructure
{
    public class IdentityInitializer
    {
        public static void Initialize(CoreDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}

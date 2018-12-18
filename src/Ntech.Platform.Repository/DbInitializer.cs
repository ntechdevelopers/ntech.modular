using Ntech.Contract.Entity.SubDatabase;
using Ntech.Infrastructure;
using System.Linq;

namespace Ntech.Platform.Repository
{
    public class DbInitializer
    {
        public static void Initialize(BaseDataContext context)
        {
            context.Database.EnsureCreated();

            if (context is ModuleAPIDataContext)
            {
                // Look for any values.
                var subDatabase = context as ModuleAPIDataContext;
                if (subDatabase.Values.Any())
                {
                    return;   // DB has been seeded
                }

                var values = new[]
                {
                new Value { Name = "Nino"},
                new Value { Name = "Nino2"},
                new Value { Name = "Nino3"},
                new Value { Name = "Nino4"},
                new Value { Name = "Nino5"}
            };
                foreach (var value in values)
                {
                    subDatabase.Values.Add(value);
                }

                context.SaveChanges();
            }
        }
    }
}

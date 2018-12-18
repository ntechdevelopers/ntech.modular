using Microsoft.EntityFrameworkCore;

namespace Ntech.Core.Server
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}

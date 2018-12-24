using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ntech.Contract.Entity;
using Ntech.Infrastructure;

namespace Ntech.Modules.Core
{
    public class CoreConfiguration: IEntityTypeConfiguration<IdentityEntity>
    {
        private string tableName;

        public CoreConfiguration(string tableName)
        {
            this.tableName = tableName;
        }

        public void Configure(EntityTypeBuilder<IdentityEntity> builder)
        {
            builder.ToTable(this.tableName);

        }
    }
}

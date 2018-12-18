using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ntech.Core.Server
{
    public interface IClientsModuleInitializer
    {
        void Init(IApplicationBuilder applicationBuilder, string sourcePath, IConfiguration configuration);

        void InitService(IServiceCollection services, string modulePath);
    }
}

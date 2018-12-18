using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ntech.Core.Server
{
    public interface IModuleInitializer
    {
        void Init(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}

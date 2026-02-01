using Microsoft.Extensions.DependencyInjection;

namespace PgExtension.GUI.Services
{
    public class LazyResolver<T> : Lazy<T>
        where T : class
    {
        public LazyResolver(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        { }
    }
}

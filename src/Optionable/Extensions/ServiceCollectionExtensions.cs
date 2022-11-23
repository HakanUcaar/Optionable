using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOptionable<T>(
            [NotNull] this IServiceCollection services,
            [NotNull] Action<IOption> optionsAction) where T : IOptionable
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            services.AddSingleton(typeof(IOptionable), typeof(T));
            return services;
        }

        public static IServiceCollection AddOptionable<T>(
            [NotNull] this IServiceCollection services) where T : IOptionable
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(typeof(IOptionable), typeof(T));
            return services;
        }
    }
}

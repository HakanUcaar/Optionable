using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseOptionableOptions([NotNull] this IApplicationBuilder app, [NotNull] IOption option)
        {
            var services = app.ApplicationServices;
            var asd = services.GetService(option.GetType()) ?? Activator.CreateInstance(option.GetType());

            return app;
        }
    }
}

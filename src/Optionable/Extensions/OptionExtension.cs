using Microsoft.Extensions.Configuration;
using Optionable.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optionable
{
    public static class OptionExtension
    {
        public static IOptionable AddOption<T>(this IOptionable source, Action<T> option) where T : IOption
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var optInstance = Activator.CreateInstance<T>();
            option(optInstance);
            source.Options.Add(optInstance);

            return source;
        }
        public static T? GetOption<T>(this IOptionable source) where T : IOption
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var option = source.Options.FirstOrDefault(x => x.GetType() == typeof(T));

            return option is null ? default(T) : (T)option;
        }
        public static IOptionable Configure<T>(this IOptionable source, IConfigurationSection section) where T : IOption
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (section is null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            T? option;
            if (section.GetChildren().Any(x => x.Key == typeof(T).Name))
            {
                var configure = section.GetChildren().FirstOrDefault(x => x.Key == typeof(T).Name);

                if (configure is null)
                {
                    throw new ArgumentNullException(nameof(T));
                }
                option = configure.Get<T>();
            }
            else
            {
                option = section.Get<T>();
            }

            if (option is null)
            {
                throw new ArgumentNullException(nameof(T));
            }
            source.Options.Add(option);

            return source;
        }
        public static ISectionBinder UseSection(this IOptionable source, IConfigurationSection section)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (section is null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            var sectionBinder = new SectionBinder(source, section);
            return sectionBinder;
        }
        public static ISectionBinder Configure<T>(this ISectionBinder binder) where T : IOption
        {
            T? option;
            if (binder.Section.GetChildren().Any(x => x.Key == typeof(T).Name))
            {
                var configure = binder.Section.GetChildren().FirstOrDefault(x => x.Key == typeof(T).Name);
                if (configure is null)
                {
                    throw new ArgumentNullException(nameof(T));
                }
                option = configure.Get<T>();
            }
            else
            {
                if (binder.Section.Key != typeof(T).Name)
                {
                    throw new OptionNotFoundException();
                }

                option = binder.Section.Get<T>();
            }

            if (option is null)
            {
                throw new ArgumentNullException(nameof(T));
            }

            binder.Source.Options.Add(option);

            return binder;
        }
    }
}

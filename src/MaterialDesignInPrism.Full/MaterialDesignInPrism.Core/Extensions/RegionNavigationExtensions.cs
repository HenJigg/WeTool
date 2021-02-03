using MaterialDesignInPrism.Core.Common;
using MaterialDesignThemes.Wpf;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignInPrism.Core.Extensions
{
    public static class RegionNavigationExtensions
    {
        public static NavigationResult Navigate(this INavigateAsync navigation, string target)
        {
            if (navigation == null)
                throw new ArgumentNullException(nameof(navigation));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            var targetUri = new Uri(target, UriKind.RelativeOrAbsolute);

            NavigationResult navigationResult = null;
            navigation.RequestNavigate(targetUri, arg =>
            {
                navigationResult = arg;
            });
            return navigationResult;
        }
    }
}

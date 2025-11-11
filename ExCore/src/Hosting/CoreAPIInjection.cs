
using ExCore.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExCore.Hosting
{
    public static class CoreAPIInjection
    {
        public static IServiceCollection AddExCoreAPI(this IServiceCollection self)
        {
            self.AddSingleton<ExCoreAPI>();
            return self;
        }
    }
}

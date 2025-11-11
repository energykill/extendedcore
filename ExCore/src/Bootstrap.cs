using ExCore.API;
using ExCore.Core;
using ExCore.Hosting;
using ExCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace ExCore
{
    [PluginMetadata(Id = "ExCore", Version = "local", Name = "ExCore", Author = "EnergyKill™", Description = "No description.", Website = "https://energykill.net")]
    public class Bootstrap : BasePlugin
    {
        private ServiceProvider? _provider;
        private readonly ExCoreAPI _ExCoreAPI;

        public Bootstrap(ISwiftlyCore core) : base(core)
        {
            Core.Configuration
                .InitializeJsonWithModel<ChatConfig>("chat.jsonc", "Chat")
                .Configure(builder =>
                {
                    builder.AddJsonFile("chat.jsonc", false, true);
                });
            
            var collection = new ServiceCollection();

            collection
                .AddSwiftly(Core)
                .AddChatModule()
                .AddExCoreAPI();

            _provider = collection.BuildServiceProvider();
            _provider.UseChatModule();


            _ExCoreAPI = _provider.GetRequiredService<ExCoreAPI>();
        }

        public override void ConfigureSharedInterface(IInterfaceManager interfaceManager)
        {
            interfaceManager.AddSharedInterface<IExCore, ExCoreAPI>("ExCore.API", _ExCoreAPI);
        }

        public override void Load(bool hotReload)
        {
        }

        public override void Unload()
        {
            _provider!.Dispose();
        }
    }
}

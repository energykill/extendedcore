using ExCore.Models;
using ExCore.Modules.Chat;
using Microsoft.Extensions.DependencyInjection;

namespace ExCore.Hosting
{
    public static class ChatModuleInjection
    {
        public static IServiceCollection AddChatModule(this IServiceCollection self)
        {
            self.AddSingleton<ChatManager>();
            self.AddSingleton<ChatService>();

            self.AddOptions<ChatConfig>()
            .BindConfiguration("Chat")
            .ValidateOnStart();
            return self;
        }

        public static void UseChatModule(this IServiceProvider self)
        {
            self.GetRequiredService<ChatManager>();
            self.GetRequiredService<ChatService>();
        } 
    }
}
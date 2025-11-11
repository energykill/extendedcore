using ExCore.API.Modules.Chat;

namespace ExCore.API
{
    public interface IExCore
    {
        public IChatManager Chat { get; }
    }
    
}
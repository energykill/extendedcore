using ExCore.API;
using ExCore.API.Modules.Chat;

namespace ExCore.Core
{
    public class ExCoreAPI : IExCore
    {
        public IChatManager Chat => throw new NotImplementedException();
    }
}

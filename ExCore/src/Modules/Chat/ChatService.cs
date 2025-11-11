
using System.Text;
using ExCore.Enums;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace ExCore.Modules.Chat
{
    public class ChatService
    {

        private readonly ISwiftlyCore _Core;
        private readonly ChatManager _chatManager;

        public ChatService(ISwiftlyCore core, ChatManager chat)
        {
            _Core = core;
            _chatManager = chat;
            core.Registrator.Register(this);
        }

        [ServerNetMessageHandler]
        public HookResult OnSayText2MessageHandler(CUserMessageSayText2 msg)
        {
            if (!_chatManager.IsEnable) return HookResult.Continue;

            var player = _Core.PlayerManager.GetPlayer(msg.Entityindex - 1);
            if (player == null) return HookResult.Continue;

            var newMessage = FormatMessage(player, msg.Messagename);

            if (newMessage.Equals(msg.Messagename)) return HookResult.Continue;
            msg.Messagename = newMessage;
            return HookResult.Continue;
        }

        [ServerNetMessageHandler]
        public HookResult OnRadioTextMessageHandler(CCSUsrMsg_RadioText msg)
        {
            if (!_chatManager.IsEnable) return HookResult.Continue;
            var player = _Core.PlayerManager.GetPlayer(msg.Client);

            if (player == null) return HookResult.Continue;
            var token = msg.Params[2];
            var newMessage = FormatMessage(player, token, true);

            if( newMessage.Equals(token)) return HookResult.Continue;
            msg.MsgName = newMessage;

            return HookResult.Continue;
        }

        private string FormatMessage(IPlayer player, string token, bool isRadio = false)
        {
            string placeholder = _chatManager.GetPlaceholder(token);
            if (placeholder.Equals(token)) return token;

            StringBuilder name = new();
            name.Append(_chatManager.GetPlayerNameColor(player)).Append(isRadio ? player.Controller.PlayerName : "%s1");

            StringBuilder message = new();
            message.Append(_chatManager.GetPlayerMessageColor(player)).Append(isRadio ? "" : "%s2");

            return Helper.Colored(placeholder
                .Replace("{name}", name.ToString())
                .Replace("[compteam]", GetCompTeammateColor(player))
                .Replace("{location}", isRadio ? "%s2" : "%s3")
                .Replace("{message}", message.ToString())
            );
        }
        
        private string GetCompTeammateColor(IPlayer player)
        {
            string color = (CompTeammateColor_t)player.Controller.CompTeammateColor switch
            {
                CompTeammateColor_t.GREY => Helper.ChatColors.Grey,
                CompTeammateColor_t.BLUE => Helper.ChatColors.Blue,
                CompTeammateColor_t.GREEN => Helper.ChatColors.Green,
                CompTeammateColor_t.YELLOW => Helper.ChatColors.Yellow,
                CompTeammateColor_t.ORANGE => Helper.ChatColors.Orange,
                CompTeammateColor_t.PURPLE => Helper.ChatColors.Purple,
                _ => Helper.ChatColors.White,
            };
            return color;
        }
    }
}
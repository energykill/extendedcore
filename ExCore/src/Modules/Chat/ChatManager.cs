
using ExCore.API.Modules.Chat;
using ExCore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Players;
using System.Text.Json;

namespace ExCore.Modules.Chat
{
    public class ChatManager
    {

        public bool IsEnable { get; set; } = false;
        private Dictionary<string, string> _chatPlaceholders = new();
        private Dictionary<ulong, PlayerChatConfig> _playerSettings = new();
        public ChatManager(IOptionsMonitor<ChatConfig> options, ILogger<ChatManager> logger)
        {
            LoadChatOptions(options.CurrentValue);

            options.OnChange((config) =>
            {
                try
                {
                    logger.LogInformation("Chat config changed, reloading...");
                    LoadChatOptions(config);
                    logger.LogInformation("Chat config reloaded.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error reloading chat config.");
                }
            });

        }
        private void LoadChatOptions(ChatConfig config)
        {
            IsEnable = config.Enable;
            _chatPlaceholders = config.Placeholders.ToDictionary();
            _playerSettings = config.Players.ToDictionary(x => ulong.Parse(x.Key), x => x.Value);
        }

        public string GetPlaceholder(string token)
        {
            if (_chatPlaceholders.TryGetValue(token, out var placehoder))
            {
                return placehoder;
            }
            return token;
        }

        public string GetPlayerNameColor(IPlayer player)
        {
            if (_playerSettings.TryGetValue(player.SteamID, out var playerData))
            {
                return playerData.NameColor;
            }

            string color = player.Controller.TeamNum switch
            {
                (byte)Team.T => Helper.ChatColors.Orange,
                (byte)Team.CT => Helper.ChatColors.Blue,
                (byte)Team.Spectator => Helper.ChatColors.Grey,
                _ => Helper.ChatColors.White,
            };

            return color;
        }
        
        public string GetPlayerMessageColor(IPlayer player)
        {
            if (_playerSettings.TryGetValue(player.SteamID, out var playerData))
            {
                return playerData.MessageColor;
            }
            return Helper.ChatColors.White;
        }

    }
}
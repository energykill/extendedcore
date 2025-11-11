
using System.Text.Json.Serialization;

namespace ExCore.Models
{
    public class ChatConfig
    {
        [JsonPropertyName("enable")]
        [JsonPropertyOrder(-1)]
        public bool Enable { get; set; } = false;
        [JsonPropertyName("placeholders")]
        [JsonPropertyOrder(0)]
        public Dictionary<string, string> Placeholders = new()
        {
            { "Cstrike_Chat_T", "[T] {name}: {message}" },
            { "Cstrike_Chat_T_Loc", "[T] {name} [green]@{location}[/]: {message}" },
            { "Cstrike_Chat_T_Dead", "[T] {name} [grey][DEAD][/]: {message}" },
            { "Cstrike_Chat_CT", "[CT] {name}: {message}" },
            { "Cstrike_Chat_CT_Loc", "[CT] {name} [green]@{location}[/]: {message}" },
            { "Cstrike_Chat_CT_Dead", "[CT] {name} [grey][DEAD][/]: {message}" },
            { "Cstrike_Chat_All", "[ALL] {name}: {message}" },
            { "Cstrike_Chat_AllDead", "[ALL] {name}: {message}" },
            { "Cstrike_Chat_AllSpec", "[ALL] {name} [/][SPEC]: {message}" },
            { "Cstrike_Chat_Spec", "{name} [/][SPEC]: {message}" },

            { "#SFUI_TitlesTXT_Fire_in_the_hole", "{name}[green]@{location}[red]➟ HE Grenade!" },
            { "#SFUI_TitlesTXT_Molotov_in_the_hole", "{name}[green]@{location}[red]➟ Molotov!" },
            { "#SFUI_TitlesTXT_Flashbang_in_the_hole", "{name}[green]@{location}[blue]➟ Flashbang!" },
            { "#SFUI_TitlesTXT_Incendiary_in_the_hole", "{name}[green]@{location}[red]➟ Incendiary!" },
            { "#SFUI_TitlesTXT_Smoke_in_the_hole", "{name}[green]@{location}[grey]➟ Smooke!" },
            { "#SFUI_TitlesTXT_Decoy_in_the_hole", "{name}[green]@{location}[/]➟ Decoy!" }
        };
        [JsonPropertyName("players")]
        [JsonPropertyOrder(1)]
        public Dictionary<string, PlayerChatConfig> Players = new()
        {
            {"76561198101370503", new PlayerChatConfig
            {
                NameColor = "[purple]",
                MessageColor = "[red]"
            }
            }
        };
    }

    public class PlayerChatConfig
    {
        [JsonPropertyName("name_color")]
        [JsonPropertyOrder(-1)]
        public required string NameColor { get; set; }
        [JsonPropertyName("message_color")]
        [JsonPropertyOrder(0)]
        public required string MessageColor { get; set; }
    }
}
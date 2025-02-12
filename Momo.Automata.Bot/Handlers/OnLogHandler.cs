using Discord;
using Discord.WebSocket;
using Momo.Automata.Bot.Utils;
using Momo.Automata.Bot.Utils.DiscordMessageMaker;
using Momo.Automata.Bot.Extensions;

namespace Momo.Automata.Bot.Handlers;

public static class OnLogHandler {
    private static readonly ILogger Logger = LogHelper.CreateLogger(typeof(OnLogHandler));

    public static async Task OnLogAsync(DiscordSocketClient client, LogMessage message) {
        if (
            message.Exception is not null &&
            // Discord bot auto reconnect
            message.Exception is not GatewayReconnectException &&
            // Discord timeout
            message.Exception.Message != "Cannot respond to an interaction after 3 seconds!" &&
            message.Exception.Message != "WebSocket connection was closed"
        ) {
            await client.SendMessageInAdminAlertChannel(embed: DiscordMessageMakerForError.MakeErrorFromLog(message));
        }

        Logger.Log(
            message.Severity.ToLogLevel(),
            message.Exception,
            "{Source}: {Message}",
            message.Source,
            message.Message
        );
    }
}
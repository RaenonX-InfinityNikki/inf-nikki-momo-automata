using Discord;
using Discord.WebSocket;
using Momo.Automata.Bot.Controllers.Mongo;
using Momo.Automata.Bot.Utils;
using Momo.Automata.Bot.Utils.DiscordMessageMaker;
using Momo.Automata.Bot.Extensions;

namespace Momo.Automata.Bot.Handlers.EventHandlers;

public static class GuildMemberLeftEventHandler {
    private static readonly ILogger Logger = LogHelper.CreateLogger(typeof(GuildMemberLeftEventHandler));

    public static async Task OnEvent(IDiscordClient client, SocketUser user) {
        Logger.LogInformation(
            "User {UserId} (@{Username}) left the server, removing associated activation if any",
            user.Id,
            user.Username
        );
        var subscriptionDuration = await ActivationController.RemoveDiscordActivationAndGetSubscriptionDuration(
            user.Id.ToString()
        );

        if (subscriptionDuration is not null) {
            await client.SendMessageInAdminAlertChannel(
                embed: await DiscordMessageMakerForActivation.MakeUserUnsubscribed(user, subscriptionDuration)
            );
        }
    }
}
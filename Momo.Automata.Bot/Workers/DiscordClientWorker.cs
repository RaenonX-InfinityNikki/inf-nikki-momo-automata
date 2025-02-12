using Discord;
using Discord.WebSocket;
using Momo.Automata.Bot.Handlers;
using Momo.Automata.Bot.Utils;
using Momo.Automata.Bot.Extensions;

namespace Momo.Automata.Bot.Workers;

public class DiscordClientWorker(IServiceProvider services, DiscordSocketClient client) : BackgroundService {
    private async Task SendTestMessageMasterAlert() {
        var message = await client.SendMessageInAdminAlertChannel(
            "`SYSTEM` Admin alert sending test - Master Alert"
        );

        await message.AutoDeleteAfterSeconds(10);
    }

    private async Task SendTestMessageRoleRestrictedAlert() {
        var message = await client.SendMessageInRoleRestrictedChannel(
            "`SYSTEM` Admin alert sending test - Role Restricted"
        );

        await message.AutoDeleteAfterSeconds(10);
    }

    private Task SendTestMessage() {
        return Task.WhenAll(
            SendTestMessageMasterAlert(),
            SendTestMessageRoleRestrictedAlert()
        );
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
        client.Log += message => OnLogHandler.OnLogAsync(client, message);

        await services.GetRequiredService<InteractionHandler>().InitializeAsync();

        await client.LoginAsync(TokenType.Bot, ConfigHelper.GetDiscordToken());
        await client.StartAsync();

        await SendTestMessage();

        await Task.Delay(Timeout.Infinite, cancellationToken);
    }
}
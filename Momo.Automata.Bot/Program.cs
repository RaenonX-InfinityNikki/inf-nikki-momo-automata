using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Momo.Automata.Bot.Extensions;
using Momo.Automata.Bot.Handlers;
using Momo.Automata.Bot.Workers;
using Momo.Automata.Bot.Workers.ActivationChecker;
using Momo.Automata.Bot.Workers.ActivationChecker.Removal;

var socketConfig = new DiscordSocketConfig {
    GatewayIntents =
        GatewayIntents.All &
        ~GatewayIntents.GuildScheduledEvents &
        ~GatewayIntents.GuildInvites &
        ~GatewayIntents.GuildPresences,
    AlwaysDownloadUsers = true,
};

var builder = WebApplication.CreateBuilder(args)
    .BuildCommon();

builder.Services.ConfigureBackgroundServiceExceptionBehaviorToIgnore();
builder.Services.AddCorsFromConfig();
builder.Services
    .AddSingleton(socketConfig)
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
    .AddSingleton<InteractionHandler>()
    .AddHostedService<DiscordClientWorker>()
    .AddHostedService<ActivationCheckerWorker>()
    .AddHostedService<ActivationKeyRemovalWatcher>()
    .AddHostedService<ActivationDataRemovalWatcher>()
    .AddControllers();

var app = builder
    .Build()
    .InitLogging();

app.UseCors();
app.MapControllers();
await app.BootAsync();
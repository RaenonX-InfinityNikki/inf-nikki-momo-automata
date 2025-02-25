﻿using Discord.Interactions;
using JetBrains.Annotations;

namespace Momo.Automata.Bot.Modules.SlashCommands;

[Group("bot", "Commands for bot management or bot utilities.")]
public class BotSlashModule : InteractionModuleBase<SocketInteractionContext> {
    [SlashCommand("ping", "Pings the bot and returns its latency.")]
    [UsedImplicitly]
    public Task PingAsync() {
        return RespondAsync($"Bot Latency: {Context.Client.Latency} ms", ephemeral: true);
    }
}
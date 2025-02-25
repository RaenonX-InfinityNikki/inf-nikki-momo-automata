﻿using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Momo.Automata.Bot.Utils.DiscordMessageMaker;
using Momo.Automata.Bot.Extensions;
using JetBrains.Annotations;

namespace Momo.Automata.Bot.Modules.SlashCommands;

[Group("lottery", "Commands for doing lottery.")]
public class LotterySlashModule : InteractionModuleBase<SocketInteractionContext> {
    [SlashCommand("role", "Do a role-based member lottery.")]
    [DefaultMemberPermissions(GuildPermission.Administrator)]
    [UsedImplicitly]
    public Task RoleBasedLotteryAsync(
        [Summary("role", "Lottery target role.")] SocketRole role,
        [Summary("count", "Count of members to pull.")] int count
    ) {
        try {
            var targetRoleId = role.Id;

            var result = Context.Client.GetCurrentWorkingGuild()
                .Roles
                .Single(x => x.Id == targetRoleId)
                .Members
                .ToArray()
                .GetRandomElements(count)
                .ToArray();

            return RespondAsync(
                string.Join(" ", result.Select(x => MentionUtils.MentionUser(x.Id))),
                embed: DiscordMessageMakerForLottery.MakeLotteryResult(targetRoleId, count, result)
            );
        } catch (ArgumentException e) {
            return RespondAsync(e.Message, ephemeral: true);
        }
    }
}
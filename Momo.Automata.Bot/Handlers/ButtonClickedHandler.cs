using Discord.WebSocket;
using Momo.Automata.Bot.Controllers.Mongo;
using Momo.Automata.Bot.Models;

namespace Momo.Automata.Bot.Handlers;

public static class ButtonClickedHandler {
    public static async Task DisplayRoleButtonClicked(
        ButtonInteractionInfo info,
        SocketGuildUser user
    ) {
        await user.RemoveRolesAsync(
            DiscordTrackedRoleController
                .FindAllTrackedRoleIdsByRoleIds(user.Roles.Select(r => r.Id).ToArray())
                .Select(r => r.RoleId)
        );
        await user.AddRoleAsync(info.CustomId);
    }

    public static Task AddRoleButtonClicked(
        ButtonInteractionInfo info,
        SocketGuildUser user
    ) {
        return user.AddRoleAsync(info.CustomId);
    }

    public static Task RemoveRoleButtonClicked(
        ButtonInteractionInfo info,
        SocketGuildUser user
    ) {
        return user.RemoveRoleAsync(info.CustomId);
    }
}
using Discord.WebSocket;

namespace Momo.Automata.Bot.Models.ActivationChecker;

public record ActivationCheckerExternalActivation {
    public required ActivationPropertiesModel ActivationProperties { get; init; }

    public required SocketGuildUser DiscordUser { get; init; }
}
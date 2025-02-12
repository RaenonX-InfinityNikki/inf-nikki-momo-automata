using JetBrains.Annotations;

namespace Momo.Automata.Bot.Models;

public record DiscordActivationMessageModel {
    [UsedImplicitly]
    public required string UserId { get; init; }

    [UsedImplicitly]
    public required string Link { get; init; }
}
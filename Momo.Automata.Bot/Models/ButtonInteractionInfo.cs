using Momo.Automata.Bot.Enums;

namespace Momo.Automata.Bot.Models;

public record ButtonInteractionInfo {
    public required ButtonId ButtonId { get; init; }
    public required ulong CustomId { get; init; }
}
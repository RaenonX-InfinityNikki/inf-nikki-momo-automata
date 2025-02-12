using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Momo.Automata.Bot.Models;

[BsonIgnoreExtraElements]
public record ActivationContactModel {
    [UsedImplicitly]
    public required string? Discord { get; init; }
}
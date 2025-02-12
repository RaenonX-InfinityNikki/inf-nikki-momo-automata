using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Momo.Automata.Bot.Models;

// To ignore `_id`
[BsonIgnoreExtraElements]
public record ActivationKeyModel : ActivationPropertiesModel {
    [UsedImplicitly]
    public required string Key { get; init; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [UsedImplicitly]
    public required DateTime GeneratedAt { get; init; }
}
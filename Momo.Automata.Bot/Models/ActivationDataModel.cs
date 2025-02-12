using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Momo.Automata.Bot.Models;

// To ignore `_id`
[BsonIgnoreExtraElements]
public record ActivationDataModel : ActivationKeyModel {
    [UsedImplicitly]
    public required ObjectId UserId { get; init; }
}
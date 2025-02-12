using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Momo.Automata.Bot.Models;

[BsonIgnoreExtraElements]
public record RoleRecordModel {
    [UsedImplicitly]
    public required ulong UserId { get; init; }

    [UsedImplicitly]
    public required ulong[] Roles { get; init; }
}
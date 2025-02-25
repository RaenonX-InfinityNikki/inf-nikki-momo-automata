﻿using JetBrains.Annotations;

namespace Momo.Automata.Bot.Models;

public record SubscribedUserModel {
    [UsedImplicitly]
    public required string RoleId { get; init; }

    [UsedImplicitly]
    public required string UserId { get; init; }

    [UsedImplicitly]
    public required ushort Discriminator { get; init; }

    [UsedImplicitly]
    public required string Username { get; init; }
}
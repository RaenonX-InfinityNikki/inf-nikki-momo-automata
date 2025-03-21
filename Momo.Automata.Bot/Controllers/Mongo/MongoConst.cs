﻿using Momo.Automata.Bot.Extensions;
using Momo.Automata.Bot.Models;
using Momo.Automata.Bot.Utils;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Controllers.Mongo;

public static class MongoConst {
    public static readonly string Url = ConfigHelper.GetMongoDbUrl();

    public static readonly IMongoClient Client = new MongoClient(Url).Initialize();

    private static readonly IMongoDatabase AuthDatabase = Client.GetDatabase("auth");

    public static readonly IMongoCollection<ActivationDataModel>
        AuthActivationDataCollection =
            AuthDatabase.GetCollection<ActivationDataModel>("activation");

    public static readonly IMongoCollection<ActivationKeyModel>
        AuthActivationKeyCollection =
            AuthDatabase.GetCollection<ActivationKeyModel>("activationKey");

    public static readonly IMongoCollection<ActivationPresetModel>
        AuthActivationPresetCollection =
            AuthDatabase.GetCollection<ActivationPresetModel>("activationPreset");

    private static readonly IMongoDatabase DiscordDatabase = Client.GetDatabase("discord");

    public static readonly IMongoCollection<RoleRecordModel>
        DiscordRoleRecordCollection =
            DiscordDatabase.GetCollection<RoleRecordModel>("role/record");

    public static readonly IMongoCollection<TrackedRoleModel>
        DiscordTrackedRoleCollection =
            DiscordDatabase.GetCollection<TrackedRoleModel>("role/tracked");

    public static readonly IMongoCollection<RoleRestrictionModel>
        DiscordRestrictedRoleCollection =
            DiscordDatabase.GetCollection<RoleRestrictionModel>("role/restricted");
}
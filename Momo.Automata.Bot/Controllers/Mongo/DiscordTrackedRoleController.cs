using Discord;
using Momo.Automata.Bot.Models;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Controllers.Mongo;

public static class DiscordTrackedRoleController {
    public static Task SaveTrackedRole(IRole role) {
        return MongoConst.DiscordTrackedRoleCollection
            .UpdateOneAsync(
                Builders<TrackedRoleModel>.Filter.Where(x => x.RoleId == role.Id),
                Builders<TrackedRoleModel>.Update.Set(x => x.RoleId, role.Id),
                new UpdateOptions { IsUpsert = true }
            );
    }

    public static Task RemoveTrackedRole(ulong roleId) {
        return MongoConst.DiscordTrackedRoleCollection
            .DeleteOneAsync(
                Builders<TrackedRoleModel>.Filter.Where(x => x.RoleId == roleId)
            );
    }

    public static TrackedRoleModel[] FindAllTrackedRoles() {
        return MongoConst.DiscordTrackedRoleCollection
            .Find(FilterDefinition<TrackedRoleModel>.Empty)
            .ToEnumerable()
            .ToArray();
    }

    public static TrackedRoleModel[] FindAllTrackedRoleIdsByRoleIds(ulong[] roleIds) {
        return MongoConst.DiscordTrackedRoleCollection
            .Find(Builders<TrackedRoleModel>.Filter.In(x => x.RoleId, roleIds))
            .ToEnumerable()
            .ToArray();
    }
}
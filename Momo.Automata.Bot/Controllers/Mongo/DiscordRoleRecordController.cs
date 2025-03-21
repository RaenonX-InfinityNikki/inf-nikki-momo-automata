using Momo.Automata.Bot.Models;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Controllers.Mongo;

public static class DiscordRoleRecordController {
    public static Task RemoveRoles(ulong userId, ulong[] roles) {
        return MongoConst.DiscordRoleRecordCollection.UpdateOneAsync(
            Builders<RoleRecordModel>.Filter.Where(x => x.UserId == userId),
            Builders<RoleRecordModel>.Update.PullAll(x => x.Roles, roles)
        );
    }

    public static Task BulkRemoveRoles(ulong[] userIds, ulong[] roles) {
        return MongoConst.DiscordRoleRecordCollection.UpdateManyAsync(
            Builders<RoleRecordModel>.Filter.In(x => x.UserId, userIds),
            Builders<RoleRecordModel>.Update.PullAll(x => x.Roles, roles)
        );
    }

    public static Task AddRoles(ulong userId, ulong[] roles) {
        return MongoConst.DiscordRoleRecordCollection.UpdateOneAsync(
            Builders<RoleRecordModel>.Filter.Where(x => x.UserId == userId),
            Builders<RoleRecordModel>.Update.AddToSetEach(x => x.Roles, roles),
            new UpdateOptions { IsUpsert = true }
        );
    }

    public static Task BulkAddRoles(ulong[] userIds, ulong[] roles) {
        if (userIds.Length == 0) {
            // Nothing to do if `userIds` is empty
            return Task.CompletedTask;
        }

        return MongoConst.DiscordRoleRecordCollection.BulkWriteAsync(
            userIds
                .Select(
                    userId => new UpdateOneModel<RoleRecordModel>(
                        Builders<RoleRecordModel>.Filter.Where(r => r.UserId == userId),
                        Builders<RoleRecordModel>.Update.AddToSetEach(r => r.Roles, roles)
                    ) {
                        IsUpsert = true,
                    }
                )
                .ToList()
        );
    }

    public static ulong[] FindRoleIdsByUserId(ulong userId) {
        var result = MongoConst.DiscordRoleRecordCollection.Find(
            Builders<RoleRecordModel>.Filter.Where(x => x.UserId == userId)
        ).FirstOrDefault();

        return result?.Roles ?? [];
    }

    public static async Task<Dictionary<ulong, RoleRecordModel>> GetRoleRecordLookup(IEnumerable<ulong> userIds) {
        var result = await MongoConst.DiscordRoleRecordCollection.Find(
            Builders<RoleRecordModel>.Filter.In(x => x.UserId, userIds)
        ).ToListAsync();

        return result.ToDictionary(x => x.UserId);
    }
}
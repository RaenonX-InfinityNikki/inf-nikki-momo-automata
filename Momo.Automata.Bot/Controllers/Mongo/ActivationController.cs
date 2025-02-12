using Momo.Automata.Bot.Models;
using Momo.Automata.Bot.Utils;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Controllers.Mongo;

public static class ActivationController {
    public static async Task<TimeSpan?> RemoveDiscordActivationAndGetSubscriptionDuration(string userId) {
        var removeActivationKeyTask = MongoConst.AuthActivationKeyCollection.FindOneAndDeleteAsync(
            Builders<ActivationKeyModel>.Filter.Where(x => x.Contact.Discord == userId)
        );
        var removeActivationDataTask = MongoConst.AuthActivationDataCollection.FindOneAndDeleteAsync(
            Builders<ActivationDataModel>.Filter.Where(x => x.Contact.Discord == userId)
        );

        await Task.WhenAll(removeActivationKeyTask, removeActivationDataTask);

        var earliestGeneration = new[] {
            (await removeActivationKeyTask)?.GeneratedAt,
            (await removeActivationDataTask)?.GeneratedAt,
        }.Min();

        if (earliestGeneration is null) {
            return null;
        }

        return DateTime.UtcNow - earliestGeneration.Value;
    }

    public static async Task<ActivationPropertiesModel[]> GetExternalSubscribersWithDiscordContact() {
        var activationKeyList = await MongoConst.AuthActivationKeyCollection.Find(
            x =>
                (
                    x.Source == GlobalConst.SubscriptionSource.Github ||
                    x.Source == GlobalConst.SubscriptionSource.Patreon ||
                    x.Source == GlobalConst.SubscriptionSource.Afdian
                ) &&
                x.Contact.Discord != null
        ).ToListAsync();
        var activationDataList = await MongoConst.AuthActivationDataCollection.Find(
            x =>
                (
                    x.Source == GlobalConst.SubscriptionSource.Github ||
                    x.Source == GlobalConst.SubscriptionSource.Patreon ||
                    x.Source == GlobalConst.SubscriptionSource.Afdian
                ) &&
                x.Contact.Discord != null
        ).ToListAsync();

        return [..activationKeyList, ..activationDataList];
    }
}
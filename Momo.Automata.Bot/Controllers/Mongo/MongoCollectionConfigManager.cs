using MongoDB.Bson;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Controllers.Mongo;

public static class MongoCollectionConfigManager {
    public static void EnableChangeStreamPreAndPostImagesOnCollection<T>(IMongoCollection<T> collection) {
        collection.Database.RunCommand(
            new JsonCommand<BsonDocument>(
                $"{{" +
                $"  collMod: \"{collection.CollectionNamespace.CollectionName}\"" +
                $"  changeStreamPreAndPostImages: {{enabled: true}}" +
                $"}}"
            )
        );
    }
}
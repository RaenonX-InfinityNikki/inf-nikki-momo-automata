using Discord.WebSocket;
using Momo.Automata.Bot.Controllers.Mongo;
using Momo.Automata.Bot.Models;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Workers.ActivationChecker.Removal;

public class ActivationDataRemovalWatcher(
    DiscordSocketClient client,
    ILogger<ActivationDataRemovalWatcher> logger
) : ActivationRemovalWatcher<ActivationDataModel>(client, logger) {
    protected override IMongoCollection<ActivationDataModel> GetMongoCollection() {
        return MongoConst.AuthActivationDataCollection;
    }
}
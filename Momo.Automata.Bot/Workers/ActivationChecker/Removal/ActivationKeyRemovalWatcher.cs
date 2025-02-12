using Discord.WebSocket;
using Momo.Automata.Bot.Controllers.Mongo;
using Momo.Automata.Bot.Models;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Workers.ActivationChecker.Removal;

public class ActivationKeyRemovalWatcher(
    DiscordSocketClient client,
    ILogger<ActivationKeyRemovalWatcher> logger
) : ActivationRemovalWatcher<ActivationKeyModel>(client, logger) {
    protected override IMongoCollection<ActivationKeyModel> GetMongoCollection() {
        return MongoConst.AuthActivationKeyCollection;
    }
}
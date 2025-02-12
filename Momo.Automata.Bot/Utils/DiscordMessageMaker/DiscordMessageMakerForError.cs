using Discord;
using Discord.Interactions;
using Discord.Net;
using Momo.Automata.Bot.Enums;
using IResult = Discord.Interactions.IResult;

namespace Momo.Automata.Bot.Utils.DiscordMessageMaker;

public static class DiscordMessageMakerForError {
    public static Embed MakeError(IResult result) {
        return new EmbedBuilder()
            .WithColor(Colors.Danger)
            .WithTitle($"Error - {result.Error}")
            .WithDescription(result.ErrorReason)
            .WithCurrentTimestamp()
            .Build();
    }
    
    public static Embed MakeError(ExecuteResult result) {
        var builder = new EmbedBuilder()
            .WithColor(Colors.Danger)
            .WithTitle($"Error - {result.Error}")
            .WithDescription(result.ErrorReason);

        var actualExceptionMessage = result.Exception.InnerException?.Message;
        if (actualExceptionMessage is not null) {
            builder.AddField("Actual Error", actualExceptionMessage);
        }

        return builder.WithCurrentTimestamp().Build();
    }

    public static Embed MakeErrorFromLog(LogMessage message) {
        return new EmbedBuilder()
            .WithColor(Colors.Warning)
            .WithTitle($"{message.Source}: {message.Message}")
            .WithDescription($"```{message.Exception}```")
            .WithCurrentTimestamp()
            .Build();
    }

    public static Embed MakeGeneralException(Exception e) {
        return new EmbedBuilder()
            .WithColor(Colors.Warning)
            .WithTitle($"{e.Source}: {e.Message}")
            .WithDescription($"```{e.StackTrace}```")
            .WithCurrentTimestamp()
            .Build();
    }

    public static Embed MakeDiscordHttpException(HttpException e) {
        return new EmbedBuilder()
            .WithColor(Colors.Warning)
            .WithTitle($"{e.Source}: {e.Message} ({e.DiscordCode})")
            .WithDescription($"```{e.StackTrace}```")
            .WithCurrentTimestamp()
            .Build();
    }
}
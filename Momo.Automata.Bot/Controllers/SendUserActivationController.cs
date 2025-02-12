using Discord;
using Discord.Net;
using Discord.WebSocket;
using Momo.Automata.Bot.Extensions;
using Momo.Automata.Bot.Models;
using Momo.Automata.Bot.Utils;
using Momo.Automata.Bot.Utils.DiscordMessageMaker;
using Microsoft.AspNetCore.Mvc;

namespace Momo.Automata.Bot.Controllers;

[ApiController]
[Route("/send-activation")]
public class SendUserActivationController(
    ILogger<SubscribedUserController> logger,
    DiscordSocketClient client,
    IHostEnvironment env
) : ControllerBase {
    [HttpPost(Name = "SendActivationMessage")]
    public OkResult Post(DiscordActivationMessageModel[] activationMessages) {
        logger.LogInformation(
            "Received {Count} activation messages to send",
            activationMessages.Length
        );

        Task.WhenAll(
            activationMessages.Select(
                async x => {
                    var userId = Convert.ToUInt64(x.UserId);
                    var user = client.GetCurrentWorkingGuild().GetUser(userId);

                    if (user is null) {
                        logger.LogInformation(
                            "Failed to send activation message to {UserId} - User not found",
                            x.UserId
                        );
                        await client.SendMessageInAdminAlertChannel(
                            $"Failed to send activation message to {MentionUtils.MentionUser(userId)} - User not found"
                        );
                        return;
                    }

                    logger.LogInformation(
                        "Activation message sent to {UserId} with link {ActivationLink} - requested via API",
                        x.UserId,
                        x.Link
                    );
                    // Prevent accidentally sending messages to the users
                    if (env.IsProduction()) {
                        try {
                            await user.SendMessageAsync(
                                x.Link,
                                embeds: DiscordMessageMakerForActivation.MakeActivationNote()
                            );
                        } catch (HttpException e) {
                            await client.SendMessageInAdminAlertChannel(
                                $"Error occurred during activation link delivery to {MentionUtils.MentionUser(user.Id)} by API\n" +
                                $"> {x.Link}",
                                DiscordMessageMakerForError.MakeDiscordHttpException(e)
                            );
                        }
                    }

                    await Task.WhenAll(
                        DiscordSubscriberMarker.MarkUserSubscribed(user),
                        client.SendMessageInAdminAlertChannel(
                            $"Activation message sent to {MentionUtils.MentionUser(userId)} with [link]({x.Link}) - requested via API"
                        )
                    );
                }
            )
        );

        return Ok();
    }
}
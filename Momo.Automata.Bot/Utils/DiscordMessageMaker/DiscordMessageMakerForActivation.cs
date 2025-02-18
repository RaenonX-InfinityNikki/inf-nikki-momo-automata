using Discord;
using Momo.Automata.Bot.Enums;
using Momo.Automata.Bot.Models;
using Momo.Automata.Bot.Extensions;

namespace Momo.Automata.Bot.Utils.DiscordMessageMaker;

public static class DiscordMessageMakerForActivation {
    public static Embed[] MakeActivationNote() {
        return [
            new EmbedBuilder()
                .WithColor(Colors.Info)
                .WithTitle("English")
                .WithDescription(
                    """
                    Thanks for your support! Click on the link below to activate after logging in to the website.
                    - If it redirects to the homepage the link has been activated.
                    - The link is only valid for the first click. Subsequent clicks will show "Activation Failed."
                    - If the link keeps failing, please contact <@503484431437398016>.
                    - If the paid content suddenly becomes inaccessible please contact <@503484431437398016>.
                    """
                )
                .Build(),
            new EmbedBuilder()
                .WithColor(Colors.Warning)
                .WithTitle("简体中文")
                .WithDescription(
                    """
                    感谢支持！登入网站后，点击连结即可启用。
                    - 点击连结后，如果有跳转回首页则代表已启用。
                    - 连结只限点击一次。重複点击将会出现 "Activation Failed"。
                    - 如果无法启用，请联络 <@503484431437398016> 协助处理。
                    - 如果付费内容在订阅期间忽然失效的话，也请联络 <@503484431437398016>。
                    """
                )
                .Build(),
            new EmbedBuilder()
                .WithColor(Colors.Success)
                .WithTitle("繁體中文")
                .WithDescription(
                    """
                    感謝支持！登入網站後，點擊連結即可啟用。
                    - 點擊連結後，如果有跳轉回首頁則代表已啟用。
                    - 連結只限點擊一次。重複點擊將會出現 "Activation Failed"。
                    - 如果無法啟用，請聯絡 <@503484431437398016> 協助處理。
                    - 如果付費內容在訂閱期間忽然失效的話，也請聯絡 <@503484431437398016>。
                    """
                )
                .Build(),
        ];
    }

    public static Embed MakeUserDataNotCached(ulong userId, IUser updated) {
        return new EmbedBuilder()
            .WithColor(Colors.Warning)
            .WithAuthor(updated)
            .WithTitle("Member Update - User data not cached")
            .AddField("User", updated.Mention)
            .WithFooter($"ID: {userId}")
            .WithCurrentTimestamp()
            .Build();
    }

    public static Task<Embed> MakeUserSubscribed(IUser user, HashSet<ActivationPresetRole> roles) {
        return MakeUserSubscribed(user, roles, Colors.Success);
    }

    public static async Task<Embed> MakeUserSubscribed(
        IUser user,
        HashSet<ActivationPresetRole> roles,
        Color color,
        bool isExternal = false
    ) {
        if (!isExternal) {
            await DiscordSubscriberMarker.MarkUserSubscribed(user);
        }

        var builder = new EmbedBuilder()
            .WithColor(color)
            .WithAuthor(user)
            .WithTitle("Member Subscribed")
            .AddField("User", user.Mention)
            .AddField("External (Non-Discord)", isExternal)
            .WithFooter($"ID: {user.Id}")
            .WithCurrentTimestamp();

        foreach (var presetRole in roles) {
            var roleMention = MentionUtils.MentionRole(presetRole.RoleId);
            var suffix = presetRole.Suspended ? " (Suspended)" : "";

            builder = builder.AddField(
                "Role",
                $"{roleMention}{suffix}"
            );
        }

        return builder.Build();
    }

    public static async Task<Embed> MakeUserUnsubscribed(
        IUser user,
        TimeSpan? subscriptionDuration,
        IEnumerable<ulong>? roleIds = null,
        bool isExternal = false
    ) {
        var builder = new EmbedBuilder()
            .WithColor(Colors.Danger)
            .WithAuthor(user)
            .WithTitle("Member Unsubscribed")
            .AddField("User", user.Mention)
            .AddField(
                "Subscription Duration",
                subscriptionDuration.HasValue ? subscriptionDuration.Value.ToString("c") : "(N/A)"
            )
            .AddField("External (Non-Discord)", isExternal)
            .WithFooter($"ID: {user.Id}")
            .WithCurrentTimestamp();

        if (roleIds is null) {
            return builder.Build();
        }

        if (!isExternal) {
            // If `roleIds` is not null, it means that the user is still in the server.
            // Therefore, mark the user unsubscribed, which removes the role
            await DiscordSubscriberMarker.MarkUserUnsubscribed(user);
        }

        builder = builder.AddField("Role", roleIds.Select(MentionUtils.MentionRole).MergeToSameLine());

        return builder.Build();
    }
}
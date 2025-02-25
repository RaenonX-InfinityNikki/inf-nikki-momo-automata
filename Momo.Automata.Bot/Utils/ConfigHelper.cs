﻿using Momo.Automata.Bot.Extensions;

namespace Momo.Automata.Bot.Utils;

public static class ConfigHelper {
    private static IConfiguration? _config;

    private static IConfiguration Config => _config ?? throw new InvalidOperationException("Config not initialized");

    public static void Initialize(IConfiguration? configuration) {
        _config = configuration;
    }

    private static IConfigurationSection GetDiscordSection() {
        return Config.GetRequiredSection("Discord");
    }

    public static string GetDiscordToken() {
        return GetDiscordSection().GetRequiredValue<string>("Token");
    }

    public static ulong GetDiscordWorkingGuild() {
        return GetDiscordSection().GetRequiredValue<ulong>("Guild");
    }

    private static IConfigurationSection GetDiscordChannelSection() {
        return GetDiscordSection().GetRequiredSection("Channels");
    }

    public static ulong GetDiscordAdminAlertChannelId() {
        return GetDiscordChannelSection().GetRequiredValue<ulong>("AdminAlert");
    }

    public static ulong GetDiscordRoleRestrictedNotificationChannelId() {
        return GetDiscordChannelSection().GetRequiredValue<ulong>("RoleRestricted");
    }

    private static IConfigurationSection GetDiscordRolesSection() {
        return GetDiscordSection().GetRequiredSection("Roles");
    }

    public static ulong GetDiscordSubscriberRoleId() {
        return GetDiscordRolesSection().GetRequiredValue<ulong>("Subscriber");
    }

    private static IConfigurationSection GetRoleRestrictionSection() {
        return GetDiscordRolesSection().GetRequiredSection("Restrict");
    }

    public static ulong GetRoleRestrictionOwnerUserId() {
        return GetRoleRestrictionSection().GetRequiredValue<ulong>("OwnerUserIdForPing");
    }

    private static IConfigurationSection GetApiSection() {
        return Config.GetRequiredSection("Api");
    }

    private static IConfigurationSection GetInternalApiSection() {
        return GetApiSection().GetRequiredSection("Internal");
    }

    private static IConfigurationSection GetInternalApiEndpoints() {
        return GetInternalApiSection().GetRequiredSection("Endpoints");
    }

    public static string GetInternalApiGenerateActivation() {
        return GetInternalApiEndpoints().GetRequiredValue<string>("GenerateActivation");
    }

    private static IConfigurationSection GetInternalApiTokenSection() {
        return GetInternalApiSection().GetRequiredSection("Token");
    }

    public static string GetInternalInboundApiToken() {
        return GetInternalApiTokenSection().GetRequiredValue<string>("Inbound");
    }

    public static string GetInternalOutboundApiToken() {
        return GetInternalApiTokenSection().GetRequiredValue<string>("Outbound");
    }

    public static string GetAllowedOrigin() {
        return Config.GetRequiredValue<string>("AllowedOrigin");
    }

    public static string GetMongoDbUrl() {
        return Config.GetRequiredSection("Mongo").GetRequiredValue<string>("Url");
    }
}
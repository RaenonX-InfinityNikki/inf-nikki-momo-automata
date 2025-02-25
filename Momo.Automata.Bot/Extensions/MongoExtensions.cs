﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Momo.Automata.Bot.Extensions;

public static class MongoExtensions {
    public static IMongoClient Initialize(this IMongoClient client) {
        RegisterConvention();
        RegisterSerializer();

        return client;
    }

    private static void RegisterConvention() {
        ConventionRegistry.Register(
            "CamelCaseConvention",
            new ConventionPack { new CamelCaseElementNameConvention() },
            _ => true
        );
    }

    private static void RegisterSerializer() {
        RegisterGlobalSerializer();
    }

    private static void RegisterGlobalSerializer() {
        // By default, `decimal` are stored in `string`, which is undesired
        BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
    }
}
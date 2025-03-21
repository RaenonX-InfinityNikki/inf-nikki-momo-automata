﻿using System.Security.Cryptography;
using System.Text;
using Momo.Automata.Bot.Enums;
using Momo.Automata.Bot.Utils;

namespace Momo.Automata.Bot.Extensions;

public static class StringExtensions {
    private static T? EnumToString<T>(this string value) where T : struct, Enum {
        var converted = Enum.TryParse(value, out T outSummaryKey);

        if (!converted) {
            return null;
        }

        return outSummaryKey;
    }

    public static ModalId? ToModalId(this string value) {
        return EnumToString<ModalId>(value);
    }

    public static ModalFieldId? ToModalFieldId(this string value) {
        return EnumToString<ModalFieldId>(value);
    }

    public static string MergeToSameLine(this IEnumerable<string> lines) {
        return string.Join(" / ", lines);
    }

    public static string MergeToSameLine(this IEnumerable<ulong> lines) {
        return string.Join(" / ", lines);
    }

    public static string MergeLines(this IEnumerable<string> lines) {
        return StringHelper.MergeLines(lines.ToArray());
    }

    public static string ToSha256Hash(this string value) {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        var builder = new StringBuilder();

        foreach (var b in hashBytes) {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public static int ToInt(this string str, int defaultValue = 0) {
        return int.TryParse(str, out var val) ? val : defaultValue;
    }
}
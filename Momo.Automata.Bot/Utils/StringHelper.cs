﻿namespace Momo.Automata.Bot.Utils;

public static class StringHelper {
    public static string MergeLines(params string[] lines) {
        return string.Join("\n", lines);
    }
}
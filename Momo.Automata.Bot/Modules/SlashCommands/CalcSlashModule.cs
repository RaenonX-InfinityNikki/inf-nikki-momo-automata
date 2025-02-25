﻿using Discord.Interactions;
using Eval.net;
using JetBrains.Annotations;

namespace Momo.Automata.Bot.Modules.SlashCommands;

[Group("calc", "Commands for calculating things.")]
public class CalcSlashModule : InteractionModuleBase<SocketInteractionContext> {
    [SlashCommand("math", "Calculates math expression using Eval.NET.")]
    [UsedImplicitly]
    public Task MathCalcAsync([Summary(description: "Math expression to evaluate.")] string expression) {
        return RespondAsync(
            $"Result: **{Evaluator.Execute(expression, EvalConfiguration.DecimalConfiguration)}**\n" +
            $"> Evaluated expression: `{expression}`"
        );
    }
}
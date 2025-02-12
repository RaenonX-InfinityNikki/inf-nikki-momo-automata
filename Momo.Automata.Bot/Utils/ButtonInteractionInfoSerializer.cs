using System.Text.Json;
using Momo.Automata.Bot.Models;

namespace Momo.Automata.Bot.Utils;

public static class ButtonInteractionInfoSerializer {
    public static string Serialize(ButtonInteractionInfo info) {
        return JsonSerializer.Serialize(info);
    }

    public static ButtonInteractionInfo? Deserialize(string json) {
        return JsonSerializer.Deserialize<ButtonInteractionInfo>(json);
    }
}
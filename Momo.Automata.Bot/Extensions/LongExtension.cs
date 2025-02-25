namespace Momo.Automata.Bot.Extensions;

public static class LongExtension {
    public static DateTime ToDateTimeFromSecond(this long value) {
        return DateTimeOffset.FromUnixTimeSeconds(value).UtcDateTime;
    }
}
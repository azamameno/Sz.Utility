namespace System.Collections.Generic;

public static class DictionaryExtension {
    public static T GetValue<T>(this Dictionary<string, T> dict, string? key, T defaultValue) {
        return !string.IsNullOrWhiteSpace(key) ? dict.GetValueOrDefault(key, defaultValue) : defaultValue;
    }

    public static T? GetValue<T>(this Dictionary<string, T> dict, string? key) {
        return !string.IsNullOrWhiteSpace(key) ? dict.GetValueOrDefault(key) : default;
    }

    public static TResult? GetValue<T, TResult>(this Dictionary<string, T> dict, string? key, Func<T, TResult> func) {
        if (string.IsNullOrWhiteSpace(key))
            return default;
        return dict.TryGetValue(key, out T? value) && value != null ? func(value) : default;
    }

    public static TResult GetValue<T, TResult>(this Dictionary<string, T> dict, string? key, Func<T, TResult> func, TResult defaultValue) {
        if (string.IsNullOrWhiteSpace(key))
            return defaultValue;
        return dict.TryGetValue(key, out T? value) && value != null ? func(value) : defaultValue;
    }

    public static string GetValue(this Dictionary<string, string> dict, string? key) {
        return dict.GetValue(key, string.Empty);
    }

    public static string GetValue<T>(this Dictionary<string, T> dict, string? key, Func<T, string> func) {
        return dict.GetValue<T, string>(key, func, string.Empty);
    }
}

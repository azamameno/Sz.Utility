using System.ComponentModel;

namespace SzUtility.Extensions;

public static class EnumExtension {
    public static bool TryGet<TAttribute>(this Enum value, out TAttribute? attr)
        where TAttribute : Attribute {
        var field = value.GetType().GetField(value.ToString()) ?? throw new InvalidEnumArgumentException(nameof(value));
        var customAttr = Attribute.GetCustomAttribute(field, typeof(TAttribute));
        if (customAttr is TAttribute descAttr) {
            attr = descAttr;
            return true;
        }

        attr = null;
        return false;
    }

    public static TAttribute? GetValue<TAttribute>(this Enum value)
        where TAttribute : Attribute {
        return value.TryGet(out TAttribute? attr) && attr != null ? attr : null;
    }

    public static TResult? GetValue<TAttribute, TResult>(this Enum value, Func<TAttribute, TResult> selector)
        where TAttribute : Attribute {
        return value.TryGet(out TAttribute? attr) && attr != null ? selector(attr) : default;
    }

    public static TResult GetValue<TAttribute, TResult>(this Enum value, Func<TAttribute, TResult> selector, TResult defaultValue)
        where TAttribute : Attribute {
        return value.TryGet(out TAttribute? attr) && attr != null ? selector(attr) : defaultValue;
    }

    public static string Description(this Enum value) {
        return value.GetValue<DescriptionAttribute, string>(o => o.Description) ?? string.Empty;
    }
}

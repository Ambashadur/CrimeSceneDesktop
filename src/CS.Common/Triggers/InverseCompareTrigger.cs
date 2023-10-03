using System.Globalization;
using Microsoft.Maui.Platform;

namespace CS.Common.Triggers;

public sealed class InverseCompareTrigger : StateTriggerBase
{
    public InverseCompareTrigger() {
        UpdateState();
    }

    public object Property {
        get => GetValue(PropertyProperty);
        set => SetValue(PropertyProperty, value);
    }

    public static readonly BindableProperty PropertyProperty =
        BindableProperty.Create(nameof(Property), typeof(object), typeof(InverseCompareTrigger), null,
            propertyChanged: OnPropertyChanged);

    private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue) {
        ((InverseCompareTrigger)bindable).UpdateState();
    }

    public object Value {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(object), typeof(InverseCompareTrigger), null,
            propertyChanged: OnValueChanged);

    static void OnValueChanged(BindableObject bindable, object oldvalue, object newvalue) {
        ((InverseCompareTrigger)bindable).UpdateState();
    }

    protected override void OnAttached() {
        base.OnAttached();
        UpdateState();
    }

    private void UpdateState() {
        SetActive(!AreEqual(Property, Value));
    }

    private bool AreEqual(object value1, object value2) {
        if (value1 == value2) return true;
        if (value1 != null && value2 != null) return AreEqualType(value1, value2) || AreEqualType(value2, value1);

        return false;
    }

    private bool AreEqualType(object value1, object value2) {
        if (value2 is Enum)
            value1 = ConvertToEnum(value2.GetType(), value1);
        else
            value1 = Convert.ChangeType(value1, value2.GetType(), CultureInfo.InvariantCulture);

        return value2.Equals(value1);
    }

    private object ConvertToEnum(Type enumType, object value) {
        try {
            return Enum.IsDefined(enumType, value) ? Enum.ToObject(enumType, value) : null;
        } catch {
            return null;
        }
    }
}

using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(IntDropdownAttribute))]
public class IntDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use IntDropdown with int only");
            return;
        }

        var attr = (IntDropdownAttribute)attribute;
        var entries = AttributeRegistry.GetEntries(attr.providerName);

        if (entries == null || entries.Count == 0)
        {
            EditorGUI.LabelField(position, label.text, $"No entries for '{attr.providerName}'");
            return;
        }

        var options = entries ?? new List<DropdownEntry>();
        var labels = options.Select(o => o.label).ToArray();
        var values = options.Select(o => o.value).ToArray();

        int index = System.Array.IndexOf(values, property.intValue);
        if (index < 0 || index >= values.Length) index = 0;

        int newIndex = EditorGUI.Popup(position, label.text, index, labels);
        property.intValue = values[newIndex];
    }
}

using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(VisibilityDropdownAttribute))]
public class VisibilityDropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use VisibilityDropdown with int only");
            return;
        }

        var attr = (VisibilityDropdownAttribute)attribute;
        var entries = AttributeRegistry.GetEntries(attr.providerName);

        if (entries == null || entries.Count == 0)
        {
            EditorGUI.LabelField(position, label.text, $"No entries for '{attr.providerName}'");
            return;
        }

        var options = entries ?? new List<DropdownEntry>();
        var labels = options.Select(o => o.label).ToArray();
        int mask = property.intValue;
        mask = EditorGUILayout.MaskField("Visible states", mask, labels);
        property.intValue = mask;
    }
}

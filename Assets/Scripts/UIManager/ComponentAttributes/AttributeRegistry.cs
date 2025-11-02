using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeRegistryType
{
    [Serializable]
    public class DropdownEntry
    {
        public string label;
        public int value;
    }

    public List<DropdownEntry> PanelHolderTypes;
    public List<DropdownEntry> PanelAnimatorTypes;
    public List<DropdownEntry> WidgetLayoutStates;
    public List<DropdownEntry> WidgetAnimationTypes;
}

public static class AttributeRegistry
{
    private static AttributeRegistryType data;

    private const string DefaultFilePath = "Assets/Scripts/UIManagerRegistry.json";

    public static void Reload()
    {
        data = new AttributeRegistryType();

#if UNITY_EDITOR
        try
        {
            string json = File.ReadAllText(DefaultFilePath);
            data = JsonUtility.FromJson<AttributeRegistryType>(json);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to read JSON '{DefaultFilePath}': {e.Message}");
        }

#endif
    }

    public static List<AttributeRegistryType.DropdownEntry> GetEntries(string providerName)
    {
        if (data == null)
            Reload();

        if (providerName == "PanelHolderTypes")
        {
            return data.PanelHolderTypes;
        }
        else if (providerName == "PanelAnimatorTypes")
        {
            return data.PanelAnimatorTypes;
        }
        else if (providerName == "WidgetLayoutStates")
        {
            return data.WidgetLayoutStates;
        }
        else if (providerName == "WidgetAnimationTypes")
        {
            return data.WidgetAnimationTypes;
        }
        
        return null;
    }
}

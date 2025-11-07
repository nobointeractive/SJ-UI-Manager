using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DropdownEntry
    {
        public string label;
        public int value;
    }

public static class AttributeRegistry
{
    private static UIConfiguration data;

    private const string DefaultFilePath = "Assets/Resources/GameUIConfiguration.asset";

    private static List<DropdownEntry> cachedPanelHolderTypes = null;
    private static List<DropdownEntry> cachedFlyerHolderTypes = null;
    private static List<DropdownEntry> cachedWidgetHolderTypes = null;
    private static List<DropdownEntry> cachedWidgetLayoutStates = null;

    public static void Reload()
    {
        data = new UIConfiguration();

#if UNITY_EDITOR
        try
        {
            // read scriptable object
            var asset = AssetDatabase.LoadAssetAtPath<UIConfiguration>(DefaultFilePath);
            if (asset != null)
            {
                data = asset;

                // clear caches
                cachedPanelHolderTypes = null;
                cachedFlyerHolderTypes = null;
                cachedWidgetHolderTypes = null;
                cachedWidgetLayoutStates = null;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to read JSON '{DefaultFilePath}': {e.Message}");
        }

#endif
    }

    public static List<DropdownEntry> GetEntries(string providerName)
    {
        if (data == null)
            Reload();

        if (providerName == "PanelHolderTypes")
        {
            if (cachedPanelHolderTypes == null)
            {
                cachedPanelHolderTypes = data.PanelHolders.ConvertAll(holder => new DropdownEntry { label = holder.name, value = data.PanelHolders.IndexOf(holder) });
            }
            return cachedPanelHolderTypes;
        }
        else if (providerName == "FlyerHolderTypes")
        {
            if (cachedFlyerHolderTypes == null)
            {
                cachedFlyerHolderTypes = data.FlyerHolders.ConvertAll(holder => new DropdownEntry { label = holder.name, value = data.FlyerHolders.IndexOf(holder) });
            }
            return cachedFlyerHolderTypes;
        }
        else if (providerName == "WidgetHolderTypes")
        {
            if (cachedWidgetHolderTypes == null)
            {
                cachedWidgetHolderTypes = data.WidgetHolders.ConvertAll(holder => new DropdownEntry { label = holder.name, value = data.WidgetHolders.IndexOf(holder) });
            }
            return cachedWidgetHolderTypes;
        }
        else if (providerName == "WidgetLayoutStates")
        {
            if (cachedWidgetLayoutStates == null)
            {
                cachedWidgetLayoutStates = new List<DropdownEntry>();
                var states = data.WidgetLayoutStates;
                for (int i = 0; i < states.Length; i++)
                {
                    cachedWidgetLayoutStates.Add(new DropdownEntry { label = states[i], value = 1 << i });
                }
            }
            return cachedWidgetLayoutStates;
        }     

        return null;
    }
}

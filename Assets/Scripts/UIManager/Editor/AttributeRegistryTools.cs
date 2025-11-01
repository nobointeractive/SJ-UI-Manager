#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class IntDropdownMenu
{
    [MenuItem("Window/UIManager/Reload Attribute Registry")]    
    public static void ReloadAttributeRegistry()
    {
        AttributeRegistry.Reload();
        EditorApplication.RepaintProjectWindow();
        EditorApplication.RepaintHierarchyWindow();
    }
}
#endif

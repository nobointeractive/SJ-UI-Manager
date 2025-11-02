using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class VisibilityDropdownAttribute : PropertyAttribute
{
    public string providerName;
    public VisibilityDropdownAttribute(string providerName = null)
    {
        this.providerName = providerName;
    }
}

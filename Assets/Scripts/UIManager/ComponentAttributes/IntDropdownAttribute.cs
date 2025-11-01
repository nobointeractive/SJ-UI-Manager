using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class IntDropdownAttribute : PropertyAttribute
{
    public string providerName;
    public IntDropdownAttribute(string providerName = null)
    {
        this.providerName = providerName;
    }
}

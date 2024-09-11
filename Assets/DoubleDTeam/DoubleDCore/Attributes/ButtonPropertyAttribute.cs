using UnityEngine;

namespace DoubleDCore.Attributes
{
    public class ButtonPropertyAttribute : PropertyAttribute
    {
        public string MethodName { get; }

        public ButtonPropertyAttribute(string methodName)
        {
            MethodName = methodName;
        }
    }
}
using System;
using System.Reflection;

namespace FiberCore.Common
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FieldData : Attribute
    {
        public string Tooltip     { get; private set; }
        public string Description { get; private set; }


        public FieldData(string description, string tooltip)
        {
            Description = description;
            Tooltip     = tooltip;
        }

        public static string GetValue(string objectName, Type classType, ValueType valueType)
        {
            #if UNITY_EDITOR
            var fieldInfo = classType.GetField(objectName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            var value = "";

            switch (valueType)
            {
                case ValueType.Description:
                    value = fieldInfo.GetCustomAttribute<FieldData>().Description;
                    break;
                case ValueType.Tooltip:
                    value = fieldInfo.GetCustomAttribute<FieldData>().Tooltip;
                    break;

            }

            return value;
            #else
            return null;
            #endif
        }

        public enum ValueType
        {
            Description,
            Tooltip,
        }
    }

    
}
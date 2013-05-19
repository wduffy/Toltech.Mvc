using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
    public class DisplayNameResourceAttribute : DisplayNameAttribute
    {
        private readonly PropertyInfo nameProperty;

        public DisplayNameResourceAttribute(Type resourceType, string resourceName) : base(resourceName)
        {
            nameProperty = resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }

        public override string DisplayName
        {
            get
            {
                if (nameProperty == null)
                    return base.DisplayName;

                return (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }
        }
    }
}

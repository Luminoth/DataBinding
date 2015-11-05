using System;

namespace DataBinding.Data
{
    public delegate bool ValidateValueCallback(object value);

    public class DependencyProperty
    {
        public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
        {
            DependencyProperty dependencyProperty = new DependencyProperty
            {
                Name = name,
                PropertyType = propertyType,
                OwnerType = ownerType
            };

            return dependencyProperty;
        }

        public string Name { get; private set; }

        public Type PropertyType { get; private set; }

        public Type OwnerType { get; private set; }

        private DependencyProperty()
        {
        }
    }
}

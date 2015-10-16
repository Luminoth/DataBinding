namespace DataBinding.Data
{
    public delegate bool ValidateValueCallback(object value);

    public class DependencyProperty
    {
        public static DependencyProperty Register(string name)
        {
            DependencyProperty dependencyProperty = new DependencyProperty
            {
                Name = name
            };

            return dependencyProperty;
        }

        public string Name { get; private set; }

        private DependencyProperty()
        {
        }
    }
}

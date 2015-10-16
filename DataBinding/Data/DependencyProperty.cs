namespace DataBinding.Data
{
    public delegate bool ValidateValueCallback(object value);

    public class DependencyProperty
    {
        public static DependencyProperty Register(string name, ValidateValueCallback validateValueCallback = null)
        {
            DependencyProperty dependencyProperty = new DependencyProperty
            {
                Name = name,
                ValidateValueCallback = validateValueCallback
            };

            return dependencyProperty;
        }

        public string Name { get; private set; }

        protected ValidateValueCallback ValidateValueCallback { get; private set; }

        private DependencyProperty()
        {
        }
    }
}

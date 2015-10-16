using System.Collections.Generic;

namespace DataBinding.Data
{
    public class DependencyObject
    {
        private readonly Dictionary<DependencyProperty, IBinding> _bindings = new Dictionary<DependencyProperty, IBinding>();

        public void SetBinding(DependencyProperty dependencyProperty, IBinding binding)
        {
            _bindings[dependencyProperty] = binding;
        }

        public object GetValue(DependencyProperty dependencyProperty)
        {
            IBinding binding;
            if(_bindings.TryGetValue(dependencyProperty, out binding)) {
                return binding.GetValue();
            }
            return null;
        }

        public void SetValue(DependencyProperty dependencyProperty, object value)
        {
            IBinding binding;
            if(_bindings.TryGetValue(dependencyProperty, out binding)) {
                binding.SetValue(value);
            }
        }
    }
}

using System.Collections.Generic;
using System.Reflection;

namespace DataBinding.Data
{
    public class DependencyObject
    {
        private readonly Dictionary<DependencyProperty, IBinding> _bindings = new Dictionary<DependencyProperty, IBinding>();

        public void SetBinding(DependencyProperty dependencyProperty, IBinding binding)
        {
            _bindings[dependencyProperty] = binding;
        }

        protected bool SetBinding(DependencyProperty dependencyProperty, object owner, string bindingValue)
        {
            bindingValue = bindingValue.Trim();
            if(!bindingValue.StartsWith("{") && !bindingValue.EndsWith("}")) {
                return false;
            }
            bindingValue = bindingValue.Substring(1, bindingValue.Length - 2).Trim();

            string[] scratch = bindingValue.Split('.');
            if(0 == scratch.Length || scratch.Length > 2) {
                return false;
            }

            if(scratch.Length > 1) {
                PropertyInfo propertyInfo = owner.GetType().GetProperty(scratch[0].Trim());
                if(null == propertyInfo) {
                    return false;
                }

                SetBinding(dependencyProperty, new Binding
                    {
                        Source = propertyInfo.GetValue(owner, null),
                        SourceProperty = scratch[1].Trim()
                    }
                );
            } else {
                SetBinding(dependencyProperty, new Binding
                    {
                        Source = owner,
                        SourceProperty = scratch[0].Trim()
                    }
                );
            }

            return true;
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

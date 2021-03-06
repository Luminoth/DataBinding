﻿using System.Collections.Generic;
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
            if(0 == scratch.Length) {
                return false;
            }

            object source = owner;
            int propertyIndex = 0;

            string propertyName;
            PropertyInfo propertyInfo;
            for(int i=0; i<scratch.Length-1; ++i) {
                propertyName = scratch[i].Trim();

                propertyInfo = source.GetType().GetProperty(propertyName);
                if(null == propertyInfo) {
                    return false;
                }

                source = propertyInfo.GetValue(source, null);
                if(null == source) {
                    return false;
                }

                propertyIndex = i+1;
            }

            propertyName = scratch[propertyIndex].Trim();
            propertyInfo = source.GetType().GetProperty(propertyName);
            if(null == propertyInfo) {
                return false;
            }

            SetBinding(dependencyProperty, new Binding
                {
                    Source = source,
                    SourceProperty = scratch[propertyIndex].Trim()
                }
            );

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

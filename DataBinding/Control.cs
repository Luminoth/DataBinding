using System.Collections.Generic;

using DataBinding.Data;

namespace DataBinding
{
    public abstract class Control : DependencyObject
    {
        public abstract void BuildFromDict(object owner, Dictionary<string, string> dict);
    }
}

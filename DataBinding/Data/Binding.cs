using System.Reflection;

namespace DataBinding.Data
{
    public interface IBinding
    {
        object GetValue();

        void SetValue(object value);
    }

    public class Binding : IBinding
    {
        public object Source { get; set; }

        public string SourceProperty { get; set; }

        public object GetValue()
        {
            PropertyInfo propertyInfo = Source.GetType().GetProperty(SourceProperty);
            if(null == propertyInfo) {
                return null;
            }
            return propertyInfo.GetValue(Source, null);
        }

        public void SetValue(object value)
        {
            PropertyInfo propertyInfo = Source.GetType().GetProperty(SourceProperty);
            if(null == propertyInfo) {
                return;
            }
            propertyInfo.SetValue(Source, value, null);
        }
    }
}

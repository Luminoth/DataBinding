using System.Collections.Generic;

using DataBinding.Data;

namespace DataBinding
{
    public class TextEdit : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextEdit));

        private class DefaultModel
        {
            public string Text { get; set; }
        }

        private readonly DefaultModel _defaultModel = new DefaultModel();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TextEdit()
        {
            SetBinding(TextProperty, new Binding
                {
                    Source = _defaultModel,
                    SourceProperty = "Text"
                }
            );
        }

        public override void BuildFromDict(object parent, Dictionary<string, string> dict)
        {
            string value;
            if(dict.TryGetValue("Text", out value)) {
                if(!SetBinding(TextProperty, parent, value)) {
                    Text = value;
                }
            }
        }
    }
}

using DataBinding.Data;

namespace DataBinding
{
    public class TextEdit : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text");

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
    }
}

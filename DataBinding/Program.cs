using System;
using System.Collections.Generic;

using DataBinding.Data;

namespace DataBinding
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();

            program.TestNoBinding();
            program.TestBinding();
            program.TestDictBindingString();
            program.TestDictBindingPropertyString();
        }

        public string TestText { get; set; } = string.Empty;

        public Model MyModel { get; set; } = new Model();

        public void TestNoBinding()
        {
            Model model = new Model
            {
                Name = "Unbound No Binding"
            };

            TextEdit textEdit = new TextEdit();

            Console.WriteLine($"Model name before: {model.Name} (expecting 'Unbound No Binding')");
            textEdit.Text = "Bound";
            Console.WriteLine($"Model name after: {model.Name} (expecting 'Unbound No Binding')");

            Console.WriteLine($"Text edit value before: {textEdit.Text} (expecting 'Bound')");
            model.Name = "TwoWayBound";
            Console.WriteLine($"Text edit value after: {textEdit.Text} (expecting 'Bound')");
        }

        public void TestBinding()
        {
            Model model = new Model
            {
                Name = "Unbound Binding"
            };

            TextEdit textEdit = new TextEdit();
            textEdit.SetBinding(TextEdit.TextProperty, new Binding
                {
                    // TODO: this gets parsed
                    Source = model,
                    SourceProperty = "Name"
                }
            );

            Console.WriteLine($"Model name before: {model.Name} (expecting 'Unbound No Binding')");
            textEdit.Text = "Bound";
            Console.WriteLine($"Model name after: {model.Name} (expecting 'Bound')");

            Console.WriteLine($"Text edit value before: {textEdit.Text} (expecting 'Bound')");
            model.Name = "TwoWayBound";
            Console.WriteLine($"Text edit value after: {textEdit.Text} (expecting 'TwoWayBound')");
        }

        public void TestDictBindingString()
        {
            var dict = new Dictionary<string, string>
            {
                { "Type", "TextEdit" },
                { "Text", "{TestText}" }
            };

            TextEdit textEdit = new TextEdit();
            textEdit.BuildFromDict(this, dict);

            Console.WriteLine($"String before: {TestText} (expected '')");
            textEdit.Text = "Bound";
            Console.WriteLine($"String after: {TestText} (expected 'Bound')");
        }

        public void TestDictBindingPropertyString()
        {
            var dict = new Dictionary<string, string>
            {
                { "Type", "TextEdit" },
                { "Text", "{MyModel.Name}" }
            };

            TextEdit textEdit = new TextEdit();
            textEdit.BuildFromDict(this, dict);

            Console.WriteLine($"Model name before: {MyModel.Name} (expected '')");
            textEdit.Text = "Bound";
            Console.WriteLine($"Model name after: {MyModel.Name} (expected 'Bound')");
        }
    }
}

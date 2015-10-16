using System;
using System.Collections.Generic;

using DataBinding.Data;

namespace DataBinding
{
    internal class Program
    {
        private static readonly Dictionary<string, string> TestDict = new Dictionary<string, string>
        {
            { "Type", "TextEdit" },
            { "Text", "{model.Name}" }
        };

        public static void Main(string[] args)
        {
            TestNoBinding();
            TestBinding();
        }

        public static void TestNoBinding()
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

        public static void TestBinding()
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
    }
}

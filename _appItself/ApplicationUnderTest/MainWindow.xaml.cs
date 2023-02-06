using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace ApplicationUnderTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] quotes = new string[]
        {
            "no way",
            "yes way",
            "maybe later",
            "nope."
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int indexArray = random.Next(quotes.Length);

            MyTextBox.Text = $"{quotes[indexArray]}";
        }
    }
}

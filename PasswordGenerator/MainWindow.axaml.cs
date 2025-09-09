using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using PasswordGenerator.Core.Enums;
using PasswordGenerator.Core.Models;
using System;

namespace PasswordGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GeneratePasswordButtonHandler(object sender, RoutedEventArgs e)
        {
            int length = Convert.ToInt32(LengthSlider.Value);
            string result = GeneratorPasswords.GeneratePassword(length,
                (bool)LowercaseCheck.IsChecked!, (bool)UppercaseCheck.IsChecked!, (bool)NumbersCheck.IsChecked!, 
                (bool)SymbolsCheck.IsChecked!);
            PasswordText.Text = result;
            var levelPassword = GeneratorPasswords.GetLevelComplexity(result);
            switch (levelPassword)
            {
                case LevelComplexity.VeryLow:
                    StrengthIndicator.Width = FullStrengthIndicator.Bounds.Width / 5;
                    StrengthIndicator.Fill = new SolidColorBrush(Colors.Red);
                    StrengthText.Text = "Очень низкая";
                    break;
                case LevelComplexity.Low:
                    StrengthIndicator.Width = FullStrengthIndicator.Bounds.Width / 5 * 2;
                    StrengthIndicator.Fill = new SolidColorBrush(Colors.Red);
                    StrengthText.Text = "Низкая";
                    break;
                case LevelComplexity.Middle:
                    StrengthIndicator.Width = FullStrengthIndicator.Bounds.Width / 5 * 3;
                    StrengthIndicator.Fill = new SolidColorBrush(Colors.Yellow);
                    StrengthText.Text = "Средняя";
                    break;
                case LevelComplexity.High:
                    StrengthIndicator.Width = FullStrengthIndicator.Bounds.Width / 5 * 4;
                    StrengthIndicator.Fill = new SolidColorBrush(Colors.Green);
                    StrengthText.Text = "Высокая";
                    break;
                case LevelComplexity.VeryHigh:
                    StrengthIndicator.Width = FullStrengthIndicator.Bounds.Width;
                    StrengthIndicator.Fill = new SolidColorBrush(Colors.Green);
                    StrengthText.Text = "Очень высокая";
                    break;
                default:
                    break;
            }
        }

        public void CopyPasswordButtonHandler(object sender, RoutedEventArgs e)
        {
            Clipboard?.SetTextAsync(PasswordText.Text);
        }

        public void ChangedSliderValueHandler(object sender, RoutedEventArgs e)
        {
            LengthValue.Text = Convert.ToInt32(LengthSlider.Value).ToString();
        }
    }
}
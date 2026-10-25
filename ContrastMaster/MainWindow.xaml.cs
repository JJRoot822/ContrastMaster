using ContrastMaster.Enumerations;
using ContrastMaster.Models;
using ContrastMaster.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ContrastMaster;

public partial class MainWindow : Window
{
    public RGBColor ForegroundColor { get; } = new() { Red = 0, Green = 0, Blue = 0 };
    public RGBColor BackgroundColor { get; } = new() { Red = 255, Green = 255, Blue = 255 };
    public CheckerSettings Settings { get; } = new() { Standard = WCAGStandard.DoubleA, ContentType = ContentType.TextContent, IsBoldText = false, FontSize = 12 };

    public MainWindow()
    {
        InitializeComponent();

        DataContext = this;

        Settings.PropertyChanged += SettingsPropertyChanged;
        ForegroundColor.PropertyChanged += ColorPropertyChanged;
        BackgroundColor.PropertyChanged += ColorPropertyChanged;

        UpdateSettingsControls();
        UpdateContrastResults();
    }

    public void IncreaseForegroundRed(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Red < 255)
        {
            ForegroundColor.Red++;
        }
    }

    public void IncreaseForegroundGreen(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Green < 255)
        {
            ForegroundColor.Green++;
        }
    }

    public void IncreaseForegroundBlue(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Blue < 255)
        {
            ForegroundColor.Blue++;
        }
    }

    public void DecreaseForegroundRed(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Red > 0)
        {
            ForegroundColor.Red--;
        }
    }

    public void DecreaseForegroundGreen(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Green > 0)
        {
            ForegroundColor.Green--;
        }
    }

    public void DecreaseForegroundBlue(object sender, RoutedEventArgs e)
    {
        if (ForegroundColor.Blue > 0)
        {
            ForegroundColor.Blue--;
        }
    }

    public void IncreaseBackgroundRed(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Red < 255)
        {
            BackgroundColor.Red++;
        }
    }

    public void IncreaseBackgroundGreen(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Green < 255)
        {
            BackgroundColor.Green++;
        }
    }

    public void IncreaseBackgroundBlue(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Blue < 255)
        {
            BackgroundColor.Blue++;
        }
    }

    public void DecreaseBackgroundRed(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Red > 0)
        {
            BackgroundColor.Red--;
        }
    }

    public void DecreaseBackgroundGreen(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Green > 0)
        {
            BackgroundColor.Green--;
        }
    }

    public void DecreaseBackgroundBlue(object sender, RoutedEventArgs e)
    {
        if (BackgroundColor.Blue > 0)
        {
            BackgroundColor.Blue--;
        }
    }

    public void IncreaseFontSize(object sender, RoutedEventArgs e)
    {
        Settings.FontSize++;
    }

    public void DecreaseFontSize(object sender, RoutedEventArgs e)
    {
        if (Settings.FontSize > 1)
        {
            Settings.FontSize--;
        }
    }

    public void ContentTypeRadioButtonGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            radioButton.IsChecked = true;
        }
    }

    private void SettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CheckerSettings.Standard) ||
            e.PropertyName == nameof(CheckerSettings.ContentType))
        {
            UpdateSettingsControls();
        }

        UpdateContrastResults();
    }

    private void ColorPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        UpdateContrastResults();
    }

    private void UpdateSettingsControls()
    {
        if (Settings.Standard == WCAGStandard.TripleA)
        {
            rdoNonTextContent.IsEnabled = false;

            if (Settings.ContentType == ContentType.NonTextContent)
            {
                Settings.ContentType = ContentType.TextContent;
            }
        }
        else
        {
            rdoNonTextContent.IsEnabled = true;
        }

        bool textContent = Settings.ContentType == ContentType.TextContent;

        cbxBoldText.IsEnabled = textContent;

        btnDecreaseFontSize.IsEnabled = textContent;
        txtFontSize.IsEnabled = textContent;
        btnIncreaseFontSize.IsEnabled = textContent;
    }

    private void UpdateContrastResults()
    {
        double ratio = ContrastCalculator.CalculateContrastRatio(ForegroundColor, BackgroundColor);
        double truncatedRatio = Math.Truncate(ratio * 1000) / 1000;

        bool passes;

        switch (Settings.Standard)
        {
            case WCAGStandard.DoubleA:
                switch (Settings.ContentType)
                {
                    case ContentType.TextContent:
                        passes = ContrastCalculator.PassesAAText(ratio, Settings.IsBoldText, Settings.FontSize);

                        break;

                    case ContentType.NonTextContent:
                        passes = ContrastCalculator.PassesAANonText(ratio);

                        break;
                    default:
                        return;
                }
                break;
            case WCAGStandard.TripleA:
                passes = ContrastCalculator.PassesAAAText(ratio, Settings.IsBoldText, Settings.FontSize);

                break;
            default:
                return;
        }

        txtBlockResults.Text = truncatedRatio+ ":1 - " + (passes ? "Pass" : "Fail");
        txtBlockResults.Foreground = passes ? Brushes.Green : Brushes.Red;

        swatchForeground.Fill = new SolidColorBrush(Color.FromRgb((byte)ForegroundColor.Red, (byte)ForegroundColor.Green, (byte)ForegroundColor.Blue));
        swatchBackground.Fill = new SolidColorBrush(Color.FromRgb((byte)BackgroundColor.Red, (byte)BackgroundColor.Green, (byte)BackgroundColor.Blue));

        if (IsLoaded)
        {
 AccessibilityUtilities.AnnounceMostRecent(txtBlockResults, "ContrastResult", txtBlockResults.Text);
        }
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContrastMaster.Models;

public class RGBColor : INotifyPropertyChanged
{
    private int _red;
    private int _green;
    private int _blue;

    public int Red
    {
        get => _red;
        set
        {
            int clampedValue = Math.Clamp(value, 0, 255);

            if (_red != clampedValue)
            {
                _red = clampedValue;
                OnPropertyChanged();
            }
        }
    }

    public int Green
    {
        get => _green;
        set
        {
            int clampedValue = Math.Clamp(value, 0, 255);

            if (_green != clampedValue)
            {
                _green = clampedValue;
                OnPropertyChanged();
            }
        }
    }

    public int Blue
    {
        get => _blue;
        set
        {
            int clampedValue = Math.Clamp(value, 0, 255);

            if (_blue != clampedValue)
            {
                _blue = clampedValue;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
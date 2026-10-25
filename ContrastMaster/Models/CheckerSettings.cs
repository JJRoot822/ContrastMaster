using ContrastMaster.Enumerations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContrastMaster.Models;

public class CheckerSettings: INotifyPropertyChanged
{
    private WCAGStandard _standard;
    private ContentType _contentType;
    private bool _isBoldText;
    private int _fontSize;

    public WCAGStandard Standard
    {
        get => _standard;
        set
        {
            if (_standard != value)
            {
                _standard = value;
                OnPropertyChanged();
            }
        }
    }

    public ContentType ContentType
    {
        get => _contentType;
        set
        {
            if (_contentType!= value)
            {
                _contentType = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsBoldText
    {
        get => _isBoldText;
        set
        {
            if (_isBoldText != value)
            {
                _isBoldText= value;
                OnPropertyChanged();
            }
        }
    }

    public int FontSize
    {
        get => _fontSize;
        set
        {
            int clampedValue = Math.Max(value, 1);

            if (_fontSize != clampedValue)
            {
                _fontSize = clampedValue;
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
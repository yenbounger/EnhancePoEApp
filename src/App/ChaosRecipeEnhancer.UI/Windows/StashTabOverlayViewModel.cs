using System.Collections.ObjectModel;
using System.Windows.Controls;
using ChaosRecipeEnhancer.UI.Utilities.ZemotoCommon;

namespace ChaosRecipeEnhancer.UI.Windows;

internal sealed class StashTabOverlayViewModel : ViewModelBase
{
    private bool _isEditing;

    public bool IsEditing
    {
        get => _isEditing;
        set => SetProperty(ref _isEditing, value);
    }

    public readonly ObservableCollection<TabItem> OverlayStashTabList = new();
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GroupProject.ViewModels;

namespace GroupProject;

public partial class HistogramsView : UserControl
{
    public HistogramsView()
    {
        InitializeComponent();
        DataContext = new HistogramsViewModel();
    }
}
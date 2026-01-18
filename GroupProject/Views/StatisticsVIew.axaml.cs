using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GroupProject.ViewModels;

namespace GroupProject;

public partial class StatisticsVIew : UserControl
{
    public StatisticsVIew()
    {
        InitializeComponent();
        DataContext = new StatisticsViewModel();
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Emit;

namespace GroupProject.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private object _currentView;
        [ObservableProperty]
        private byte[] image;


        [RelayCommand]
        private void ShowStatistics()
        {
            CurrentView = new StatisticsViewModel();
        }
        [RelayCommand]
        private void ShowHistograms()
        {
            CurrentView = new HistogramsViewModel();
        }
        [RelayCommand]
        private void ShowConvertion()
        {
            CurrentView = new ConvertionViewModel();
        }
    }
}

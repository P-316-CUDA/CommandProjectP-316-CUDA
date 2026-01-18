
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using GP_models.Models;
using ReactiveUI;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private object _currentView;
        [ObservableProperty]
        private byte[] image;

        //DataBase
        [ObservableProperty]
        private ObservableCollection<ConvertionRecord> convertionRecords;

        //Hitograms
        [ObservableProperty]
        private AvaPlot _originalPlot;

        [ObservableProperty]
        private AvaPlot _convertedPlot;

        public Interaction<FilePickerSaveOptions, IStorageFile?> ShowSaveDialog { get; } = new Interaction<FilePickerSaveOptions, IStorageFile?>();


        public MainWindowViewModel()
        {
            using (AppDbContext context = new AppDbContext())
            {
                convertionRecords = new ObservableCollection<ConvertionRecord>();
                convertionRecords.AddRange(context.ConvertionRecords.ToArray());
            }
        }
    }
}

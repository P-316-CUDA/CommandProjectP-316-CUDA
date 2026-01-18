using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using GP_models.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.ViewModels
{
    public partial class StatisticsViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ConvertionRecord> convertionRecords;
        public Interaction<FilePickerSaveOptions, IStorageFile?> ShowSaveDialog { get; } = new Interaction<FilePickerSaveOptions, IStorageFile?>();

        public StatisticsViewModel()
        {
            using (AppDbContext context = new AppDbContext())
            {
                convertionRecords = new ObservableCollection<ConvertionRecord>();
                convertionRecords.AddRange(context.ConvertionRecords.ToArray());
            }
        }
    }
}

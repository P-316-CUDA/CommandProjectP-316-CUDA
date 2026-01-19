
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using GP_models.Models;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace GroupProject.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private byte[]? image;
        [ObservableProperty]
        private Bitmap? previewImage;
        [ObservableProperty]
        private bool canAddImage = true;

        public Interaction<FilePickerSaveOptions, IStorageFile?> ShowSaveDialog { get; } = new Interaction<FilePickerSaveOptions, IStorageFile?>();
        public Interaction<FilePickerOpenOptions, IReadOnlyList<IStorageFile>?> ShowOpenFileDialog { get; } = new Interaction<FilePickerOpenOptions, IReadOnlyList<IStorageFile>?>();

        //DataBase
        [ObservableProperty]
        private ObservableCollection<ConvertionRecord> convertionRecords;

        //Hitograms
        [ObservableProperty]
        Double[] values;




        public MainWindowViewModel()
        {
            values = [20, 50, 40, 20, 40, 30, 50, 20, 50, 40];
            using (AppDbContext context = new AppDbContext())
            {
                convertionRecords = new ObservableCollection<ConvertionRecord>();
                convertionRecords.AddRange(context.ConvertionRecords.ToArray());
            }
        }

        [RelayCommand]
        private async Task UploadImage()
        {
            try
            {
                var files = await ShowOpenFileDialog.Handle(new FilePickerOpenOptions
                {
                    Title = "Select an image",
                    AllowMultiple = false,
                    FileTypeFilter = new[]
                    {
                        new FilePickerFileType("Image files")
                        {
                            Patterns = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" }
                        }
                    }
                }).FirstAsync();

                if (files.Count == 0 || files[0] is not IStorageFile file)
                    return;

                await using var stream = await file.OpenReadAsync();
                using var memoryStream = new MemoryStream();

                await stream.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                Image = imageBytes;

                memoryStream.Seek(0, SeekOrigin.Begin);
                PreviewImage = Bitmap.DecodeToWidth(memoryStream, 400);

                CanAddImage = false;
            }
            catch (Exception ex)
            {
                //
            }
        }
        [RelayCommand]
        private void Clear()
        {
            Image = null;
            PreviewImage = null;
            CanAddImage = true;
        }
        [RelayCommand]
        private async Task ExportToJson()
        {
            try
            {
                var options = new FilePickerSaveOptions
                {
                    Title = "Сохранить результаты как JSON",
                    DefaultExtension = "json",
                    ShowOverwritePrompt = true,
                    FileTypeChoices = new[]
                    {
                        new FilePickerFileType("JSON файл") { Patterns = new[] { "*.json" } }
                    }
                };

                var file = await ShowSaveDialog.Handle(options);

                var records = ConvertionRecords.ToList();

                string json = JsonConvert.SerializeObject(records, Formatting.Indented);

                await using var stream = await file.OpenWriteAsync();
                await using var writer = new StreamWriter(stream);
                await writer.WriteAsync(json);
                await writer.FlushAsync();

            }
            catch (Exception ex)
            {
                //
            }

        }
    }
}

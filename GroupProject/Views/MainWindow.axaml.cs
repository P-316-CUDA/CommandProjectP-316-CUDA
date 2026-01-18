using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using GroupProject.ViewModels;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace GroupProject
{
    public partial class MainWindow : Window
    {
        private byte[] image;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
        private async void ImageUpload(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Button button = sender as Button;
            var files = await this.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions
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
            });

            if (files.Count != 0 && files[0] is IStorageFile file)
                try
                {
                    await using var stream1 = await file.OpenReadAsync();

                    using var memoryStream = new MemoryStream();

                    await stream1.CopyToAsync(memoryStream);

                    image = memoryStream.ToArray();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                }
            using var stream2 = new MemoryStream(image);
            //Uploa.Source = Bitmap.DecodeToWidth(stream2, 400);
            Img.Source = Bitmap.DecodeToWidth(stream2, 400);
            button.IsEnabled = false;
            button.IsVisible = false;
        }
        private void Clear(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            image = null;
            Img.Source = null;
            AddImageButton.IsEnabled = true;
            AddImageButton.IsVisible = true;
        }
    }
}
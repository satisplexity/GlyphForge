using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace GlyphForge.Presentation
{
    public partial class MainWindow : Window
    {
        private string _selectedFolder;

        private List<SvgIcon> _createdIcons;

        private string _buildedString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();

            dialog.Title = "Select folder";
            
            if(dialog.ShowDialog() == true)
            {
                _selectedFolder = dialog.FolderName;

                _folderName_TextBlock.Text = _selectedFolder;
            }
        }

        private void CreateDictionaryButton_Click(object sender, RoutedEventArgs e)
        {
            _createdIcons = SvgLoader.LoadFromFolder(_selectedFolder);

            PresentIcons();

            _buildedString = ResourceDictionaryGenerator.Generate(_createdIcons);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new()
            {
                Filter = "XAML ResourceDictionary (*.xaml)|*.xaml",
                FileName = "Icons.xaml"
            };

            if (saveDialog.ShowDialog() != true)
                return;

            File.WriteAllText(saveDialog.FileName,_buildedString,Encoding.UTF8);
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(_buildedString);
            
            MessageBox.Show("Copied to clipboard!");
        }

        private void PresentIcons()
        {
            _iconsPanel.Children.Clear();

            foreach(SvgIcon icon in _createdIcons)
            {
                System.Windows.Shapes.Path path = new()
                {
                    Fill = Brushes.White,
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(5),
                    Stretch = Stretch.Uniform,
                    Data = icon.Geometry
                };

                _iconsPanel.Children.Add(path);
            }
        }
    }
}
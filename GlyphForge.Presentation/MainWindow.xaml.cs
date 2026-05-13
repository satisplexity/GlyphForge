using Microsoft.Win32;
using System.Windows;

namespace GlyphForge.Presentation
{
    public partial class MainWindow : Window
    {
        private string _selectedFolder;

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

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace MediaPlayerPrototype1_FolderSelection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string[] files;

        private string filepath;
        public string Filepath
        {
            get { return filepath; }
            set
            {
                filepath = value;
                OnPropertyChanged();
                FileList.Items.Clear();
                ListFilenames(filepath);
            }
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = dialog.ShowDialog();
            if(result == WinForms.DialogResult.OK)
            {
                Filepath = dialog.SelectedPath;
            }
            else
            {
                Filepath = "No filepath selected";
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListFilenames(string directory)
        {
            foreach (var filename in Directory.EnumerateFiles(directory)) //list files in directory
            {
                FileList.Items.Add(filename);
            }
            foreach (var dirname in Directory.EnumerateDirectories(directory)) //list all 
            {
                ListFilenames(dirname);
            }
        }

        private void OnPropertyChanged( [CallerMemberName] string PropertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

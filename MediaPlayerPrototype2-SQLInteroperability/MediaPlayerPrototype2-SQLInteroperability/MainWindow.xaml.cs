using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
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

namespace MediaPlayerPrototype2_SQLInteroperability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int nextID = 0;
        SQLiteConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            connection = new SQLiteConnection("Data Source=C:/Users/wtjor/source/repos/MEDIA_PLAYER_PROTOTYPES/MediaPlayerPrototypes/MediaPlayerPrototype2-SQLInteroperability/MediaPlayerPrototype2-SQLInteroperability/database.sqlite");
            connection.Open();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string input_text;

        public string InputText
        {
            get { return input_text; }
            set {
                input_text = value;
                OnPropertyChanged(input_text);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            InputText = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
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

namespace MediaPlayerPrototype2_SQLInteroperability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        static int nextID = 0;
        public static SQLiteConnection connection;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            IEnumerable<string> result = Directory.EnumerateFiles("C:/Users/wtjor/source/repos/MEDIA_PLAYER_PROTOTYPES/MediaPlayerPrototypes/MediaPlayerPrototype2-SQLInteroperability/MediaPlayerPrototype2-SQLInteroperability", "database.sqlite");
            if (result.ToArray().Length == 0) //database doesn't exist
                connection = new SQLiteConnection("Data Source=C:/Users/wtjor/source/repos/MEDIA_PLAYER_PROTOTYPES/MediaPlayerPrototypes/MediaPlayerPrototype2-SQLInteroperability/MediaPlayerPrototype2-SQLInteroperability/database.sqlite");
            else
                connection = new SQLiteConnection("Data Source=C:/Users/wtjor/source/repos/MEDIA_PLAYER_PROTOTYPES/MediaPlayerPrototypes/MediaPlayerPrototype2-SQLInteroperability/MediaPlayerPrototype2-SQLInteroperability/database.sqlite;New=False");
            try
            {
                connection.Open();
                string createTable = @"CREATE TABLE items(
                                    id INTEGER PRIMARY KEY,
                                    name TEXT NOT NULL
                                    );";
                SQLiteCommand command = new SQLiteCommand(createTable, connection);
                command.ExecuteNonQuery();

            }
            catch (SQLiteException except)
            {
                // Display the exception
                Console.WriteLine(except.Message);
            }
            connection.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private string input_text;

        public string InputText
        {
            get { return input_text; }
            set {
                input_text = value;
                OnPropertyChanged();
            }
        }
        
        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                string addInputText = "INSERT INTO items VALUES (@nextID, @InputText)";
                SQLiteCommand insertInputCmd = new SQLiteCommand(addInputText, connection);
                insertInputCmd.Parameters.AddWithValue("@nextID", nextID);
                insertInputCmd.Parameters.AddWithValue("@InputText", InputText);
                insertInputCmd.ExecuteNonQuery();
            }
            catch (SQLiteException except)
            {
                // Display the exception
                Console.WriteLine(except.Message);
            }
            txtInput.Clear();
            connection.Close();
        }
    }
}

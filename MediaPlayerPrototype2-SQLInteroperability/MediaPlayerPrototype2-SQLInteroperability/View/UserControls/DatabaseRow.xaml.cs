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

namespace MediaPlayerPrototype2_SQLInteroperability.View.UserControls
{
    /// <summary>
    /// Interaction logic for DatabaseRow.xaml
    /// </summary>
    public partial class DatabaseRow : UserControl, INotifyPropertyChanged
    {
        public DatabaseRow()
        {
            DataContext = this;
            InitializeComponent();
        }

        //delete this row from the database and destroy this object
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.connection.Open();
                string delete = "DELETE FROM items WHERE id = @id";
                SQLiteCommand delCmd = new SQLiteCommand(delete, MainWindow.connection);
                delCmd.Parameters.AddWithValue("@id", ID);
                delCmd.ExecuteNonQuery();
                MainWindow.connection.Close();
            }
            catch (SQLiteException except)
            {
                // Display the exception
                Console.WriteLine(except.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        public int ID
        {
            get { return id; }
            set { 
                id = value;
                OnPropertyChanged();
            }
        }
        private string itemname;
        public string ItemName
        {
            get { return itemname; }
            set { 
                itemname = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}

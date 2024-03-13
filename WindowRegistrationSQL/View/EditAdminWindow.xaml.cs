using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;


namespace WindowRegistrationSQL.View
{
    /// <summary>
    /// Логика взаимодействия для EditAdminWindow.xaml
    /// </summary>
    public partial class EditAdminWindow : Window
    {
        public string loginNew;
        public string passwordNew;
        public EditAdminWindow(string login, string password)
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();
            Login.Text = login;
            PasswordText.Text = password;
            loginNew = login;
            passwordNew = password;
        }

        // Кнопка закрытия
        private void ButtonClickExit(object sender, RoutedEventArgs e) // кнопка закрытия
        {
            this.Close();
        }

        // Перемещение формы курсором мыши
        private void PanelHeaderMouseDown(object sender, MouseButtonEventArgs e) // перемещение окна при нажатии мыши
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // Редавтирование данных
        public void ButtonClickEnter(object sender, RoutedEventArgs e)
        {
           // SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"Update LoginPassword set LoginAdmin = @Login, PasswordAdmin = @Password where LoginAdmin = '{loginNew}'";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Login", Login.Text);
            sqlCmd.Parameters.AddWithValue("@Password", PasswordText.Text);
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("The data has been successfully added to the database");
            }
            else
            {
                MessageBox.Show("Error when adding data");
            }
        }
    }
}


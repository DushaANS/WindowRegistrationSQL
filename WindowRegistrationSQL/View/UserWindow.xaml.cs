using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WindowRegistrationSQL.View;

namespace WindowRegistrationSQL.VIew
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public object loginNew { get; set; }

        // Вывод данных профиля
        public UserWindow(object login)
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();
            LoginUserWindow.Content = login;
            loginNew = login;


          //  string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
           string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM RegistrationUser where LoginUser = '{login}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PasswordUserWindow.Content = reader["PasswordUser"].ToString();
                    BirthdayUserWindow.Content = reader["Birthday"].ToString();
                    BirthPlaceUserWindow.Content = reader["BirthPlace"].ToString();
                    PlaceOfStudyUserWindow.Content = reader["PlaceOfStudy"].ToString();
                }
            }
        }

        // перемещение окна при нажатии мыши
        private void PanelHeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        // кнопка закрытия
        private void ButtonClickExit(object sender, RoutedEventArgs e)
        {
            //string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
            ////  string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();
            //string deleteQuery = "truncate table TimeUserLogin";
            //SqlCommand command = new SqlCommand(deleteQuery, connection);
            //command.ExecuteNonQuery();
            this.Close();
        }

        // Выход из профиля
        private void ButtonClickExitProfile(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SafeClick(object sender, RoutedEventArgs e)
        {
           // SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
           SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"Update RegistrationUser set Biographia = @Biografia where LoginUser = '{loginNew}'";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Biografia", BiografiaUser.Text);
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Данные успешно добавлены в базу");
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных");
            }
        }

        private void BiographiaUserOpen(object sender, RoutedEventArgs e)
        {
            AutobiographyUser autobiographyUser = new AutobiographyUser(loginNew);
            autobiographyUser.Show();
        }

        private void ViewMessage(object sender, RoutedEventArgs e)
        {
            UserMessage userMessage = new UserMessage(loginNew, loginNew);
            userMessage.MessageAdminText.Visibility = Visibility.Collapsed;
            userMessage.MessageAdmin.Visibility = Visibility.Collapsed;
            userMessage.EnterAdmin.Visibility = Visibility.Collapsed;
            userMessage.loginUserWindow.Visibility = Visibility.Collapsed;
            userMessage.Show();
        }
    }
}


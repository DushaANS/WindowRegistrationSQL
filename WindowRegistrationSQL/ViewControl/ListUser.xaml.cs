using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using WindowRegistrationSQL.View;
using WindowRegistrationSQL.VIew;

namespace WindowRegistrationSQL.ViewControl
{
    /// <summary>
    /// Логика взаимодействия для ListUser.xaml
    /// </summary>
    /// 
    public partial class ListUser : UserControl
    {
        public ListUser(object login)
        {
            InitializeComponent();
            loginAdmin = login;
        }

        public object login { get; set; }
        public object loginAdmin { get; set; }


        public AdminWindow AdminWindow;

        // удаление профиля
        private void DeleteProfile(object sender, RoutedEventArgs e)
        {

            using (SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;"))
           // using (SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;"))
            {
                sqlData.Open();
                SqlCommand command = new SqlCommand($"DELETE FROM RegistrationUser where LoginUser = '{LoginUsers.Content}'", sqlData);
                command.ExecuteNonQuery();
                AdminWindow.LoadData("");
            }
        }

        // Редактирование профия
        private void EditProfile(object sender, RoutedEventArgs e)
        {
            login = LoginUsers.Content;
            UserEdit userEdit = new UserEdit(login);
            userEdit.Show();
            AdminWindow.LoadData("");
        }

        // Открытие профиля
        private void OpenProfile(object sender, RoutedEventArgs e)
        {
            login = LoginUsers.Content;
            UserWindow userWindow = new UserWindow(login);
            userWindow.Exit.Visibility = Visibility.Collapsed;
            userWindow.UsersMessage.Visibility = Visibility.Collapsed;
            userWindow.BiografiaUser.Visibility = Visibility.Collapsed;
            userWindow.SafeBio.Visibility = Visibility.Collapsed;
            userWindow.UserBio.Visibility = Visibility.Collapsed;
            userWindow.Show();
        }

        private void OpenBio(object sender, RoutedEventArgs e)
        {
            login = LoginUsers.Content;
            AutobiographyUser autobiographyUser = new AutobiographyUser(login);
            autobiographyUser.Show();
        }

        private void MessageUsers(object sender, RoutedEventArgs e)
        {
            login = LoginUsers.Content;
            UserMessage userMessage = new UserMessage(login, loginAdmin);
            userMessage.MessageUser.Visibility = Visibility.Collapsed;
            userMessage.EnterUser.Visibility = Visibility.Collapsed;
            userMessage.loginAdminWindow.Visibility = Visibility.Collapsed;
            userMessage.Show();
        }

        private void CreateChat(object sender, RoutedEventArgs e)
        {

            login = LoginUsers.Content;
             SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
           // SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"ALTER TABLE MessageUser ADD {login} VARCHAR(500)";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                MessageBox.Show("Чат создан");
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении данных");
            }
        }
    }
}


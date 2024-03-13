using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindowRegistrationSQL.View
{

    /// <summary>
    /// Логика взаимодействия для UserMessage.xaml
    /// </summary>
    public partial class UserMessage : Window
    {
        public object loginNewUser { get; set; }
        public object loginNewAdmin { get; set; }
        public UserMessage(object loginUser, object loginAdmin)
        {

            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();
            loginUserWindow.Content = loginUser;
            loginUserWindowNew1.Content = loginUser;
            loginNewUser = loginUser;


           // string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
           string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM LoginAdminTime";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    loginAdminWindow.Content = reader["LoginAdmin"].ToString();
                    loginNewAdmin = loginAdminWindow.Content;
                }
            }

            string querynew = $"SELECT [{loginNewUser}] FROM MessageUser WHERE [{loginNewUser}] = [{loginNewUser}];"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querynew, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserMessageHistory.Items.Add(reader[$"{loginNewUser}"].ToString());
                }

            }
        }


            // кнопка закрытия
            private void ButtonClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // перемещение окна при нажатии мыши
        private void PanelHeaderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void EnterMessage(object sender, RoutedEventArgs e)
        {
         //   SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"Insert into MessageUser ([{loginNewUser}]) values (@Login)";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Login", MessageAdmin.Text);
            int rowsAffected = sqlCmd.ExecuteNonQuery();
            if (MessageAdmin.Text != null)
            {
                sqlCmd.Parameters.AddWithValue("@Login", MessageAdmin.Text);
            }
            else
            {
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

        private void EnterMessageUser(object sender, RoutedEventArgs e)
        {
          //  SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
           SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"Insert into MessageUser ([{loginNewUser}]) values (@Login)";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Login", MessageUser.Text);
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

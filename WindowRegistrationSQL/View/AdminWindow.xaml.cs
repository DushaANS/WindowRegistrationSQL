using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WindowRegistrationSQL.View;

namespace WindowRegistrationSQL.VIew
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        DispatcherTimer timer;
        double panelWidth;
        bool hidden;

        public string loginNew;
        public string passwordNew;

        public AdminWindow(DataLoginAndPassword dataLoginAndPassword)
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();
            LoadDataToListBox();

            timer = new DispatcherTimer(); 
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += TimerTick;

            LoginAdminWindow.Content = dataLoginAndPassword.Login;
            PasswordAdminWindow.Content = dataLoginAndPassword.Password;

            loginNew = dataLoginAndPassword.Login;
            passwordNew = dataLoginAndPassword.Password;
            MainWindow mainWindow = new MainWindow();
            RegistrationUser registrationUser = new RegistrationUser();

            panelWidth = sidePanel.Width;
        }

        // вывод списка входящих пользователей
        private void LoadDataToListBox() 
        {
            //string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
            string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";
            string query = "SELECT * FROM TimeUserLogin";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        UserHistory.Items.Add(reader["LoginUserTime"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // вывод списка пользователей
        public void LoadData(string stroka) 
        {
            ListUser.Children.Clear();
            //using (SqlConnection connection = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;"))
           using (SqlConnection connection = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;"))
            {
                connection.Open(); //подключение БД
                SqlCommand command = new SqlCommand($"Select LoginUser From RegistrationUser" + stroka, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ViewControl.ListUser listUser = new ViewControl.ListUser(loginNew);
                        listUser.LoginUsers.Content = reader[0];
                        try
                        {
                            // client.Photo.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + reader[9].ToString()));
                        }
                        catch (Exception)
                        {
                        }
                        listUser.AdminWindow = this;
                        ListUser.Children.Add(listUser);
                    }
                }
            }
        }

        // метод вывода данных
        private void ListOpr(string stroka) 
        {
            LoadData("");
        }

        // передача в данный метод данные
        private void UpdateList(object sender, RoutedEventArgs e) 
        {
            ListOpr("");
        }

        // перемещение окна через курсор
        private void PanelHeaderMouseDown(object sender, MouseButtonEventArgs e) 
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // кнопка выхода с очищением списка входящих пользователей
        private void ButtonClickExit(object sender, RoutedEventArgs e) 
        {
           // string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
           //// string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";

           // using (SqlConnection connection = new SqlConnection(connectionString))
           // {
           //     connection.Open();

           //     string deleteQueryTimeUserLogin = "TRUNCATE TABLE TimeUserLogin";
           //     SqlCommand commandTimeUserLogin = new SqlCommand(deleteQueryTimeUserLogin, connection);
           //     commandTimeUserLogin.ExecuteNonQuery();

           //     string deleteQueryLoginAdminTime = "TRUNCATE TABLE LoginAdminTime";
           //     SqlCommand commandLoginAdminTime = new SqlCommand(deleteQueryLoginAdminTime, connection);
           //     commandLoginAdminTime.ExecuteNonQuery();

           //     this.Close();
           //     }
           this.Close();
        }

        // перемещение букового окна со слайдером
        private void TimerTick(object sender, EventArgs e) 
        {
            if (hidden)
            {
                sidePanel.Width += 2;
                if (sidePanel.Width >= panelWidth)
                {
                    timer.Stop();
                    hidden = false;
                }
            }
            else
            {
                sidePanel.Width -= 2;
                if (sidePanel.Width <= 35)
                {
                    timer.Stop();
                    hidden = true;
                }
            }
        }

        // метод для скроллинга окна
        private void ButtonClickScroll(object sender, RoutedEventArgs e) 
        {
            timer.Start();
        }

        // возвращение в окно входа
        private void ButtonExitProfile(object sender, RoutedEventArgs e) 
        {
            // string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
            //// string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";

            // using (SqlConnection connection = new SqlConnection(connectionString))
            // {
            //     connection.Open();

            //     string deleteQueryTimeUserLogin = "TRUNCATE TABLE TimeUserLogin";
            //     SqlCommand commandTimeUserLogin = new SqlCommand(deleteQueryTimeUserLogin, connection);
            //     commandTimeUserLogin.ExecuteNonQuery();

            //     string deleteQueryLoginAdminTime = "TRUNCATE TABLE LoginAdminTime";
            //     SqlCommand commandLoginAdminTime = new SqlCommand(deleteQueryLoginAdminTime, connection);
            //     commandLoginAdminTime.ExecuteNonQuery();

            //     MainWindow mainWindow = new MainWindow();
            //     mainWindow.Show();
            //     this.Hide();
            // }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();

        }

        // ввод в таблицу входящих пользователей
        private void UserHistorySelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) 
        {

            ListUser.Children.Clear();
           // using (SqlConnection connection = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;"))
           using (SqlConnection connection = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;"))
            {
                connection.Open(); //подключение БД
                SqlCommand command = new SqlCommand($"Select LoginUser From RegistrationUser");
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    List<string> logins = new List<string>();
                    // Заполнение списка логинами пользователей (можно получить из SQL)

                    foreach (string login in logins)
                    {
                        UserHistory.Items.Add(login);
                    }
                }
                connection.Close();
            }
        }

        // Переход в окно редактирования
        private void EditProfile(object sender, RoutedEventArgs e)
        {
            EditAdminWindow editAdminWindow = new EditAdminWindow(loginNew, passwordNew);
            editAdminWindow.Show();
        }
    }
}
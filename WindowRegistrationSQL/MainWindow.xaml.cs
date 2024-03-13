using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WindowRegistrationSQL.View;
using WindowRegistrationSQL.VIew;

namespace WindowRegistrationSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();
            
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

        // вход User
        private void ButtonClickEnter(object sender, RoutedEventArgs e) // кнопка enter
        {
            SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            //SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            try
            {
                if (sqlData.State == ConnectionState.Closed) // проверка подключения
                {
                    sqlData.Open();
                    string query = "SELECT COUNT(1) FROM RegistrationUser WHERE LoginUser=@Login AND PasswordUser=@Password"; 
                    SqlCommand sqlCmd = new SqlCommand(query, sqlData);
                    sqlCmd.CommandType = CommandType.Text;
                    string password = Password.Password;
                    string login = Login.Text;
                    sqlCmd.Parameters.AddWithValue("@Login", Login.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (count == 1) // проверка правильности пароля или логина
                    {
                        string checkQuery = "Insert into TimeUserLogin (LoginUserTime)values(@Login)"; // вписывание User во временную таблицу
                        SqlCommand checkCommand = new SqlCommand(checkQuery, sqlData);
                        checkCommand.CommandType = CommandType.Text;
                        checkCommand.Parameters.AddWithValue("@Login", Login.Text);
                        int rowsAffected = checkCommand.ExecuteNonQuery();

                        VIew.UserWindow userWindow = new VIew.UserWindow(login); // переход в окно User
                        userWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login or password is incorrect.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlData.Close();
            }
        }

        // переход в окно регистрации
        private void ButtonClickRegistration(object sender, RoutedEventArgs e) // кнопка регистрации
        {
            VIew.RegistrationUser registrationUser = new VIew.RegistrationUser();
            registrationUser.Show();
            this.Close();
        }

        public object loginNewAdmin {  get; set; }

        // вход Admin
        private void ButtonClickAdministrator(object sender, RoutedEventArgs e) 
        {
            SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            //SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            try
            {
                if (sqlData.State == ConnectionState.Closed)
                {
                    sqlData.Open();
                    string query = "SELECT COUNT(1) FROM LoginPassword WHERE LoginAdmin=@Login AND PasswordAdmin=@Password";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlData);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Login", Login.Text);
                    sqlCmd.Parameters.AddWithValue("@Password", Password.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (count == 1)
                    {
                        string checkQuery = "Insert into LoginAdminTime (LoginAdmin)values(@Login)"; // вписывание User во временную таблицу
                        SqlCommand checkCommand = new SqlCommand(checkQuery, sqlData);
                        checkCommand.CommandType = CommandType.Text;
                        checkCommand.Parameters.AddWithValue("@Login", Login.Text);
                        int rowsAffected = checkCommand.ExecuteNonQuery();

                        DataLoginAndPassword dataLoginAndPassword = new DataLoginAndPassword(); // класс в который передаются данные входа
                        dataLoginAndPassword.Login = Login.Text;
                        dataLoginAndPassword.Password = Password.Password;

                        VIew.AdminWindow adminWindow = new VIew.AdminWindow(dataLoginAndPassword);
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login or password is incorrect.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlData.Close();
            }
        }

        // показать пароль
        private void CheckBoxChecked(object sender, RoutedEventArgs e)  
        {
            
            PasswordText.Text = Password.Password;
            if (Password.Visibility == Visibility.Visible)
            {
                PasswordText.Visibility = Visibility.Visible;
                Password.Visibility = Visibility.Collapsed;
            }
        }

        // скрыть пароль
        private void PasswordCheckBoxUnchecked(object sender, RoutedEventArgs e) 
        {
            PasswordText.Text = Password.Password;
            if (PasswordText.Visibility == Visibility.Visible)
            {
                PasswordText.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Visible;
            }
        }
    }
}
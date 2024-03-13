using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WindowRegistrationSQL.VIew
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUser.xaml
    /// </summary>
    public partial class RegistrationUser : Window
    {
        public RegistrationUser()
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            InitializeComponent();

        }

        // управление панелью указателем мыши
        private void PanelHeaderMouseDown(object sender, MouseButtonEventArgs e) // перемещение окна курсора
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        // кнопка закрытия
        private void Button_Click(object sender, RoutedEventArgs e) // кнопка закрытия 
        {
            this.Close();
        }

        // кнопка сохранения данных
        private void Safe(object sender, RoutedEventArgs e)
        {
           // SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
            SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string checkQuery = "SELECT COUNT(*) FROM RegistrationUser WHERE LoginUser = @Login";
            SqlCommand checkCommand = new SqlCommand(checkQuery, sqlData);
            checkCommand.Parameters.AddWithValue("@Login", LoginReg.Text);
            int count = (int)checkCommand.ExecuteScalar();

            if (count != 0) // проверка на индентичность логина в таблице
            {
                MessageBox.Show("This username is already present");
            }
            else if (count == 0) // вписывание данных в таблицу
            {
                string query = "Insert into RegistrationUser (LoginUser,PasswordUser,Birthday,BirthPlace,PlaceOfStudy)" +
                    "values(@Login,@Password,@Birthday,@BirthPlace,@PlaceOfStudy)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlData);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Login", LoginReg.Text);
                sqlCmd.Parameters.AddWithValue("@Password", PasswordReg.Text);
                sqlCmd.Parameters.AddWithValue("@Birthday", BirthdayReg.Text);
                sqlCmd.Parameters.AddWithValue("@BirthPlace", BirthPlaceReg.Text);
                sqlCmd.Parameters.AddWithValue("@PlaceOfStudy", PlaceOfStudyReg.Text);
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

        // возвращение в окно входа
        private void ReturnToTheLoginWindow(object sender, RoutedEventArgs e) // кнопка перехода в окно входа
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
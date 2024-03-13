using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using WindowRegistrationSQL.ViewControl;
using System.Net.Sockets;

namespace WindowRegistrationSQL.View
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Window
    {
        public object loginNew { get; set; }
        public UserEdit(object login)
        {
            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;
            loginNew = login;
            InitializeComponent();
        }

        public ListUser ListUser;

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

        // Сохранение информации
        private void ButtonClickSafe(object sender, RoutedEventArgs e)
        {
         //   SqlConnection sqlData = new SqlConnection(@"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;");
          SqlConnection sqlData = new SqlConnection(@"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;");
            sqlData.Open();
            string query = $"Update RegistrationUser set LoginUser = @Login, PasswordUser = @Password, Birthday = @Birthday, BirthPlace = @BirthPlace, PlaceOfStudy = @PlaceOfStudy where LoginUser = '{loginNew}'";
            SqlCommand sqlCmd = new SqlCommand(query, sqlData);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Login", LoginEdit.Text);
            sqlCmd.Parameters.AddWithValue("@Password", PasswordEdit.Text);
            sqlCmd.Parameters.AddWithValue("@Birthday", BirthdayEdit.Text);
            sqlCmd.Parameters.AddWithValue("@BirthPlace", BirthPlaceEdit.Text);
            sqlCmd.Parameters.AddWithValue("@PlaceOfStudy", PlaceOfStudyEdit.Text);
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
    }
}

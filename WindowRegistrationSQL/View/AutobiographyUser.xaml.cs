using System;
using System.Collections.Generic;
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

namespace WindowRegistrationSQL.View
{
    /// <summary>
    /// Логика взаимодействия для AutobiographyUser.xaml
    /// </summary>
    public partial class AutobiographyUser : Window
    {
        public AutobiographyUser(object login)
        {

            this.AllowsTransparency = true;
            this.WindowStyle = WindowStyle.None;

            InitializeComponent();

           // string connectionString = @"Data Source=SQL-SERVER; Initial Catalog=DataWindow; Integrated Security = true;";
          string connectionString = @"Data Source=PC_ANC\MSSQL; Initial Catalog=DataWindow; Integrated Security = true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT * FROM RegistrationUser where LoginUser = '{login}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    BiografiaUser.Text = reader["Biographia"].ToString();
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
    }
}

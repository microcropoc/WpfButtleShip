using System;
using System.Collections.Generic;
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

namespace WpfButtleShip
{
    /// <summary>
    /// Interaction logic for WindowTestField.xaml
    /// </summary>
    public partial class WindowTestField : Window
    {
        public WindowTestField(int[,] mat)
        {
            InitializeComponent();
            Refresh(mat);
        }

        public void Refresh(int[,] mat)
        {
            StringBuilder strBilld = new StringBuilder();
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    strBilld.Append(mat[i, j].ToString()+' ');
                }
                strBilld.AppendLine();
            }

            txtMAtrix.Text = strBilld.ToString();
        }
    }
}

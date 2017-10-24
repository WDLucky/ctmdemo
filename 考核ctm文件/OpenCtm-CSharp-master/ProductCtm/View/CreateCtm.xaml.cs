using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductCtm.View
{
    /// <summary>
    /// CreateCtm.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class CreateCtm : Window
    {
        public CreateCtm()
        {
            InitializeComponent();
        }
        private void tb_PreviewText(object sender, TextCompositionEventArgs e)

        {

            Regex re = new Regex("[^1-9]");

            e.Handled = re.IsMatch(e.Text);

        }
    }
}

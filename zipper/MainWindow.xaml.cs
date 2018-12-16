using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Compression;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace zipper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public String path;
        public String filename;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void zip(object sender, RoutedEventArgs e)
        {
            ZipFile.CreateFromDirectory(path,filename);
        }

        private void browse(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                filename = dialog.SafeFileName;
                pathname.Text = dialog.FileName;
            }
        }
    }
}

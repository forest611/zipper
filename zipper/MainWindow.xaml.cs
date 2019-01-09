using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace zipper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public string place; //保存先
        public string zipPath; //圧縮するフォルダのディレクトリ
        public string fileName;
        public PresetSetting set;

        public MainWindow()
        {
            InitializeComponent();
            set = new PresetSetting();
            set.loadPreset(this);
        }


        //圧縮処理
        private void zip(object sender, RoutedEventArgs e)
        {
             
            try
            {
                zipPath = pathname1.GetLineText(0);

                place = pathname2.GetLineText(0) + @"\" + fileName;
                if ((bool)timeCheck.IsChecked) place = place + " " + DateTime.Now.ToString("MMddHHmm");

                ZipFile.CreateFromDirectory(zipPath,place+".zip", CompressionLevel.Optimal,false, Encoding.UTF8);
                MessageBox.Show("ファイルの圧縮が完了しました");
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("圧縮するファイル、圧縮したファイルの保存先を設定してください");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("圧縮先のファイルにアクセスできませんでした");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("圧縮済みの同名ファイルが存在する \n もしくは正しく記述できていない可能性があります");
            }
            place = "";

        }

        //圧縮するフォルダを選択
        private void browse1(object sender, RoutedEventArgs e)
        {
            zipPath = "";
            var openDlg = new CommonOpenFileDialog();
            openDlg.IsFolderPicker = true;
            openDlg.ShowDialog();
            pathname1.Text = openDlg.FileName;
            fileName = System.IO.Path.GetFileName(openDlg.FileName);
        }
        //zipを保存する場所を設定
        private void browse2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog saveArea = new CommonOpenFileDialog();
            saveArea.IsFolderPicker = true;
            saveArea.ShowDialog();
            pathname2.Text = saveArea.FileName;
        }



        private void setPreset(object sender, RoutedEventArgs e)
        {
            set.setPreset(pathname1.GetLineText(0), pathname2.GetLineText(0) + @"\" + fileName, (bool)timeCheck.IsChecked);
            MessageBox.Show("設定を保存しました");
        }

        private void clickPreset(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            try
            {
                ZipFile.CreateFromDirectory(zipPath, place + ".zip", CompressionLevel.Optimal, false, Encoding.UTF8);
            }
            catch (System.ArgumentNullException)
            {
                MessageBox.Show("プリセットの圧縮するファイル、圧縮したファイルの保存先を \n 正しく設定できていません");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("圧縮先のファイルにアクセスできませんでした");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("圧縮済みの同名ファイルが存在する \n もしくはプリセットに正しく記述できていない可能性があります");
            }


        }
    }
} 

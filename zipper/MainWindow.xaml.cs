using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
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
        public Dictionary<string,string[]> presetData = new Dictionary<string,string[]>();

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
            catch (System.ArgumentException)
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
            openDlg.ShowDialog(); //add1.1
            try
            {
                pathname1.Text = openDlg.FileName;
                fileName = System.IO.Path.GetFileName(openDlg.FileName);
            }
            catch (Exception)
            {
            }

        }
        //zipを保存する場所を設定
        private void browse2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog saveArea = new CommonOpenFileDialog();
            saveArea.IsFolderPicker = true;
            saveArea.ShowDialog(); //add 1.1
            try
            {
                pathname2.Text = saveArea.FileName;
            }
            catch (Exception)
            {
            }
        }


        //プリセットを設定
        private void setPreset(object sender, RoutedEventArgs e)
        {
            if (pathname1.GetLineText(0)== string.Empty || pathname2.GetLineText(0) == string.Empty)
            {
                MessageBox.Show("圧縮するフォルダのパス、圧縮したファイルを保存するパスを設定してください");
                return;
            }
                if (presetName.GetLineText(0) == string.Empty)
            {
                MessageBox.Show("プリセット名を設定してください");
                return;
            }
            set.setPreset(pathname1.GetLineText(0), pathname2.GetLineText(0) + @"\" + fileName,presetName.GetLineText(0), (bool)timeCheck.IsChecked);
            set.loadPreset(this);
            MessageBox.Show("設定を保存しました");
        }

        //プリセットをクリック
        private void clickPreset(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string[] data;
            presetData.TryGetValue(button.Name, out data);

            try
            {
                if (data[3].Equals("YES"))
                {
                    data[1] = data[1] + " " + DateTime.Now.ToString("MMddHHmm");
                }
                ZipFile.CreateFromDirectory(data[0], data[1]+ ".zip", CompressionLevel.Optimal, false, Encoding.UTF8);
                MessageBox.Show("ファイルの圧縮が完了しました");

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
            catch (NullReferenceException)//add 1.1
            {
                MessageBox.Show("未設定のプリセットです");
            }


        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            List<string> lines = new List<string>();
            lines.AddRange(File.ReadAllLines(set.presetPath));
            try
            {
                switch (button.Name)
                {
                    case "delete0":
                        lines.RemoveAt(0);
                        break;
                    case "delete1":
                        lines.RemoveAt(1);
                        break;
                    case "delete2":
                        lines.RemoveAt(2);
                        break;
                    case "delete3":
                        lines.RemoveAt(3);
                        break;

                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("未設定のプリセットです");
                return;
            }
            File.WriteAllLines(set.presetPath, lines);
            set.loadPreset(this);
            MessageBox.Show("プリセットを削除しました");
        }
    }
} 

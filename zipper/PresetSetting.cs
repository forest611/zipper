using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace zipper
{
    public class  PresetSetting
    {

        //preset.txtの場所（appのあるディレクトリと同じ)
        public string presetPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\presets.txt";

        public void setPreset(string zipPath,string place,string presetName,bool time)
        {
            using (StreamWriter sw = new StreamWriter(presetPath,true))
            {
                var isTime = "NO";
                if (time)
                {
                    isTime = "YES";
                }
                sw.WriteLine(zipPath + "/" + place + "/"+presetName+"/"+isTime);
                sw.Close();
            }
        }

        public void loadPreset(MainWindow main)
        {
            try
            {
                main.presetData.Clear();


                using (StreamReader sr = new StreamReader(presetPath))
                {
                    string path;
                    for(int i = 0; i !=4;i++)
                    {
                        //if (path == null || path == string.Empty) break;
                        var obj = main.FindName("preset" + i);
                        Button button = (Button)obj;

                        if ((path = sr.ReadLine()) != null)//if can read line.
                        {
                            string[] data = path.Split('/');
                            button.Content = data[2] + "\n" + data[3];

                            //add data
                            main.presetData.Add("preset" + i, data);
                            Console.WriteLine("load preset" + i);
                            continue;
                        }
                        button.Content = "未設定"; //no setting
                    }
                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("create preset.txt");
                StreamWriter sw = new StreamWriter(presetPath, true);
                sw.Close();
            }
        }

    }
}

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

        public void setPreset(String zipPath,String place,bool time)
        {
            using (StreamWriter sw = new StreamWriter(presetPath,true))
            {
                var isTime = "NO";
                if (time)
                {
                    isTime = "YES";
                }
                sw.WriteLine(zipPath + "/" + place + "/"+isTime);
                sw.Close();
            }
        }

        public void loadPreset(MainWindow main)
        {
            try
            {
                using (StreamReader sr = new StreamReader(presetPath))
                {
                    string path;
                    for(int i = 0; (path = sr.ReadLine()) != null;i++)
                    {
                        if (path == null || path == string.Empty) break;
                        var obj = main.FindName("preset" + i);
                        Button b = (Button)obj;
                        string[] data = path.Split('/');
                        b.Content = data[0] + "\n" + data[1] + "\n" + data[2];
                        Console.WriteLine("load preset" + i);
                    }
                }

            }
            catch (FileNotFoundException)
            {
                StreamWriter sw = new StreamWriter(presetPath, true);
                sw.Close();
            }
        }

    }
}

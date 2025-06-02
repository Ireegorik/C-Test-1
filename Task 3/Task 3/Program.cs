using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_3
{

    enum LogFormat
    {
        WithSpace,
        WithPlane,
        Error
    }

    class LogEntry
    {
        public LogFormat format;
        public string input;
        public string output;
        public LogEntry(string input)
        {
            this.input = input;
            bool isOk = false;
            if (input[2] == '.')
            {
                format = LogFormat.WithSpace; isOk = true;
            }
            if (input[4] == '-')
            {
                format = LogFormat.WithPlane; isOk = true;
            }

            /*
            string[] format1 = input.Split(' '); 
            if(format1.Length == 6)
            {
                format = LogFormat.WithSpace; isOk = true;
            }
            format1 = input.Split('|');
            if (format1.Length == 5)
            {
                format = LogFormat.WithPlane; isOk = true;
            }
            */
            if (isOk == false) 
                format = LogFormat.Error;
            getOutput();
        }
        private void getOutput()
        {
            string Method = "DEFAULT";
            //2025-03-10    15:14:49.523    INFO    DEFAULT Версия программы: '3.4.0.48729'

            switch (format)
            {
                case LogFormat.WithSpace: // 10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'
                    {
                        string[] format1 = input.Split(' ');
                        bool isOk = false;
                        if (format1[2].ToUpper().Contains("INFO")) { format1[2] = "INFO"; isOk = true; }
                        if (format1[2].ToUpper().Contains("WARN")) { format1[2] = "WARN"; isOk = true; }
                        if (format1[2].ToUpper().Contains("ERROR")) { format1[2] = "ERROR"; isOk = true; }
                        if (format1[2].ToUpper().Contains("DEBUG")) { format1[2] = "DEBUG"; isOk = true; }
                        if (isOk == false)
                        {
                            format = LogFormat.Error;
                            getOutput();
                            return;
                        }
                        DateTime date = DateTime.ParseExact(format1[0], "dd.MM.yyyy", null);
                        string formattedDate = date.ToString("yyyy-MM-dd");
                        output = formattedDate + "\t" + format1[1] + "\t" + format1[2] + "\t" + Method + "\t";
                        for (int i = 3; i < format1.Length; i++)
                        {
                            output += format1[i] + " ";
                        }


                    }
                    break;
                case LogFormat.WithPlane: //2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'
                    {
                        string[] format1 = input.Split('|');
                        bool isOk = false;
                        Method = format1[3];
                        if (format1[1].ToUpper().Contains("INFO")) { format1[1] = "INFO"; isOk = true; }
                        if (format1[1].ToUpper().Contains("WARN")) { format1[1] = "WARN"; isOk = true; }
                        if (format1[1].ToUpper().Contains("ERROR")) { format1[1] = "ERROR"; isOk = true; }
                        if (format1[1].ToUpper().Contains("DEBUG")) { format1[1] = "DEBUG"; isOk = true; }
                        if (isOk == false)
                        {
                            format = LogFormat.Error;
                            getOutput();
                            return;
                        }
                        string[] subFormat = format1[0].Split(' ');
                        output = subFormat[0] + "\t" + subFormat[1] + "\t" + format1[1] + "\t" + Method + "\t";
                        for (int i = 4; i < format1.Length; i++)
                        {
                            output += format1[i] + " ";
                        }
                    }
                    break;
                case LogFormat.Error:
                    output = input;
                    StreamWriter f = new StreamWriter("problems.txt", true);
                    f.WriteLine(output);
                    f.Close();
                    break;
            }
        }   

    }


    class Program
    {
        static List<LogEntry> logs;
        static string PathToLog = System.Environment.CurrentDirectory + "\\Log.txt";
        static void Main(string[] args)
        {
            if(File.Exists("problems.txt"))
            {
                File.Delete("problems.txt");
            }
            Console.WriteLine("Запущено чтение файла " + PathToLog);
            Console.WriteLine();
            logs = new List<LogEntry>();
            string s;
            StreamReader f = new StreamReader(PathToLog);
            while ((s = f.ReadLine()) != null)
            {
                logs.Add(new LogEntry(s));
            }
            f.Close();
            Console.WriteLine("Чтение файла " + PathToLog + " завершено.");
            Console.WriteLine();

            Console.WriteLine("Стандартизированный лог-файл:");
            Console.WriteLine();
            foreach (LogEntry lg in logs)
            {
                if(lg.format != LogFormat.Error)
                Console.WriteLine(lg.output);

            }
            Console.ReadKey();

        }
    }
}

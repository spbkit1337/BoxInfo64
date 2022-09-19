using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



//мои юзынги =)

using System.Management; // нужна чтобы обращаться к wmi чтобы через нее получать инфу по железу

using System.IO; // тоже нажна для получения инфы по железу


using System.Net; //нужна для получения инфы о ip

//Самая главная библеотека блогадаря которой возможно получить данные о температуре(и не только) процессора , видеокарты , материнки и т.д
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;

using ConsoleApp1; //имя библеотеки чтобы посмотреть ее код зайди в файл Monitorinfo.cs
using System.Security.Policy;

//вот ссылка на эту библеотеку https://gist.github.com/grandsilence/cd7ce9d8bf87a5414b685e3e32542dd3




namespace BoxInfo64
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000; //тут создаю интервал в секундах для того чтобы обновлялся label с надпесью "свободная память"     1 секунда!!!!
            timer1.Start(); //включаю таймер при загрузки формы то есть программы



            timer3.Interval = 1000; //я создал timer3  чтобы каждую 1 сек обновлялся label с температурой
            timer3.Start(); //включаю таймер при загрузки формы то есть программы

            getdrive();  //HDD и SSD нужно для получения инфы о разделах 

        }


        //HDD и SSD 
        private void getdrive() //вообще он нужен для вывода инфы в combo box но сюда я решил еще впихнуть инфу в list box чтобы не засорять код в  form load
        {

            //для вывода в combobox1 разделов диска  например c:/
            string[] driv = Directory.GetLogicalDrives();
            foreach (string item in driv)
            {
                comboBox1.Items.Add(item.ToString());
            }

            //Вывод модели жесткого диска например toshiba

            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject moDisk in mosDisks.Get())
            {
                listBox1.Items.Add(moDisk["Model"].ToString());

            }

            //Вывод размера
            ManagementObjectSearcher mosSizes = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject moSizes in mosSizes.Get())
            {

                var totalsize = Math.Round(System.Convert.ToDouble(moSizes["Size"]) / 1024 / 1024 / 1024);
                listBox2.Items.Add(totalsize.ToString() + " ГБ");
            }




            ////вывод раздела диска например c:/

            //ManagementObjectSearcher path = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            //foreach (ManagementObject p in path.Get())
            //{
            //    var path_disk = p["Caption"].ToString();
            //    //var text = "Раздел ";

            //    listBox3.Items.Add(path_disk).ToString();

            //}




        }

          


       

        private void Form1_Load(object sender, EventArgs e)
        {
            //Если прога не сможет найти какой-то элемент в пк или ноуте то эти label которые я ниже перечислил будут пусты
            label21.Text = ""; //модель монитора
            label48.Text = ""; //разрешение монитора
            label18.Text = ""; //модель материнки




            //HDD и SSD тут обьявляю пустой текст чтобы при загрузке проги не было label 
            label38.Text = "";
            label39.Text = "";
            label40.Text = "";
            label41.Text = "";
            label42.Text = "";

            //здесь я присвоил пустые значения для того чтобы при загрузке формы не было надписей label которые выдводят температуру :)
            label45.Text = "";
            label46.Text = "";




            //ОС
            foreach (var mo in new ManagementObjectSearcher("root\\cimv2", "select * from Win32_OperatingSystem").Get())
            {
                label3.Text = (string)mo["Caption"]; //версия ОС
                label5.Text = (string)mo["OSArchitecture"]; //разрядность x32 или x64
            }



            //процессор
            foreach (var mo in new ManagementObjectSearcher("root\\cimv2", "select * from win32_Processor").Get())
            {
                label8.Text = (string)mo["Name"]; //названия процессора

                label12.Text = mo["MaxClockSpeed"].ToString() + " " + " МГц"; //макс. частота


            }







            ////видеокарта
            foreach (var mo in new ManagementObjectSearcher("root\\cimv2", "select * from win32_VideoController").Get())
            {
                label15.Text = (string)mo["name"]; //названия видюхи



            }

            //монитор тут я юзаю сторонюю библеотеку
            var monitors = MonitorsInfo.GetMergedFriendlyNames();
            foreach (var monitor in monitors)
            {
                label21.Text = (string)monitor;
            }
                
            //этот код (WMI)тоже выводит модель монитора но например в виртуалке или на ноутбуке  будет ошибка поэтому проще юзать библеотеку(на основном пк проблем нет)
            // монитор
            //foreach (var monik in new ManagementObjectSearcher("root\\wmi", "select * from WmiMonitorID").Get())
            //{

            //    foreach (var monitor in (ushort[])monik["UserFriendlyName"])
            //    {
            //        label21.Text += (char)monitor;
            //    }

            //}


            //// разрешие экрана
            var scope = new ManagementScope();
            scope.Connect();

            var query = new ObjectQuery("SELECT * FROM Win32_VideoController");

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                var results = searcher.Get();
                foreach (var result in results)
                {

                    label48.Text = result.GetPropertyValue("CurrentHorizontalResolution").ToString() + " x " + result.GetPropertyValue("CurrentVerticalResolution").ToString();
                }
            }



            //Тут код материнки первый вариант работает почему то не всегда
            //Материнка
            //foreach (var mo in new ManagementObjectSearcher("root\\cimv2", "select * from Win32_BaseBoard").Get())
            //{
            //    label18.Text = (string)mo["manufacturer"] + (string)mo["product"];
            //}

            
            //Тут я юзаю отдельную библеотеку https://drive.google.com/drive/folders/1--GMIwuge6KdexB5CxqJG6LNSibgBjGL
            //это код тоже выводит материнку при этом работает всегда 

            //Материнка
            label18.Text = HardwareInfo.GetBoardMaker() + " " + HardwareInfo.GetBoardProductId();



        }



        //оперативка
        private void timer1_Tick(object sender, EventArgs e)
        {

                //здесь я сделал таймер в котором каждую секунду будет обновляться значение оперативной памяти преимущественно сколько свободна


                ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
                ManagementObjectCollection results = searcher.Get();


                foreach (ManagementObject result in results)
                {

                    var totalram = Math.Round(System.Convert.ToDouble(result["TotalVisibleMemorySize"]) / 1024 / 1024);

                    label31.Text = totalram.ToString() + " " + " ГБ"; //общее количество памяти


                    var freeram = Math.Round(System.Convert.ToDouble(result["FreePhysicalMemory"]) / 1024 / 1024, 2);

                    label33.Text = freeram.ToString() + " " + " ГБ"; //память которая не занята то есть свободна

                }          
        }



        //температура

        //тут создаю переменные с плавающей запятой (float) для процессора и видюхи

        // CPU Temperature
        static float cpuTemp;

        // GPU Temperature
        static float gpuTemp;

        private void timer3_Tick(object sender, EventArgs e)
        {
            //определяем новый объект компьютера, предоставляемый библиотекой DLL Open Hardware Monitor
            Computer myComputer = new Computer();


            //Эти параметры указывают библиотеке Open Hardware Monitor возвращать данные для любых процессоров или видеокарт, подключенных в данный момент к системе
            myComputer.GPUEnabled = true;

            myComputer.CPUEnabled = true;


            //устанавливаем соединение между  кодом и файлом DLL
            myComputer.Open();



            //CPU процессор (с проверкой больше и меньше чтобы менять цвет)
            foreach (var hardware in myComputer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            //присвавываем  переменной число с датчика
                            cpuTemp = sensor.Value.GetValueOrDefault();

                            //выводим в label
                            //тут условие если температура меньше 50 то надпись будет зеленой
                            if (cpuTemp < 50)
                            {
                                label45.ForeColor = Color.Lime;
                                label45.Text = "" + cpuTemp.ToString() + " °C";
                            }
                            else if (cpuTemp > 50) //если температура больше 50 то надпись будет красной
                            {
                                label45.ForeColor = Color.Red;
                                label45.Text = "" + cpuTemp.ToString() + " °C";
                            }

                        }
                    }
                }
            }



            //GPU видеокарта (с проверкой больше и меньше чтобы менять цвет)
            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            //присвавываем  переменной число с датчика
                            gpuTemp = sensor.Value.GetValueOrDefault();

                            //выводим в label
                            //тут условие если температура меньше 50 то надпись будет зеленой
                            if (gpuTemp < 50)
                            {
                                label46.ForeColor = Color.Lime;
                                label46.Text = "" + gpuTemp.ToString() + " °C";
                            }
                            else if (gpuTemp > 50) //если температура больше 50 то надпись будет красной
                            {
                                label46.ForeColor = Color.Red;
                                label46.Text = "" + gpuTemp.ToString() + " °C";
                            }

                        }
                    }
                }
            }

        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

                //HDD и SSD 
                //тут выводится инфа по label смотря какой раздел ты выбрал 



                string getinfo = comboBox1.SelectedItem.ToString();
                DriveInfo drive = new DriveInfo(getinfo);


                label38.Text = drive.VolumeLabel; //не выводит модель , а имя тома
                label39.Text = drive.Name;
                label40.Text = drive.DriveFormat;
                label41.Text = drive.TotalSize / 1024 / 1024 / 1024 + " " + "ГБ";
                //label20.Text = drive.TotalFreeSpace / 1024 / 1024 / 1024 + " " + "ГБ";

                var totalfree = Math.Round(System.Convert.ToDouble(drive.TotalFreeSpace) / 1024 / 1024 / 1024, 2).ToString();
                label42.Text = totalfree + " " + "ГБ";
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
                //По нажатию на эту кнопку список жестких дисков обновится


                // Сначало просто очистится все элемент лист боксов и комбо бокса
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                comboBox1.Items.Clear();

                //а затем я вызову функцию которую писал раньше чтобы не копировать один и тот же код.И эта функция снова выведет элементы.Таким образом обновятся данные
                getdrive();

      
        }







        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Form2 f = new Form2();
                f.Show();     
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
                System.Windows.Forms.Application.Exit();
        }


    }
}

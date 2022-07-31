using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;        //추가
 

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        //파일 드래그해서 
        private string stFolderPath1;
        private System.IO.DirectoryInfo di;


        //그래프
        int Min = Int32.MaxValue;
        int Max = Int32.MinValue;
        int Min2 = Int32.MaxValue;
        int Max2 = Int32.MinValue;
        double[] ecg = new double[100000];
        double[] ppg = new double[100000];
        private int ecgLength;
        private int ppgLength;
        private int Tick = 0;
        private bool isTimerOn = true;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Using Chart Control"; //From의 이름을 설정
            textBox1.AllowDrop = true;
        }




        //button 눌렀을때 label에 있는 text값이 바뀌는 code 
        private void button1_Click_1(object sender, EventArgs e)
        {
            lblHelloWorld.Text = "Hello World!";
        }

        private void lblHelloWorld_Click(object sender, EventArgs e)
        {

        }
        //button 눌렀을때 label에 있는 text값이 바뀌는 code 

        //chart 그리기
        private void chart1_Click_1(object sender, EventArgs e)
        {

        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            /*
            // Series 삭제
            chart1.Series.Clear();

            // 객체 생성
            Series Chart1 = chart1.Series.Add("Series1");
            Series Chart2 = chart1.Series.Add("Series2");

            // 타이틀 객체 생성
            Title title = new Title();

            title.Text = "타이틀 제목";
            title.ForeColor = Color.Blue;
            title.Font = new Font("맑은고딕", 25, FontStyle.Bold);
            chart1.Titles.Add(title);

            // 범례 설정
            Chart1.LegendText = "데이터 1";
            Chart2.LegendText = "데이터 2";

            // 차트 종류 설정
            Chart1.ChartType = SeriesChartType.Line;   // 선
            Chart2.ChartType = SeriesChartType.Point;  // 점

            // 차트 색상 설정
            Chart1.Color = Color.LightGreen;
            Chart2.Color = Color.Red;

            // 차트 굵기 설정
            Chart1.BorderWidth = 5;

            // 범례1 데이터
            for (double i = 0; i < 2 * Math.PI; i += 0.1)
            {
                Chart1.Points.AddXY(i, Math.Sin(i));
            }

            // 범례2 데이터
            for (double i = 0; i < 2; i += 0.1)
            {
                Chart2.Points.AddXY(i, i);
            }
            */

            ecgppgRead();
            chart2.Series["ecg"].BorderWidth = 1;
            chart2.Series["ppg"].Color = Color.Red;
            for (int x = 0; x < ecgLength; x += 1)
            {
                chart2.Series["ecg"].Points.AddXY((double)x, ecg[x]);
            }
            for (int x = 0; x < ppgLength; x += 1)
            {
                chart2.Series["ppg"].Points.AddXY((double)x, ppg[x]);
            }
            Controls.Add(chart2);
        }//chart 그리기

        //textbox에 폴더를 드래그하면 listbox에 ('.txt'파일 형식의 파일들을 lsit로 보여줌)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = stFolderPath1;

            di = new System.IO.DirectoryInfo(stFolderPath1);
            
            foreach(System.IO.FileInfo File in di.GetFiles())
            {
                if(File.Extension.ToLower().CompareTo(".txt")==0)
                {
                    string FileNameOnly = File.Name.Substring(0, File.Name.Length);
                    listBox1.Items.Add(FileNameOnly);
                }
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                var directoryName = (string[])e.Data.GetData(DataFormats.FileDrop);
                stFolderPath1 = directoryName[0];
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //textbox에 폴더를 드래그하면 listbox에 ('.txt'파일 형식의 파일들을 lsit로 보여줌)


        //file의 값을 읽어오는 코드
        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader SR = new StreamReader("C:\\Users\\dydrk\\source\\repos\\WindowsFormsApp3\\WindowsFormsApp3\\test_log.txt");    //읽어들일 TXT 파일의 경로를 
                                                                    //매개변수로 StreamReader 생성

            string line;                                            //한 줄씩 읽은 후, 그 값을 저장시킬 변수
            bool check = false;
            string result = "";                                     //전체 라인을 저장시킬 변수
            while ((line = SR.ReadLine()) != null)                  //line변수에 SR에서 한줄을 읽은 걸 저장, 읽은 줄이 null이 아닐때까지 반복
            {
                if (check == false && line.StartsWith("network statistics:")==true)
                {
                    check = true;
                }
                else if(check==true && line.StartsWith("packet wakeup events:")!=true)
                {
                    result += line;                                     //전체 라인 변수에 그 값을 저장
                    result += "\r\n";
                }//표출을 위해서 추가
                else if(line.StartsWith("packet wakeup events:") == true)
                {
                    break;
                }
            }

            textBox2.Text = result;                                 //textbox폼에 전체 읽은 문구를 표출
            SR.Close();                                             //StreamReader를 닫아줌
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader SR = new StreamReader("writeTest.txt");    //읽어들일 TXT 파일의 경로를 
                                                                    //매개변수로 StreamReader 생성

            string result = "";                                     //읽은 TXT파일을 저장시킬 변수
            result = SR.ReadToEnd();                                //처음부터 끝까지 읽은 후 저장

            textBox2.Text = result;                                 //textbox폼에 전체 읽은 문구를 표출
            SR.Close();                                             //StreamReader를 닫아줌
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        //file의 값을 읽어오는 코드


        //txt 파일을 읽어서 그래프 그리기
        private void chart2_Click(object sender, EventArgs e)
        {
            if (isTimerOn)
            {
                timer1.Stop(); isTimerOn = false;
            }
            else
            {
                timer1.Start();
                isTimerOn = true;
            }
        }
        private void ecgppgRead()
        {
            string filename = @"C:\\Users\\dydrk\\source\\repos\\WindowsFormsApp3\\WindowsFormsApp3\\a101.txt";
            string[] lines = System.IO.File.ReadAllLines(filename);
            string filename2 = @"C:\\Users\\dydrk\\source\\repos\\WindowsFormsApp3\\WindowsFormsApp3\\PPG.txt";
            string[] lines2 = System.IO.File.ReadAllLines(filename2);
            int i = 0;
            foreach (string line in lines)
            {
                ecg[i] = Convert.ToDouble(line);
                if (ecg[i] < Min)
                    Min = Convert.ToInt32(ecg[i]);
                if (ecg[i] > Max)
                    Max = Convert.ToInt32(ecg[i]); i++;
            }
            ecgLength = i;
            Console.WriteLine("Min = {0}, Max ={1}", Min, Max);
            i = 0;
            foreach (string line in lines2)
            {
                ppg[i] = Convert.ToDouble(line);
                if (ppg[i] < Min2) Min2 = Convert.ToInt32(ppg[i]);
                if (ppg[i] > Max2) Max2 = Convert.ToInt32(ppg[i]);
                i++;
            }
            ppgLength = i;
            Console.WriteLine("Min2 = {0}, Max2 = {1}", Min2, Max2);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ecgChartSetting(Tick * 10);
            Tick += 1;
        }
        private void ecgChartSetting(int minX)
        {
            chart2.ChartAreas["ChartArea1"].AxisX.Minimum = minX;
            chart2.ChartAreas["ChartArea1"].AxisX.Maximum = minX + 1500;
            chart2.ChartAreas["ChartArea1"].AxisY.Minimum = Min / 100 * 100;
            chart2.ChartAreas["ChartArea1"].AxisY.Maximum = Max / 100 * 100;
            chart2.ChartAreas["ChartArea2"].AxisX.Minimum = minX;
            chart2.ChartAreas["ChartArea2"].AxisX.Maximum = minX + 1500;
            chart2.ChartAreas["ChartArea2"].AxisY.Minimum = Min2;
            chart2.ChartAreas["ChartArea2"].AxisY.Maximum = Max2; ;
        }
    }
}

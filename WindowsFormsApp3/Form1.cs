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


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private string stFolderPath1;
        private System.IO.DirectoryInfo di;

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
        }//chart 그리기

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
    }
}

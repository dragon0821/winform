using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    static class Program
    {
        // 단일 스레드 아파트 먼트(Single-threaded apartment) 어노테이션 설정
        [STAThread]
        //실행함수
        static void Main()
        {
            //이건 Visual 스타일의 설정하는 것, 즉 설정하지 않으면 조금 옛날 분위기의 윈도우 프로그램이 생성
            Application.EnableVisualStyles();
            //이건 Text 랜더링에 대한 설정인데, 문자에 대한 간격 설정과 기타 등등의 설정, Default는 false로 설정
            Application.SetCompatibleTextRenderingDefault(false);
            //윈도우 프로그램의 메시지를 돌리는 함수
            Application.Run(new Form1());
        }
    }
}

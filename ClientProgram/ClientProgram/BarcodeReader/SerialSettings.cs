using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.IO.Ports;

namespace ClientProgram
{
    public class SerialSettings
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        // SerialSettings의 다른 Instance를 만들 수 없도록 private로 표시
        private SerialSettings() { }

        // 서로 다른 클래스에서의 Property 접근을 위한 Instance 선언
        public static readonly SerialSettings Instance = new SerialSettings();
    }
}

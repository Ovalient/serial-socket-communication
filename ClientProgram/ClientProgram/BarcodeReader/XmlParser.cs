using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace ClientProgram
{
    class XmlParser
    {
        // Xml 파일 위치
        string path = Environment.CurrentDirectory + "\\BarcodeConfig.xml";

        /// <summary>
        /// 시리얼 포트 값 세팅
        /// </summary>
        public void SerialPortSettings()
        {
            XElement xElem = XElement.Load(path);

            var result = from xe in xElem.Elements("SerialPort")
                         select xe;

            var barcode = result.FirstOrDefault();

            if(barcode != null)
            {
                // Xml 파싱한 결과가 string이므로 각 개체로 변환
                string portName = barcode.Element("PortName").Value;
                int baudRate = Convert.ToInt32(barcode.Element("BaudRate").Value);
                Parity parity = (Parity)Enum.Parse(typeof(Parity), barcode.Element("Parity").Value);
                int dataBits = Convert.ToInt32(barcode.Element("DataBits").Value);
                StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), barcode.Element("StopBits").Value);

                SerialSettings.Instance.PortName = portName;
                SerialSettings.Instance.BaudRate = baudRate;
                SerialSettings.Instance.Parity = parity;
                SerialSettings.Instance.DataBits = dataBits;
                SerialSettings.Instance.StopBits = stopBits;
            }
        }

        /// <summary>
        /// 모델 이름 가져오기
        /// </summary>
        /// <param name="barcodeNum"></param>
        /// <returns></returns>
        public string GetModelName(string barcodeNum)
        {
            string modelName = "";
            XElement xElem = XElement.Load(path);

            var result = from xe in xElem.Elements("Barcode")
                         where xe.Attribute("Id").Value == barcodeNum
                         select xe;

            var barcode = result.FirstOrDefault();
            if(barcode != null)
            {
                modelName = barcode.Element("Name").Value;
            }
            return modelName;
        }

        public string XmlParser_SetPath()
        {
            return path;
        }
    }
}

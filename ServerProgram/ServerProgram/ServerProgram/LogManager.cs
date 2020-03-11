using System;
using System.Diagnostics;
using System.IO;

namespace ServerProgram
{
    class LogManager : TraceListener
    {
        /// <summary>
        /// 파일 이름
        /// </summary>
        private readonly string filename;

        /// <summary>
        /// 현재 시간
        /// </summary>
        private DateTime time;

        /// <summary>
        /// 스트림 쓰기용 버퍼
        /// </summary>
        private StreamWriter trace;

        /// <summary>
        /// 파일 이름
        /// </summary>
        /// <param name="filename"></param>
        public LogManager(string filename)
        {
            string path;
            path = Environment.CurrentDirectory + "\\Logs\\";
            DirectoryInfo di = new DirectoryInfo(path);

            if (!di.Exists)
                di.Create();

            this.filename = filename;
            this.trace = new StreamWriter(path + this.GenerateFilename(), true)
            {
                AutoFlush = true
            };
        }

        /// <summary>
        /// 쓰기 관련
        /// </summary>
        /// <param name="filename"></param>
        public override void Write(string msg)
        {
            this.CheckRollover();
            if(this.trace.BaseStream.CanWrite)
            {
                this.trace.Write(msg);
            }
        }

        /// <summary>
        /// 쓰기 라인 관련
        /// </summary>
        /// <param name="msge"></param>
        public override void WriteLine(string msg)
        {
            this.CheckRollover();
            if(this.trace.BaseStream.CanWrite)
                this.trace.WriteLine(DateTime.Now + " : " + DateTime.Now.Millisecond + " - " + msg);
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                this.trace.Close();
            }
        }
        
        /// <summary>
        /// 날짜 체크
        /// </summary>
        private void CheckRollover()
        {
            if(this.time.CompareTo(DateTime.Today) != 0)
            {
                this.trace.Close();
                this.trace = new StreamWriter(this.GenerateFilename(), true)
                {
                    AutoFlush = true
                };
            }
        }

        /// <summary>
        /// 파일 이름 만들기
        /// </summary>
        /// <returns></returns>
        private string GenerateFilename()
        {
            this.time = DateTime.Today;

            string name = Path.Combine(
                Path.GetFileNameWithoutExtension(this.filename) + "_" +
                this.time.ToString("yyyy-MM-dd") + Path.GetExtension(this.filename) + ".txt");

            return name;
        }
    }
}

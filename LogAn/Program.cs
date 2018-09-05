using System;

namespace LogAn
{
    internal class Program
    {
        private static void Main(string[] args)
        {

        }
    }

    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; set; } = false;
        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName must be norm");
            }

            if (fileName.EndsWith(".SLF"))
            {
                WasLastFileNameValid = true;
                return true;
            }

            return false;
        }
    }

    public class MemCalculator
    {
        private int _sum = 0;

        public void Add(int number)
        {
            _sum += number;
        }

        public int Sum()
        {
            int temp = _sum;
            _sum = 0;
            return temp;
        }

    }
}

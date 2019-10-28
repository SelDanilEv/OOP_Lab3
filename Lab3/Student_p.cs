using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
     public partial class Student
    {
        private int _money;
        private readonly string _name;
        private short _retakes;
        private static bool yeap = true;

        private int xxx;

        public int Onlyget
        {
            get { return xxx; }
        }

        public int Onlyset
        {
            set { if (value != 0) xxx = value; }
        }

        public int _Money
        {
            get { return _money; }
            set { if (value >= 0) _money = value; }
        }

        public static int countOfStudents = 0;

        static public int CountOfStudents()
        {
            return countOfStudents++;
        }

        public void WithRef(ref int a,out int cc)
        {
            cc = a*a;
        }
        

        public string GetName() => _name;
        public int GetMoney() => _money;
        public short GetRetakes() => _retakes;
        public bool GetYeap() => yeap;
    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Student sanyok1 = new Student("Sanyok",100,4);
            Console.WriteLine($"\nИмя студента : {sanyok1.GetName()}");
            Console.WriteLine($"Активы : {sanyok1.GetMoney()}");
            Console.WriteLine($"Пересдачи : {sanyok1.GetRetakes()}\n");
            Student sanyok2 = new Student();
            Console.WriteLine($"\nИмя студента : {sanyok2.GetName()}");
            Console.WriteLine($"Активы : {sanyok2.GetMoney()}");
            Console.WriteLine($"Пересдачи : {sanyok2.GetRetakes()}\n");
            Student sanyok3 = new Student();
            Console.WriteLine($"\nИмя студента : {sanyok3.GetName()}");
            Console.WriteLine($"Активы : {sanyok3.GetMoney()}");
            Console.WriteLine($"Пересдачи : {sanyok3.GetRetakes()}\n");

            Console.WriteLine($"1 EQUAL 2 ? : {sanyok1.Equals(sanyok2)}");
            Console.WriteLine($"2 EQUAL 3 ? : {sanyok2.Equals(sanyok3)}");

            Console.WriteLine($"HashCode 1 : {sanyok1.GetHashCode()}");
            Console.WriteLine($"HashCode 2 : {sanyok2.GetHashCode()}");


            Student copy1 = new Student("Copy1", 1000, 5);

            Student copystudent=new Student(copy1);

            OutputS.Output(sanyok1);
            OutputS.Output(sanyok2);
            OutputS.Output(copy1);
            OutputS.Output(copystudent);

            var man = new {Name= "Sasha",money= 125,retakes= 12 };
            Console.WriteLine(man.ToString());
            Console.WriteLine(man.GetType());

            Console.WriteLine("----------------------------------------");
            myList<string> lst = new myList<string>();
            lst.addItem("Sanya1");
            lst.addItem("Sanya2");
            lst.addItem("Sanya3");

            lst.printAll();

            lst.removeItem("Sanya2");

            Console.WriteLine("\nLST first:");
            lst.printAll();

            Console.WriteLine($"\nBool метод для Sanya2 : {lst.attend("Sanya2")}\n");
            myList<string> lst2 = new myList<string>();
            lst2.addItem("Misha1");
            lst2.addItem("Misha2");
            lst2.addItem("Sanya2");
            lst2.addItem("Sanya1");
            lst2.addItem("Misha3");

            Console.WriteLine("\nLST second:");
            lst2.printAll();

            lst.Merge(lst2);

            Console.WriteLine("\nAfter merge:");
            lst.printAll();


            Singleton<int> singleton = new Singleton<int>();
            singleton.addItem(25);
            singleton.addItem(15);

            Console.WriteLine("\nSingleton : ");
            singleton.printAll();
            Singleton<int> singleton2 = new Singleton<int>();
            Console.WriteLine("\nSingleton2 : ");
            singleton2.printAll();
            singleton.addItem(100);
            Console.WriteLine("\nAfter add to first. The second: ");
            singleton2.printAll();


            Console.ReadLine();
        }
    }

    public class myList<T>
    {
        private T[] items = new T[0];

        public void addItem(T item)
        {
            Array.Resize(ref items, items.Count() + 1);
            items[items.Count() - 1] = item;
        }

        public void removeItem(T item)
        {
            items[Array.IndexOf(items, item)] = items[items.Count() - 1];
            Array.Resize(ref items, items.Count() - 1);
        }

        public IEnumerable<T> getItems()
        {
            return items;
        }

        public void printAll()
        {
            Console.WriteLine();
            foreach (var oneitem in getItems())
            {
                Console.WriteLine(oneitem);
            }
            Console.WriteLine();
        }

        public bool attend(T item)
        {
            bool rc=false;
            foreach (var oneitem in getItems())
            {
                if (item.ToString() == oneitem.ToString()) rc = true;
            }
            return rc;
        }

        public void Merge(myList<T> second)
        {
            foreach (var oneitem in second.getItems())
            {
                if (!(this.attend(oneitem))) this.addItem(oneitem);
            }
        }
    }

    public partial class Student : IDisposable
    {
        public Student()
        {
            _name = "Sanya";
            _money = 50;
            _retakes = 0;
        }

        static Student()
        {
            Console.WriteLine("Static constructor");
            CountOfStudents();
        }


        ~Student()
        {
            Console.WriteLine("I am distructor");
        }

        public Student(Student std)
        {
            _name = std._name;
            _money = std._money;
            _retakes = std._retakes;
            CountOfStudents();
        }


        public Student(string name,int money, short retakes)
        {
            _name = name;
            _money = money;
            _retakes = retakes;
        }

        public bool Equals(Student std)
        {
            if (_money == std.GetMoney() && _name == std.GetName() && _retakes == std.GetRetakes())
                return true;
            else return false;
        }

        public override int GetHashCode()
        {
            int hash = _name.GetHashCode() + _money.GetHashCode() + _retakes.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            string str = "Name: " + _name + ",money: " + _money + ",retakes: "+_retakes+'.';
            return str;
        }


        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
    }

    static class OutputS
    {
        public static void Output(Student std)
        {
            Console.WriteLine();
            Console.WriteLine(std.ToString());
            Console.WriteLine(std.GetYeap());
        }
    }
    
    class Singleton<T>:IDisposable
    {
        private static Singleton<T> instance;

        private static T[] items = new T[0];
        public void addItem(T item)
        {
            Array.Resize(ref items, items.Count() + 1);
            items[items.Count() - 1] = item;
        }

        public void removeItem(T item)
        {
            items[Array.IndexOf(items, item)] = items[items.Count() - 1];
            Array.Resize(ref items, items.Count() - 1);
        }

        public IEnumerable<T> getItems()
        {
            return items;
        }

        public Singleton()
        { }

        public static Singleton<T> getInstance()
        {
            if (instance == null)
                instance = new Singleton<T>();
            return instance;
        }

        public void printAll()
        {
            Console.WriteLine();
            foreach (var oneitem in getItems())
            {
                Console.WriteLine(oneitem);
            }
            Console.WriteLine();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

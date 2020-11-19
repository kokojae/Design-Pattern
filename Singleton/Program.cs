// 참고 : https://gmlwjd9405.github.io/2018/07/06/singleton-pattern.html

using System;
using System.Threading;

namespace Singleton
{
    class Program
    {
        private static readonly int THREAD_NUM = 5;

        public static void Main(string[] args)
        {
            UserThread[] user = new UserThread[THREAD_NUM];
            ThreadStart[] ts = new ThreadStart[THREAD_NUM];
            Thread[] thread = new Thread[THREAD_NUM];
            for (int i = 0; i < THREAD_NUM; i++)
            {
                // UserThread 인스턴스 생성
                user[i] = new UserThread((i + 1).ToString());
                ts[i] = new ThreadStart(user[i].Run);
                thread[i] = new Thread(ts[i]);
                thread[i].Name = user[i].name;
                thread[i].Start();
            }

            for (int i = 0; i < THREAD_NUM; i++)
                thread[i].Join();
        }
    }

    //// 정적 변수에 인스턴스를 만들어 바로 초기화 하는 방법
    //public class Printer
    //{
    //    // static 변수에 외부에 제공할 자기 자신의 인스턴스를 만들어 초기화
    //    private static Printer printer = new Printer();
    //    private Printer() { }

    //    // 자기 자신의 인스턴스를 외부에 제공
    //    public static Printer GetPrinter()
    //    {
    //        return printer;
    //    }

    //    public void Print(String str)
    //    {
    //        Console.WriteLine(str);
    //    }
    //}

    //// JAVA에서 인스턴스를 만드는 메서드에 동기화 하는 방법
    //// C#에서는 Monitor나 lock를 이용하여 블럭단위로
    //// 임계구역을 설정한다.
    //public class Printer
    //{
    //    // 외부에 제공할 자기 자신의 인스턴스
    //    private static Printer printer = null;
    //    private int counter = 0;
    //    private static readonly object thisLock = new object();

    //    private Printer() { }

    //    // 자바에선 인스턴스를 만드는 메서드를
    //    // synchronized로 동기화가 가능하지만 (임계 구역)
    //    // C#은 lock이나 Monitor를 이용하여 구간을 잠글 수 있다.
    //    public static Printer GetPrinter()
    //    {
    //        lock (thisLock)
    //        {
    //            if (printer == null)
    //            {
    //                printer = new Printer(); // Printer 인스턴스 생성
    //            }
    //            return printer;
    //        }
    //    }

    //    public void Print(string str)
    //    {
    //        // 오직 하나의 스레드만 접근을 허용함 (임계 구역)
    //        // 성능을 위해 필요한 부분만을 임계 구역으로 설정한다.
    //        lock (thisLock)
    //        {
    //            counter++;
    //            Console.WriteLine(str + counter);
    //        }
    //    }
    //}

    // 정적 클래스
    // 정적 메서드로만 이루어진 정적 클래스를 사용하면
    // 싱글턴과 동일한 효과를 얻을 수 있다.
    public class Printer
    {
        private static int counter = 0;
        private static readonly object thisLock = new object();

        // 매서드 동기화 (임계구역)
        // 메서드의 모든 부분을 lock 블럭으로 감싸 메서드 전체를 동기화 시킴
        public static void Print(string str)
        {
            lock (thisLock)
            {
                counter++;
                Console.WriteLine(str + counter);
            }
        }
    }

    public class UserThread
    {
        public string name { get; private set; }

        // 스레드 생성
        public UserThread(string name) { this.name = name; }
        // 현재 스레드 이름 출력
        public void Run()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
        }
    }

    //// Enum 클래스
    //// Thread-safety와 Serialization이 보장된다.
    //// Reflection을 통한 공격에도 안전하다.
    //// 따라서 Enum을 이용해서 Singleton을 구현하는 것이 가장 좋은 방법이다.
    //// 라고 하는데 C#과 JAVA의 enum이 달라서 C#에선 불가능한 방법임
    //public enum SingletonTest
    //{
    //    INSTANCE;

    //    public static SingletonTest GetInstance()
    //    {
    //      return INSTANCE;
    //    }
    //}
}

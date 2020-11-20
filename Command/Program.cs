// 참고 : https://gmlwjd9405.github.io/2018/07/07/command-pattern.html

using System;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Lamp lamp = new Lamp();
            ICommand lampOnCommand = new LampOnCommand(lamp);
            Alarm alarm = new Alarm();
            ICommand alarmStartCommand = new AlarmStartCommand(alarm);

            Button button1 = new Button(lampOnCommand); // 램프 켜는 Command 설정
            button1.Pressed(); // 램프 켜는 기능 수행

            Button button2 = new Button(alarmStartCommand); // 알람 울리는 Command 설정
            button2.Pressed(); // 알람 울리는 기능 수행
            button2.SetCommand(lampOnCommand); // 다시 램프 켜는 Command로 설정
            button2.Pressed(); // 램프 켜는 기능 수행
        }
    }

    public interface ICommand
    {
        public abstract void Execute();
    }

    public class Button
    {
        private ICommand theCommand;
        // 생성자에서 버튼을 눌렀을 때 필요한 기능을 인지로 받는다.
        public Button(ICommand theCommand) { SetCommand(theCommand); }
        public void SetCommand(ICommand newCommand) { this.theCommand = newCommand; }
        // 버튼이 눌리면 주어진 Command의 execute 메서드를 호출한다.
        public void Pressed() { theCommand.Execute(); }
    }

    public class Lamp
    {
        public void TurnOn()
        {
            Console.WriteLine("Lamp On");
        }
    }

    // 램프를 켜는 LampOnCommand 클래스
    public class LampOnCommand : ICommand
    {
        private Lamp theLamp;

        public LampOnCommand(Lamp theLamp) { this.theLamp = theLamp; }
        // Command 인터페이스의 execute 메서드
        public void Execute() { theLamp.TurnOn(); }
    }

    public class Alarm
    {
        public void Start() { Console.WriteLine("Alarming"); }
    }

    // 알람을 울리는 AlarmStartCommand 클래스
    public class AlarmStartCommand : ICommand
    {
        private Alarm theAlarm;

        public AlarmStartCommand(Alarm theAlarm) { this.theAlarm = theAlarm; }
        // Command 인터페이스의 execute 메서드
        public void Execute() { theAlarm.Start(); }
    }
}

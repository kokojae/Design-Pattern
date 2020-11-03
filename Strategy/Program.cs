// 참고 : https://gmlwjd9405.github.io/2018/07/06/strategy-pattern.html

using System;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot taekwonV = new TaekwonV("TaekwonV");
            Robot atom = new Atom("Atom");

            taekwonV.SetMovingStrategy(new WalkingStrategy());
            taekwonV.SetAttackStrategy(new MissileStrategy());
            atom.SetMovingStrategy(new FlyingStrategy());
            atom.SetAttackStrategy(new PunchStrategy());

            Console.WriteLine("My name is " + taekwonV.GetName());
            taekwonV.Move();
            taekwonV.Attack();

            Console.WriteLine();
            Console.WriteLine("My name is " + atom.GetName());
            atom.Move();
            atom.Attack();
        }
    }

    public abstract class Robot
    {
        private string name;
        private IAttackStrategy attackStrategy;
        private IMovingStrategy movingStrategy;

        public Robot(string name) { this.name = name; }
        public string GetName() { return name; }
        public void Attack() { attackStrategy.Attack(); }
        public void Move() { movingStrategy.Move(); }

        public void SetAttackStrategy(IAttackStrategy attackStrategy)
        {
            this.attackStrategy = attackStrategy;
        }
        public void SetMovingStrategy(IMovingStrategy movingStrategy)
        {
            this.movingStrategy = movingStrategy;
        }
    }

    public class TaekwonV : Robot
    {
        public TaekwonV(string name) : base(name) { }
    }
    public class Atom : Robot
    {
        public Atom(string name) : base(name) { }
    }

    public interface IAttackStrategy
    {
        public void Attack();
    }
    public class MissileStrategy : IAttackStrategy
    {
        public void Attack()
        {
            Console.WriteLine("I have Missile.");
        }
    }
    public class PunchStrategy : IAttackStrategy
    {
        public void Attack()
        {
            Console.WriteLine("I have strong punch.");
        }
    }

    public interface IMovingStrategy
    {
        public void Move();
    }
    public class FlyingStrategy : IMovingStrategy
    {
        public void Move()
        {
            Console.WriteLine("I can fly.");
        }
    }
    public class WalkingStrategy : IMovingStrategy
    {
        public void Move()
        {
            Console.WriteLine("I can only walk.");
        }
    }
}

using System;
using MyClassLib;
using System.Threading;


namespace ConsoleApp
{
    public class TankBattle
    {
        private int winFirstTank;
        private Tank first;
        private int winSecondTank;
        private Tank second;
        private int draw;

        public TankBattle(string firstTankName, string secondTankName)
        {
            first = new Tank(firstTankName);
            second = new Tank(secondTankName);
            winFirstTank = 0;
            winSecondTank = 0;
            draw = 0;
        }
        public void Battle(uint numOfBattle = 20, int speedOfBattle = 50, bool IsShowBattle = true)
        {
            for (uint i = 0; i < numOfBattle; i++)
            {
                try
                {
                    Tank tmp = (first * second);        // реалізовуєм битву через оператор множення
                    if (tmp.Name == first.Name) winFirstTank++;
                    else winSecondTank++;
                    if (IsShowBattle) Console.WriteLine(tmp + "Winner");
                }
                catch (Exception ex)
                {
                    if (IsShowBattle)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t" + ex.Message);
                        Console.ResetColor();
                    }
                    draw++;
                }
                first.BornAgain();
                second.BornAgain();
                Thread.Sleep(speedOfBattle);
            }
        }
        public override string ToString()
        {
            return $"\n{first.Name}\twin {winFirstTank}  times\n" +
                   $"{second.Name}\twin {winSecondTank}  times\n" +
                   $"Draw         { draw}  times";
        }
    }
    public enum TankID
    {
        First, Second, other
    }
    public class TankBattle_V2
    {
        private int winFirstTank;
        private int winSecondTank;
        private int draw;
        private Tank[] first_tanks;
        private Tank[] second_tanks;

        public int FirstLenght { get => first_tanks.Length; }
        public int SecondLenght { get => second_tanks.Length; }
        public int WinFirstTank { get => winFirstTank; set { winFirstTank = value; } }
        public int WinSecondTank { get => winSecondTank; set { winSecondTank = value; } }
        public int Draw { get => draw; set { draw = value; } }
        public Tank this[TankID id, int num]
        {
            get
            {
                if (id == TankID.First)
                {
                    FirstCheck(num);
                    return first_tanks[num];
                }
                else if ((id == TankID.Second))
                {
                    SecondCheck(num);
                    return second_tanks[num];
                }
                else throw new Exception("Error tanks identificator");
            }
        }
        private void FirstCheck(int num)
        {
            if (((num < 0) || (num >= first_tanks.Length)))
                throw new IndexOutOfRangeException("First Tanks Index Out Of Range");
        }
        private void SecondCheck(int num)
        {
            if (((num < 0) || (num >= second_tanks.Length)))
                throw new IndexOutOfRangeException("Second Tanks Index Out Of Range");
        }
        public TankBattle_V2(string firstTankName, string secondTankName, int numOfButtle)
        {
            first_tanks = new Tank[numOfButtle];
            second_tanks = new Tank[numOfButtle];
            for (int i = 0; i < numOfButtle; i++)
            {
                first_tanks[i] = new Tank(firstTankName);
                second_tanks[i] = new Tank(secondTankName);
            }
            winFirstTank = 0;
            winSecondTank = 0;
            draw = 0;
        }
        public override string ToString()
        {
            if (first_tanks.Length != 0 && second_tanks.Length != 0)
                return $"\n{first_tanks[0].Name}\twin {winFirstTank}  times\n" +
                       $"{second_tanks[0].Name}\twin {winSecondTank}  times\n" +
                       $"Draw         { draw}  times";
            else
                return "There were no battles";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TankBattle battle = new TankBattle("Pantera", "T-34");
            battle.Battle(100,50);        // можна вказати три параметри, (кількість битв, швидкість битви(мілісекунди), чи виводити інформацію про битву)
            Console.WriteLine(battle);      // виводить результат битв
            Console.ReadLine();
            Console.Clear();

            /* Другий спосіб, інший клас(незручний як на мене)*/
            TankBattle_V2 battle_V2 = new TankBattle_V2("Pantera", "T-34", 100);     // імена танків і кількість битв
            if (battle_V2.FirstLenght == battle_V2.SecondLenght)
            {
                for (int i = 0; i < battle_V2.FirstLenght; i++)
                {
                    try
                    {
                        Tank tmp = battle_V2[TankID.First, i] * battle_V2[TankID.Second, i];        // реалізовуєм битву через оператор множення
                        if (tmp.Name == battle_V2[TankID.First, i].Name) battle_V2.WinFirstTank++;
                        else battle_V2.WinSecondTank++;
                        Console.WriteLine(tmp + "Winner");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t" + ex.Message);
                        Console.ResetColor();
                        battle_V2.Draw++;
                    }
                    Thread.Sleep(25);
                }
            }
            Console.WriteLine(battle_V2);
            Console.ReadLine();
        }
    }
}

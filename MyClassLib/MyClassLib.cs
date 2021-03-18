using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLib
{
    internal static class MyRand
    {
        static Random rand;
        static MyRand()
        {
            rand = new Random();
        }
        public static int GetRand(int minValue = 0, int maxValue = 10000)
        {
            return rand.Next(minValue, maxValue);
        }
    }
    public class Tank
    {
        private string name;
        private int ammunition;
        private int armor;
        private int maneuverability;
        private int point;

        public string Name { get => name; }
        public int Armor { get => armor; }
        public int Ammunition { get => ammunition; }
        public int Maneuverability { get => maneuverability; }
        //public int Point { get => point; }
        public Tank(string name)
        {
            this.name = name;
            BornAgain();
        }
        public static Tank operator *(Tank first, Tank second)
        {
            if (first.name == second.name) throw new Exception("Tank is friendly!!!");
            if (first.point != 0 || second.point != 0) throw new Exception("The tanks were already in battle");
            
            if (first.ammunition > second.ammunition) first.point++;        //перевіряєм параметри
            else if(first.ammunition < second.ammunition) second.point++;
            
            if (first.armor > second.armor) first.point++;
            else if (first.armor < second.armor) second.point++;
            
            if (first.maneuverability > second.maneuverability) first.point++;
            else if (first.maneuverability < second.maneuverability) second.point++;
            
            if (first.point == second.point) throw new Exception("The battle ended in a draw!!!");

            return first.point > second.point ? first : second;
        }
        public void BornAgain()
        {
            this.ammunition = MyRand.GetRand(1, 100);
            this.armor = MyRand.GetRand(1, 100);
            this.maneuverability = MyRand.GetRand(1, 100);
            this.point = 0;
        }
        public override string ToString()
        {
            return $"{name}, point: {point} --> ";
        }

    }
}

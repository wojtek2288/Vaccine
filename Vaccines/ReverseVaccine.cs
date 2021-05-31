using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class ReverseVaccine : IVaccine
    {
        public string Immunity => "ACTGAGACAT";

        public double DeathRate => 0.05f;
        private double Death = 0;

        private Random randomElement = new Random(0);
        public override string ToString()
        {
            return "ReverseVaccine";
        }
        public void VaccinateDog(Dog d)
        {
            char[] charArr = Immunity.ToCharArray();
            Array.Reverse(charArr);
            string s = charArr.ToString();

            d.Immunity = s;
            this.Death += DeathRate;
        }

        public void VaccinateCat(Cat c)
        {
            c.Alive = false;
            this.Death += DeathRate;
            Console.WriteLine("Cat has died");
        }

        public void VaccinatePig(Pig p)
        {
            if (randomElement.NextDouble() < Death)
            {
                p.Alive = false;
                Console.WriteLine("Pig has died");
            }   
            else
            {
                char[] charArr = Immunity.ToCharArray();
                Array.Reverse(charArr);
                string s = charArr.ToString();
                s += Immunity;

                p.Immunity = s;
            }
            this.Death += DeathRate;
        }
    }
}

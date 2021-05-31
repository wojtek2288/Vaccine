using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class Vaccinator3000 : IVaccine
    {
        public string Immunity => "ACTG";
        public double DeathRate => 0.1f;

        private Random randomElement = new Random(0);
        public override string ToString()
        {
            return "Vaccinator3000";
        }

        public void VaccinateDog(Dog d)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                d.Alive = false;
                Console.WriteLine("Dog has died");
            }  
            else
            {
                for (int i = 0; i < 3000; i++)
                {
                    d.Immunity += Immunity[randomElement.Next(0, Immunity.Length)];
                }
            }
        }
        public void VaccinateCat(Cat c)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                c.Alive = false;
                Console.WriteLine("Cat has died");
            }
            else
            {
                for (int i = 0; i < 300; i++)
                {
                    c.Immunity += Immunity[randomElement.Next(0, Immunity.Length)];
                }
            }
        }
        public void VaccinatePig(Pig p)
        {
            if (randomElement.NextDouble() < 3*DeathRate)
            {
                p.Alive = false;
                Console.WriteLine("Pig has died");
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    p.Immunity += Immunity[randomElement.Next(0, Immunity.Length)];
                }
            }
        }
    }
}

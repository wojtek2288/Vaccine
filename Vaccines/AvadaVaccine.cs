using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class AvadaVaccine : IVaccine
    {
        public string Immunity => "ACTAGAACTAGGAGACCA";

        public double DeathRate => 0.2f;

        private Random randomElement = new Random(0);

        public override string ToString()
        {
            return "AvadaVaccine";
        }

        public void VaccinateDog(Dog d)
        {
            d.Immunity = this.Immunity;
        }

        public void VaccinateCat(Cat c)
        {
            if (randomElement.NextDouble() < DeathRate)
            {
                c.Alive = false;
                Console.WriteLine("Cat has died");
            }
            else
                c.Immunity = Immunity.Substring(3);
        }

        public void VaccinatePig(Pig p)
        {
            p.Alive = false;
            Console.WriteLine("Pig has died");
        }
    }
}

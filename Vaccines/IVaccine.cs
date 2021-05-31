using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    interface IVaccine
    {
        public string Immunity { get; }
        public double DeathRate { get; }
        public void VaccinateDog(Dog d);
        public void VaccinateCat(Cat c);
        public void VaccinatePig(Pig p);
    }
}

using System;
using System.Collections.Generic;
using Task3.Subjects;
using Task3.Vaccines;

namespace Task3
{
    class Program
    {
        public class MediaOutlet
        {
            public void Publish(IEnumerable<VirusData> vD)
            {
                foreach(var data in vD)
                    Console.WriteLine(data.ToString());
            }
        }

        public class Tester
        {
            public void Test()
            {
                var vaccines = new List<IVaccine>() { new AvadaVaccine(), new Vaccinator3000(), new ReverseVaccine() };

                foreach (var vaccine in vaccines)
                {
                    Console.WriteLine($"Testing {vaccine}");
                    var subjects = new List<ISubject>();
                    int n = 5;
                    for (int i = 0; i < n; i++)
                    {
                        subjects.Add(new Cat($"{i}"));
                        subjects.Add(new Dog($"{i}"));
                        subjects.Add(new Pig($"{i}"));
                    }

                    foreach (var subject in subjects)
                    {
                        // process of vaccination
                        subject.GetVaccinated(vaccine);
                    }

                    var genomeDatabase = Generators.PrepareGenomes();
                    var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
                    // iteration over SimpleGenomeDatabase using solution from 1)
                    // subjects should be tested here using GetTested function
                    SimpleSearch SS = new SimpleSearch(simpleDatabase, genomeDatabase);

                    foreach(var virus in SS.Go())
                    {
                        foreach (var subject in subjects)
                        {
                            subject.GetTested(virus);
                        }
                    }

                    int aliveCount = 0;
                    foreach (var subject in subjects)
                    {
                        if (subject.Alive) aliveCount++;
                    }
                    Console.WriteLine($"{aliveCount} alive!");
                }
            }
        }
        public static void Main(string[] args)
        {
            var genomeDatabase = Generators.PrepareGenomes();
            var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
            var excellDatabase = Generators.PrepareExcellDatabase(genomeDatabase);
            var overcomplicatedDatabase = Generators.PrepareOvercomplicatedDatabase(genomeDatabase);
            var mediaOutlet = new MediaOutlet();

            //1 etap
            SimpleSearch SS = new SimpleSearch(simpleDatabase, genomeDatabase);
            ExcellSearch ES = new ExcellSearch(excellDatabase, genomeDatabase);
            OvercomplicatedSearch OS = new OvercomplicatedSearch(overcomplicatedDatabase, genomeDatabase);

            Console.WriteLine("Simple data base");
            mediaOutlet.Publish(SS.Go());
            Console.WriteLine();

            Console.WriteLine("Excell data base");
            mediaOutlet.Publish(ES.Go());
            Console.WriteLine();

            Console.WriteLine("Overcomplicated data base");
            mediaOutlet.Publish(OS.Go());
            Console.WriteLine();

            //2 etap
            Func<VirusData, bool> fun1 = x => x.DeathRate > 15;
            Func<VirusData, VirusData> fun2 = x => new VirusData(x.VirusName, x.DeathRate + 10, x.InfectionRate, x.Genomes);

            Console.WriteLine("Filtr DeathRate > 15");
            filters DR15 = new filters(fun1);
            mediaOutlet.Publish(DR15.Change(ES.Go()));
            Console.WriteLine();

            Console.WriteLine("Mapping DeathRate + 10 and filtering DeathRate > 15");
            mapping DR10 = new mapping(fun2);
            List<Changing> list = new List<Changing>();
            list.Add(DR10);
            list.Add(DR15);

            Modify mod = new Modify();
            mediaOutlet.Publish(mod.ModifyData(ES.Go(), list));
            Console.WriteLine();

            Console.WriteLine("Concatenating");
            List<IEnumerable<VirusData>> list_con = new List<IEnumerable<VirusData>>();
            list_con.Add(ES.Go());
            list_con.Add(OS.Go());
            Concat c = new Concat();
            mediaOutlet.Publish(c.Concatenate(list_con));
            // testing animals
            var tester = new Tester();
            tester.Test();
        }
    }
}

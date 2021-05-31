using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    public interface Search
    {
        public IEnumerable<VirusData> Go();
    }
    public class SimpleSearch : Search
    {
        public SimpleDatabase sDB { get; set; }
        public SimpleGenomeDatabase gDB { get; set; }

        public SimpleSearch(SimpleDatabase sDB, SimpleGenomeDatabase gDB)
        {
            this.sDB = sDB;
            this.gDB = gDB;
        }
        public IEnumerable<VirusData> Go()
        {
            foreach(var row in sDB.Rows)
            {
                List<GenomeData> list = new List<GenomeData>();
                foreach(var genome in gDB.genomeDatas)
                {
                    if (genome.Id == row.GenomeId)
                        list.Add(genome);
                }
                VirusData vD = new VirusData(row.VirusName, row.DeathRate, row.InfectionRate, list);
                yield return vD;
            }
        }
    }

    public class ExcellSearch : Search
    {
        public ExcellDatabase eDB { get; set; }
        public SimpleGenomeDatabase gDB { get; set; }
        public ExcellSearch(ExcellDatabase eDB, SimpleGenomeDatabase gDB)
        {
            this.eDB = eDB;
            this.gDB = gDB;
        }
        public IEnumerable<VirusData> Go()
        {
            string[] names = eDB.Names.Split(';');
            string[] DeathRates = eDB.DeathRates.Split(';');
            string[] InfectionRates = eDB.InfectionRates.Split(';');
            string[] GenomeIds = eDB.GenomeIds.Split(';');

            for(int i = 0; i < names.Length; i++)
            {
                List<GenomeData> list = new List<GenomeData>();
                foreach (var genome in gDB.genomeDatas)
                {
                    if (genome.Id.ToString() == GenomeIds[i])
                        list.Add(genome);
                }
                VirusData vD = new VirusData(names[i], double.Parse(DeathRates[i]), double.Parse(InfectionRates[i]), list);
                yield return vD;
            }
        }
    }

    public class OvercomplicatedSearch : Search
    {
        public OvercomplicatedDatabase oDB { get; set; }
        public SimpleGenomeDatabase gDB { get; set; }
        public OvercomplicatedSearch(OvercomplicatedDatabase oDB, SimpleGenomeDatabase gDB)
        {
            this.oDB = oDB;
            this.gDB = gDB;
        }
        public IEnumerable<VirusData> Go()
        {
            Queue<INode> queue = new Queue<INode>();
            queue.Enqueue(oDB.Root);

            while(queue.Count != 0)
            {
                var u = queue.Dequeue();
                List<GenomeData> list = new List<GenomeData>();
                foreach (var genome in gDB.genomeDatas)
                {
                    foreach(var tag in genome.Tags)
                    {
                        if(tag == u.GenomeTag)
                        {
                            list.Add(genome);
                            break;
                        } 
                    }
                }
                VirusData vD = new VirusData(u.VirusName, u.DeathRate, u.InfectionRate, list);
                yield return vD;

                if (u.Children.Count == 0)
                    continue;
                else
                {
                    foreach (var child in u.Children)
                        queue.Enqueue(child);
                }
            }
        }
    }
}

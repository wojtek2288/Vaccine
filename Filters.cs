using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    public interface Changing
    {
        public IEnumerable<VirusData> Change(IEnumerable<VirusData> vD);
    }

    public class filters : Changing
    {
        public Func<VirusData, bool> fun;
        public filters(Func<VirusData, bool> fun)
        {
            this.fun = fun;
        }
        public IEnumerable<VirusData> Change(IEnumerable<VirusData> vD)
        {
            foreach(var virus in vD)
            {
                if (fun(virus) == true)
                    yield return virus;
            }
        }

    }

    public class mapping : Changing
    {
        public Func<VirusData, VirusData> fun;
        public mapping(Func<VirusData, VirusData> fun)
        {
            this.fun = fun;
        }
        public IEnumerable<VirusData> Change(IEnumerable<VirusData> vD)
        {
            foreach (var virus in vD)
            {
                yield return fun(virus);
            }
        }
    }

    public class Modify
    {
        public IEnumerable<VirusData> ModifyData(IEnumerable<VirusData> vD,List<Changing> list)
        {
            IEnumerable<VirusData> p = vD;
            foreach(var i in list)
            {
                p = i.Change(p);
            }
            return p;
        }
    }

    public class Concat
    {
        public IEnumerable<VirusData> Concatenate(List<IEnumerable<VirusData>> list)
        {
            foreach(var i in list)
            {
                foreach(var j in i)
                {
                    yield return j;
                }
            }
        }
    }
}

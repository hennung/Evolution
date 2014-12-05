using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class Globals
{
    public static int DNA_LENGTH = 10;
    public static int DNA_SIZE = 10;
    public static Random rnd = new Random();
}
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] mum = new bool[Globals.DNA_LENGTH];
            bool[] dad = new bool[Globals.DNA_LENGTH];
            
            Genome Adam = new Genome();
            Population Humanity = new Population(2);
            Encounter Life = new Encounter();

            for (int i = 0; i < 18; i++)
            {
                Life.Mating(Humanity.PopList[0], Humanity.PopList[1], Humanity);
            }

            for (int i = 0; i < 1000; i++) {
                Life.Fight(Humanity);
                int pick = Globals.rnd.Next(1,Humanity.PopList.Count());
                if (pick == 0)
                    pick +=1;
                Life.Mating(Humanity.PopList[pick],Humanity.PopList[pick-1],Humanity);
            }
            Life.Fight(Humanity);
            foreach (Genome unit in Humanity.PopList)
            {
                unit.print();
            }
            Console.Read();
        }
    }
    class Genome
    {
        int[] mother = new int[Globals.DNA_LENGTH];
        int[] father = new int[Globals.DNA_LENGTH];
        int[] activedata = new int[Globals.DNA_LENGTH];
        public int[] getActiveData()
        {
            return this.activedata;
        }
        public Genome()
        {
            for (int i = 0; i < Globals.DNA_LENGTH; i++)
            {
                int num = Globals.rnd.Next(Globals.DNA_SIZE);
                this.mother[i] = num;
                num = Globals.rnd.Next(Globals.DNA_SIZE);
                this.father[i] = num;
            }
            this.MergeGenome();
        }
        public Genome(int[] mother, int[] father)
        {
            this.mother = mother;
            this.father = father;
            this.MergeGenome();
           
        }
        private void MergeGenome()
        {

             for (int i = 0; i < this.mother.Length; i++)
                {
                    int num = Globals.rnd.Next(0, 10);
                    if (num == 0)
                    {
                        this.activedata[i] = Globals.rnd.Next(0, Globals.DNA_SIZE);
                    }
                    else if (num < 6)
                    {
                        this.activedata[i] = this.mother[i];
                    }
                    else{
                        this.activedata[i] = this.father[i];
                    }
                }
        }      
        public void print()
        {
            Console.Write(this + " ");
            foreach(int DNA in this.activedata)
            Console.Write(DNA + " ");
            Console.WriteLine("");
            return;
        }
    }
    class Population
    {   
        public List<Genome> PopList;
        public Population(int size)
        {
            PopList = new List<Genome>(size);
            Random rnd = new Random();
            for (int i = 0; i < size; i++) 
            {
                PopList.Add(new Genome());
            }
            Console.WriteLine("Made a population of "+PopList.Count +" elements");
        }
        public List<Genome> PopulationList
        {
            get{ return PopList;}
        }
    }
    class Encounter
    {
        public void Mating(Genome male, Genome female,Population Population)
        {
            Genome child = new Genome(female.getActiveData(), male.getActiveData());
            Population.PopList.Add(child);
        }
        public void Fight(Population Population)
        {
            Genome attacker = Population.PopList[Globals.rnd.Next(0, Population.PopList.Count())];
            Genome defender = Population.PopList[Globals.rnd.Next(0, Population.PopList.Count())];
            int attackersum = attacker.getActiveData().Sum();
            int defendersum = defender.getActiveData().Sum();
          //  Console.WriteLine("Attacker: " + attackersum + "Defender: " + defendersum);
            if (attackersum < defendersum)
            {
                Population.PopList.Remove(attacker);
            }
            else
            {
                Population.PopList.Remove(defender);
            }
        }
    }
}
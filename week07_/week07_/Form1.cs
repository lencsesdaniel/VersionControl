using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week07_.Entities;

namespace week07_
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234);
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        List<int> nbrOfMales1 = new List<int>();
        List<int> nbrOfFemales1 = new List<int>();
        public Form1()
        {
            InitializeComponent();
            BirthProbabilities=GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            

        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthprobabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthprobabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        pBirth=double.Parse(line[2])
                    });
                }
            }

            return birthprobabilities;
        }


        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathprobabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathprobabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        pDeath = double.Parse(line[2])
                    });
                }
            }

            return deathprobabilities;
        }


        private void SimStep(int year, Person person)
        {
            if (!person.IsAlive) return;

            byte age = (byte)(year - person.BirthYear);

            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.pDeath).FirstOrDefault();
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.pBirth).FirstOrDefault();
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        private void Simulation() 
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                
                
            }
        }

        private void DisplayResults()
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                richTextBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv (*.csv)| *.csv";
            if(openFileDialog.ShowDialog()== DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
            Population = GetPopulation(textBox1.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Simulation();
        }
    }
}

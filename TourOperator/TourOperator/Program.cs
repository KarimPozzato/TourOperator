using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourOperator
{
    public interface CD
    {
        void insert(IComparable key, Object attribute);
        Object find(IComparable key);
        Object remove(IComparable key);
    }
    public interface Container
    {
        bool isEmpty();
        void makeEmpty();
        int size();
    }

    public class TourOperator : CD, Container
    {
        private string nextClientCode;
        private Dictionary<IComparable, object> dizionario;

        public TourOperator(string codice)
        {
            this.nextClientCode = codice;
            this.dizionario = new Dictionary<IComparable, object>();
        }

        public void add(string nome, string dest)
        {
            string codice = this.nextClientCode;
            this.nextClientCode = this.NextCode(codice);
            this.dizionario[codice] = new Client(nome, dest);
        }

        //Override del metodo to string, per la visualizazione degli elementi
        public override string ToString()
        {
            string stringa = "";

            foreach (KeyValuePair<IComparable, object> cliente in this.dizionario)
            {
                string codice = cliente.Key.ToString();
                Client temp = (Client)cliente.Value;
                //stringa concatenata
                stringa += codice + " : " + temp.name + " : " + temp.dest;
            }

            return stringa;
        }

        // Implementazione delle parti di CD

        public void insert(IComparable key, object attribute)
        {
            if (this.dizionario.ContainsKey(key))
            {
                this.dizionario[key] = attribute;
            }
            else
            {
                this.dizionario.Add(key, attribute);
            }
        }

        public object find(IComparable key)
        {

            if (this.dizionario.ContainsKey(key))
            {
                return this.dizionario[key];
            }
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException();
            }
        }

        public object remove(IComparable key)
        {
            if (this.dizionario.ContainsKey(key))
            {
                object attribute = this.dizionario[key];
                this.dizionario.Remove(key);
                return attribute;
            }
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException();
            }
        }

        // Implementazione delle parti del Container

        public bool isEmpty()
        {
            return this.dizionario.Count == 0;
        }

        public void makeEmpty()
        {
            this.dizionario.Clear();
        }

        public int size()
        {
            return this.dizionario.Count;
        }

        // Class Client
        private class Client
        {
            public string name;
            public string dest;
            public Client(string aName, string aDest)
            {
                this.name = aName;
                this.dest = aDest;
            }
        }

        //Classe coppia
        private class Coppia : IComparable
        {
            public string code;
            public Client client;

            public Coppia(string aCode, Client aClient)
            {
                this.code = aCode;
                this.client = aClient;
            }

            public int CompareTo(object obj)
            {
                Coppia tmpC = (Coppia)obj;
                return this.code.CompareTo(tmpC.code);
            }
        }

        //Metodo per generare nuovi codici

        private string NextCode(string code)
        {
            //dichiarazione variabili
            char car = code[0];
            int max = int.Parse(code.Substring(1));

            //controlla se numero supera 999
            if (max < 999)
                max++;
            else
            {
                car++;
                //controlla se il carattere supera la lettera z
                if (car > 'Z')
                {
                    throw new Exception("Pieno");
                }
                max = 0;
            }
            string newCodice = car + max.ToString();

            return newCodice;
        }
    }

    class Program
    {
        //main
        static void Main(string[] args)
        {
            //dichiarazione variabile

            string nome;
            string dest;

            //chiede al utente il codice iniziale

            Console.WriteLine("Inserisci Il codice");
            //inizializzazione TourOp
            TourOperator tourOp = new TourOperator(Console.ReadLine());

            while (true)
            {
                //chiede al utente il nome
                Console.WriteLine("Inserire le informazioni, premi 0 per uscire: ");
                Console.WriteLine("Nome: ");
                nome = Console.ReadLine();

                //Se andiamo a inserire 0 si ferma il ciclo
                if (nome == "0")
                    break;

                //chiede al utente la destinazione
                Console.WriteLine("Destinazione: ");
                dest = Console.ReadLine();
                tourOp.add(nome, dest);
            }

            //stampiamo il contenuto di TourOp

            Console.WriteLine(tourOp.ToString());
            Console.ReadLine();
        }
    }
}
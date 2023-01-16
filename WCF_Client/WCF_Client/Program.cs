using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WCF_Client.ServiceReference1;

namespace WCF_Client
{
    internal class Program
    {
        public static ServiceReference1.Service1Client kliens;

        public static Kutya EgyKutyaGet()
        {
            Kutya Kutya = new Kutya();
            WebClient client = new WebClient();
            JObject jObject = new JObject();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.DownloadString("http://localhost:3000/" + "EgyKutyaAdatai");
            
            Kutya = JsonConvert.DeserializeObject<Kutya>(result);
            return Kutya;
        }

        public static List<Kutya> KutyakListaja()
        {
            List<Kutya> listaVissza = new List<Kutya>();
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.DownloadString("http://localhost:3000/" + "Kutyak");

            listaVissza = JsonConvert.DeserializeObject<List<Kutya>>(result);
            return listaVissza;
        }

        class KutyaAdat
        {
            public string ClassName = "kutya";
            public Kutya kutya { get; set; }
        }

        class id
        {
            public int ID { get; set; }

            public id(int id)
            {
                this.ID = id;
            }
        }
        public static string EgyKutyaAdd(Kutya kutya)
        {
            KutyaAdat egyAdat = new KutyaAdat();
            egyAdat.kutya = kutya;
            //Console.WriteLine(egyAdat.ClassName);
            //Console.WriteLine(egyAdat.kutya);
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.UploadString("http://localhost:3000/EgyKutyaAdd","POST", JsonConvert.SerializeObject(egyAdat));
            return result;
        }

        public static string EgyKutyaPut(Kutya kutya)
        {
            KutyaAdat egyAdat = new KutyaAdat();
            egyAdat.kutya = kutya;
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.UploadString("http://localhost:3000/EgyKutyaModosit", "PUT", JsonConvert.SerializeObject(egyAdat));
            return result;
        }

        public static string EgyKutyaDelete(int ID)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(new id(ID)));
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result = client.UploadString("http://localhost:3000/EgyKutyaTorol", "DELETE", JsonConvert.SerializeObject(new id(ID)));
            return result;
        }

        public static string EgyKutyaDeleteID(int ID)
        {
            //Console.WriteLine(JsonConvert.SerializeObject(new id(ID)));
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string result="";
            try
            {
                result = client.UploadString($"http://localhost:3000/EgyKutyaTorol?ID={ID}", "DELETE","");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        static void Main(string[] args)
        {
            kliens = new ServiceReference1.Service1Client();

            Kutya kutya = new Kutya();
            kutya.ID = 17;
            kutya.Nev = "Yato";
            kutya.Gazdi = "Mind1";
            kutya.Eletkor = 42;
            kutya.Fajta = "Orosz agár";
            kutya.Neme = true;
            kutya.LabakSzama = 5;
            Console.WriteLine(kliens.EgyKutyaAddCS(kutya));
            kutya.ID = 123;
            kutya.Gazdi = "Bence";
            Console.WriteLine(EgyKutyaAdd(kutya));
            kutya.ID = 9;
            kutya.Gazdi = "ÁDÁM2";
            Console.WriteLine(kliens.EgyKutyaPutCS(kutya));

            Kutya ek = kliens.EgyKutyaGetCS();
            Console.WriteLine(ek.Gazdi);
            Kutya egyKutya = EgyKutyaGet();
            Console.WriteLine(egyKutya.Gazdi);
            for (int i = 0; i < 0; i++)
            {
                kliens.EgyKutyaPostCS();
            }
            kutya.ID = 17;
            kutya.LabakSzama = 100;
            Console.WriteLine(EgyKutyaPut(kutya));
            Console.WriteLine(kliens.EgyKutyaDeleteCS(123));
            for (int i = 1; i < 10; i++)
            {
                kutya.ID = i;
                kutya.Gazdi = "For ciklus";
                kliens.EgyKutyaAddCS(kutya);
            }
            Console.WriteLine(EgyKutyaDelete(17));
            Console.WriteLine(EgyKutyaDeleteID(3));
            List<Kutya> kutyaLista = new List<Kutya>(kliens.KutyakListajaCS());
            KutyakListaja();
            Console.ReadKey();
            kliens.Close();
        }
    }
}

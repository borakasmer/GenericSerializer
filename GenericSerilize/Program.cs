using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GenericSerilize
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Application app = new Application();
            app.Active = true;
            app.City = "Istanbul";
            app.GeneralList.Add("Makale yaz");
            app.GeneralList.Add("Musluğu tamir et");
            app.GeneralList.Add("Güncellemeleri yap");
            app.Rank = 5;
            app.BasicSave();
            Console.Write("Application Kaydedildi.");
            Console.ReadLine();

            Application app2 = new Application();
            app2.BasicLoad();
            Console.WriteLine("Şehir: " + app2.City);
            Console.WriteLine("ID: " + app2.ID);
            Console.WriteLine("Sıra: " + app2.Rank);
            Console.WriteLine("İşler:");
            foreach (string job in app2.GeneralList)
            {
                Console.WriteLine("." + job);
            }
            Console.ReadLine();
            */

            
            Application app = new Application();
            app.Active = true;
            app.City = "Seattle";
            app.GeneralList.Add("Daha güzel kod yaz.");
            app.GeneralList.Add("Publish yapmayı unutma.");
            app.GeneralList.Add("Yemek söyle.");
            app.Rank = 5;
            //app.BasicSave();            
            string fileName = @"c:\data\Application3.xml";
            SaveBetter(fileName, app);
            //Save<Application>(fileName, app);
            //Console.Write("Generic Application Kaydedildi.");
            //Console.ReadLine();

            var fileData = Load<Application>(fileName);
            Console.WriteLine("Şehir: " + fileData.City);
            Console.WriteLine("ID: " + fileData.ID);
            Console.WriteLine("Sıra: " + fileData.Rank);
            Console.WriteLine("İşler:");
            foreach (string job in fileData.GeneralList)
            {
                Console.WriteLine("." + job);
            }
            Console.ReadLine();
        }

        public static bool Save<T>(string FileName, Object Obj)
        {
            var xs = new XmlSerializer(typeof(T));
            using (TextWriter sw = new StreamWriter(FileName))
            {
                xs.Serialize(sw, Obj);
            }

            if (File.Exists(FileName))
                return true;
            else return false;
        }

        public static bool SaveBetter(string FileName, Object Obj)
        {
            var xs = new XmlSerializer(Obj.GetType());

            using (TextWriter sw = new StreamWriter(FileName))
            {
                xs.Serialize(sw, Obj);
            }
            if (File.Exists(FileName))
                return true;
            else return false;
        }

        public static T Load<T>(string FileName)
        {
            Object rslt;

            if (File.Exists(FileName))
            {
                var xs = new XmlSerializer(typeof(T));

                using (var sr = new StreamReader(FileName))
                {
                    rslt = (T)xs.Deserialize(sr);
                }
                return (T)rslt;
            }
            else
            {
                return default(T);
            }
        }
    }

    public class Application
    {
        const string FileSavePath = @"c:\data\Application.xml";
        public Guid ID { get; set; }
        public string City { get; set; }
        public int Rank { get; set; }
        public bool Active { get; set; }
        public List<string> GeneralList { get; set; }

        public Application()
        {
            ID = Guid.NewGuid();
            GeneralList = new List<string>();
        }

        public void BasicSave()
        {
            var xs = new XmlSerializer(typeof(Application));

            using (TextWriter sw = new StreamWriter(FileSavePath))
            {
                xs.Serialize(sw, this);
            }
        }

        public void BasicLoad()
        {
            var xs = new XmlSerializer(typeof(Application));

            using (var sr = new StreamReader(FileSavePath))
            {
                var tempData = (Application)xs.Deserialize(sr);
                ID = tempData.ID;
                City = tempData.City;
                Rank = tempData.Rank;
                Active = tempData.Active;
                GeneralList = tempData.GeneralList;
            }
        }
    }
}

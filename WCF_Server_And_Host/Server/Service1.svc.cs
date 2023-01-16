using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace Server
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        public static List<Car> carList = new List<Car>();
        Random random = new Random();

        public static HashSet<int> carIndex = new HashSet<int>();

        public static int Pozicio(int id)
        {
            int index = 0;
            int soroSzama = carList.Count;
            while (index < soroSzama) 
            {
                if (carList[index].ID == id)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public List<Car> CarListDB()
        {
            List<Car> carList2 = new List<Car>();
            DatabaseManager.ISQL tableCarManager = new DatabaseManager.CarManager();
            List<Record> records = tableCarManager.Select();
            foreach (Record egyRecord in records)
            {
                if (egyRecord is Car)
                {
                    carList2.Add(egyRecord as Car);
                }
            }
            return carList2;
        }
        public List<Car> CarListDBSpec(string name)
        {
            List<Car> carList2 = new List<Car>();
            DatabaseManager.ISQL tableCarManager = new DatabaseManager.CarManager();
            List<Record> records = tableCarManager.SelectSpec(name);
            foreach (Record egyRecord in records)
            {
                if (egyRecord is Car)
                {
                    carList2.Add(egyRecord as Car);
                }
            }
            return carList2;
        }

        public string CarPostDB(Car car)
        {
            DatabaseManager.CarManager tableCarManager = new DatabaseManager.CarManager();
            if (tableCarManager.Insert(car)>0)
            {
                return "Az autó adatainak a tárolása sikeresen megtörtént.";
            }
            else
            {
                return "Az autó adatainak a tárolása sikertelen!";
            }
        }
        public Car OneCarGet()
        {
            Car car = new Car();
            car.ID = 1;
            car.Make = "Ford";
            car.Model = "Mustang";
            car.Year = 2000;
            car.Color = "White";
            car.Vin = "2G4GR5ER6D9243453";
            Console.WriteLine("Adatok lekérve...");
            return car;
        }

        public Car OneCarGetCS()
        {
            return OneCarGet();
        }

        public Car OneCarPost()
        {
            Car car = new Car();
            car.ID = random.Next(1, 10001);
            car.Make = "Mitsubishi";
            car.Model = "Evo";
            car.Year = 1999;
            car.Color = "Black";
            car.Vin = "2G4GR5ER6D965756";
            carList.Add(car);
            Console.WriteLine("Működik a post");
            return car;
        }

        public Car OneCarPostCS()
        {
            return OneCarPost();
        }

        public List<Car> CarList()
        {
            Console.WriteLine("Carlist lekérve");
            return carList;
        }

        public List<Car> CarListCS()
        {
            return CarList();
        }

        public string OneCarAddCS(Car car)
        {
            if (car != null && car.ID != null)
            {
                int id = (int)car.ID;
                if (!carIndex.Contains(id))
                {
                    carList.Add(car);
                    carIndex.Add(id);
                    return "Adat hozzáadása sikeres.";
                }
            }
            return "Az adat hozzáadás sikertelen!";
        }

        public string OneCarAdd(Car car)
        {
            Console.WriteLine(car);
            return OneCarAddCS(car);
        }

        public string OneCarPutCS(Car car)
        {
            if (car != null && car.ID != null)
            {
                int id = (int)car.ID;
                if (carIndex.Contains(id))
                {
                    int index = Pozicio(id);
                    if (index != -1)
                    {
                        carList[index] = car;
                        return "Adat módosítása sikeres.";
                    }
                }
            }
            return "Adatok módosítása sikertelen";
        }

        public string OneCarPut(Car car)
        {
            Console.WriteLine(car);
            return OneCarPutCS(car);
        }

        public string OneCarPatchCS(Car car)
        {
            Console.WriteLine(car);
            return OneCarPutCS(car);
        }

        public string OneCarPatch(Car car)
        {
            Console.WriteLine(car);
            return OneCarPutCS(car);
        }

        public string OneCarDeleteCS(int ID)
        {
            if (ID != null)
            {
                int id = (int)ID;
                if (carIndex.Contains(id))
                {
                    int index = Pozicio(id);
                    if (index != -1)
                    {
                        carList.RemoveAt(index);
                        carIndex.Remove(id);
                        return "Adat törlése sikeres.";
                    }
                }
            }
            return "Adatok törlése sikertelen";
        }

        public string OneCarDelete(int ID)
        {
            return OneCarDeleteCS(ID);
        }

        public string OneCarDeleteID(int ID)
        {
            return OneCarDeleteCS(ID);
        }
        public string CarDeleteDBID(int id)
        {
            DatabaseManager.CarManager tableCarManager = new DatabaseManager.CarManager();
            if (tableCarManager.Delete(id) > 0)
            {
                return "Az autó adatainak törlése sikeresen megtörtént.";
            }
            else
            {
                return "Az autó adatainak törlése sikertelen!";
            }
        }
        public string CarDeleteDB(int ID)
        {
            return CarDeleteDBID(ID);
        }
        public string CarPutDB(Car car)
        {
            DatabaseManager.CarManager tableCarManager = new DatabaseManager.CarManager();
            if (tableCarManager.Update(car) > 0)
            {
                return "Az autó adatainak módosítása sikeresen megtörtént.";
            }
            else
            {
                return "Az autó adatainak módosítása sikertelen!";
            }
        }
        public string CarPutDBCS(Car car)
        {
            return CarPutDB(car);
        }

        public string CarPostDBCS(Car car)
        {
            return CarPostDB(car);
        }
    }
}

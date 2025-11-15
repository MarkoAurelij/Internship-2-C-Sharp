namespace FuelTracker
{
    internal class Program
    {

        static List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
        static int ID_assigner_Users = 1;
        static int ID_assigner_Travels = 1;

        static void Main(string[] args)
        {
            AddUser("Janko", "Šegadura", new DateTime(2025, 12, 11));
            AddUser("Mirta", "Kanjac", new DateTime(2025, 11, 24));
            AddUser("Želimir", "Škarpona", new DateTime(2025, 12, 28));

            AddTravel(1, new DateTime(2025, 12, 20), 200, 18, 1.09);
            AddTravel(1, new DateTime(2025, 12, 26), 120, 11, 1.34);
            AddTravel(1, new DateTime(2025, 12, 30), 500, 51, 1.22);
            AddTravel(2, new DateTime(2025, 11, 20), 30, 3.5, 1.24);
            AddTravel(3, new DateTime(2025, 11, 30), 2000, 624, 1.44);

            MainMenu();
        }

        static void AddTravel(int userId, DateTime date, double distance, double fuelAmount, double fuelPrice)
        {
            var user = users.Find(u => (int)u["id"] == userId);
            if (user == null)
            {
                Console.WriteLine("Korisnik ne postoji.");
                return;
            }

            var travel = new Dictionary<string, object>();
            travel["id"] = ID_assigner_Travels++;
            travel["date"] = date;
            travel["distance"] = distance;
            travel["fuelAmount"] = fuelAmount;
            travel["fuelPrice"] = fuelPrice;
            travel["totalCost"] = fuelAmount * fuelPrice;

            ((List<Dictionary<string, object>>)user["travels"]).Add(travel);

        }

        static void AddUser(string firstName, string lastName, DateTime birthDate)
        {
            var user = new Dictionary<string, object>();
            user["id"] = ID_assigner_Users++;
            user["firstName"] = firstName;
            user["lastName"] = lastName;
            user["birthDate"] = birthDate;
            user["travels"] = new List<Dictionary<string, object>>(); // initialize travel list

            users.Add(user);
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA");
                Console.WriteLine("1 - Korisnici");
                Console.WriteLine("2 - Putovanja");
                Console.WriteLine("0 - Izlaz iz aplikacije");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UsersMenu();
                        break;

                    case "2":
                        Console.WriteLine("Otvoren izbornik: Putovanja");
                        Wait();
                        break;

                    case "0":
                        Console.WriteLine("Izlaz iz aplikacije...");
                        return;  

                    default:
                        Console.WriteLine("Neispravan unos!");
                        Wait();
                        break;
                }
            }
        }

        static void UsersMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("KORISNICI");
                Console.WriteLine("1 - Unos novog korisnika");
                Console.WriteLine("2 - Brisanje korisnika");
                Console.WriteLine("3 - Uređivanje korisnika");
                Console.WriteLine("4 - Pregled svih korisnika");
                Console.WriteLine("0 - Povratak na glavni izbornik");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Otvoren izbornik: Unos novog korisnika");
                        EnterNewUser();
                        break;

                    case "2":
                        Console.WriteLine("Otvoren izbornik: Brisanje korisnika");
                        Wait();
                        break;

                    case "3":
                        Console.WriteLine("Otvoren izbornik: Uređivanje korisnika");
                        Wait();
                        break;

                    case "4":
                        Console.WriteLine("Otvoren izbornik: Pregled svih korisnika");
                        Wait();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Neispravan unos!");
                        Wait();
                        break;
                }
            }
        }

        static void EnterNewUser()
        {
            Console.Clear();
            Console.WriteLine("Unos novog korisnika");

            Console.Write("Unesite ime: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Ime ne smije biti prazno!");
                Wait();
                return;
            }

            Console.Write("Unesite prezime: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Prezime ne smije biti prazno!");
                Wait();
                return;
            }

            Console.Write("Unesite datum rođenja (YYYY-MM-DD): ");
            string dateInput = Console.ReadLine();
            DateTime birthDate;
            if (!DateTime.TryParse(dateInput, out birthDate))
            {
                Console.WriteLine("Neispravan datum!");
                Wait();
                return;
            }
            int age = DateTime.Now.Year - birthDate.Year;

            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                Console.WriteLine("Korisnik mora biti stariji od 18 godina!");
                Wait();
                return;
            }


            AddUser(firstName, lastName, birthDate);

            Console.WriteLine("Korisnik uspješno dodan!");
            return;
        }


        static void Wait()
        {
            Console.WriteLine("\nPritisnite ENTER za nastavak...");
            Console.ReadLine();
        }
    }
}

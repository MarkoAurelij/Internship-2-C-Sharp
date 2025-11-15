using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;

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
            user["travels"] = new List<Dictionary<string, object>>(); 

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
                        TravelsMenu();
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
        static void TravelsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Putovanja");
                Console.WriteLine("1 - Unos novog putovanja");
                Console.WriteLine("2 - Brisanje putovanja");
                Console.WriteLine("3 - Uređivanje putovanja");
                Console.WriteLine("4 - Pregled svih putovanja");
                Console.WriteLine("5 - Izvještaji i analize");
                Console.WriteLine("0 - povratak na glavni izbornik");
                Console.WriteLine("Odabir: ");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        AddTravel();
                        break;

                    case "2":
                        DeleteTravel();
                        break;

                    case "3":
                        EditTravelById();
                        break;

                    case "4":
                        Wait();
                        break;

                    case "5":
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

        static void UsersMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Korisnici");
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
                        UserDeletion();
                        break;

                    case "3":
                        Console.WriteLine("Otvoren izbornik: Uređivanje korisnika");
                        UserEditing();
                        break;

                    case "4":
                        Console.WriteLine("Otvoren izbornik: Pregled svih korisnika");
                        UserListMenu();
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Unesi novog korisnika:");

                Console.Write("Unesite ime: ");
                string firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Niste unjeli ništa!");
                    Wait();
                    return;
                }

                Console.Write("Unesite prezime: ");
                string lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("Niste unjeli ništa");
                    Wait();
                    return;
                }

                Console.Write("Unesite datum rođenja (YYYY-MM-DD): ");
                string dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    Console.WriteLine("Niste unjeli ništa!");
                    Wait();
                    return;
                }
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
                    continue;
                }

                Console.WriteLine("Jeste li sigurni da želite dodati tog korisnika (Y/N) ?");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "Y":
                        AddUser(firstName, lastName, birthDate);

                        Console.WriteLine("Korisnik uspješno dodan!");
                        return;
                    case "N":
                        Console.WriteLine("Korisnik nije dodan!");
                        continue;

                }

            }
            
        }

        static void UserDeletion()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("BRISANJE PUTOVANJA");
                Console.WriteLine("1 - Brisanje prema imenu i prezimenu");
                Console.WriteLine("2 - Brisanje prema ID-u");
                Console.WriteLine("0 - Povratak na Korisnici menu");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Otvoren izbornik: Brisanje prema imenu i prezimenu");
                        UserDeletionNameSurname();
                        break;

                    case "2":
                        Console.WriteLine("Otvoren izbornik Brisanje prema ID-u");
                        UserDeletionID();
                        break;

                    case "0":
                        Console.WriteLine("Otvoren izbornik Korisnici");
                        return;
                        break;

                    default:
                        Console.WriteLine("Neispravan Unos!");
                        Wait();
                        break;

                }
            }
            
        }

        static void UserDeletionNameSurname()
        {
            Console.Clear();
            Console.WriteLine("BRISANJE KORISNIKA PO IMENU I PREZIMENU");
            Console.Write("Unesite ime korisnika:");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Prazno ime, vraćam se u izbornik...");
                Wait();
                return; 
            }

            Console.Write("Unesite prezime korisnika: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Prazno prezime, vraćam se u izbornik...");
                Wait();
                return; 
            }

            var user = users.Find(u =>
                (string)u["firstName"] == firstName &&
                (string)u["lastName"] == lastName
            );

            if (user == null)
            {
                Console.WriteLine("Korisnik nije pronađen!");
                Wait();
                return;
            }

            Console.Write($"Jeste li sigurni da želite obrisati korisnika {firstName} {lastName}? (Y/N): ");
            string confirm = Console.ReadLine().ToUpper();

            if (confirm == "Y")
            {
                users.Remove(user);
                Console.WriteLine("Korisnik je uspješno obrisan!");
            }
            else
            {
                Console.WriteLine("Brisanje otkazano.");
            }

            Wait();
        }
        static void UserDeletionID()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("BRISANJE KORISNIKA PO ID-U");
                Console.WriteLine("Unesite ID korisnika: ");
                string input = Console.ReadLine();
                int ID;

                if (!int.TryParse(input, out ID))
                {
                    Console.WriteLine("Neispravan unos! ID mora biti broj.");
                    Wait();
                    return;
                }
                var user = users.Find(u => (int)u["id"] == ID);

                if (user == null)
                {
                    Console.WriteLine($"Korisnik s ID {ID} ne postoji!");
                    Wait();
                    return;
                }

                Console.WriteLine($"Korisnik {user["firstName"]} {user["lastName"]} pronađen!");

                users.Remove(user);
                return;
            }
        }
      
        static void UserEditing()
        {
            while(true) 
            {
                Console.Clear();
                Console.WriteLine("UREĐIVANJE PODATAKA O KORISNIKU");
                Console.WriteLine("1 - Uredite korisnika prema ID-u");
                Console.WriteLine("0 - Povratak na Korisnici menu");
                string choice = Console.ReadLine();
                switch (choice) 
                {
                    case "1":

                        EditingInterface();
                        break;

                    case "0":
                        return;
                        break;

                    default:
                        Console.WriteLine("Neispravan unos!");
                        Wait();
                        break;

                }

            }

            



        }
        static void EditingInterface()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Napiši ID korisnika čije podatke želiš urediti:");
                string input = Console.ReadLine();
                int ID;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Prazan ID vračam se u Menu za Uređivanje");
                    Wait();
                    return;
                }

                if (!int.TryParse(input, out ID))
                {
                    Console.WriteLine("Neispravan unos! ID mora biti broj.");
                    Wait();
                    return;
                }
                var user = users.Find(u => (int)u["id"] == ID);

                if (user == null)
                {
                    Console.WriteLine($"Korisnik s ID {ID} ne postoji!");
                    Wait();
                    return;
                }

                EditingInterfaceSecondLayer(user);
                return;

            }
        }
        static void EditingInterfaceSecondLayer(Dictionary<string, object> user)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("1 - Promjeni ime korisnika");
                Console.WriteLine("2 - Promjeni prezime korisnika");
                Console.WriteLine("3 - Promjeni datum rođenja korisnika");
                Console.WriteLine("0 - vrati se u upis ID-a korisnika");
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        Console.Write("Unesite novo ime: ");
                        string newFirstName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newFirstName))
                        {
                            user["firstName"] = newFirstName;
                            Console.WriteLine("Ime je uspješno promijenjeno!");
                        }
                        else
                        {
                            Console.WriteLine("Ime ne smije biti prazno!");
                        }
                        break;
                    case "2":
                        Console.Write("Unesite novo prezime: ");
                        string newSurname = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newSurname))
                        {
                            user["lastname"] = newSurname;
                            Console.WriteLine("Ime je uspješno promijenjeno!");
                        }
                        else
                        {
                            Console.WriteLine("Ime ne smije biti prazno!");
                        }
                        break;
                    case "3":
                        Console.Write("Unesite novi datum rođenja (YYYY-MM-DD): ");
                        string dateInput = Console.ReadLine();
                        DateTime newBirthDate;

                        if (!DateTime.TryParse(dateInput, out newBirthDate))
                        {
                            Console.WriteLine("Neispravan format datuma!");
                            Wait();
                            break;
                        }

                        if (newBirthDate > DateTime.Now)
                        {
                            Console.WriteLine("Datum rođenja ne može biti u budućnosti!");
                            Wait();
                            break;
                        }

                        if (newBirthDate > DateTime.Now.AddYears(-18))
                        {
                            Console.WriteLine("Korisnik mora imati najmanje 18 godina!");
                            Wait();
                            break;
                        }

                        user["birthDate"] = newBirthDate;
                        Console.WriteLine("Datum rođenja uspješno promijenjen!");
                        Wait();
                        break;

                    case "0":
                        EditingInterface();
                        break;

                    default:
                        Console.WriteLine("Neispravan unos!");
                        Wait();
                        break;




                }

            }
        }
        static void UserListMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("PREGLED SVIH KORISNIKA");
                Console.WriteLine("1 - Svi korisnici abecedno po prezimenu");
                Console.WriteLine("2 - Svi korisnici stariji od 20 godina");
                Console.WriteLine("3 - Svi korisnici s barem 2 putovanja");
                Console.WriteLine("0 - Povratak");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PrintUsersAphabetically();
                        break;
                    case "2":
                        PrintUsersOlderThan20();
                        break;
                    case "3":
                        PrintUsersWithTwoTravels();
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

        static void PrintUsersAphabetically()
        {
            Console.Clear();
            Console.WriteLine("Svi korisnici izlistani (abecedno po prezimenu):\n");

            var sorted = users.OrderBy(u => (string)u["lastName"]).ToList();

            foreach (var user in sorted)
            {
                Console.WriteLine($"{user["id"]}. {user["firstName"]} {user["lastName"]} - Rođen: {((DateTime)user["birthDate"]).ToString("yyyy-MM-dd")}");
            }

            Wait();
        }

        static void PrintUsersOlderThan20()
        {
            Console.Clear();
            Console.WriteLine("KORISNICI STARIJI OD 20 GODINA:\n");

            var list = users.Where(u =>
            {
                DateTime birth = (DateTime)u["birthDate"];
                int age = DateTime.Now.Year - birth.Year;
                if (birth > DateTime.Now.AddYears(-age)) age--;
                return age > 20;
            }).ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("Nema korisnika starijih od 20 godina.");
            }
            else
            {
                foreach (var user in list)
                {
                    int age = DateTime.Now.Year - ((DateTime)user["birthDate"]).Year;
                    if (((DateTime)user["birthDate"]) > DateTime.Now.AddYears(-age)) age--;
                    Console.WriteLine($"{user["id"]}. {user["firstName"]} {user["lastName"]} - {age} godina");
                }
            }

            Wait();
        }


        static void PrintUsersWithTwoTravels()
        {
            Console.Clear();
            Console.WriteLine("Korisnici s barem 2 putovanja:\n");

            var list = users.Where(u =>
            {
                var travels = (List<Dictionary<string, object>>)u["travels"];
                return travels.Count >= 2;
            }).ToList(); 

            if (list.Count == 0)
            {
                Console.WriteLine("Nema korisnika s 2 ili više putovanja.");
            }
            else
            {
                foreach (var user in list)
                {
                    var travels = (List<Dictionary<string, object>>)user["travels"];
                    Console.WriteLine($"{user["id"]}. {user["firstName"]} {user["lastName"]} - Putovanja: {travels.Count}");
                }
            }

            Wait();
        }

        static void AddTravel()
        {
            Console.Clear();
            Console.WriteLine("Unos novog putovanja");
            Console.Write("Unesite ID korisnika kojem se dodaje putovanje: ");
            string userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput)) return;

            if (!int.TryParse(userInput, out int userId))
            {
                Console.WriteLine("Neispravan ID!");
                Wait();
                return;
            }

            var user = users.Find(u => (int)u["id"] == userId);
            if (user == null)
            {
                Console.WriteLine("Korisnik ne postoji!");
                Wait();
                return;
            }


            Console.Write("Datum putovanja (YYYY-MM-DD): ");
            string dateInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dateInput)) return;
            if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                Console.WriteLine("Neispravan datum!");
                Wait();
                return;
            }

            Console.Write("Kilometraža: ");
            string kmInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(kmInput)) return;
            if (!double.TryParse(kmInput, out double km) || km <= 0)
            {
                Console.WriteLine("Neispravna kilometraža!");
                Wait();
                return;
            }

            Console.Write("Potrošeno gorivo (L): ");
            string fuelInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fuelInput)) return;
            if (!double.TryParse(fuelInput, out double fuel) || fuel <= 0)
            {
                Console.WriteLine("Neispravna količina goriva!");
                Wait();
                return;
            }

            Console.Write("Cijena goriva po litri: ");
            string priceInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(priceInput)) return;
            if (!double.TryParse(priceInput, out double price) || price <= 0)
            {
                Console.WriteLine("Neispravna cijena!");
                Wait();
                return;
            }

            double totalCost = fuel * price;

            var travel = new Dictionary<string, object>();
            travel["id"] = ID_assigner_Travels++;
            travel["date"] = date;
            travel["km"] = km;
            travel["fuel"] = fuel;
            travel["price"] = price;
            travel["totalCost"] = totalCost;

            ((List<Dictionary<string, object>>)user["travels"]).Add(travel);

            Console.WriteLine($"Putovanje uspješno dodano! Ukupni trošak: {totalCost:F2} HRK");
            Wait();
        }
        static void DeleteTravel()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Brisanje putovanja:");
                Console.WriteLine("1 - brisanje po ID-u");
                Console.WriteLine("2 - brisanje putovanja skupljih od unesenog iznosa");
                Console.WriteLine("3 - brisanje putovanja jeftinijih od unesenog iznosa");
                Console.WriteLine("0 - Povratak");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DeleteTravelById();
                        break;

                    case "2":
                        DeleteTravelsMoreExpensiveThan();
                        break;

                    case "3":
                        DeleteTravelsCheaperThan();
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

        static void DeleteTravelById()
        {
            Console.Clear();
            Console.WriteLine("Brisanje putovanja po ID-u");

            Console.Write("Unesite ID putovanja: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input, out int travelId))
            {
                Console.WriteLine("Neispravan ID!");
                Wait();
                return;
            }

            bool found = false;
            foreach (var user in users)
            {
                var travels = (List<Dictionary<string, object>>)user["travels"];
                var travel = travels.Find(t => (int)t["id"] == travelId);
                if (travel != null)
                {
                    travels.Remove(travel);
                    Console.WriteLine("Putovanje uspješno izbrisano!");
                    found = true;
                    break;
                }
            }

            if (!found)
                Console.WriteLine("Putovanje s tim ID-em ne postoji.");

            Wait();
        }

        static void DeleteTravelsMoreExpensiveThan()
        {
            Console.Clear();
            Console.WriteLine("Brisanje putovanja skupljih od unesenog iznosa:");
            Console.Write("Unesite minimalni iznos: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!double.TryParse(input, out double minPrice) || minPrice < 0)
            {
                Console.WriteLine("Neispravan iznos!");
                Wait();
                return;
            }

            int removedCount = 0;
            foreach (var user in users)
            {
                var travels = (List<Dictionary<string, object>>)user["travels"];
                removedCount += travels.RemoveAll(t => (double)t["totalCost"] > minPrice);
            }

            Console.WriteLine($"{removedCount} putovanja izbrisano.");
            Wait();
        }

        static void DeleteTravelsCheaperThan()
        {
            Console.Clear();
            Console.WriteLine("Brisanje putovanja jeftinijih od unesenog iznosa:");
            Console.Write("Unesite maksimalni iznos: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!double.TryParse(input, out double maxPrice) || maxPrice < 0)
            {
                Console.WriteLine("Neispravan iznos!");
                Wait();
                return;
            }

            int removedCount = 0;
            foreach (var user in users)
            {
                var travels = (List<Dictionary<string, object>>)user["travels"];
                removedCount += travels.RemoveAll(t => (double)t["totalCost"] < maxPrice);
            }

            Console.WriteLine($"{removedCount} putovanja izbrisano.");
            Wait();
        }

        static void EditTravelById()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Uređivanje putovanja po ID-u");
                Console.Write("Unesite ID putovanja za uređivanje: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;

                if (!int.TryParse(input, out int travelId))
                {
                    Console.WriteLine("Neispravan ID!");
                    Wait();
                    continue;
                }

                Dictionary<string, object> travel = null;
                Dictionary<string, object> travelUser = null;

                foreach (var user in users)
                {
                    var travels = (List<Dictionary<string, object>>)user["travels"];
                    travel = travels.Find(t => (int)t["id"] == travelId);
                    if (travel != null)
                    {
                        travelUser = user;
                        break;
                    }
                }

                if (travel == null)
                {
                    Console.WriteLine("Putovanje s tim ID-em ne postoji!");
                    Wait();
                    continue;
                }

                Console.WriteLine("\nPritisnite ENTER za zadržavanje trenutne vrijednosti.\n");

                Console.Write($"Datum putovanja ({((DateTime)travel["date"]).ToString("yyyy-MM-dd")}): ");
                string dateInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dateInput))
                {
                    if (!DateTime.TryParse(dateInput, out DateTime newDate))
                    {
                        Console.WriteLine("Neispravan datum!");
                        Wait();
                        continue;
                    }
                    travel["date"] = newDate;
                }

                Console.Write($"Kilometraža ({travel["distance"]} km): ");
                string kmInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(kmInput))
                {
                    if (!double.TryParse(kmInput, out double newDistance) || newDistance <= 0)
                    {
                        Console.WriteLine("Neispravna kilometraža!");
                        Wait();
                        continue;
                    }
                    travel["distance"] = newDistance;
                }

                Console.Write($"Potrošeno gorivo ({travel["fuelAmount"]} L): ");
                string fuelInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(fuelInput))
                {
                    if (!double.TryParse(fuelInput, out double newFuel) || newFuel <= 0)
                    {
                        Console.WriteLine("Neispravna količina goriva!");
                        Wait();
                        continue;
                    }
                    travel["fuelAmount"] = newFuel;
                }

                Console.Write($"Cijena goriva po litri ({travel["fuelPrice"]} HRK): ");
                string priceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(priceInput))
                {
                    if (!double.TryParse(priceInput, out double newPrice) || newPrice <= 0)
                    {
                        Console.WriteLine("Neispravna cijena!");
                        Wait();
                        continue;
                    }
                    travel["fuelPrice"] = newPrice;
                }

                travel["totalCost"] = (double)travel["fuelAmount"] * (double)travel["fuelPrice"];

                Console.WriteLine($"\nPutovanje uspješno uređeno! Novi ukupni trošak: {travel["totalCost"]:F2} HRK");
                Wait();
                return;
            }
        }








        static void Wait()
        {
            Console.WriteLine("\nPritisnite ENTER za nastavak...");
            Console.ReadLine();

        }



        
    }
}

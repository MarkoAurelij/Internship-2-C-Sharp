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
                Console.WriteLine("UNESI NOVOG KORISNIKA");

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



        static void Wait()
        {
            Console.WriteLine("\nPritisnite ENTER za nastavak...");
            Console.ReadLine();

        }

        
    }
}

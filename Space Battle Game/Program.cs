using System;

namespace SpaceBattleGame
{
    interface IAttack
    {
        void Attack(Alien alien);
    }

    abstract class Character
    {
        protected string name;
        protected int health;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Character(string name, int health)
        {
            this.name = name;
            this.health = health;
        }

        public abstract void ShowInfo();

        public virtual void TakeDamage(int damage)
        {
            health -= damage;

            if (health < 0)
                health = 0;
        }
    }

    class Weapon
    {
        public string WeaponName;
        public int Damage;

        public Weapon(string weaponName, int damage)
        {
            WeaponName = weaponName;
            Damage = damage;
        }

        public static bool operator >(Weapon w1, Weapon w2)
        {
            return w1.Damage > w2.Damage;
        }

        public static bool operator <(Weapon w1, Weapon w2)
        {
            return w1.Damage < w2.Damage;
        }
    }

    class Spaceship
    {
        public string ShipName;
        public int Fuel;
        public int Energy;

        public Spaceship()
        {
            ShipName = "Basic Ship";
            Fuel = 100;
            Energy = 100;
        }

        public Spaceship(string shipName, int fuel, int energy)
        {
            ShipName = shipName;
            Fuel = fuel;
            Energy = energy;
        }

        public Spaceship(Spaceship s)
        {
            ShipName = s.ShipName;
            Fuel = s.Fuel;
            Energy = s.Energy;
        }
    }

    class Player : Character, IAttack
    {
        public Spaceship Ship;
        public Weapon Weapon;

        public Player(string name, int health,
            Spaceship ship, Weapon weapon)
            : base(name, health)
        {
            Ship = ship;
            Weapon = weapon;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("\n===== PLAYER INFO =====");
            Console.WriteLine("Name   : " + Name);
            Console.WriteLine("Health : " + Health);
            Console.WriteLine("Ship   : " + Ship.ShipName);
            Console.WriteLine("Fuel   : " + Ship.Fuel);
            Console.WriteLine("Energy : " + Ship.Energy);
            Console.WriteLine("Weapon : " + Weapon.WeaponName);
        }

        public void Attack(Alien alien)
        {
            if (Ship.Energy >= 10)
            {
                alien.TakeDamage(Weapon.Damage);
                Ship.Energy -= 10;

                Console.WriteLine(
                    Name + " attacked " +
                    alien.Name + " with " +
                    Weapon.WeaponName);
            }
            else
            {
                Console.WriteLine("Not enough energy!");
            }
        }

        public void Attack(Alien alien, int bonusDamage)
        {
            if (Ship.Energy >= 15)
            {
                alien.TakeDamage(
                    Weapon.Damage + bonusDamage);

                Ship.Energy -= 15;

                Console.WriteLine(
                    "Special Attack! Damage = " +
                    (Weapon.Damage + bonusDamage));
            }
            else
            {
                Console.WriteLine("Not enough energy!");
            }
        }
    }

    class Alien : Character
    {
        public int Damage;

        public Alien(string name,
            int health,
            int damage)
            : base(name, health)
        {
            Damage = damage;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("\nAlien : " + Name);
            Console.WriteLine("Health: " + Health);
        }

        public void Attack(Player player)
        {
            player.TakeDamage(Damage);

            Console.WriteLine(
                Name + " attacked player for " +
                Damage + " damage.");
        }
    }

    class Planet
    {
        public string PlanetName;

        public Planet(string name)
        {
            PlanetName = name;
        }

        public void Explore(Player player)
        {
            if (player.Ship.Fuel >= 20)
            {
                player.Ship.Fuel -= 20;

                Console.WriteLine(
                    "Travelled to " +
                    PlanetName);

                Console.WriteLine(
                    "Fuel Left: " +
                    player.Ship.Fuel);
            }
            else
            {
                Console.WriteLine(
                    "Not enough fuel!");
            }
        }

        public void CollectResource()
        {
            Console.WriteLine(
                "Collected resources from "
                + PlanetName);
        }
    }

    class Inventory
    {
        string[] items = new string[10];
        int count = 0;

        public void AddItem(string item)
        {
            if (count < items.Length)
            {
                items[count++] = item;

                Console.WriteLine(
                    item +
                    " added to inventory.");
            }
            else
            {
                Console.WriteLine(
                    "Inventory Full!");
            }
        }

        public void ShowItems()
        {
            Console.WriteLine(
                "\n===== INVENTORY =====");

            if (count == 0)
            {
                Console.WriteLine(
                    "Inventory Empty");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(
                    (i + 1) +
                    ". " +
                    items[i]);
            }
        }
    }

    class SpaceGame
    {
        static int AlienDefeated = 0;

        public static void ShowResult()
        {
            Console.WriteLine(
                "\nAliens Defeated: " +
                AlienDefeated);
        }

        static void Main(string[] args)
        {
            Console.WriteLine(
                "===== SPACE BATTLE GAME =====");

            Console.WriteLine(
                "\nSelect Spaceship");

            Console.WriteLine(
                "1. Falcon (Fuel 100, Energy 100)");

            Console.WriteLine(
                "2. Destroyer (Fuel 150, Energy 80)");

            Console.WriteLine(
                "3. Titan (Fuel 200, Energy 120)");

            Console.Write("Choice: ");

            int shipChoice =
                Convert.ToInt32(
                Console.ReadLine());

            Spaceship ship;

            switch (shipChoice)
            {
                case 1:
                    ship =
                        new Spaceship(
                        "Falcon", 100, 100);
                    break;

                case 2:
                    ship =
                        new Spaceship(
                        "Destroyer", 150, 80);
                    break;

                case 3:
                    ship =
                        new Spaceship(
                        "Titan", 200, 120);
                    break;

                default:
                    ship =
                        new Spaceship();
                    break;
            }

            Spaceship backupShip =
                new Spaceship(ship);

            Weapon laser =
                new Weapon("Laser", 20);

            Weapon rocket =
                new Weapon("Rocket", 35);

            Player player =
                new Player(
                    "Bishal",
                    100,
                    ship,
                    laser);

            Planet mars =
                new Planet("Mars");

            Planet jupiter =
                new Planet("Jupiter");

            Planet saturn =
                new Planet("Saturn");

            Inventory bag =
                new Inventory();

            Alien alien1 =
                new Alien(
                    "Alien Scout",
                    40,
                    10);

            Alien alien2 =
                new Alien(
                    "Alien Warrior",
                    70,
                    15);

            Alien boss =
                new Alien(
                    "Alien King",
                    150,
                    25);

            int choice;

            do
            {
                Console.WriteLine(
                    "\n===== MENU =====");

                Console.WriteLine(
                    "1. Show Player Info");

                Console.WriteLine(
                    "2. Explore Mars");

                Console.WriteLine(
                    "3. Explore Jupiter");

                Console.WriteLine(
                    "4. Explore Saturn");

                Console.WriteLine(
                    "5. Collect Resource");

                Console.WriteLine(
                    "6. Fight Alien Scout");

                Console.WriteLine(
                    "7. Fight Alien Warrior");

                Console.WriteLine(
                    "8. Fight Final Boss");

                Console.WriteLine(
                    "9. Show Inventory");

                Console.WriteLine(
                    "10. Compare Weapons");

                Console.WriteLine(
                    "11. Show Result");

                Console.WriteLine(
                    "12. Exit");

                Console.Write(
                    "Enter Choice: ");

                choice =
                    Convert.ToInt32(
                    Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        player.ShowInfo();
                        break;

                    case 2:
                        mars.Explore(player);
                        break;

                    case 3:
                        jupiter.Explore(player);
                        break;

                    case 4:
                        saturn.Explore(player);
                        break;

                    case 5:
                        bag.AddItem("Fuel Cell");
                        bag.AddItem("Energy Pack");

                        player.Ship.Fuel += 20;
                        player.Ship.Energy += 20;

                        Console.WriteLine(
                            "Fuel and Energy Increased!");
                        break;

                    case 6:
                        player.Attack(alien1);

                        if (alien1.Health > 0)
                            alien1.Attack(player);

                        if (alien1.Health <= 0)
                        {
                            AlienDefeated++;
                            Console.WriteLine(
                                "Alien Scout Defeated!");
                        }
                        break;

                    case 7:
                        player.Attack(alien2, 10);

                        if (alien2.Health > 0)
                            alien2.Attack(player);

                        if (alien2.Health <= 0)
                        {
                            AlienDefeated++;
                            Console.WriteLine(
                                "Alien Warrior Defeated!");
                        }
                        break;

                    case 8:

                        if (AlienDefeated < 2)
                        {
                            Console.WriteLine(
                                "Defeat all aliens first!");
                            break;
                        }

                        player.Attack(boss, 20);

                        if (boss.Health > 0)
                            boss.Attack(player);

                        if (boss.Health <= 0)
                        {
                            Console.WriteLine(
                                "\nYOU DEFEATED THE ALIEN KING!");

                            Console.WriteLine(
                                "YOU SAVED THE GALAXY!");

                            Console.WriteLine(
                                "YOU WIN!");
                        }

                        break;

                    case 9:
                        bag.ShowItems();
                        break;

                    case 10:

                        if (rocket > laser)
                        {
                            Console.WriteLine(
                                "Rocket is stronger than Laser");
                        }
                        else
                        {
                            Console.WriteLine(
                                "Laser is stronger");
                        }

                        break;

                    case 11:
                        ShowResult();
                        break;

                    case 12:
                        Console.WriteLine(
                            "Exiting Game...");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid Choice!");
                        break;
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine(
                        "\nGAME OVER!");

                    break;
                }

            } while (choice != 12);
        }
    }
}

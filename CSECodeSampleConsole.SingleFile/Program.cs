using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSECodeSampleConsole.SingleFile
{
    /// <summary>
    /// INSTRUCTIONS:
    /// Using this file ONLY (you can copy to a .cs file, just send it back as a plain text file), write a C# console application demonstrating the following logic. 
    /// 1. Present a menu to the user with 3 options [View Persons, Add Person, Exit]
    /// 2. If the user selects 'View Persons', the application should show a list of persons in the application.
    /// 3. If the user selects 'Add Person', they should be instructed to enter a name. The program should assign a unique id to the new person. 
    /// 4. If a person selects 'Exit', the application should close. Otherwise, the application should stay running and/or indicate that the choice was invalid. 
    /// 5. Optional: Create a 4th feature where you can search by name and the program indicates whether or not the name is in the system. 
    /// 6. Use an in memory list to store person objects.
    /// 7. Use multiple classes and object oriented design to write your program (don't put all of your logic in the Program class).
    /// 8. Submit this file as a TEXT file in your application. 
    /// 9. Download a free trial of Visual Studio or use the free Community edition.
    /// 10. What we look for: 
    ///    - Does the program meet the requirements/instructions and is it stable?
    ///    - Is the code clean & appropriately commented? 
    ///    - Is there an understandable object oriented design?
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var menu = new UserMenu();
            menu.Display();
        }

    }

    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Generic interface to be used as contract for creating a concrete repository of specified Type.
    /// </summary>
    /// <typeparam name="T">The Type of items the repository will contain</typeparam>
    internal interface IRepository<T>
    {
        List<T> GetAll();
        void Create(string name);
        bool TryFind(string name, out List<T> entities);
    }

    internal class UserMenu
    {
        private bool _exitRequested;
        private readonly IRepository<Person> _repo;
        private readonly Dictionary<int, Action> _menuItemMap;

        public UserMenu()
        {
            _repo = new InMemoryPeopleRepository();
            _menuItemMap = new Dictionary<int, Action>();
            InitializeMenuItems();
        }

        /// <summary>
        /// Shows the list of user selection items and prompts for user input.
        /// Input is routed to a dictionary where each numbered menu item is mapped to an associated action.
        /// Invalid or unparseable input prompt the user of an error and display the menu again
        /// </summary>
        public void Display()
        {
            if (_exitRequested) return;

            try
            {
                Console.WriteLine("\n1.) View Persons");
                Console.WriteLine("2.) Add Person");
                Console.WriteLine("3.) Lookup Person");
                Console.WriteLine("4.) Exit");
                Console.Write("\nPlease Enter Your Selection: ");
                var input = Convert.ToInt32(Console.ReadLine().Trim());

                _menuItemMap[input].Invoke();
            }
            catch (Exception)
            {
                OnInvalidSelection();
            }
        }

        /// <summary>
        /// Assigns user selected values to associated Action methods. 
        /// </summary>
        private void InitializeMenuItems()
        {
            _menuItemMap.Add(1, OnViewPersons);
            _menuItemMap.Add(2, OnAddPerson);
            _menuItemMap.Add(3, OnPersonSearch);
            _menuItemMap.Add(4, OnExit);
        }

        private void OnAddPerson()
        {
            try
            {
                Console.Write("\nPlease Enter The Name Of The Person You Would Like To Add: ");
                var input = Console.ReadLine();

                _repo.Create(input);

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("\nCannot Insert Blank Name.");
            }
            finally
            {
                Display();
            }
        }

        private void OnViewPersons()
        {
            try
            {
                var personsList = _repo.GetAll();

                Console.WriteLine($"\nDisplaying ({personsList.Count}) People");
                foreach (var person in personsList)
                    Console.WriteLine($"\t{personsList.IndexOf(person) + 1}.) {person.Id} - {person.Name}");
            }
            catch (Exception)
            {
                Console.WriteLine("\nWe're Sorry, There currently no people to display.");
            }
            finally
            {
                Display();
            }
        }

        private void OnPersonSearch()
        {
            try
            {
                Console.Write("\nPlease Enter A Name To Lookup: ");
                var input = Console.ReadLine();

                if (_repo.TryFind(input, out var personsList))
                {
                    Console.WriteLine($"Found ({personsList.Count}) Matching Person(s).");
                    foreach (var person in personsList)
                    {
                        Console.WriteLine($"\t{personsList.IndexOf(person) + 1}.) {person.Id} - {person.Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"No Results Found For: {input}");
                }
            }
            catch (Exception)
            {
                OnInvalidSelection();
            }
            finally
            {
                Display();
            }
        }

        private void OnInvalidSelection()
        {
            Console.WriteLine("\nInvalid Selection, Please Try Again.");
            Display();
        }

        private void OnExit()
        {
            Console.Write("\nAre You Sure You Would Like To Exit? [Y/N]: ");
            var input = Console.ReadLine();

            if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                _exitRequested = true;

            Display();
        }
    }

    internal class InMemoryPeopleRepository : IRepository<Person>
    {
        private readonly List<Person> _people;

        public InMemoryPeopleRepository()
        {
            _people = new List<Person>
            {
                new Person {Id = 1, Name = "John Smith"},
                new Person {Id = 2, Name = "Jane Doe"},
                new Person {Id = 3, Name = "Bill Brasky"},
                new Person {Id = 4, Name = "Steve Smith"}
            };
        }


        /// <summary>
        /// Get all of the People in the repository
        /// </summary>
        /// <returns>List of People objects</returns>
        /// <exception cref="Exception"></exception>
        public List<Person> GetAll()
        {
            if (!_people.Any() || _people == null)
                throw new Exception("Collection contains no values or was not initialized.");

            return _people;
        }

        /// <summary>
        /// Creates and Adds new Person to the repository
        /// </summary>
        /// <param name="name">Name of person</param>
        /// <exception cref="ArgumentNullException">Thrown when provided name is empty or null</exception>
        public void Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            _people.Add(new Person()
            {
                Id = UniqueId.GenerateId(_people.Select(p => p.Id).ToList()),
                Name = name.Trim()
            });
        }

        /// <summary>
        /// Searches for names that contain the string provided as argument.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="entities">Output variable containing List of matches</param>
        /// <returns>Boolean to indicate match found, exposes out variable containing List of matching Person objects</returns>
        /// <exception cref="Exception">Thrown on null or empty string paramenter</exception>
        /// <exception cref="NullReferenceException">Thrown on null or empty repository</exception>
        /// <remarks>Search will yeild a match if the <param name="name">Name</param> exists as a substring.
        /// If user searches for "John" and the List contains a person with Name "John Smith" a match will be found.
        /// If user searches for "John" and the List contains "John Smith" and "John Stevens" both will be matches and
        /// exposed by the <param name="entities">entities</param> output variable.
        /// </remarks>
        public bool TryFind(string name, out List<Person> entities)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Search Criteria Cannot Be Empty");

            if (!_people.Any() || _people == null)
                throw new NullReferenceException("Cannot Search Null Or Empty Collection.");

            entities = _people.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
            if (entities.Any())
                return true;

            return false;
        }
    }

    internal static class UniqueId
    {
        /// <summary>
        /// Creates unique integer ids given a list of existing ids
        /// </summary>
        /// <param name="existingIds"></param>
        /// <returns></returns>
        public static int GenerateId(List<int> existingIds)
        {
            if (!existingIds.Any())
                return 1;

            // If the minimum Id is greater than one there could be usable ID's between 1 and the Minimum value
            if (existingIds.Min() > 1)
            {
                // Create and return the minimum value in range of ints between 1 and the minimum existing id 
                return Enumerable.Range(1, existingIds.Min()).Min();
            }

            // Create a range of integer id's between the existing minimum and maximum id's
            var idRange = Enumerable.Range(existingIds.Min(), existingIds.Max()).ToList();

            // Take all of the id's in the generated range that are not present in existingId list
            var missingIds = idRange.Where(id => !existingIds.Contains(id)).ToList();

            // No missing Id's means there are no open id's available in the existingId range
            // Increment the max Id by 1 and return
            if (!missingIds.Any())
                return existingIds.Max() + 1;

            // the minimum value from missing Id's will be the first open Id
            return missingIds.Min();
        }
    }


}

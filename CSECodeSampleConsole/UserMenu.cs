using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSECodeSampleConsole
{
    public class UserMenu
    {
        private bool _exitRequested;
        private readonly IRepository<Person> _repo;
        private Dictionary<int, Action> _menuItemMap;
        
        public UserMenu()
        {
            _repo = new InMemoryPeopleRepository();
            _menuItemMap = new Dictionary<int, Action>();
            InitializeMenuItems();            
        }



        public void Display()
        {
            if (_exitRequested) return;

            try
            {
                Console.WriteLine("\n1.) View Persons");
                Console.WriteLine("2.) Add Person");
                Console.WriteLine("3.) Exit");
                Console.Write("\nPlease Enter Your Selection: ");
                var input = Convert.ToInt32(Console.ReadLine().Trim());

                _menuItemMap[input].Invoke();
            }
            catch (Exception)
            {
                OnInvalidSelection();
            }
        }

        private void InitializeMenuItems()
        {
            _menuItemMap.Add(1, OnViewPersons);
            _menuItemMap.Add(2, OnAddPerson);
            _menuItemMap.Add(3, OnExit);
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
                foreach(var person in personsList)
                    Console.WriteLine($"\t{personsList.IndexOf(person)}.) {person.Id} - {person.Name}");
                
            }
            catch(Exception)
            {                
                Console.WriteLine("\nWe're Sorry, There currently no people to display."); 
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
}

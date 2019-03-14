using System;
using System.Collections.Generic;
using System.Linq;


namespace CSECodeSampleConsole
{
    public class InMemoryPeopleRepository : IRepository<Person>
    {
        private readonly List<Person> _people;

        public InMemoryPeopleRepository()
        {            
            _people = new List<Person>
            {
                new Person { Id = 1, Name = "John Smith" },
                new Person { Id = 2, Name = "Jane Doe" },
                new Person { Id = 3, Name = "Bill Brasky" },
                new Person { Id = 4, Name = "Steve Smith" }        
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
            if(string.IsNullOrEmpty(name))
                throw new Exception("Search Criteria Cannot Be Empty");

            if (!_people.Any() || _people == null)
                throw new NullReferenceException("Cannot Search Null Or Empty Collection.");

            entities = _people.Where(p => p.Name.Contains(name)).ToList();
            if (entities.Any())
                return true;

            return false;
        }
    }
}

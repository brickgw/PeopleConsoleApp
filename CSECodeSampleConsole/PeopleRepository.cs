using System;
using System.Collections.Generic;
using System.Linq;


namespace CSECodeSampleConsole
{
    public class PeopleRepository
    {
        private readonly List<Person> _people;

        public PeopleRepository()
        {            
            _people = new List<Person>
            {
                new Person { Id = 1, Name = "John Smith" },
                new Person { Id = 2, Name = "Jane Doe" },
                new Person { Id = 3, Name = "Bill Brasky" },
                new Person { Id = 4, Name = "Steve Smith" }        
            };            
        }

        public List<Person> GetAll()
        {
            if (!_people.Any() || _people == null)
                throw new Exception("Collection contains no values or was not initialized.");
            
            return _people;
        }

        public void Insert(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Invalid Argument: Cannot insert null entity.");

            entity.Id = CreateUniqueId();
            _people.Add(entity);            
        }

        public int CreateUniqueId()
        {
            var randomGenerator = new Random();
            var existingIds = _people.Select(p => p.Id).ToList();
            var id = randomGenerator.Next(existingIds.Max(), existingIds.Max() + existingIds.Count);

            while (existingIds.Contains(id))
                randomGenerator.Next(existingIds.Max(), existingIds.Max() + existingIds.Count);

            return id;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSECodeSampleConsole
{
    public static class UniqueId
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

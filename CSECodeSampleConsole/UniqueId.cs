using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSECodeSampleConsole
{
    public static class UniqueId
    {
        public static int GenerateId(List<int> existingIds)
        {
            var randomGenerator = new Random();

            var id = randomGenerator.Next(existingIds.Max(), existingIds.Max() + existingIds.Count);

            while (existingIds.Contains(id))
                id = randomGenerator.Next(existingIds.Max(), existingIds.Max() + existingIds.Count);

            return id;
        }
    }
}

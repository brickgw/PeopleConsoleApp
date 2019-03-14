using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSECodeSampleConsole
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

    //internal class Person
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemIONet.Data;
using SystemIONet.Models;
using SystemIONet.Helpers;

namespace SystemIONet.Workflows;

public class AddStudentWorkflow
{
    public void Execute()
    {
        Console.Clear();
        Console.WriteLine("Add Student");
        Console.WriteLine(ConsoleIO.SeparatorBar);
        Console.WriteLine();

        Student newStudent = new();

        newStudent.FirstName = ConsoleIO.GetRequiredStringFromUser("First Name: ");
        newStudent.LastName = ConsoleIO.GetRequiredStringFromUser("Last Name: ");
        newStudent.Major = ConsoleIO.GetRequiredStringFromUser("Major: ");
        newStudent.GPA = ConsoleIO.GetRequiredDecimalFromUser("GPA: ");

        Console.WriteLine();
        ConsoleIO.PrintStudentListHeader();
        Console.WriteLine(ConsoleIO.StudentLineFormat, newStudent.LastName + ", " + newStudent.FirstName, newStudent.Major, newStudent.GPA);

        Console.WriteLine();
        if (ConsoleIO.GetYesNoAnswerFromUser("Add the following information") == "Y")
        {
            StudentRepository repo = new(Settings.FilePath);
            repo.Add(newStudent);
            Console.WriteLine("Student Added!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Add Cancelled");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;

class MainClass
{
    class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }


    static int ID = 1;
    static List<Person> people = new List<Person>();

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nAdd - Add a person");
            Console.WriteLine("Edit - Edit a person");
            Console.WriteLine("Remove - Remove a person");
            Console.WriteLine("List - List all persons");
            Console.WriteLine("Exit");
            Console.Write("\nEnter your action: ");
            string choice = Convert.ToString(Console.ReadLine());

            switch (choice)
            {
                case "Add":
                    AddPerson();
                    break;
                case "Edit":
                    EditPerson();
                    break;
                case "Remove":
                    RemovePerson();
                    break;
                case "List":
                    ListAllPersons();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    static void AddPerson()
    {
        Console.Write("\nName: ");
        string fn = Console.ReadLine();

        Console.Write("LastName: ");
        string ln = Console.ReadLine();

        Person user = new Person(ID++, fn, ln);
        people.Add(user);

        Console.WriteLine("\nPerson added successfully");
    }

    static void EditPerson()
    {
        Console.Write("\nEnter the id of the person you want to edit: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Person person = people.Find(p => p.Id == id);
        if (person == null)
        {
            Console.WriteLine("\nNo person found with id " + id);
            return;
        }

        Console.Write("\nEnter the new first name: ");
        string newFirstName = Console.ReadLine();

        Console.Write("Enter the new last name: ");
        string newLastName = Console.ReadLine();

        person.FirstName = newFirstName;
        person.LastName = newLastName;

        Console.WriteLine("\nPerson edited successfully");
    }


    static void RemovePerson()
    {
        Console.Write("\nEnter the id of the person you want to remove: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Person person = people.Find(p => p.Id == id);
        if (person == null)
        {
            Console.WriteLine("\nNo person found with id " + id);
            return;
        }

        people.Remove(person);

        Console.WriteLine("Person removed successfully");
    }

    static void ListAllPersons()
    {
        Console.WriteLine("Id\tFirst Name\tLast Name");
        foreach (var person in people)
        {
            Console.WriteLine(person.Id + "\t" + person.FirstName + "\t\t" + person.LastName);
        }
    }
}

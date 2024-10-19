using System;
using System.Collections.Generic;
using System.Linq;
class Person
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
}
class PersonImplementation
{
    public string GetName(IList<Person> person)
    {
        string result = "";
        foreach (var p in person)
        {
            result += p.Name + " " + p.Address + " ";
        }
        return result.Trim();

    }
    public double Average(IList<Person> person)
    {
        return person.Average(p => p.Age);

    }
    public int Max(IList<Person> person)
    {
        return person.Max(p => p.Age);
    }
}
class Source
{
    static void Main(string[] args)
    {
        var line1 = System.Console.ReadLine().Trim();
        var num = int.Parse(line1);
        IList<Person> p = new List<Person>();

        for (int i = 0; i < num; i++)
        {
            string name = Console.ReadLine();
            string address = Console.ReadLine();
            int age = Int32.Parse(Console.ReadLine());
            p.Add(new Person { Name = name, Address = address, Age = age });
        }
        PersonImplementation personImp = new PersonImplementation();
        Console.WriteLine(personImp.GetName(p));
        Console.WriteLine(personImp.Average(p));
        Console.WriteLine(personImp.Max(p));
    }
}

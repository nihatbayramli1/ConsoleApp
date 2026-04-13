using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;
using Spectre.Console;

namespace CourseSystem.Controllers;

public class StudentController
{
    private readonly StudentService _studentService = new();
    private readonly GroupService _groupService;

    public StudentController(GroupService groupService)
    {
        _groupService = groupService;
    }

    public void Create()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student name: ");
        string name = Console.ReadLine();

        Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student surname: ");
        string surname = Console.ReadLine();

        int age;
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student age: ");
            if (!int.TryParse(Console.ReadLine(), out age)) { AnsiConsole.MarkupLine("[red]Please enter a valid age![/]"); continue; }
            break;
        }

        var allGroups = _groupService.GetAll();
        if (allGroups.Count == 0) { AnsiConsole.MarkupLine("[red]No groups in the system! Please create a group first.[/]"); return; }

        Console.WriteLine();
        for (int i = 0; i < allGroups.Count; i++)
            AnsiConsole.MarkupLine($"[white]{i + 1}.[/] {allGroups[i].Name} (Teacher: {allGroups[i].Teacher})");

        Group selectedGroup = null;
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Select group number: ");
            if (!int.TryParse(Console.ReadLine(), out int groupChoice) || groupChoice < 1 || groupChoice > allGroups.Count)
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid number![/]");
                continue;
            }
            selectedGroup = allGroups[groupChoice - 1];
            break;
        }

        try
        {
            var result = _studentService.Create(new Student { Name = name, Surname = surname, Age = age, Group = selectedGroup });
            AnsiConsole.MarkupLine($"[green]Created! Id: {result.Id} | Name: {result.Name} {result.Surname} | Age: {result.Age} | Group: {result.Group.Name}[/]");
        }
        catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); }
    }

    public void GetById()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]"); continue; }
            try
            {
                var s = _studentService.GetById(id);
                AnsiConsole.MarkupLine($"[white]Id: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age} | Group: {s.Group.Name}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void Delete()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]"); continue; }
            try
            {
                _studentService.Delete(id);
                AnsiConsole.MarkupLine("[green]Student deleted successfully![/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void Update()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]"); continue; }

            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter new name: ");
            string name = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter new surname: ");
            string surname = Console.ReadLine();

            int age;
            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Blue, text: "Enter new age: ");
                if (!int.TryParse(Console.ReadLine(), out age)) { AnsiConsole.MarkupLine("[red]Please enter a valid age![/]"); continue; }
                break;
            }

            var allGroups = _groupService.GetAll();
            if (allGroups.Count == 0) { AnsiConsole.MarkupLine("[red]No groups in the system![/]"); return; }

            Console.WriteLine();
            for (int i = 0; i < allGroups.Count; i++)
                AnsiConsole.MarkupLine($"[white]{i + 1}.[/] {allGroups[i].Name} (Teacher: {allGroups[i].Teacher})");

            Group selectedGroup = null;
            while (true)
            {
                Helper.PrintConsole(ConsoleColor.Blue, text: "Select group number: ");
                if (!int.TryParse(Console.ReadLine(), out int groupChoice) || groupChoice < 1 || groupChoice > allGroups.Count)
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number![/]");
                    continue;
                }
                selectedGroup = allGroups[groupChoice - 1];
                break;
            }

            try
            {
                var result = _studentService.Update(id, new Student { Name = name, Surname = surname, Age = age, Group = selectedGroup });
                AnsiConsole.MarkupLine($"[green]Updated! Id: {result.Id} | Name: {result.Name} {result.Surname} | Age: {result.Age} | Group: {result.Group.Name}[/]");
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); }
            break;
        }
    }

    public void GetAllByAge()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter age: ");
            if (!int.TryParse(Console.ReadLine(), out int age)) { AnsiConsole.MarkupLine("[red]Please enter a valid age![/]"); continue; }
            try
            {
                var students = _studentService.GetAllByAge(age);
                foreach (var s in students)
                    AnsiConsole.MarkupLine($"[white]Id: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age} | Group: {s.Group.Name}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void GetAllByGroupId()
    {
        var allGroups = _groupService.GetAll();
        if (allGroups.Count == 0) { AnsiConsole.MarkupLine("[red]No groups in the system![/]"); return; }

        foreach (var g in allGroups)
            AnsiConsole.MarkupLine($"[white]Id: {g.Id} | Name: {g.Name} | Teacher: {g.Teacher}[/]");

        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group ID: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId)) { AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]"); continue; }
            try
            {
                var students = _studentService.GetAllByGroupId(groupId);
                foreach (var s in students)
                    AnsiConsole.MarkupLine($"[white]Id: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age} | Group: {s.Group.Name}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void SearchByNameOrSurname()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter name or surname: ");
            string search = Console.ReadLine();
            try
            {
                var students = _studentService.SearchByNameOrSurname(search);
                foreach (var s in students)
                    AnsiConsole.MarkupLine($"[white]Id: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age} | Group: {s.Group.Name}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }
}
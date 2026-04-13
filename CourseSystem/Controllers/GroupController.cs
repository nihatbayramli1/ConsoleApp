using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;
using Spectre.Console;

namespace CourseSystem.Controllers;

public class GroupController
{
    public GroupService GroupService { get; } = new();

    public void Create()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group name: ");
        string groupName = Console.ReadLine();

        Helper.PrintConsole(ConsoleColor.Blue, text: "Enter teacher name: ");
        string teacher = Console.ReadLine();

        while (true)
        {
            Helper.PrintConsole(ConsoleColor.White, text: "Enter room count: ");
            if (!int.TryParse(Console.ReadLine(), out int roomCount))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid number![/]");
                continue;
            }
            try
            {
                var result = GroupService.Create(new Group { Name = groupName, Teacher = teacher, RoomCount = roomCount });
                AnsiConsole.MarkupLine($"[green]Created! Id: {result.Id} | Name: {result.Name} | Teacher: {result.Teacher} | Rooms: {result.RoomCount}[/]");
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); }
            break;
        }
    }

    public void GetById()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]");
                continue;
            }
            var group = GroupService.GetById(id);
            if (group == null) { AnsiConsole.MarkupLine("[red]Group not found![/]"); continue; }
            AnsiConsole.MarkupLine($"[white]Id: {group.Id} | Name: {group.Name} | Teacher: {group.Teacher} | Rooms: {group.RoomCount}[/]");
            break;
        }
    }

    public void GetAll()
    {
        var groups = GroupService.GetAll();
        if (groups.Count == 0) { AnsiConsole.MarkupLine("[red]No groups in the system![/]"); return; }
        foreach (var g in groups)
            AnsiConsole.MarkupLine($"[white]Id: {g.Id} | Name: {g.Name} | Teacher: {g.Teacher} | Rooms: {g.RoomCount}[/]");
    }

    public void Delete()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]");
                continue;
            }
            try
            {
                GroupService.Delete(id);
                AnsiConsole.MarkupLine("[green]Group deleted successfully![/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void Update()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid ID![/]");
                continue;
            }

            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter new group name: ");
            string groupName = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter new teacher name: ");
            string teacher = Console.ReadLine();

            while (true)
            {
                Helper.PrintConsole(ConsoleColor.White, text: "Enter new room count: ");
                if (!int.TryParse(Console.ReadLine(), out int roomCount))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number![/]");
                    continue;
                }
                try
                {
                    var result = GroupService.Update(id, new Group { Name = groupName, Teacher = teacher, RoomCount = roomCount });
                    AnsiConsole.MarkupLine($"[green]Updated! Id: {result.Id} | Name: {result.Name} | Teacher: {result.Teacher} | Rooms: {result.RoomCount}[/]");
                }
                catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); }
                break;
            }
            break;
        }
    }

    public void GetAllByTeacher()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter teacher name: ");
            string teacher = Console.ReadLine();
            try
            {
                var groups = GroupService.GetAllByTeacher(teacher);
                foreach (var g in groups)
                    AnsiConsole.MarkupLine($"[white]Id: {g.Id} | Name: {g.Name} | Teacher: {g.Teacher} | Rooms: {g.RoomCount}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void GetAllByRoom()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter room count: ");
            if (!int.TryParse(Console.ReadLine(), out int roomCount))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid number![/]");
                continue;
            }
            try
            {
                var groups = GroupService.GetAllByRoom(roomCount);
                foreach (var g in groups)
                    AnsiConsole.MarkupLine($"[white]Id: {g.Id} | Name: {g.Name} | Teacher: {g.Teacher} | Rooms: {g.RoomCount}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }

    public void SearchByName()
    {
        while (true)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Enter group name to search: ");
            string name = Console.ReadLine();
            try
            {
                var groups = GroupService.SearchByName(name);
                foreach (var g in groups)
                    AnsiConsole.MarkupLine($"[white]Id: {g.Id} | Name: {g.Name} | Teacher: {g.Teacher} | Rooms: {g.RoomCount}[/]");
                break;
            }
            catch (Exception ex) { AnsiConsole.MarkupLine($"[red]{ex.Message}[/]"); break; }
        }
    }
}
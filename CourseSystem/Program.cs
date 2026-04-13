using CourseSystem.Controllers;
using Spectre.Console;

namespace CourseSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupController = new GroupController();
            var studentController = new StudentController(groupController.GroupService);

            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[White]Main Menu[/]");
                AnsiConsole.MarkupLine("[white]1.[/] Groups");
                AnsiConsole.MarkupLine("[white]2.[/] Students");
                AnsiConsole.MarkupLine("[red]0.[/] Exit");
                AnsiConsole.Markup("\n[grey]Your choice: [/]");

                string mainChoice = Console.ReadLine();

                if (mainChoice == "0") break;

                if (mainChoice == "1")
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[yellow]Groups[/]");
                    AnsiConsole.MarkupLine("[white]1.[/] Create");
                    AnsiConsole.MarkupLine("[white]2.[/] Update");
                    AnsiConsole.MarkupLine("[white]3.[/] Delete");
                    AnsiConsole.MarkupLine("[white]4.[/] Find with ID");
                    AnsiConsole.MarkupLine("[white]5.[/] Find with teacher");
                    AnsiConsole.MarkupLine("[white]6.[/] Find with count");
                    AnsiConsole.MarkupLine("[white]7.[/] Search by name");
                    AnsiConsole.MarkupLine("[white]8.[/] Show all");
                    AnsiConsole.MarkupLine("[red]0.[/] Back");
                    AnsiConsole.Markup("\n[grey]Your choice: [/]");

                    switch (Console.ReadLine())
                    {
                        case "1": groupController.Create(); break;
                        case "2": groupController.Update(); break;
                        case "3": groupController.Delete(); break;
                        case "4": groupController.GetById(); break;
                        case "5": groupController.GetAllByTeacher(); break;
                        case "6": groupController.GetAllByRoom(); break;
                        case "7": groupController.SearchByName(); break;
                        case "8": groupController.GetAll(); break;
                        case "0": continue;
                        default: AnsiConsole.MarkupLine("[red]Invalid choice![/]"); break;
                    }
                }
                else if (mainChoice == "2")
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[yellow]======= Students =======[/]");
                    AnsiConsole.MarkupLine("[white]1.[/] Create");
                    AnsiConsole.MarkupLine("[white]2.[/] Update");
                    AnsiConsole.MarkupLine("[white]3.[/] Delete");
                    AnsiConsole.MarkupLine("[white]4.[/] Get by ID");
                    AnsiConsole.MarkupLine("[white]5.[/] Get by age");
                    AnsiConsole.MarkupLine("[white]6.[/] Get by group ID");
                    AnsiConsole.MarkupLine("[white]7.[/] Search by name or surname");
                    AnsiConsole.MarkupLine("[red]0.[/] Back");
                    AnsiConsole.Markup("\n[grey]Your choice: [/]");

                    switch (Console.ReadLine())
                    {
                        case "1": studentController.Create(); break;
                        case "2": studentController.Update(); break;
                        case "3": studentController.Delete(); break;
                        case "4": studentController.GetById(); break;
                        case "5": studentController.GetAllByAge(); break;
                        case "6": studentController.GetAllByGroupId(); break;
                        case "7": studentController.SearchByNameOrSurname(); break;
                        case "0": continue;
                        default: AnsiConsole.MarkupLine("[red]Invalid choice![/]"); break;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Invalid choice![/]");
                }

                Console.ReadKey(true);
            }
        }
    }
}
using System;
using System.Linq;
using ToDoList;
using Microsoft.EntityFrameworkCore;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ToDoContext();

            Console.WriteLine("To-Do List Application");
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add To-Do Item");
                Console.WriteLine("2. View All To-Do Items");
                Console.WriteLine("3. Update To-Do Item");
                Console.WriteLine("4. Delete To-Do Item");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddToDoItem(db);
                        break;
                    case "2":
                        ViewToDoItems(db);
                        break;
                    case "3":
                        UpdateToDoItem(db);
                        break;
                    case "4":
                        DeleteToDoItem(db);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddToDoItem(ToDoContext db)
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            var newItem = new ToDoItem { Title = title, Description = description, IsCompleted = false };
            db.ToDoItems.Add(newItem);
            db.SaveChanges();

            Console.WriteLine("To-Do item added successfully!");
        }

        static void ViewToDoItems(ToDoContext db)
        {
            var items = db.ToDoItems.ToList();
            if (!items.Any())
            {
                Console.WriteLine("No to-do items found.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Title: {item.Title}, Description: {item.Description}, Completed: {item.IsCompleted}");
                }
            }
        }

        static void UpdateToDoItem(ToDoContext db)
        {
            Console.Write("Enter the ID of the to-do item to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var item = db.ToDoItems.Find(id);
                if (item != null)
                {
                    Console.Write("Enter new title: ");
                    item.Title = Console.ReadLine();

                    Console.Write("Enter new description: ");
                    item.Description = Console.ReadLine();

                    Console.Write("Is it completed? (yes/no): ");
                    item.IsCompleted = Console.ReadLine().ToLower() == "yes";

                    db.SaveChanges();
                    Console.WriteLine("To-Do item updated successfully!");
                }
                else
                {
                    Console.WriteLine("To-Do item not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void DeleteToDoItem(ToDoContext db)
        {
            Console.Write("Enter the ID of the to-do item to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var item = db.ToDoItems.Find(id);
                if (item != null)
                {
                    db.ToDoItems.Remove(item);
                    db.SaveChanges();
                    Console.WriteLine("To-Do item deleted successfully!");
                }
                else
                {
                    Console.WriteLine("To-Do item not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
    }
}

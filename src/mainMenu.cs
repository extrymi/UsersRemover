namespace UsersTool;
using System;
using System.Threading;
public class mainMenu
{
   
    public static void menu(string[] args)
    
    {
        
        string[] menuItems = args;
        Console.Clear();
        Console.WriteLine("СУПЕР СИЛА - СИЛ. АДМИНА\n");
        
        int row = Console.CursorTop;
        int col = Console.CursorLeft;
        int index = 0;
        List<int> specialKeys = new List<int>();
        while (true)
        {
            DrawMenu(menuItems, row, col, index, specialKeys);
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.DownArrow:
                    if (index < menuItems.Length - 1)
                        index++;
                    else index = 0;
                    Console.WriteLine(index);
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0)
                        index--;
                    else index = menuItems.Length - 1;
                    Console.WriteLine(index);
                    break;
                case ConsoleKey.Backspace:
                    if (specialKeys.Contains(index))
                    {
                        specialKeys.Remove(index);
                    }
                    else
                    {
                        specialKeys.Add(index);
                    }
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    switch (index)
                    {
                        default:
                            if (!specialKeys.Contains(index)) {
                                specialKeys.Add(index);
                            }
                            for (int i = 0; i < specialKeys.Count; ++i)
                            {
                                string command;
                                command = $"net user {menuItems[specialKeys[i]]} /delete";
                                Console.WriteLine($"Удаляю пользователя: {menuItems[specialKeys[i]]}: {command}");
                                System.Diagnostics.Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                Thread.Sleep(50);
                                Console.WriteLine();
                                
                                Console.WriteLine($@"Попытка удалить папку пользователя по пути C:\Users\{menuItems[specialKeys[i]]}");
                                try
                                {
                                    command = $"takeown /F C:\\Users\\{menuItems[specialKeys[i]]} /R /A";
                                    Console.WriteLine($@"Делаем администраторов владельцами папки: {command}");
                                    System.Diagnostics.Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    Thread.Sleep(50);
                                    Console.WriteLine();

                                    command = $"icacls C:\\Users\\{menuItems[specialKeys[i]]} /T /Inheritance:e /grant *S-1-5-32-544:F";
                                    Console.WriteLine($@"Предоставляем все права администраторам: {command}");
                                    System.Diagnostics.Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    Thread.Sleep(50);
                                    Console.WriteLine();

                                    command = $"DEL /F /Q /S C:\\Users\\{menuItems[specialKeys[i]]} > NUL";
                                    Console.WriteLine($@"Удаляем папку: ");
                                    System.Diagnostics.Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    
                                    command = $"rmdir /s /q C:\\Users\\{menuItems[specialKeys[i]]}";
                                    System.Diagnostics.Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    Thread.Sleep(50);
                                    
                                }
                                catch
                                {   
                                    Thread.Sleep(100);
                                    
                                }
                                
                            }
                            Console.WriteLine();
                            Console.WriteLine("Нажмите любую клавишу для завершения работы...");
                            Console.ReadKey();
                            return;


                            break;
                    }
                    break;
                    
            }
        }
        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для завершения работы...");
        Console.ReadKey();
    }

    private static void DrawMenu(string[] items, int row, int col, int index, List<int> list)
    {
        Console.SetCursorPosition(col, row);
        for (int i = 0; i < items.Length; i++)
        {
            if (i == index || list.Contains(i))
            {
                Console.WriteLine($"[ * ]{items[i]}");
            } else
            {
                Console.WriteLine($"[   ]{items[i]}");
            }
            
        }
        Console.WriteLine();
    }

}
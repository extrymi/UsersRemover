using Microsoft.VisualBasic;

namespace UsersTool;
using System;
using System.Diagnostics;
using System.Threading;
public class mainMenu
{
   
    public static void menu(string[] args)
    
    {
        
        string[] menuItems = args;
        Console.Clear();
        Console.WriteLine("СУПЕР СИЛА - СИЛ. АДМИНА:\n");
        
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
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0)
                        index--;
                    else index = menuItems.Length - 1;
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
                            Boolean takeOwn = false;
                            Boolean takePerm = false;
                            new addParametersMenu();
                            Console.Clear();
                            int[] permission = addParametersMenu.menu(["Далее", "Назначить администраторов владельцами файлов/папок (ЧУТЬ МЕДЛЕНЕЕ)", "Предоставить администраторам все права на файлы/папок (МЕДЛЕННО)"]);
                            if (permission[0] == 1) takeOwn = true;
                            if (permission[1] == 1) takePerm = true;
                            if (!specialKeys.Contains(index)) {
                                specialKeys.Add(index);
                            }
                            for (int i = 0; i < specialKeys.Count; ++i)
                            {
                                string command;

                                command = "query session";
                                Process terminateSessionProcess = new Process();
                                terminateSessionProcess.StartInfo.FileName = "cmd.exe";
                                terminateSessionProcess.StartInfo.Arguments = "/c " + command;
                                terminateSessionProcess.StartInfo.UseShellExecute = false;
                                terminateSessionProcess.StartInfo.RedirectStandardOutput = true;
                                terminateSessionProcess.Start();
                                
                                
                                string output = terminateSessionProcess.StandardOutput.ReadToEnd();
                                terminateSessionProcess.WaitForExit();
                                string Ready = output.Substring(output.IndexOf(menuItems[specialKeys[i]]) + menuItems[specialKeys[i]].Length).Trim();
                                string[] words = Ready.Split(new char[] { ' ' });
                                Ready = words[0];

                                command = $"logoff {Ready}";
                                Console.WriteLine("Попытка завершить сеанс пользователя...");
                                Process.Start("CMD.exe", $"/C {command}").WaitForExit();
                                Thread.Sleep(50);
                                Console.WriteLine();
                                
                                command = $"net user \"{menuItems[specialKeys[i]]}\" /delete";
                                Console.WriteLine($"Удаляю пользователя {menuItems[specialKeys[i]]}: {command}");
                                Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                Thread.Sleep(50);
                                Console.WriteLine();
                                
                                Console.WriteLine($@"Попытка удалить папку пользователя по пути C:\Users\{menuItems[specialKeys[i]]}");
                                try
                                {
                                    command = $"echo Y | takeown /F \"C:\\Users\\{menuItems[specialKeys[i]]}\" /R /A";
                                    Process takeownProcess = new Process();
                                    takeownProcess.StartInfo.FileName = "cmd.exe";
                                    takeownProcess.StartInfo.Arguments = "/c " + command;
                                    
                                    
                                    command = $"echo Y | icacls \"C:\\Users\\{menuItems[specialKeys[i]]}\" /T /C /Inheritance:e /grant *S-1-5-32-544:F";
                                    Process everythingPermissionProcess = new Process();
                                    everythingPermissionProcess.StartInfo.FileName = "cmd.exe";
                                    everythingPermissionProcess.StartInfo.Arguments = "/c " + command;
                                    
                                    if (takeOwn)
                                    {
                                        Console.WriteLine("Делаем администраторов владельцами папки:\n");
                                        takeownProcess.Start();
                                        takeownProcess.WaitForExit();
                                    }
                                    if (takePerm)
                                    {
                                        Console.WriteLine("Gредоставляем все права администраторам:\n");
                                        everythingPermissionProcess.Start();
                                        everythingPermissionProcess.WaitForExit();
                                    }
                                    
                                    Console.WriteLine();

                                    command = $"DEL /F /Q /S \"C:\\Users\\{menuItems[specialKeys[i]]}\" > NUL";
                                    Console.WriteLine($@"Удаляем папку: ");
                                    Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    
                                    command = $"rmdir /s /q \"C:\\Users\\{menuItems[specialKeys[i]]}\"";
                                    Process.Start("CMD.exe",$"/C  {command}").WaitForExit();
                                    Thread.Sleep(50);
                                    
                                }
                                catch
                                {   
                                    Thread.Sleep(100);
                                    
                                }
                                
                            }
                            Console.WriteLine();
                            for (int i = 0; i < specialKeys.Count; ++i)
                            {
                                if (Directory.Exists($"C:\\Users\\{menuItems[specialKeys[i]]}"))
                                {
                                    Console.WriteLine($"Не удалось удалить папку пользователя {menuItems[specialKeys[i]]}");
                                } else Console.WriteLine($"Папка пользователя {menuItems[specialKeys[i]]}, успешно удалена!");
                            }
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
namespace UsersTool;

public class addParametersMenu
{
    public static int[] menu(string[] args)
    
    {
        
        string[] menuItems = args;
        Console.Clear();
        Console.WriteLine("Дополнительные параметры:\n");
        
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
                            int[] choice = new int[2];
                            if (!specialKeys.Contains(index)) {
                                specialKeys.Add(index);
                            }
                            if (specialKeys.Contains(1)) choice[0] = 1; else choice[0] = 0;
                            if (specialKeys.Contains(2)) choice[1] = 1; else choice[1] = 0;
                            Console.ReadKey();
                            return choice;
                            

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
        Console.WriteLine("ВЫБИРАЙТЕ ДОП. ПАРАМЕТРЫ ТОЛЬКО ЕСЛИ БЕЗ НИХ УДАЛИТЬ ПОЛЬЗОВАТЕЛЯ И ВСЕ ЕГО ФАЙЛЫ НЕ ПОЛУЧАЕТСЯ\n");
    }
}
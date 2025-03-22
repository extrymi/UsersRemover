// See https://aka.ms/new-console-template for more information
namespace UsersTool;
using System.DirectoryServices.AccountManagement;

internal class Program
{   
    
    public static void Main(string[] args)
    {   List<string> users = new List<string>();
        // mainMenu.Menu(["mainMenu1"]);
        PrincipalContext context = new PrincipalContext(ContextType.Machine);
        UserPrincipal user = new UserPrincipal(context);
        PrincipalSearcher searcher = new PrincipalSearcher(user);
        foreach (var principal in searcher.FindAll())
        {
            users.Add(principal.SamAccountName);
        } 
        new mainMenu();
        mainMenu.menu(users.ToArray());

    }
    
}
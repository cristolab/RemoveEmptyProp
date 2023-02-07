namespace RemoveEmptyProp 
{
    using System;
    using Tools.Utils;
    internal class Program
    {
        private static List<string>? _ghostStrings; 

        private static void Main(string[] args)        
        {   
            ConsoleUtil.WriteColoredBlock("Line Remover Tool \r\n version:1.0.0", ConsoleColor.DarkCyan);
            if (args.Length != 1) {
                Console.WriteLine("Usage: RemoveEmptyProp.exe [FILE.DAT] or [*.TXT]"); 
                Console.WriteLine("(Press Enter)"); 
                Console.ReadLine();               
            } else 
            {
                InitGhostList();                        
                var fCleaner = new FileCleaner(filter: args[0],
                                            ghostStrings: _ghostStrings);   
                fCleaner.RemoveGhosts();            
            }
        }

        private static void InitGhostList() {
            _ghostStrings = new List<string>();
            _ghostStrings.Add("properties : {}");
        }
    }
}

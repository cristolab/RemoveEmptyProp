namespace RemoveEmptyProp 
{
    using System;
    using Tools;    

    internal class Program
    {
        private static List<string>? _ghostStrings; 

        //////////////////////////////////////////////////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////////////////////////////////////////////////
        private static void Main(string[] args)        
        {   
            ConsoleUtil.WriteColoredBlock("Line Remover Tool \r\n version:1.0.0", ConsoleColor.DarkCyan);
            if (args.Length != 1) {
                Console.WriteLine("Usage: RemoveEmptyProp.exe [FILE.DAT] or [*.TXT]"); 
            } else 
            {
                if (InitGhostList()) {
                    var fCleaner = new FileCleaner(filter: args[0], ghostStrings: _ghostStrings);   
                    fCleaner.RemoveGhosts();            
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("(Press Enter)"); 
            Console.ReadLine();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////
        // Initialisation de la liste des chaines ghost à detecter et à purger des fichiers cibles
        //////////////////////////////////////////////////////////////////////////////////////////////
        private static bool InitGhostList() {
            _ghostStrings = null;
            try {
                _ghostStrings = ConfigReader.GetGhostStringList();
            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message); 
            }            
            return (_ghostStrings != null);
        }
    }
}

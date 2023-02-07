///////////////////////////////////////////////////////////////
//                   Tools.Utils
// object : Outil de controle sur filtres/fichiers
///////////////////////////////////////////////////////////////
namespace Tools.Utils
{
    public class FileCleaner {

        // Ctor
        public FileCleaner(string filter, List<string>? ghostStrings) {
            if (filter.Trim() == "") throw new ArgumentNullException(nameof(filter));            
            if ((ghostStrings == null) || (ghostStrings.Count < 1)) throw new ArgumentException(nameof(ghostStrings));            
            this._filter = filter;
            this._current_path = Path.GetDirectoryName(filter);
            this._ghostStrings = ghostStrings;            
        }        

        // Removing Ghost Strings in the selection of files
        public void RemoveGhosts() {
            try {
                ConsoleUtil.WriteColoredBlock("Start removing ghosts", ConsoleColor.Cyan);
                IEnumerable<string> fileList = getFileList();
                IEnumerable<string> savedLines;            
                string tempFileName;
                int origNbLines;                
                int nbRemovedLines;
                foreach (var fname in fileList) {
                    Console.Write($"> cleaning [{Path.GetFileName(fname)}] : ");
                    tempFileName = Path.GetTempFileName();
                    origNbLines = File.ReadAllLines(fname).Count();                    
                    savedLines = File.ReadLines(fname).Where(line => !(_ghostStrings.Contains(line.Trim())));
                    nbRemovedLines = origNbLines - savedLines.Count();

                    File.WriteAllLines(tempFileName, savedLines);
                    File.Delete(fname);
                    File.Move(tempFileName, fname);                                    
                    Console.ForegroundColor = nbRemovedLines > 0 ? ConsoleColor.Green : ConsoleColor.DarkGray;
                    Console.WriteLine($" OK => {nbRemovedLines}/{origNbLines} lines removed ");
                    Console.ForegroundColor = ConsoleColor.White;            
                }
                
                ConsoleUtil.WriteColoredBlock($"End removing ghosts ({fileList.Count()} files processed)", ConsoleColor.Cyan);
                Console.WriteLine("(Press Enter)"); 
                Console.ReadLine();   

            } catch(Exception e) {
                Console.WriteLine("");    
                Console.ForegroundColor = ConsoleColor.Red;            
                Console.WriteLine("> @@ Error RemoveGhost(): " + e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("(Press Enter)"); 
                Console.ReadLine();                
                throw;   
            }
        }

        // Getting the list of file matching with the inner filter
        private IEnumerable<string> getFileList() {            
            if ((_current_path == null) ||(_current_path.Trim() == "")) {
                return Directory.EnumerateFiles(Directory.GetCurrentDirectory(), _filter, SearchOption.TopDirectoryOnly);   
            } else {
                return Directory.EnumerateFiles(_current_path, Path.GetFileName(_filter), SearchOption.TopDirectoryOnly);
            }            
        }

        private readonly string _filter;

        private readonly List<string> _ghostStrings;

        private readonly string? _current_path;
    }

    // Console Utils class (static)
    public static class ConsoleUtil {
        public static void WriteColoredBlock(string message, ConsoleColor color) {
            if (message.Trim() == "") throw new ArgumentException(nameof(message));
            string[] lines = message.Split("\r\n");
            string prefixLine;
            int maxLength = 0;
            foreach (string line in lines) {
                maxLength = line.Length > maxLength ? line.Length : maxLength;
            }

            Console.ForegroundColor = color;                        
            Console.WriteLine("*".PadRight(maxLength + 6, '*'));
            foreach (string line in lines) {
                Console.Write("*");
                Console.ForegroundColor = ConsoleColor.White;
                prefixLine = "  " + line;
                Console.Write(prefixLine.PadRight(maxLength + 4, ' '));
                Console.ForegroundColor = color;            
                Console.WriteLine("*");           
            }

            Console.WriteLine("*".PadRight(maxLength + 6, '*'));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}


///////////////////////////////////////////////////////////////
//                   Tools.Config
// object : outil de configuration
///////////////////////////////////////////////////////////////
namespace RemoveEmptyProp.Tools
{
    using System.Reflection;

    public static class ConfigReader {

        public const string CONFIG_FILE_NAME = "RemoveStrings.txt";        

        public static List<string> GetGhostStringList() {                        
            string? exeDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);            
            if (!String.IsNullOrEmpty(exeDirPath)) {
                string configPath = Path.Combine(exeDirPath, CONFIG_FILE_NAME);
                Console.WriteLine(configPath);
                if (File.Exists(configPath)) {
                    return File.ReadAllLines(configPath).ToList();
                } else {
                    throw new Exception($"Error ConfigReader(): Unable to find the configuration file {configPath} !");                                
                }                                        
            } else {
                throw new Exception($"Error ConfigReader(): Unable to get the exe path!");
            }
        }
    }
}

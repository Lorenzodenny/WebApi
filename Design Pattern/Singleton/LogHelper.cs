namespace WebApi.Model.Static
{
    public class LogHelper
    {
        private static LogHelper _instance;
        private static readonly object _lock = new object();

        // Costruttore privato impedisce la creazione di istanze esterne
        private LogHelper() { }

        // Proprietà statica per accedere all'istanza
        public static LogHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // Sicurezza thread-safe
                    {
                        if (_instance == null)
                        {
                            _instance = new LogHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        // Proprietà per impostare l'azione di log corrente
        public Action<string> CurrentLogAction { get; set; } = Console.WriteLine;


        // Configura il metodo di logging in base all'ambiente
        public void ConfigureLogging(bool isProduction)
        {
            CurrentLogAction = isProduction ? LogToFile : Console.WriteLine;
        }


        // Metodo per loggare su file
        private void LogToFile(string message)
        {
            // Implementazione di un log su file
            File.AppendAllText("log.txt", message + Environment.NewLine);
        }


        // Metodo per loggare i messaggi
        public void Log(string message)
        {
            CurrentLogAction(message);
        }
    }

}

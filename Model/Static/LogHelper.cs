namespace WebApi.Model.Static
{
    public static class LogHelper
    {
        public static Action<string> CurrentLogAction { get; set; } = Console.WriteLine;
        public static void Log(string message)
        {
            CurrentLogAction(message);
        }

        public static void ConfigureLogging(bool isProduction)
        {
            if (isProduction)
            {
                CurrentLogAction = LogToFile;
            }
            else
            {
                CurrentLogAction = Console.WriteLine;
            }
        }

        private static void LogToFile(string message)
        {
            // Implementazione di un log su file
            File.AppendAllText("log.txt", message + Environment.NewLine);
        }
    }
}

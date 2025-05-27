namespace StockProject.Data.Services
{
    public class LogService
    {
        private readonly string _logFilePath;
        public LogService()
        {
            //var logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logDir); // Logs 디렉토리 생성
            
            //_logFilePath = Path.Combine(logDir, $"log_{DateTime.Now:yyyyMMdd}.csv");
            _logFilePath = Path.Combine(logDir, $"log.csv");
            
            if (!File.Exists(_logFilePath))
            {
                File.AppendAllText(_logFilePath, "Date,Action,Message" + Environment.NewLine);
            }
        }
        public async Task LogAsync(string category, string message)
        {
            var safeMessage = message.Replace("\"", "\"\""); // " → ""
            var logEntry = $"\"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\",\"{category}\",\"{safeMessage}\"{Environment.NewLine}";
            await File.AppendAllTextAsync(_logFilePath, logEntry);
        }



    }
}

    


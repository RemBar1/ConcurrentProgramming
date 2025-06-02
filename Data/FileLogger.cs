namespace ConcurrentProgramming.Data
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object fileLock = new();

        public FileLogger(string filePath)
        {
            _filePath = filePath;
            File.WriteAllText(_filePath, string.Empty);
        }

        public void Log(string message)
        {
            lock (fileLock)
            {
                File.AppendAllText(_filePath, message + Environment.NewLine);
            }
        }
    }
}
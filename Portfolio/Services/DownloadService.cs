namespace Portfolio.Services
{
    // Create this service class in your project
    public class DownloadService
    {
        public async Task<byte[]> GenerateSampleFile()
        {
            // Example: Generate a simple text file
            var content = "Hello, this is a sample file!\nGenerated at: " + DateTime.Now;
            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        public string GetContentType(string fileName)
        {
            return Path.GetExtension(fileName).ToLowerInvariant() switch
            {
                ".txt" => "text/plain",
                ".pdf" => "application/pdf",
                ".csv" => "text/csv",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }
    }
}

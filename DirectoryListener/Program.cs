// See https://aka.ms/new-console-template for more information
using DirectoryListener;
using Source.Triggers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

internal class Program
{
    private static string regexPattern = @"""([A-Za-z0-9+/=]+)""";
    private static void Main(string[] args)
    {
        string path = @"C:\Users\asger\Documents\Warcraft III\CustomMapData\MyHeroSurvivalMap"; // Set the directory to watch
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = path;

        // Watch for new files
        watcher.Filter = "*.pld"; // Monitor all files
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

        // Event triggered when a new file is created
        watcher.Created += OnNewFileDetected;

        // Start watching
        watcher.EnableRaisingEvents = true;

        Console.WriteLine($"Monitoring {path} for new files...");
        Console.ReadLine(); // Keep the console running
    }

    private static void OnNewFileDetected(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"New file detected: {e.Name} at {e.FullPath}");

        var content = string.Empty;

        try
        {
            content = File.ReadAllText(e.FullPath);
        }
        catch (IOException)
        {
            Thread.Sleep(1000);
            content = File.ReadAllText(e.FullPath);
        }
        

        // Define the regex pattern to match Base64 strings inside quotes

        // Use Regex to find matches
        MatchCollection matches = Regex.Matches(content, regexPattern);

        StringBuilder sb = new StringBuilder();

        Console.WriteLine("Extracted Base64 Values:");
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Groups[1].Value);
            sb.Append(match.Groups[1].Value);
        }

        byte[] data = Convert.FromBase64String(sb.ToString());

        // Convert byte array to readable text
        string decodedText = Encoding.UTF8.GetString(data);

        Console.WriteLine("Decoded Text: " + decodedText);

        var deserialized = JsonSerializer.Deserialize<WC3SaveWrapper<PipelineCommand>>(decodedText);
        var innerDeserialized = JsonSerializer.Deserialize<PipelineWrapper>(deserialized.SaveString);

        Console.WriteLine(innerDeserialized.PipelineCommands.First().Value.PlayerName);
        Console.WriteLine(innerDeserialized.PipelineCommands.First().Value.PipelineName);
    }
}
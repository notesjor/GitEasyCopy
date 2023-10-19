namespace GitEasyCopy
{
  internal class Program
  {
    static void Main(params string[] args)
    {
      var pathInput = args[0];
      var pathOutput = args[1];

      // Durchsuche das Input-Verzeichnis nach Unterverzeichnissen
      foreach (string dir in Directory.GetDirectories(pathInput))
      {
        try
        {
          // Überprüfe, ob es sich um ein Git-Repository handelt
          if (Directory.Exists(Path.Combine(dir, ".git")))
          {
            // Wenn es sich um ein Git-Repository handelt, kopieren Sie es nach Output
            var outputDir = Path.Combine(pathOutput, Path.GetFileName(dir));
            Directory.CreateDirectory(outputDir);

            // Kopieren nur die Git-Daten (bare repository) und keine ausgecheckten Code-Dateien
            foreach (string file in Directory.GetFiles(dir, ".git\\*", SearchOption.AllDirectories))
            {
              var destFile = Path.Combine(outputDir, file.Substring(dir.Length + 1));
              var destDir = Path.GetDirectoryName(destFile);
              if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
              File.Copy(file, destFile);
            }
          }
        }
        catch
        {
          // ignore
        }
      }
    }
  }
}
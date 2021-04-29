using System;
using System.Diagnostics;
using System.IO;


namespace NF64.Windows.ExtractPboWrap
{
    internal sealed class ExtractPboParameter
    {
        public string ExePath { get; }

        public string PboFilePath { get;}


        public ExtractPboParameter(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if (args.Length != 2)
                throw new ArgumentException("Need 2 Parameters. Ex: [ExePath] [PboFilePath]");

            this.ExePath = LoadFilePath(args[0]?.Trim('"'));
            this.PboFilePath = LoadPboFilePath(args[1]?.Trim('"'));
        }


        public ProcessStartInfo GetProcessStartInfo() => new ProcessStartInfo(this.ExePath, $"\"{this.PboFilePath}\"");


        private static string LoadFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException($"'{path}' is null or empty.", nameof(path));

            var fullPath = Path.GetFullPath(path);
            if (Directory.Exists(fullPath))
                throw new FileNotFoundException($"'{fullPath}' was not file.", path);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"'{fullPath}' could be not found.", path);
            return fullPath;
        }


        private static string LoadPboFilePath(string path)
        {
            var fullPath = LoadFilePath(path);
            var ext = Path.GetExtension(fullPath);
            if (ext != ".pbo")
                throw new FormatException($"'{fullPath}' could be not '.pbo'.");
            return fullPath;
        }
    }
}

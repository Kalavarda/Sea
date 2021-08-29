using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using Sea.Models;
using Sea.Models.Interfaces;
using Path = System.IO.Path;

namespace Sea.Repositories
{
    public class FileGameRepository: IGameRepository
    {
        public Game Load()
        {
            try
            {
                var fileInfo = new DirectoryInfo(GetFolder())
                    .GetFiles("*.game.json")
                    .OrderBy(f => f.Name)
                    .LastOrDefault();
                if (fileInfo == null)
                    return null;

                using var file = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(file);
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<Game>(json);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetBaseException().Message);
                return null;
            }
        }

        public void Save(Game game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            var json = JsonSerializer.Serialize(game, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });

            using var file = new FileStream(CreateFileName(game), FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(file);
            writer.Write(json);
        }

        private static string CreateFileName(Game game)
        {
            var folder = GetFolder();
            return Path.Combine(folder, $"{game.CreationTime:yyyy-MM-dd_HH-mm-ss}.game.json");
        }

        private static string GetFolder()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Sea");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
    }
}

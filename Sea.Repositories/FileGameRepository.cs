using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Sea.Models;
using Sea.Models.Repositories;

namespace Sea.Repositories
{
    public class FileGameRepository: IGameRepository
    {
        public async Task<Game> Load(CancellationToken cancellationToken)
        {
            var fileInfo = new DirectoryInfo(GetFolder())
                .GetFiles("*.game.json")
                .OrderBy(f => f.Name)
                .LastOrDefault();
            if (fileInfo == null)
                return null;

            await using var file = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(file);
            var json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<Game>(json);
        }

        public async Task Save(Game game, CancellationToken cancellationToken)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            var json = JsonSerializer.Serialize(game, new JsonSerializerOptions { WriteIndented = true });

            await using var file = new FileStream(CreateFileName(game), FileMode.Create, FileAccess.Write, FileShare.None);
            await using var writer = new StreamWriter(file);
            await writer.WriteAsync(json);
        }

        private static string CreateFileName(Game game)
        {
            var folder = GetFolder();
            return Path.Combine(folder, $"{game.CreationTime:yyyy-MM-dd_hh-mm-ss}.game.json");
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

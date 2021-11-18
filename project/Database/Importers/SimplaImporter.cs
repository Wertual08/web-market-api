using System;
using System.IO;
using System.Threading.Tasks;

namespace Api.Database.Importers {
    public class SimplaImporter : IDisposable {
        private readonly StreamReader reader;

        public SimplaImporter(string path) {
            reader = new StreamReader(File.OpenRead(path));
        }

        public ImporterState? ChoozeState(string line) {
            if (line.StartsWith('(')) {
                return null;
            }

            if (line.Contains("s_articles")) {
                return ImporterState.Article;
            } 

            if (line.Contains("s_categories")) {
                return ImporterState.Section;
            }

            return ImporterState.Unknown;
        }

        public Task ImportAsync() {
            return Task.Run(() => {
                string line;
                while ((line = reader.ReadLine()) is not null) {
                
                };
            });
        }

        public void Dispose() {
            reader.Dispose();
        }
    }
}
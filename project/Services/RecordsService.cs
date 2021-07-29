using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
    public class RecordsService {
        private readonly RecordsRepository RecordsRepository;
        private readonly static string storageDirectory = "storage/records";

        private string GetPath(Guid identifier) {
            return Path.Combine(storageDirectory, identifier.ToString("N"));
        }

        public RecordsService(RecordsRepository recordsRepository) {
            RecordsRepository = recordsRepository;
        }
        
        public async Task<IEnumerable<Record>> GetAsync(int page) {
            int pageSize = 32;
            return await RecordsRepository.ListAsync(page * pageSize, pageSize);
        }

        public async Task<Tuple<Stream, string>> GetAsync(string identifier) {
            var image = await RecordsRepository.FindAsync(identifier);

            if (image is null) {
                return null;
            }
            var stream = File.OpenRead(GetPath(image.Identifier));

            return new Tuple<Stream, string>(stream, image.ContentType);
        }

        public async Task<Record> PostAsync(Stream stream, string type, string name) {
            var result = new Record { ContentType = type, Name = name };
            
            Directory.CreateDirectory(storageDirectory);
            using (var file = File.OpenWrite(GetPath(result.Identifier))) {
                await stream.CopyToAsync(file);
            }

            RecordsRepository.Create(result);
            await RecordsRepository.SaveAsync();

            return result;
        }

        public async Task<Record> DeleteAsync(string identifier) {
            var result = await RecordsRepository.FindAsync(identifier);

            if (result is null) {
                return null;
            }

            File.Delete(GetPath(result.Identifier));
            RecordsRepository.Delete(result);
            await RecordsRepository.SaveAsync();

            return result;
        }
    }
}
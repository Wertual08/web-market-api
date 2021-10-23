using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;

namespace Api.Domain.Services {
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

        public async Task<(Stream, Record)> GetAsync(Guid identifier) {
            var image = await RecordsRepository.FindAsync(identifier);

            if (image is null || !File.Exists(GetPath(image.Identifier))) {
                return (null, null);
            }
            var stream = File.OpenRead(GetPath(image.Identifier));

            return (stream, image);
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

        public async Task<Record> DeleteAsync(long id) {
            var result = await RecordsRepository.FindAsync(id);

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
using Queris.ExceptionNotifier.Common.Abstract;
using Queris.ExceptionNotifier.ConfigManager.Models;
using Queris.ExceptionNotifier.Serializers;
using System.IO;
using System.Linq;
using System.Text;

namespace Queris.ExceptionNotifier.ConfigManager
{
    public class ConfigManager : ISetCounterConfig
    {
        private readonly Config _config;
        private readonly string _fileName;
        private readonly ISerializer _serializer;

        public ConfigManager(string fileName, ISerializer serializer)
        {
            _fileName = fileName;
            _serializer = serializer;

            var json = File.ReadAllText(_fileName, Encoding.Default);
            _config = _serializer.Deserialize<Config>(json);
        }

        public void Set(int value, int idReader)
        {
            _config.Readers.First(x => x.ReaderId == idReader).InitialCounter = value;
            var json = _serializer.Serialize(_config);
            File.WriteAllText(_fileName, json, Encoding.Default);
        }

        public Config Config => _config;
    }
}

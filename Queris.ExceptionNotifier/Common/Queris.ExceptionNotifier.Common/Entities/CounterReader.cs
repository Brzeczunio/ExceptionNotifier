using Queris.ExceptionNotifier.Common.Abstract;

namespace Queris.ExceptionNotifier.Common.Entities
{
    public class CounterReader : ACounterReader
    {
        public CounterReader(ISetCounterConfig config, int id) : base(config) { Id = id; }
        public void Save(int value) { Config.Set(value, Id); }

        public int GetId => Id;
    }
}

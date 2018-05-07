namespace Queris.ExceptionNotifier.Common.Entities
{
    public class FieldInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
        }
    }
}
namespace Queris.ExceptionNotifier.Common.Entities
{
    public class SendedMessages
    {
        public string Name { get; set; }
        public int Id { get; set; }
        

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Id)}: {Id}";
        }
    }
}

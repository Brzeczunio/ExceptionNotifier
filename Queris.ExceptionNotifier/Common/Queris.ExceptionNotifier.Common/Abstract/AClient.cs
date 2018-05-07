namespace Queris.ExceptionNotifier.Common.Abstract
{
    public abstract class AClient
    {
        protected AClient(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
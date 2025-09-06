namespace Domain.Entites
{
    public class BaseEntity<TKey>
    {
        public TKey ID { get; set; }

        public bool IsDeleted { get; set; }
    }
}

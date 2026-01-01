namespace Domain.Models
{
    public class BaseEntity : SoftDelete 
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }


        public virtual void Delete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
        }
    }

    public class SoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}

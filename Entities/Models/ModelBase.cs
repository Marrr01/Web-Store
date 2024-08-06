using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ModelBase
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}

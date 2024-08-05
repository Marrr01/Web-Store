using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ModelBase
    {
        [Column("id")]
        public Guid Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ModelBase
    {
        [Column("id")]
        public string Id { get; set; }
    }
}

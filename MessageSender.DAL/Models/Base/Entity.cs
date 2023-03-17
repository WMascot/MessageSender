using MessageSender.DAL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageSender.DAL.Models.Base
{
    public class Entity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}

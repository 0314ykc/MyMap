using Microsoft.AspNetCore.Identity;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMap.Models.others
{
    [Table("talkTable")]
    public class talksModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long user_id { get; set; }
        public string message { get; set; }
        public DateTime create_time { get; set; }
        public DateTime edit_time { get; set; }

        [ForeignKey(nameof(user_id))]
        [InverseProperty("talks")]
        public virtual user user { get; set; }
    }
}
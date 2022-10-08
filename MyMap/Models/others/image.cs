using Microsoft.AspNetCore.Identity;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMap.Models.others
{
    [Table("imageTable")]
    public class imageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long user_id { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public DateTime create_time { get; set; }

        [ForeignKey(nameof(user_id))]
        [InverseProperty("images")]
        public virtual user user { get; set; }
    }
}
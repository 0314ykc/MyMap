using Microsoft.AspNetCore.Identity;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace MyMap.Models.Map
{
    [Table("mapTable")]
    public class mapModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long user_id { get; set; }
        public MapType type { get; set; }
        public visibility visbility { get; set; }
        public string name { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? edit_time { get; set; }

        [ForeignKey(nameof(user_id))]
        [InverseProperty("maps")]
        public virtual user user { get; set; }

        [InverseProperty(nameof(map_placeModel.map))]
        public virtual ICollection<map_placeModel> map_places { get; set; }

    }
}
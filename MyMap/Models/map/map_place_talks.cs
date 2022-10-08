using Microsoft.AspNetCore.Identity;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMap.Models.Map
{
    [Table("map_place_talksTable")]
    public class map_place_talksModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long  map_place_id{ get; set; }
        public long user_id { get; set; }
        public string message { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? edit_time { get; set; }

        [ForeignKey(nameof(map_place_id))]
        [InverseProperty("map_place_talks")]
        public virtual map_placeModel map_place { get; set; }
        [ForeignKey(nameof(user_id))]
        [InverseProperty("map_place_talks")]
        public virtual user user { get; set; }
    }
}
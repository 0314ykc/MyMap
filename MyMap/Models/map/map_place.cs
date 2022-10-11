using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMap.Models.Map
{
    [Table("map_placeTable")]
    public class map_placeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long map_id { get; set; }
        public MapPlaceType type { get; set; }
        public string name { get; set; }
        public long? thumbnail_id { get; set; }
        public string? location { get; set; }
        public string? coordinate { get; set; }
        public string? description { get; set; }
        /// <summary>
        /// 是否去過
        /// </summary>
        public bool have_been_to { get; set; }
        /// <summary>
        /// 評分
        /// </summary>
        public int? rating { get; set; }
        public DateTime create_time { get; set; }
        public DateTime? edit_time { get; set; }

        [ForeignKey(nameof(map_id))]
        [InverseProperty("map_places")]
        public virtual mapModel map { get; set; }

        [InverseProperty(nameof(map_place_talksModel.map_place))]
        public virtual ICollection<map_place_talksModel> map_place_talks { get; set; }

    }
}
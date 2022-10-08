using Microsoft.AspNetCore.Identity;
using MyMap.Models.Map;
using MyMap.Models.others;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMap.Models.identity
{

    [Table("user")]
    public class user:IdentityUser<long>
    {
        [Key]
        [Column("id")]
        public override long Id { get; set; }

        [InverseProperty(nameof(mapModel.user))]
        public virtual ICollection<mapModel> maps { get; set; }
        [InverseProperty(nameof(map_place_talksModel.user))]
        public virtual ICollection<map_place_talksModel> map_place_talks { get; set; }
        [InverseProperty(nameof(imageModel.user))]
        public virtual ICollection<imageModel> images { get; set; }
        [InverseProperty(nameof(talksModel.user))]
        public virtual ICollection<talksModel> talks { get; set; }
    }
}
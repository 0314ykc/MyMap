using Microsoft.AspNetCore.Identity;
using MyMap.Models;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace MyMap.ViewModels.Map
{
    public class MapIndexViewModel
    {
        public ICollection<MapIndexListItem> list { get; set; }
    }

    public class MapIndexListItem
    {
        public long index { get; set; }
        public long mapId { get; set; }
        public string name { get; set; }
        public MapType type { get; set; }
        public DateTime createTime { get; set; }

    }
}
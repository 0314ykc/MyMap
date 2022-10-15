using Microsoft.AspNetCore.Identity;
using MyMap.Models;
using MyMap.Models.identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace MyMap.ViewModels.Map
{
    public class MapInfoViewModel
    {
        public long mapId { get; set; }
        public string? mapName { get; set; }
        public ICollection<MapPlaceListItem> placeList { get; set; }
    }

    public class MapPlaceListItem
    {
        public long index { get; set; }
        public long mapPlaceId { get; set; }
        public long? thumbailId { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string? coordinate { get; set; }
        public string description { get; set; }
        public MapPlaceType type { get; set; }
        public int? rating { get; set; }
        public bool haveBeenTo { get; set; }
        public DateTime createTime { get; set; }
        public DateTime? editTime { get; set; }
        public List<PlaceTalkListItem> talks { get; set; }

    }

    public class PlaceTalkListItem
    {
        public long index { get; set; }
        public long userId { get; set; }
        public string name { get; set; }
        public string? message { get; set; }
        public DateTime createTime { get; set; }
        public DateTime? editTime { get; set; }
    }
}
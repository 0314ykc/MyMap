using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyMap.Data;
using MyMap.Helper;
using MyMap.Models;
using MyMap.Models.identity;
using MyMap.Models.Map;
using MyMap.Models.others;
using MyMap.ViewModels.Map;
using System.Xml.Linq;

namespace MyMap.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<user> _userManager;
        private readonly IConfiguration _conf;
        private readonly IUploadFile _upload;

        public MapController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<user> userManager, IConfiguration conf, IUploadFile upload)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _conf = conf;
            _upload = upload;
        }

        public async Task<IActionResult> IndexAsync()
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            var mapList = _context.mapModels
                .Where(x => x.user_id == user.Id)
                .Select(m => new MapIndexListItem { mapId = m.id, name = m.name, type = m.type, visibility = m.visbility, createTime = m.create_time, editTime = m.edit_time })
                .ToList();
            mapList = mapList.Select((x, i) => { x.index = i+1; return x; }).ToList();

            MapIndexViewModel result = new MapIndexViewModel { list = mapList };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMapAsync(string name, visibility visbility, MapType type)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            if (string.IsNullOrEmpty(name))
            {
                TempData["errMsg"] = "some fileds are required.";
                return RedirectToAction("Index", "Map");
            }
            else
            {
                mapModel newMap = new mapModel
                {
                    name = name,
                    visbility = visbility,
                    type = type,
                    user_id = user.Id,
                    create_time = DateTime.Now,
                };
                _context.mapModels.Add(newMap);
                _context.SaveChanges();
            }

            return RedirectToAction("Index","Map");
        }

        [HttpPost]
        public async Task<IActionResult> EditMapAsync(long map_id,string name, visibility mapVisbility, MapType type)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            if (string.IsNullOrEmpty(name))
            {
                TempData["errMsg"] = "some fileds are required.";
                return RedirectToAction("Index", "Map");
            }
            else
            {
                var thisMap = _context.mapModels.FirstOrDefault(x => x.id == map_id);
                if (thisMap == null)
                {
                    TempData["errMsg"] = "this map not found , please try again.";
                    return RedirectToAction("Index", "Map");
                }
                thisMap.name = name;
                thisMap.visbility = mapVisbility;
                thisMap.type = type;
                thisMap.edit_time = DateTime.Now;

                _context.mapModels.Update(thisMap);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Map");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMapAsync(long map_id)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            var thisMap = _context.mapModels.FirstOrDefault(x => x.id == map_id);
            if (thisMap == null)
            {
                TempData["errMsg"] = "this map not found , please try again.";
                return RedirectToAction("Index", "Map");
            }

            _context.mapModels.Remove(thisMap);
            _context.SaveChanges();

            return RedirectToAction("Index", "Map");
        }

        public async Task<IActionResult> InfoAsync(long id)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            var mapPlaceLists = _context.map_placeModels
                .Where(x => x.map_id == id)
                .Select(x => new MapPlaceListItem
                {
                    index = 0,
                    mapPlaceId = x.id,
                    name = x.name,
                    type = x.type,
                    createTime = x.create_time,
                    editTime = x.edit_time,
                    talks = x.map_place_talks.Select(y => new PlaceTalkListItem
                    {
                        index = 0,
                        userId = y.user_id,
                        name = y.user.UserName,
                        message = y.message,
                        createTime = y.create_time,
                        editTime = y.edit_time
                    }).ToList()
                }).ToList();

            var result = new MapInfoViewModel
            {
                mapId = id,
                mapName = _context.mapModels.Where(x => x.id == id).Select(x => x.name).FirstOrDefault(),
                placeList = mapPlaceLists
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMapPlaceAsync(long map_id, [FromForm] List<IFormFile>? thumbail, string name, string description, MapPlaceType type, int rating, bool haveBeenTo = false, string? coordinate = null, string? location = null)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                TempData["errMsg"] = "some fileds are required.";
                return RedirectToAction("Info", "Map");
            }
            else
            {
                map_placeModel newMapPlace = new map_placeModel
                {
                    name = name,
                    map_id = map_id,
                    description = description,
                    type = type,
                    have_been_to = haveBeenTo,
                    coordinate = coordinate,
                    location = location,
                    rating = rating,
                    create_time = DateTime.Now,
                };

                if (thumbail != null)
                {
                    var imgIds = await this.UploadImagesAsync(thumbail);
                    newMapPlace.thumbnail_id = (imgIds == null) ? null : imgIds.FirstOrDefault();
                }

                _context.map_placeModels.Add(newMapPlace);
                _context.SaveChanges();
            }

            return RedirectToAction("Info", "Map");
        }

        [HttpPost]
        public async Task<bool> CreateMapPlaceTalksAsync(long map_place_id, string message)
        {
            user user = await _userManager.GetUserAsync(User); // get user's all data

            if (string.IsNullOrEmpty(message))
            {
                return false;
            }
            else
            {
                map_place_talksModel newMapPlaceTalks = new map_place_talksModel
                {
                    map_place_id = map_place_id,
                    user_id = user.Id,
                    message = message,
                    create_time = DateTime.Now,
                };
                _context.map_place_talksModels.Add(newMapPlaceTalks);
                _context.SaveChanges();
            }

            return true;
        }

        [HttpPost]
        public async Task<List<long>> UploadImagesAsync([FromForm] List<IFormFile> imgs)
        {
            user user = await _userManager.GetUserAsync(User);

            List<long> ids = new List<long>();

            #region multiple upload
            if (imgs != null && imgs.Any())
            {
                foreach (var img in imgs)
                {
                    if (img != null)
                    {
                        var supportedContentTypes = new[] { "image/png", "image/jpeg" };

                        //if (!supportedContentTypes.Contains(img.ContentType))
                        //{
                        //    TempData["errorMessage"] = "不支援的檔案格式。";
                        //    return RedirectToAction("ProfileFiles", new { id = profileFilesUploadModel.personId });
                        //}

                        var uploadfile = new UploadFileModel
                        {
                            file = img,
                            folder = _conf["UploadPath:Image"],
                        };

                        var fileName = _upload.UploadDataWithOriginalFileName(uploadfile);
                        if (fileName == null)
                        {
                            Console.WriteLine("upload fail, file name is null");
                            ids.Add(0);
                            continue;
                        }

                        var uploadedFilePath = uploadfile.folder + "/" + fileName;

                        imageModel newImg = new imageModel()
                        {
                            user_id = user.Id,
                            file_name = fileName,
                            file_path = uploadedFilePath,
                            create_time = DateTime.Now
                        };
                        _context.imageModels.Add(newImg);
                        await _context.SaveChangesAsync();

                        ids.Add(newImg.id);
                    }
                }
            }
            #endregion

            return ids;
        }

        //[HttpPost]
        //public async Task<object> UploadImagesAsync([FromForm] List<IFormFile> imgs)
        //{
        //    user user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    List<long> ids = new List<long>();

        //    #region multiple upload
        //    if (imgs != null && imgs.Any())
        //    {
        //        foreach (var img in imgs)
        //        {
        //            if (img != null)
        //            {
        //                var supportedContentTypes = new[] { "image/png", "image/jpeg" };

        //                //if (!supportedContentTypes.Contains(img.ContentType))
        //                //{
        //                //    TempData["errorMessage"] = "不支援的檔案格式。";
        //                //    return RedirectToAction("ProfileFiles", new { id = profileFilesUploadModel.personId });
        //                //}

        //                var uploadfile = new UploadFileModel
        //                {
        //                    file = img,
        //                    folder = _conf["UploadPath:Image"],
        //                };

        //                var fileName = _upload.UploadDataWithOriginalFileName(uploadfile);
        //                if (fileName == null)
        //                {
        //                    Console.WriteLine("upload fail, file name is null");
        //                    ids.Add(0);
        //                    continue;
        //                }

        //                var uploadedFilePath = uploadfile.folder + "/" + fileName;

        //                imageModel newImg = new imageModel()
        //                {
        //                    user_id = user.Id,
        //                    file_name = fileName,
        //                    file_path = uploadedFilePath,
        //                    create_time = DateTime.Now
        //                };
        //                _context.imageModels.Add(newImg);
        //                await _context.SaveChangesAsync();

        //                ids.Add(newImg.id);
        //            }
        //        }
        //    }
        //    #endregion

        //    return ids;
        //}


        public class UploadFileModel
        {
            [Required]
            public IFormFile file { get; set; }
            public string folder { get; set; }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HotelManagementSystem.Areas.Management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class HotelConfigurationController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IManagementRepository _repository;

        public HotelConfigurationController(IWebHostEnvironment hostingEnvironment, IManagementRepository repository)
        {
            this.hostingEnvironment = hostingEnvironment;
            _repository = repository;

        }
        [HttpGet]
        public async Task<IActionResult> Room(RoomViewModel model)
        {
            model.rooms = await _repository.GetRoomsForType(0);
            model.roomTypes = await GetRoomTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Room(Room room)
        {

            if (room.RoomID > 0)
            { await _repository.UpdateRoom(room); }
            else { await _repository.CreateRoom(room); }

            var rooms = await _repository.GetRoomsForType(room.RoomTypeID);
            var roomTypes = await GetRoomTypes();

            var result = new RoomViewModel { rooms = rooms, roomTypes = roomTypes };
            return PartialView("_RoomPartialView", result);

        }
        public async Task<IActionResult> GetRoom(int id)
        {
            var result = await _repository.GetRoom(id);
            return PartialView(result);
        }

        public async Task<IActionResult> DeleteRoom(Room room)
        {
            await _repository.DeleteRoom(room.RoomID);

            return RedirectToAction("room");
            //var model_result = new RoomViewModel { rooms = await _repository.GetRooms(), roomTypes = await GetRoomTypes() };
            //return PartialView("_RoomPartialView", model_result);

        }

        async Task<List<SelectListItem>> GetRoomTypes()
        {
            var roomTypeList = new List<SelectListItem>();

            var roomTypes = await _repository.GetRoomTypes();
            if (roomTypes != null)
                foreach (var roomType in roomTypes)
                {
                    roomTypeList.Add(new SelectListItem { Text = $"{roomType.Name}", Value = Convert.ToString(roomType.RoomTypeID) });
                }
            return roomTypeList;
        }
        [HttpGet]
        public async Task<IActionResult> RoomType()
        {
            var result = await _repository.GetRoomTypes();
            ViewData["Ammenities"] = await _repository.GetAmmenities();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RoomType(RoomTypeViewModel model, ICollection<Ammenity> ammenities)
        {
            if (ModelState.IsValid)
            {
                if (model.RoomTypeID > 0)
                {
                    var roomType = await _repository.GetRoomType(model.RoomTypeID);
                    roomType.RoomTypeID = model.RoomTypeID;
                    roomType.Name = model.Name;
                    roomType.Size = model.Size;
                    roomType.Capacity = model.Capacity;
                    roomType.Price = model.Price;
                    await _repository.UpdateRoomType(roomType);
                    if (model.Photo != null && model.Photo.Count > 0)
                    {
                        var images = await _repository.GetRoomImage(roomType.RoomTypeID);
                        if (images != null && images.Count() > 0)
                        {
                            foreach (var item in images)
                            {
                                string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                                    "images/rooms", item.ImageURL);
                                System.IO.File.Delete(filepath);

                            }
                            await _repository.DeleteRoomImages(images);


                        }
                        var imageFiles = await ProcessedPhoto(model);
                        await _repository.CreateRoomImages(roomType.RoomTypeID, imageFiles);
                    }
                }
                else
                {
                    var imageFiles = await ProcessedPhoto(model);
                    var roomType = new RoomType
                    {
                        Name = model.Name,
                        Size = model.Size,
                        Capacity = model.Capacity,
                        Price = model.Price,

                    };
                    await _repository.CreateRoomType(roomType);
                    if (imageFiles != null && imageFiles.Count > 0)
                    {
                        var result = await _repository.GetRoomTypes();
                        int id = 0;
                        foreach (var item in result)
                        {
                            id = item.RoomTypeID;
                        }
                        await _repository.CreateRoomImages(id, imageFiles);
                    }

                    //var room_services = new List<Ammenity>();
                    //foreach (var item in ammenities)
                    //{
                    //    if (item.IsActive is true)
                    //    {
                    //        room_services.Add(item);
                    //    }
                    //}
                    //await _repository.AddServicesToRoomType(id, room_services);}
                }
                var roomtype_result = await _repository.GetRoomTypes();
                return PartialView("_RoomTypePartialView", roomtype_result);
            }
            return RedirectToAction("roomtype");

        }
        public async Task<IActionResult> GetRoomType(int id)
        {
            var result = await _repository.GetRoomType(id);
            var my_model = new RoomTypeViewModel
            {
                RoomTypeID = result.RoomTypeID,
                Size = result.Size,
                Capacity = result.Capacity,
                Name = result.Name,
                Price = result.Price
            };
            return PartialView(my_model);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoomsForType(int id)
        {
            var result = await _repository.GetRoomsForType(id);

            var model = new RoomViewModel { rooms = result, roomTypes = await GetRoomTypes() };

            return PartialView("_RoomPartialView", model);
        }
        [HttpPost]
        private async Task<List<string>> ProcessedPhoto(RoomTypeViewModel model)
        {
            List<string> imageFiles = new List<string>();
            if (model.Photo != null)
            {
                foreach (var Photo in model.Photo)
                {
                    string uniqueFile;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/rooms");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFile);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    { await Photo.CopyToAsync(filestream); }
                    imageFiles.Add(uniqueFile);
                }
            }
            return imageFiles;
        }
        public async Task<IActionResult> DeleteRoomType(RoomType roomType)
        {
            var images = await _repository.GetRoomImage(roomType.RoomTypeID);
            if (images != null && images.Count() > 0)
            {
                foreach (var item in images)
                {
                    string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                        "images/rooms", item.ImageURL);
                    System.IO.File.Delete(filepath);

                }
                await _repository.DeleteRoomImages(images);


            }
            await _repository.DeleteRoomType(roomType.RoomTypeID);

            return RedirectToAction("roomtype");
            //var roomtype_result = await _repository.GetRoomTypes();
            //return PartialView("_RoomTypePartialView", roomtype_result);

        }
        public async Task<IActionResult> Ammenity()
        {
            var result = await _repository.GetAmmenities();
            return View(result);
        }
        public async Task<IActionResult> GetAmmenity(int id)
        {
            var result = await _repository.GetAmmenity(id);
            return PartialView(result);
        }
        public async Task<IActionResult> AddAmmenity(Ammenity ammenity)
        {
            await _repository.CreateAmmenity(ammenity);
            return RedirectToAction("ammenity");
        }
        public async Task<IActionResult> ModifyAmmenity(Ammenity ammenity)
        {

            if (ModelState.IsValid)
            {
                await _repository.UpdateAmmenity(ammenity);
            }

            return RedirectToAction("ammenity");

        }
        public async Task<IActionResult> DeleteAmmenity(Ammenity ammenity)
        {
            await _repository.DeleteAmmenity(ammenity.AmmenityID);

            return RedirectToAction("ammenity");

        }
        public IActionResult AmmenityDetails()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_A.Models;
using Trail_A.Repositry.Iterface;
using Trail_A.View_Model;

namespace Trail_A.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IGenericRepo<VehicleType> _vehicle;
        private readonly IGenericRepo<User> _user;
        public VehicleTypeController(IGenericRepo<VehicleType> vehicle , IGenericRepo<User> user)
        {
            _user = user;
            _vehicle = vehicle;
        }


        // GET: VehicleTypeController
        public ActionResult Index(string Name)
        {
            List<VehicleType> vehicleTypes;
            if (!string.IsNullOrEmpty(Name))
            {
                vehicleTypes = _vehicle.Search(x => x.VehicleName.Contains(Name));
            }
            else
            {
                vehicleTypes = _vehicle.GetAllObjects();
            }
            if (vehicleTypes == null)
            {
                return View(new List<User>());
            }
            return View(vehicleTypes);
        }

        // GET: VehicleTypeController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _vehicle.GetObject(id);
            if (data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            return View(data);
        }

        // GET: VehicleTypeController/Create
        public ActionResult Create()
        {
            var AdminId = _user.GetAllObjects().Where(x => x.Role == "Admin").ToList();
            var ToTheModel = new VehicleTypeViewModel()
            {
                users  = AdminId
            };
            return View(ToTheModel);
        }

        // POST: VehicleTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleTypeViewModel vehicle)
        {
            var ToTheModel = new VehicleType()
            {
                BaseFare = vehicle.BaseFare,
                CreatedByUserId = vehicle.CreatedByUserId,
                VehicleName = vehicle.VehicleName,
                VehicleTypeId = vehicle.VehicleTypeId
            };
            _vehicle.AddObject(ToTheModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: VehicleTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehicleTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VehicleTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_A.Models;
using Trail_A.Repositry.Iterface;
using Trail_A.View_Model;

namespace Trail_A.Controllers
{
    public class DriverController : Controller
    {
        private readonly IGenericRepo<Driver> _driver;
        private readonly IGenericRepo<User> _user;
        public DriverController(IGenericRepo<Driver> driver, IGenericRepo<User> user)
        {
            _driver = driver;
            _user = user;
        }
        // GET: DriverController
        public ActionResult Index(string Name)
        {
            List<Driver> drivers;
            if (!string.IsNullOrEmpty(Name))
            {
                drivers = _driver.Search(x => x.Name.Contains(Name));
            }
            else
            {
                drivers = _driver.GetAllObjects();
            }
            if (drivers == null)
            {
                return View(new List<User>());
            }
            return View(drivers);
        }

        // GET: DriverController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _driver.GetObject(id);
            if (data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            return View(data);
        }

        // GET: DriverController/Create
        public ActionResult Create()
        {
            var AdminId = _user.GetAllObjects().Where(x => x.Role == "Admin").ToList();

            var ToTheModel = new DriverViewModel()
            {
                users = AdminId
            };
            return View(ToTheModel);
        }

        // POST: DriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverViewModel driverViewModel)
        {
            var ToTheModel = new Driver()
            {
                DriverId= driverViewModel.DriverId,
                Name= driverViewModel.Name,
                Status = driverViewModel.Status,
                CreatedByUserId= driverViewModel.CreatedByUserId,
                LicenseNumber = driverViewModel.LicenseNumber,
                Phone = driverViewModel.Phone,
            };
            var CheckPhone = _driver.Search(x => x.Phone.Contains(ToTheModel.Phone)).Any();
            var check = _driver.Search(x => x.LicenseNumber.Contains(ToTheModel.LicenseNumber)).Any();
            if (check||CheckPhone)
            {
                return BadRequest("The License Number Or Phone Is Existes In Datbase");
            }
            _driver.AddObject(ToTheModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: DriverController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DriverController/Edit/5
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

        // GET: DriverController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DriverController/Delete/5
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

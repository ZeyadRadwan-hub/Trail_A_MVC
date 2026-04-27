using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_A.Models;
using Trail_A.Repositry.Iterface;
using Trail_A.View_Model;

namespace Trail_A.Controllers
{
    public class RideController : Controller
    {
        private readonly IGenericRepo<Ride> _ride;
        private readonly IGenericRepo<User> _user;
        private readonly IGenericRepo<VehicleType> _vehicle;
        private readonly IGenericRepo<Driver> _driver;

        public RideController(IGenericRepo<Ride> ride,
            IGenericRepo<User> user, 
            IGenericRepo<VehicleType> vehicle, 
            IGenericRepo<Driver> driver)
        {
            _ride = ride;
            _user = user;
            _vehicle = vehicle;
            _driver = driver;
        }


        // GET: RideController
        public ActionResult Index(string Name)
        {
            List<Ride> rides;
            if (!string.IsNullOrEmpty(Name))
            {
                rides = _ride.Search(x => x.Status.Contains(Name));
            }
            else
            {
                rides = _ride.GetAllObjects();
            }
            if (rides == null)
            {
                return View(new List<User>());
            }
            return View(rides);
        }

        // GET: RideController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _ride.GetObject(id);
            if (data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            data.user = _user.GetObject(data.UserId);
            data.driver = _driver.GetObject(data.DriverId);
            data.vehicleType = _vehicle.GetObject(data.VehicleId);
            return View(data);
        }

        // GET: RideController/Create
        public ActionResult Create()
        {
            var ToTheModel = new RideViewModel()
            {
               users = _user.GetAllObjects(),
              drivers = _driver.GetAllObjects(),
                vehicleTypes = _vehicle.GetAllObjects(),
              Date = DateTime.Now,
            };
            return View(ToTheModel);
        }

        // POST: RideController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RideViewModel rideView)
        {
            var ToTheModel = new Ride()
            {
                RideId=rideView.RideId,
                Status=rideView.Status,
                DriverId=rideView.DriverId,
                Fare=rideView.Fare,
                PickOff=rideView.PickOff,
                PickUp=rideView.PickUp,
                UserId=rideView.UserId,
                VehicleId=rideView.VehicleId,
                Date=rideView.Date,
            };
            if(ToTheModel.Status != "Available")
            {
                return BadRequest("No Drivers Avilable Now");
            }
            _ride.AddObject(ToTheModel);
            var assign = _driver.GetObject(ToTheModel.DriverId);
            if (assign != null && assign.Status == "Available")
            {
                assign.Status = "Busy";
                _driver.UpdateObject(assign);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RideController/Edit/5
        public ActionResult Edit(int id , Ride ride)
        {
            var Drv = _driver.Search(x => x.Status == "Available");
            //var data = _ride.GetObject(ride.RideId);
            var ToTheModel = new RideViewModel()
            {
                Fare = ride.Fare,
                DriverId = ride.DriverId,
                drivers = Drv,
                PickOff = ride.PickOff,
                PickUp = ride.PickUp,
                RideId = ride.RideId,
                Status = ride.Status,
                UserId = ride.UserId,
                VehicleId = ride.VehicleId,
                Date = ride.Date,
            };
            return View(ToTheModel);
        }

        // POST: RideController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RideViewModel Ride)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            if(Ride == null)
            {
                return BadRequest("The Data Is Null");
            }
            var data = _ride.GetObject(Ride.RideId);
            var ToTheModel = new Ride()
            {
                Fare = Ride.Fare,
                DriverId = Ride.DriverId,
                PickOff = Ride.PickOff,
                PickUp = Ride.PickUp,
                RideId = Ride.RideId,
                Status = Ride.Status,
                UserId = Ride.UserId,
                VehicleId = Ride.VehicleId,
                Date= Ride.Date,
            };
            _ride.UpdateObject(ToTheModel);
            var assign = _driver.GetObject(ToTheModel.DriverId);
            if (assign != null 
                && assign.Status == "Cancelled" 
                || assign.Status == "Completed")
            {
                assign.Status = "Available";
                _driver.UpdateObject(assign);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: RideController/Delete/5
        public ActionResult Delete(int id, Ride ride)
        {
            var Drv = _driver.Search(x => x.Status == "Available");
            var ToTheModel = new RideViewModel()
            {
                Fare = ride.Fare,
                DriverId = ride.DriverId,
                drivers = Drv,
                PickOff = ride.PickOff,
                PickUp = ride.PickUp,
                RideId = ride.RideId,
                Status = ride.Status,
                UserId = ride.UserId,
                VehicleId = ride.VehicleId
            };
            return View(ToTheModel);
        }

        // POST: RideController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public ActionResult Delete2(int id, Ride ride)
        {
            if (id <= 0||id!=ride.RideId)
            {
                return BadRequest("Invalid Id");
            }
            var data = _ride.GetObject(id);
            if(data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            _ride.DeleteObject(data);
            var assign = _driver.GetObject(data.DriverId);
            if (assign != null)
            {
                assign.Status = "Available";
                _driver.UpdateObject(assign);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

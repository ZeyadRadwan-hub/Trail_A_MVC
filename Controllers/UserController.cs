using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_A.Models;
using Trail_A.Repositry.Iterface;

namespace Trail_A.Controllers
{
    public class UserController : Controller
    {
        private readonly IGenericRepo<User> _user;
        public UserController(IGenericRepo<User> user)
        {
            _user = user;
        }

        // GET: UserController
        public ActionResult Index(string Name)
        {
            List<User> users;
            if(!string.IsNullOrEmpty(Name))
            {
                users = _user.Search(x => x.Name.Contains(Name));
            }
            else
            {
                users = _user.GetAllObjects();
            }
            if(users == null)
            {
                return View(new List<User>());
            }
            return View(users);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _user.GetObject(id);
            if(data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            return View(data);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if(user == null)
            {
                return BadRequest("Data Is Null");
            }
            var CheckEmail = _user.Search(x => x.EmailAddress.Contains(user.EmailAddress)).Any();
            var CheckPhone = _user.Search(x => x.Phone.Contains(user.Phone)).Any();
            if (CheckEmail||CheckPhone)
            {
                return BadRequest("This Phone Or Email Is Exsits In Database");
            }
            //if(user.Role != "Admin" || user.Role != "Customer")
            //{
            //    return BadRequest("Please write (Admin) Or (Customer)");
            //}
                _user.AddObject(user);
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _user.GetObject(id);
            if (data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            var data = new User()
            {
                EmailAddress = user.EmailAddress,
                Name = user.Name,
                Password = user.Password,
                Phone = user.Phone,
                Role = user.Role,
                UserId = user.UserId
            };
            if(data == null)
            {
                return BadRequest("Data Is Null");
            }
            _user.UpdateObject(data);
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Id");
            }
            var data = _user.GetObject(id);
            if (data == null)
            {
                return BadRequest($"This Data With Id {id} Was Not Found");
            }
            return View(data);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public ActionResult Delete2(int id)
        {
            var data = _user.GetObject(id);
            if(data == null)
            {
                return BadRequest("Data Is Null");
            }
            _user.DeleteObject(data);
            return RedirectToAction(nameof(Index));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProjectOOP.Models;

namespace MVCProjectOOP.Controllers
{
    public class EmployeesController : Controller
    {
        

        private EBSEntities db = new EBSEntities();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee login)
        {
            if (ModelState.IsValid)
            {
                EBSEntities db = new EBSEntities();

                var user = (from userlist in db.Employees
                            where userlist.LastName == login.LastName && userlist.Password == login.Password
                            select new
                            {
                                userlist.E_ID,
                                userlist.LastName
                            }).ToList();

                if (user.FirstOrDefault() != null)
                {
                    Session["UserName"] = user.FirstOrDefault().LastName;
                    Session["UserID"] = user.FirstOrDefault().E_ID;
                    
                 return RedirectToAction("Details", new { id = user.FirstOrDefault().E_ID });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View(login);
        }

        public ActionResult PaymentCollection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PaymentCollection(Code collect)
        {
            if (ModelState.IsValid)
            {
                EBSEntities db = new EBSEntities();

                var user = (from userlist in db.Codes
                            where userlist.C_ID == collect.C_ID && userlist.code1 == collect.code1 && userlist.month==collect.month
                            select new
                            {
                                userlist.coID,
                                userlist.code1
                            }).ToList();

                if (user.FirstOrDefault() != null)
                {
                    //Session["UserName"] = user.FirstOrDefault().
                    //Session["UserID"] = user.FirstOrDefault().E_ID;

                    ModelState.AddModelError("", "Transaction Done Sucessfully!");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid Client Unique Code!");
                }
            }
            return View(collect);
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult EDetail(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "E_ID,FirstName,LastName,Phone,Password,Address,City,ZIPCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "E_ID,FirstName,LastName,Phone,Password,Address,City,ZIPCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
    
}

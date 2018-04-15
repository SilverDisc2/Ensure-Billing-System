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
    public class ClientsController : Controller
    {
       // Code c1 = new Code(112,"March","0980");
       public string dd()
        {
            Code code = new Code();
            code.Codee(112, "March", "0980");
            Client clnt = new Client();
            clnt.Clientt(32,code);
            return code.code1;
        }

        public ActionResult ddd( ClientsController dd)
        {
            ClientsController cc = new ClientsController();
            cc.dd();
            ViewBag.Message = cc.dd();
            return View(dd);
        }
       
        private EBSEntities db = new EBSEntities();

        //public string tayments(int id, Payment payment)
        //{
        //    Client a = new Client();
        //    a.C_ID = id;
        //    a.Payment = payment;
        //    return (id + " " + payment.Ammount

        //          );
           

        //}
        // GET: Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
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
                            where userlist.C_ID == collect.C_ID  && userlist.month==collect.month
                            select new
                            {
                                userlist.coID,
                                userlist.C_ID
                            }).ToList();

                if (user.FirstOrDefault() != null)
                {
                    //Session["UserName"] = user.FirstOrDefault().
                    //Session["UserID"] = user.FirstOrDefault().E_ID;
                    return RedirectToAction("Details","Codes", new { id = user.FirstOrDefault().coID });


                }
                else
                {
                    ModelState.AddModelError("", "Invalid Client Unique Code!");
                }
            }
            return View(collect);
        }
    
                
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Client login)
        {
            if (ModelState.IsValid)
            {
                EBSEntities db = new EBSEntities();

                var user = (from userlist in db.Clients
                            where userlist.Email == login.Email && userlist.Password == login.Password
                            select new
                            {
                                userlist.C_ID,
                                userlist.Email
                            }).ToList();

                  
                

                if (user.FirstOrDefault() != null)
                {
                    Session["UserName"] = user.FirstOrDefault().Email;
                    Session["UserID"] = user.FirstOrDefault().C_ID;
                    return Redirect("Index");
                }
               
                
            else
            {
                ModelState.AddModelError("", "Invalid login credentials.");
            }
            }
            return View(login);
        }


        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        public int RandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 100);

        }
        //public ActionResult UniqueCode(Random random)
        // {

        //     return View(random);

        // }


        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_ID,FirstName,LastName,Phone,Email,Password,Address,City,ZIPCode,Status,UCode")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_ID,FirstName,LastName,Phone,Email,Password,Address,City,ZIPCode,Status,UCode")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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

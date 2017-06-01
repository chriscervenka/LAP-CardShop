﻿using CardGame.DAL.Logic;
using CardGame.DAL.Model;
using CardGame.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CardGame.Web.Controllers
{
    public class EditController : Controller
    {
        public Register reg = new Register();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        // GET: Default/Details/5
        public ActionResult Details()
        {
            Person dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

            //Register p = new Register();
            ////Session.Add("Person", p);

            //p.Firstname = dbUser.Firstname;
            //p.Lastname = dbUser.Lastname;
            //p.Adresse  = dbUser.Anschrift;
            //p.Hausnummer = dbUser.Hausnummer;
            //p.Ort = dbUser.Ort;
            //p.PLZ = dbUser.PLZ;
            //p.Email = dbUser.Email;
            //p.Gamertag = dbUser.Gamertag;
            //p.Password = dbUser.Password;

            return View(dbUser);
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        [HttpGet]
        public ActionResult Edit()
        {
            Person dbUser = UserManager.GetPersonByEmail(User.Identity.Name);

            //if (reg == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            //Register p = new Register();
            //Session.Add("Person", p);

            //p.Firstname = reg.Firstname;
            //p.Lastname = reg.Lastname;
            //p.Adresse = reg.Adresse;
            //p.Hausnummer = reg.Hausnummer;
            //p.Ort = reg.Ort;
            //p.PLZ = reg.PLZ;
            //p.Email = reg.Email;
            //p.Gamertag = reg.Gamertag;
            //p.Password = reg.Password;

            //AuthManager.Register(p);
            return View(dbUser);
        }


        // POST: Default/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditPost(Register ro)
        {
            ClonestoneFSEntities db = new ClonestoneFSEntities();

            Person actPerson = (from p in db.Person where p.Email == User.Identity.Name select p).FirstOrDefault();

            actPerson.Firstname = ro.Firstname;
            actPerson.Lastname = ro.Lastname;
            actPerson.Gamertag = ro.Gamertag;
            actPerson.Password = ro.Password;
            actPerson.Anschrift = ro.Adresse;
            actPerson.Hausnummer = ro.Hausnummer;
            actPerson.Ort = ro.Ort;
            actPerson.PLZ = ro.PLZ;

            db.SaveChanges();

            return RedirectToAction("Details", "Edit");
        }

        //public ActionResult EditPost([Bind(Include = "ID,Firstname,Lastname,Gamertag,Email,Password,Anschrift,PLZ,Ort,Hausnummer")] Person p)
        //{
        //    ClonestoneFSEntities db = new ClonestoneFSEntities();

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(p).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(p);
        //}

        


        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditPost(int? id)
        //{
        //    ClonestoneFSEntities db = new ClonestoneFSEntities();

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var p = db.Person.Find(id);

        //    if (TryUpdateModel(p, "",
        //       new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
        //    {
        //        try
        //        {
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch (DataException /* dex */)
        //        {
        //            //Log the error (uncomment dex variable name and add a line here to write a log.
        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //        }
        //    }
        //    return View(p);
        //}



        // GET: Default/Delete/5
        public ActionResult Delete(int? id)
        {
            ClonestoneFSEntities db = new ClonestoneFSEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person p = db.Person.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Default/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            ClonestoneFSEntities db = new ClonestoneFSEntities();

            Person p = db.Person.Find(id);
            db.Person.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

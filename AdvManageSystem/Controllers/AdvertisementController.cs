using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataInterfaces;
using Model.PocoEntity;
using System.Net;
using AdvManageSystem.Utils;
using System.Text;


namespace AdvManageSystem.Controllers
{
    public class AdvertisementController : Controller
    {
        private IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
        
        public ActionResult Index()
        {
            var data = _advertisementRepository.GetAll();

            return View(data);
        }

        
        public ActionResult GetAll()
        {
                try
                {
                    var data = _advertisementRepository.GetAll();
                    return Json(new { data = data }, JsonRequestBehavior.AllowGet);                    
                }
                catch (Exception e)
                {
                    //Log e.Message;
                    Response.StatusCode = 500;
                    return null;
                }            
        }

        [HttpPost]
        public ActionResult Add(Advertisement adv) 
        {
            if (adv != null)
            {
                try
                {
                    var newAdv = _advertisementRepository.Add(adv);

                    return Json(new { status = HttpStatusCode.OK, message = "Success", id = newAdv.Id }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e) 
                {
                    //Log e.Message;
                    return Json(new { status = HttpStatusCode.InternalServerError, message = "Server error occured", id = 0 });
                }                
            }

            return Json(new { status = HttpStatusCode.BadRequest, message = "Advertisement is null" });
        }

        public ActionResult Update(Advertisement adv)
        {
            if (adv != null)
            {
                try
                {
                    _advertisementRepository.Update(adv);

                    return Json(new { status = HttpStatusCode.OK, message = "Success" });
                }
                catch (Exception e)
                {
                    //Log e.Message;
                    return Json(new { status = HttpStatusCode.InternalServerError, message = "Server error occured" });
                }                
            }

            return Json(new { status = HttpStatusCode.BadRequest, message = "Advertisement is null" });
        }

        public ActionResult Remove(int id = 0)
        {
            if (id != 0)
            {
                try
                {
                    _advertisementRepository.Remove(id);

                    return Json(new { status = HttpStatusCode.OK, message = "Success" });
                }
                catch (Exception e)
                {
                    //Log e.Message;
                    return Json(new { status = HttpStatusCode.InternalServerError, message = "Server error occured" });
                }               
            }

            return Json(new { status = HttpStatusCode.BadRequest, message = "ItemId is null" });
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

    }
}

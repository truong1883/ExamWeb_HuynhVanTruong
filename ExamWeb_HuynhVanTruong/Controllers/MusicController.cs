using ExamWeb_HuynhVanTruong.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamWeb_HuynhVanTruong.Controllers
{
    public class MusicController : Controller
    {
        private readonly ApplicationDbContext _db;
       
        public MusicController(ApplicationDbContext db)
        {
            _db = db;
           
        }
        public IActionResult Index()
        {
            var musicList = _db.DiaNhacs.ToList();
            return View(musicList);
        }
        public IActionResult Add()
        {           
            ViewBag.dianhacList = _db.DiaNhacs.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.TuaCD
            });
            return View();
        }      
        [HttpPost]
       public IActionResult Add(DiaNhac diaNhac)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                
                //thêm product vào table Product
                _db.DiaNhacs.Add(diaNhac);
                _db.SaveChanges();
                TempData["success"] = "Product inserted success";
                return RedirectToAction("Index");
            }           
            return View();
        }
        //Hiển thị form cập nhật sản phẩm
        public IActionResult Update(int id)
        {
            var music = _db.DiaNhacs.Find(id);
            if (music == null)
            {
                return NotFound();
            }
          
          
            return View(music);
        }
       
        [HttpPost]
        public IActionResult Update(DiaNhac diaNhac)
        {
           
            ViewBag.musicList = _db.DiaNhacs.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.TuaCD
            });
            return View();
        }
        //Hiển thị form xác nhận xóa sản phẩm
        public IActionResult Delete(int id)
        {
            var music = _db.DiaNhacs.Find(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }
        //Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _db.DiaNhacs.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            // xoá hình cũ của sản phẩm
           
            // xoa san pham khoi CSDL
            _db.DiaNhacs.Remove(product);
            _db.SaveChanges();
            TempData["success"] = "Product deleted success";
            //chuyen den action index
            return RedirectToAction("Index");
        }
    }
}


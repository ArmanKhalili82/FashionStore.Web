using FashionStore.Business.ColorService;
using FashionStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers
{
    public class ColorController : Controller
    {
        private IColorService _colorService;
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _environment;
        public ColorController(IColorService colorService, IWebHostEnvironment environment)
        {
            _colorService = colorService;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var colors = await _colorService.GetAllColors();
            return View(colors);
        }

        [HttpGet]
        public IActionResult CreateColor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(Color color, IFormFile image)
        {
            if (image!= null)
            {
                var name = Path.Combine(_environment.WebRootPath + "/Colors", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                color.ImageUrl = "Colors/" + image.FileName;
            }
            await _colorService.Create(color);
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            var color = await _colorService.GetById(id);
            return View(color);
        }

        [HttpPost]
        public async Task<IActionResult> EditColor(Color color, IFormFile image)
        {
            if (image != null)
            {
                var name = Path.Combine(_environment.WebRootPath + "/Colors", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                color.ImageUrl = "Colors/" + image.FileName;

                if (!string.IsNullOrEmpty(color.ImageUrl))
                {
                    //Delete The Old Image
                    var oldImagePath = Path.Combine(name, color.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    color.ImageUrl = "Colors/" + image.FileName;
                }
            }
            await _colorService.Update(color);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _colorService.GetById(id);
            return View(color);
        }

        [HttpPost, ActionName("DeleteColor")]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

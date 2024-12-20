using IT_Chronicles.Data;
using IT_Chronicles.Models.Domain;
using IT_Chronicles.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITChroniclesDbContext _context;

        public AdminTagController(ITChroniclesDbContext iTChroniclesDbContext)
        {
            _context = iTChroniclesDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName

            };

            await _context.Tags.AddAsync(tag);
           await _context.SaveChangesAsync();


            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //Tag listed to diplay the data in the database
            var tags = await _context.Tags.ToListAsync();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {

           var tag = _context.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName

                };
                return View(editTagRequest);
            }
            return View();
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag =  await _context.Tags.FindAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

               await _context.SaveChangesAsync();
                 
                return RedirectToAction("Edit", new {id = editTagRequest.Id});
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var tag = await _context.Tags.FindAsync(editTagRequest.Id);
            if(tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }
    }
}

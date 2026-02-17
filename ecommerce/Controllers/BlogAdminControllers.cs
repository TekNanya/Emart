using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
[Route("BlogAdmin")] // This forces the URL to be /BlogAdmin
public class BlogAdminController : Controller
{
    private readonly ApplicationDbContext _context;
    public BlogAdminController(ApplicationDbContext context) => _context = context;

    [Route("")] // List view: /BlogAdmin
    public async Task<IActionResult> Index()
    {
        var posts = await _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToListAsync();
        return View(posts);
    }

    [Route("Create")]
    public IActionResult Create() => View();

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogPost post)
    {
        if (ModelState.IsValid)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }


    // GET: BlogAdmin/Edit/5
    [Route("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }

    // POST: BlogAdmin/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BlogPost post)
    {
        if (id != post.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    // POST: BlogAdmin/Delete/5
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post != null)
        {
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
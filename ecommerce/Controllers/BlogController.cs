using ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("Blog")] // This forces the URL to be /Blog
public class BlogController : Controller
{
    private readonly ApplicationDbContext _context;
    public BlogController(ApplicationDbContext context) => _context = context;

    [Route("")] // Standard list view: /Blog
    public async Task<IActionResult> Index()
    {
        var posts = await _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToListAsync();
        return View(posts);
    }

    [Route("Details/{id}")] // Detail view: /Blog/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.BlogPosts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }
}
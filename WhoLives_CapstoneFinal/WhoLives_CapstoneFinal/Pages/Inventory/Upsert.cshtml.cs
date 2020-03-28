using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhoLives.DataAccess;
using WhoLives.Models;

namespace WhoLives_CapstoneFinal.Pages.Inventory
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<InventoryItem> AssemblyComponents { get; set; }
        public List<BuildAssembly> RecipeLines { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }

        public UpsertModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InventoryItem Item { get; set; }


        // get page
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // if problems, bail
            if (id == null)
                return NotFound();

            // setup the item
            Item = await _context.InventoryItems
                .Include(i => i.Measure).FirstOrDefaultAsync(m => m.InventoryItemID == id);

            // if other problems, bail
            if (Item == null)
                return NotFound();

            //// get purchase orders for this item
            //PurchaseOrders = await _context.PurchaseOrders.Where(a => a.OrderItems.Select(b => b.ItemID).Contains(Item.ItemID)).ToListAsync();
            //// loop purchase orders and initialize vendor
            //foreach (var order in PurchaseOrders)
            //    order.Vendor = await _context.Vendors.FirstOrDefaultAsync(a => a.VendorID == order.VendorID);
            //// setup assembly recipe
            //AssemblyComponents = new List<Item>();
            //// set recipe lines
            //RecipeLines = new List<RecipeLine>();
            //// check if any assembly recipes exist
            //if (_context.AssemblyRecipes.Any(a => a.ItemID == Item.ItemID))
            //{
            //    // setup assembly recipe from db
            //    var assemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(a => a.ItemID == Item.ItemID);
            //    // setup recipe lines from db
            //    var recipeLines = await _context.RecipeLines.Where(a => a.AssemblyRecipeID == assemblyRecipe.AssemblyRecipeID).ToListAsync();
            //    // loop recipe lines
            //    foreach (var line in recipeLines)
            //    {
            //        // add assembly recipe
            //        AssemblyComponents.Add(await _context.Items.FirstOrDefaultAsync(a => a.ItemID == line.ItemID));
            //        // add current recipe lines to main list
            //        RecipeLines.Add(line);
            //    }

            //}
            //// if no assembly recipe
            //else
            //{
            //    // setup empty assembly recipe
            //    RecipeLines = new List<RecipeLine>();
            //    // setup empty recipe lines
            //    AssemblyComponents = new List<Item>();
            //}
            // setup measures for view stuff
            ViewData["MeasureID"] = new SelectList(_context.Measures, "MeasureID", "MeasureName");
            return Page();
        }

        // post save edits
        public async Task<IActionResult> OnPostAsync()
        {
            // if problems, bail
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // set current user
            Item.LastModifiedBy = User.Identity.Name;
            // set current date
            Item.LastModifiedDate = DateTime.Now;
            // set measures from view data
            Item.MeasuresID = Convert.ToInt32(Request.Form["MeasureID"]);
            // notify db of modification
            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                // try to save
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if other problems, bail
                if (!ItemExists(Item.InventoryItemID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // return to list
            return RedirectToPage("./Index");
        }

        // helper if item exists
        private bool ItemExists(int id)
        {
            // does the item exist?
            return _context.InventoryItems.Any(e => e.InventoryItemID == id);
        }

    }
}

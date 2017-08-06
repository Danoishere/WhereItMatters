using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WhereItMatters.Core;
using WhereItMatters.DataAccess;

namespace WhereItMatters.Admin.Controllers
{
    public class BudgetItemController : Controller
    {
        private readonly IRepository<BudgetItem> _budgetItemRepository;
        
        public BudgetItemController(IRepository<BudgetItem> budgetItemRepository)
        {
            _budgetItemRepository = budgetItemRepository;
        }

        public async Task<IActionResult> Edit(int requestId)
        {
            var budgetItems = await _budgetItemRepository.SearchFor(i => i.DonationRequestId == requestId).ToListAsync();
            ViewData["DonationRequestId"] = requestId;
            return View(budgetItems);
        }

        public async Task<IActionResult> Save(BudgetItem budgetItem)
        {
            if(budgetItem.Id == 0)
            {
                await _budgetItemRepository.Insert(budgetItem);
            }
            else
            {
                await _budgetItemRepository.Update(budgetItem);
            }
                        
            return RedirectToAction("Edit", new { requestId = budgetItem.DonationRequestId });
        }

        public async Task<IActionResult> Delete(int budgetItemId)
        {
            var budgetItem = await _budgetItemRepository.GetById(budgetItemId);
            await _budgetItemRepository.Delete(budgetItem);
            return RedirectToAction("Edit", new { requestId = budgetItem.DonationRequestId });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    public class CasesController : Controller
    {
        private readonly CaseService _caseService;
        private readonly CaseContactService _caseContactService;

        private readonly List<string> _plansList = new List<string>
        {
            "Cobra", "Employee Benefits", "HSA", "HRA", "LPFSA", "DCA", "Medical"
        };

        public CasesController(CaseService caseService, CaseContactService caseContactService)
        {
            _caseService = caseService;
            _caseContactService = caseContactService;
        }

        public async Task<IActionResult> GetCases()
        {
            var cases = await _caseService.GetAllCasesAsync();
            return PartialView("_CasesList", cases);
        }

        public async Task<IActionResult> GetContacts(Guid caseId)
        {
            var contacts = await _caseContactService.GetCaseContactsByCaseIdAsync(caseId);
            return PartialView("_CaseContactsList", contacts);
        }


        public async Task<IActionResult> Index()
        {
            var cases = await _caseService.GetAllCasesAsync();
            return View(cases);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var caseDetail = await _caseService.GetCaseByIdAsync(id);
            return View(caseDetail);
        }

        public IActionResult Create()
        {
            ViewBag.PlansList = _plansList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Case newCase)
        {
            if (ModelState.IsValid)
            {
                newCase.Plans = string.Join(",", newCase.SelectedPlans); // Converting selected plans to comma-separated string
                await _caseService.CreateCaseAsync(newCase);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PlansList = _plansList;
            return View(newCase);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var caseDetail = await _caseService.GetCaseByIdAsync(id);
            caseDetail.SelectedPlans = caseDetail.Plans.Split(',').ToList(); // Converting comma-separated string to list
            ViewBag.PlansList = _plansList;
            return View(caseDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Case updatedCase)
        {
            if (ModelState.IsValid)
            {
                updatedCase.Plans = string.Join(",", updatedCase.SelectedPlans); // Converting selected plans to comma-separated string
                await _caseService.UpdateCaseAsync(id, updatedCase);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PlansList = _plansList;
            return View(updatedCase);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var caseDetail = await _caseService.GetCaseByIdAsync(id);
            return View(caseDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _caseService.DeleteCaseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

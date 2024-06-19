using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website.Models;
using Website.Services;

namespace Website.Controllers
{
    public class CaseContactsController : Controller
    {
        private readonly CaseContactService _caseContactService;
        private readonly CaseService _caseService;
        private readonly List<string> _allRolesList = new List<string>
        {
            "Admin", "Accountant", "VCFO", "Support"
        };
        private readonly List<string> _responsibilitiesList = new List<string>
        {
            "Hiring", "Training", "Budgeting", "Networking"
        };
        private readonly List<string> _contactCategoryList = new List<string>
        {
            "Client", "Broker"
        };

        public CaseContactsController(CaseContactService caseContactService, CaseService caseService)
        {
            _caseContactService = caseContactService;
            _caseService = caseService;
        }

        public async Task<IActionResult> Index()
        {
            var caseContacts = await _caseContactService.GetAllCaseContactsAsync();
            return View(caseContacts);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var caseContactDetail = await _caseContactService.GetCaseContactByIdAsync(id);
            return View(caseContactDetail);
        }

        public async Task<IActionResult> Create(Guid caseId)
        {
            var caseRoles = await _caseContactService.GetRolesForCaseAsync(caseId);
            var availableRoles = _allRolesList.Except(caseRoles).ToList();

            ViewBag.CasesList = new SelectList(await _caseService.GetAllCasesAsync(), "Id", "LegalBusinessName", caseId);
            ViewBag.ResponsibilitiesList = _responsibilitiesList;
            ViewBag.ContactCategoryList = _contactCategoryList;
            ViewBag.RolesList = availableRoles;
            return View(new CaseContact { CaseId = caseId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CaseContact newCaseContact)
        {
            if (ModelState.IsValid)
            {
                newCaseContact.Roles = string.Join(",", newCaseContact.SelectedRoles); // Converting selected roles to comma-separated string
                newCaseContact.Responsibilities = string.Join(",", newCaseContact.SelectedResponsibilities); // Converting selected responsibilities to comma-separated string
                await _caseContactService.CreateCaseContactAsync(newCaseContact);
                return RedirectToAction(nameof(Index));
            }

            var caseRoles = await _caseContactService.GetRolesForCaseAsync(newCaseContact.CaseId);
            var availableRoles = _allRolesList.Except(caseRoles).ToList();

            ViewBag.CasesList = new SelectList(await _caseService.GetAllCasesAsync(), "Id", "LegalBusinessName", newCaseContact.CaseId);
            ViewBag.ResponsibilitiesList = _responsibilitiesList;
            ViewBag.ContactCategoryList = _contactCategoryList;
            ViewBag.RolesList = availableRoles;
            return View(newCaseContact);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var caseContactDetail = await _caseContactService.GetCaseContactByIdAsync(id);
            if (caseContactDetail == null)
            {
                return NotFound();
            }

            var caseRoles = await _caseContactService.GetRolesForCaseAsync(caseContactDetail.CaseId);
            var availableRoles = _allRolesList.Except(caseRoles).ToList();
            var selectedRoles = caseContactDetail.Roles?.Split(',').ToList() ?? new List<string>();

            availableRoles.AddRange(selectedRoles);
            availableRoles = availableRoles.Distinct().ToList();

            caseContactDetail.SelectedRoles = selectedRoles;
            caseContactDetail.SelectedResponsibilities = caseContactDetail.Responsibilities?.Split(',').ToList() ?? new List<string>();

            ViewBag.RolesList = new MultiSelectList(availableRoles, selectedRoles);
            ViewBag.ResponsibilitiesList = _responsibilitiesList;
            ViewBag.ContactCategoryList = _contactCategoryList;
            ViewBag.CasesList = new SelectList(await _caseService.GetAllCasesAsync(), "Id", "LegalBusinessName", caseContactDetail.CaseId);

            return View(caseContactDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CaseContact updatedCaseContact)
        {
            if (ModelState.IsValid)
            {
                updatedCaseContact.Roles = string.Join(",", updatedCaseContact.SelectedRoles); // Converting selected roles to comma-separated string
                updatedCaseContact.Responsibilities = string.Join(",", updatedCaseContact.SelectedResponsibilities); // Converting selected responsibilities to comma-separated string

                await _caseContactService.UpdateCaseContactAsync(id, updatedCaseContact);
                return RedirectToAction(nameof(Index));
            }

            var caseRoles = await _caseContactService.GetRolesForCaseAsync(updatedCaseContact.CaseId);
            var availableRoles = _allRolesList.Except(caseRoles).ToList();
            var selectedRoles = updatedCaseContact.Roles?.Split(',').ToList() ?? new List<string>();

            availableRoles.AddRange(selectedRoles);
            availableRoles = availableRoles.Distinct().ToList();

            ViewBag.RolesList = new MultiSelectList(availableRoles, selectedRoles);
            ViewBag.ResponsibilitiesList = _responsibilitiesList;
            ViewBag.ContactCategoryList = _contactCategoryList;
            ViewBag.CasesList = new SelectList(await _caseService.GetAllCasesAsync(), "Id", "LegalBusinessName", updatedCaseContact.CaseId);

            return View(updatedCaseContact);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var caseContactDetail = await _caseContactService.GetCaseContactByIdAsync(id);
            return View(caseContactDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _caseContactService.DeleteCaseContactAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

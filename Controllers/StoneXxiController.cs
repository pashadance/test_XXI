using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static stoneXXI.ViewModels;

namespace stoneXXI.Controllers;

[ApiController]
[Route("[controller]")]
public class StoneXxiController : ControllerBase
{
    private readonly Repository repository;
    public StoneXxiController(Repository repository)
    {
        this.repository = repository;
    }

    [HttpGet("get-vacancies")]
    public async Task<IActionResult> GetVacancies(int? departmentId, bool activeOnly)
    {
        var vacs = await repository.Vacancies.Where(t => (!activeOnly || t.IsActive) &&
                                                                                (!departmentId.HasValue || departmentId==t.DepartmentId))
                                                            .Select(t => new VacancyModel
                                                            {
                                                                Description = t.Description,
                                                                Id = t.Id,
                                                                Name = t.Name,
                                                                Link = t.Link
                                                            })
                                                             .ToListAsync();
        return Ok(vacs);
    }

    [HttpGet("get-candidates-by-vacancy")]
    public async Task<IActionResult> GetCandidatesByVacancy(int vacancyId)
    {
        var candidates = await repository.Candidates.Where(t => t.VacancyId==vacancyId)
                                                                    .Select(t => new CandidateModel
                                                                    {
                                                                        Id = t.Id,
                                                                        Name = t.Name,
                                                                        CurrentStageStr = t.CurrentStage.ToString(),
                                                                        HrName = t.Hr.Name,
                                                                        VacancyName = t.Vacancy.Name,
                                                                        IsPassedProbationaryPeriod = t.IsPassedProbationaryPeriod
                                                                    })
                                                                    .ToListAsync();
        return Ok(candidates);
    }

    [HttpPost("set-vacancy-test-task")]
    public async Task<IActionResult> SetVacancyTestTask(TestTask task)
    {
        var vacancy = await repository.Vacancies.FindAsync(task.VacancyId);
        vacancy.TestTask = task.Value;
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("get-vacancy-test-task")]
    public async Task<IActionResult> GetVacancyTestTask(int vacId)
    {
        var vacancy = await repository.Vacancies.FindAsync(vacId);
        return Ok(vacancy.TestTask);
    }

    [HttpPost("add-or-edit-candidate")]
    public async Task<IActionResult> AddOrEditCandidate(CandidateModel cand)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Models.Candidate candidate = null;
        if (cand.Id.HasValue)
            candidate = await repository.Candidates.FirstOrDefaultAsync(t => t.Id == cand.Id.Value);
        else
        {
            candidate = new Models.Candidate();
            await repository.Candidates.AddAsync(candidate);
        }

        candidate.Name = cand.Name;
        candidate.CurrentStage = cand.CurrentStage;
        candidate.HrId = cand.HrId;
        candidate.VacancyId = cand.VacancyId;

        await repository.SaveChangesAsync();

        return Ok(candidate.Id);
    }

    [HttpPost("set-candidate-passed")]
    public async Task<IActionResult> SetCandidatePassed([FromBody] int Id)
    {
        var candidate = await repository.Candidates.FindAsync(Id);
        candidate.IsPassedProbationaryPeriod = true;
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("change-candidate-stage")]
    public async Task<IActionResult> ChangeCandidateStage(ChangeStage cs)
    {
        var cand =await repository.Candidates.FindAsync(cs.CandId);
        cand.CurrentStage = cs.Value;
        await repository.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SetHrToCandidate()
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> AddOrEditDepartment()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEditVacancy()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEditHr()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveHr()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RemoveDepartment()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> DeactivateVacancy()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> ActivateVacancy()
    {
        return Ok();
    }
}

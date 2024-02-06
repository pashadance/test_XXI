using static stoneXXI.Models;
using System.ComponentModel.DataAnnotations;

namespace stoneXXI;

public class ViewModels
{
    public class CandidateModel 
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CurrentStageStr { get; set; }
        public Stage CurrentStage { get; set; }
        public string VacancyName { get; set; }
        public string HrName { get; set; }
        public int? HrId { get; set; }

        [Required(ErrorMessage = "Vacancy is required")]
        public int VacancyId { get; set; }
        public bool IsPassedProbationaryPeriod { get; set; }
    }

    public class VacancyModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }

    public class TestTask
    {
        public int VacancyId { get; set; }
        public string Value { get; set; }
    }

    public class ChangeStage
    {
        public int CandId { get; set; }
        public Stage Value { get; set; }
    }
}
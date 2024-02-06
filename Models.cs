using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stoneXXI;

public class Models
{
    public interface IId
    {
        int Id { get; set; }
    }
    public interface IName
    {
        string Name { get; set; }
    }

    public abstract class Entity : IId, IName
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")] // the best way is to use Resources 
        public string Name { get; set; }
    }

    public enum Stage
    {
        HrMeet,
        TechConsideration,
        TechInterview,
        Offer
    }

    public class HrSpecialist : Entity
    {
        public virtual List<Vacancy> AssignedVacancies { get; set; }
    }

    public class Department : Entity
    {
        public virtual List<Vacancy> Vacancies { get; set; }
    }

    public class Candidate : Entity
    {
        public virtual Vacancy Vacancy { get; set; }
        public virtual HrSpecialist Hr { get; set; }
        public int? HrId { get; set; }
        public int VacancyId { get; set; }
        public Stage CurrentStage { get; set; }
        public bool IsNeedTestTask { get; set; }
        public bool IsPassedProbationaryPeriod { get; set; }

    }

    public class Vacancy : Entity
    {
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } 
        public string? TestTask { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}
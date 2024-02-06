using Microsoft.EntityFrameworkCore;
using stoneXXI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Repository>(ServiceLifetime.Scoped);
builder.Services.AddControllers();

var app = builder.Build();

var scope = app.Services.CreateScope();
var repository = scope.ServiceProvider.GetRequiredService<Repository>();
repository.Database.Migrate();

#if DEBUG
//just for example
repository.Departments.Add(new Models.Department
{
    Name = "СТОУН-XXI, кадры",
});
repository.Vacancies.Add(new Models.Vacancy
{
    Name = "Программист C#",
    Link = "https://spb.hh.ru/vacancy/91523734",
    Description =
        @"Компания «СТОУН–XXI» в связи с открытием новых ИТ проектов расширяет команду IT разработки и ищет «Программиста С#» на направление технологического и функционального развития ERP системы.
Вы будете работать с продуктом, имеющим ключевую важность для бизнеса, сможете предлагать новые решения, влиять на ход проекта и конечный продукт разработки.",
    IsActive = true,
    DepartmentId = 1
});
#endif

repository.SaveChanges();

app.UseAuthorization();

app.MapControllers();

app.Run();

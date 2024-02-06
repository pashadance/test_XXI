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
    Name = "�����-XXI, �����",
});
repository.Vacancies.Add(new Models.Vacancy
{
    Name = "����������� C#",
    Link = "https://spb.hh.ru/vacancy/91523734",
    Description =
        @"�������� �����͖XXI� � ����� � ��������� ����� �� �������� ��������� ������� IT ���������� � ���� ������������� �#� �� ����������� ���������������� � ��������������� �������� ERP �������.
�� ������ �������� � ���������, ������� �������� �������� ��� �������, ������� ���������� ����� �������, ������ �� ��� ������� � �������� ������� ����������.",
    IsActive = true,
    DepartmentId = 1
});
#endif

repository.SaveChanges();

app.UseAuthorization();

app.MapControllers();

app.Run();

using DomainLayer.Entities;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services;

namespace ServiceLayer.Services.Implementations;

public class StudentService : IStudentService
{
    private StudentRepository _studentRepository = new();
    private int _count = 1;

    public Student Create(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name) || student.Name.Any(c => char.IsDigit(c)))
            throw new Exception("Student name cannot be empty or contain numbers!");

        if (string.IsNullOrWhiteSpace(student.Surname) || student.Surname.Any(c => char.IsDigit(c)))
            throw new Exception("Student surname cannot be empty or contain numbers!");

        if (student.Age < 5 || student.Age > 100)
            throw new Exception("Student age must be between 5 and 100!");

        if (student.Group is null)
            throw new Exception("Student group cannot be empty!");

        student.Id = _count++;
        _studentRepository.Create(student);
        return student;
    }

    public Student GetById(int id)
    {
        var student = _studentRepository.Get(s => s.Id == id);
        if (student is null) throw new Exception("No student found with this ID!");
        return student;
    }

    public void Delete(int id)
    {
        var student = _studentRepository.Get(s => s.Id == id);
        if (student is null) throw new Exception("Student to delete not found!");
        _studentRepository.Delete(student);
    }

    public Student Update(int id, Student student)
    {
        var existStudent = _studentRepository.Get(s => s.Id == id);
        if (existStudent is null) throw new Exception("Student to update not found!");

        if (string.IsNullOrWhiteSpace(student.Name) || student.Name.Any(c => char.IsDigit(c)))
            throw new Exception("Student name cannot be empty or contain numbers!");

        if (string.IsNullOrWhiteSpace(student.Surname) || student.Surname.Any(c => char.IsDigit(c)))
            throw new Exception("Student surname cannot be empty or contain numbers!");

        if (student.Age < 5 || student.Age > 100)
            throw new Exception("Student age must be between 5 and 100!");

        if (student.Group is null)
            throw new Exception("Student group cannot be empty!");

        existStudent.Name = student.Name;
        existStudent.Surname = student.Surname;
        existStudent.Age = student.Age;
        existStudent.Group = student.Group;
        return existStudent;
    }

    public List<Student> GetAllByAge(int age)
    {
        if (age < 5 || age > 100)
            throw new Exception("Age must be between 5 and 100!");

        var students = _studentRepository.GetAll(s => s.Age == age);
        if (students is null || students.Count == 0)
            throw new Exception("No students found with this age!");

        return students;
    }

    public List<Student> GetAllByGroupId(int groupId)
    {
        if (groupId <= 0) throw new Exception("Group ID must be greater than 0!");

        var students = _studentRepository.GetAll(s => s.Group.Id == groupId);
        if (students.Count == 0) throw new Exception("No students found in this group!");

        return students;
    }

    public List<Student> SearchByNameOrSurname(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            throw new Exception("Search field cannot be empty!");

        if (search.Any(c => char.IsDigit(c)))
            throw new Exception("Search field cannot contain numbers!");

        var students = _studentRepository.GetAll(s =>
            s.Name.ToLower().Contains(search.ToLower()) ||
            s.Surname.ToLower().Contains(search.ToLower()));

        if (students.Count == 0)
            throw new Exception("No students found with this name or surname!");

        return students;
    }
}
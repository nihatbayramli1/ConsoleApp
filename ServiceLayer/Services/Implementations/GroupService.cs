using RepositoryLayer.Repositories.Implementations;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Implementations;

public class GroupService : IGroupService
{
    private GroupRepository _groupRepository;
    private int _count = 1;

    public GroupService()
    {
        _groupRepository = new GroupRepository();
    }

    public Group Create(Group group)
    {
        if (string.IsNullOrWhiteSpace(group.Name) || group.Name.Any(c => char.IsDigit(c)))
            throw new Exception("Group name cannot be empty or contain numbers!");

        if (string.IsNullOrWhiteSpace(group.Teacher) || group.Teacher.Any(c => char.IsDigit(c)))
            throw new Exception("Teacher name cannot be empty or contain numbers!");

        if (group.RoomCount <= 0 || group.RoomCount > 100)
            throw new Exception("Room count must be between 1 and 100!");

        group.Id = _count++;
        _groupRepository.Create(group);
        return group;
    }

    public Group Update(int id, Group group)
    {
        var existGroup = GetById(id);
        if (existGroup is null) throw new Exception("Group to update not found!");

        if (string.IsNullOrWhiteSpace(group.Name) || group.Name.Any(c => char.IsDigit(c)))
            throw new Exception("Group name cannot be empty or contain numbers!");

        if (string.IsNullOrWhiteSpace(group.Teacher) || group.Teacher.Any(c => char.IsDigit(c)))
            throw new Exception("Teacher name cannot be empty or contain numbers!");

        if (group.RoomCount <= 0 || group.RoomCount > 100)
            throw new Exception("Room count must be between 1 and 100!");

        existGroup.Name = group.Name;
        existGroup.Teacher = group.Teacher;
        existGroup.RoomCount = group.RoomCount;
        return existGroup;
    }

    public void Delete(int id)
    {
        var group = GetById(id);
        if (group is null) throw new Exception("Group to delete not found!");
        _groupRepository.Delete(group);
    }

    public Group GetById(int id)
    {
        return _groupRepository.Get(l => l.Id == id);
    }

    public List<Group> GetAll()
    {
        return _groupRepository.GetAll();
    }

    public List<Group> GetAllByTeacher(string teacher)
    {
        if (string.IsNullOrWhiteSpace(teacher))
            throw new Exception("Teacher name cannot be empty!");

        if (teacher.Any(c => char.IsDigit(c)))
            throw new Exception("Teacher name cannot contain numbers!");

        var groups = _groupRepository.GetAll(g => string.Equals(g.Teacher, teacher, StringComparison.OrdinalIgnoreCase));
        if (groups.Count == 0) throw new Exception("No groups found for this teacher!");
        return groups;
    }

    public List<Group> GetAllByRoom(int roomCount)
    {
        if (roomCount <= 0 || roomCount > 100)
            throw new Exception("Room count must be between 1 and 100!");

        var groups = _groupRepository.GetAll(g => g.RoomCount == roomCount);
        if (groups.Count == 0) throw new Exception("No groups found with this room count!");
        return groups;
    }

    public List<Group> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Search name cannot be empty!");

        if (name.Any(c => char.IsDigit(c)))
            throw new Exception("Search name cannot contain numbers!");

        var groups = _groupRepository.GetAll(g => g.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        if (groups.Count == 0) throw new Exception("No groups found with this name!");
        return groups;
    }
}
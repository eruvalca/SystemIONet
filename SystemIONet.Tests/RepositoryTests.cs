using SystemIONet.Data;
using SystemIONet.Models;

namespace SystemIONet.Tests;

public class FileFixture : IDisposable
{
    private const string _filePath = @"C:\Data\SystemIO\StudentTest.txt";
    private const string _originalData = @"C:\Data\SystemIO\StudentTestSeed.txt";

    public FileFixture()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }

        File.Copy(_originalData, _filePath);
    }

    public void Dispose()
    {
    }
}

public class RepositoryTests : IClassFixture<FileFixture>
{
    private const string _filePath = @"C:\Data\SystemIO\StudentTest.txt";
    private const string _originalData = @"C:\Data\SystemIO\StudentTestSeed.txt";

    public RepositoryTests()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }

        File.Copy(_originalData, _filePath);
    }

    [Fact]
    public void CanReadDataFromFile()
    {
        StudentRepository repo = new(_filePath);

        List<Student> students = repo.List();

        Assert.Equal(4, students.Count());

        Student check = students[2];

        Assert.Equal("Jane", check.FirstName);
        Assert.Equal("Doe", check.LastName);
        Assert.Equal("Computer Science", check.Major);
        Assert.Equal(4.0M, check.GPA);
    }

    [Fact]
    public void CanAddStudentToFile()
    {
        StudentRepository repo = new(_filePath);

        Student newStudent = new();
        newStudent.FirstName = "Testy";
        newStudent.LastName = "Tester";
        newStudent.Major = "Research";
        newStudent.GPA = 3.2M;

        repo.Add(newStudent);

        List<Student> students = repo.List();

        Assert.Equal(5, students.Count());

        Student check = students[4];

        Assert.Equal("Testy", check.FirstName);
        Assert.Equal("Tester", check.LastName);
        Assert.Equal("Research", check.Major);
        Assert.Equal(3.2M, check.GPA);

    }

    [Fact]
    public void CanDeleteStudent()
    {
        StudentRepository repo = new(_filePath);
        repo.Delete(0);

        List<Student> students = repo.List();

        Assert.Equal(3, students.Count);

        Student check = students[0];

        Assert.Equal("Mary", check.FirstName);
        Assert.Equal("Jone", check.LastName);
        Assert.Equal("Business", check.Major);
        Assert.Equal(3.0M, check.GPA);
    }

    [Fact]
    public void CanEditStudent()
    {
        StudentRepository repo = new(_filePath);
        List<Student> students = repo.List();

        Student editedStudent = students[0];
        editedStudent.GPA = 3.0M;

        repo.Edit(editedStudent, 0);

        Assert.Equal(4, students.Count);

        students = repo.List();
        Student check = students[0];

        Assert.Equal("Joe", check.FirstName);
        Assert.Equal("Smith", check.LastName);
        Assert.Equal("Computer Science", check.Major);
        Assert.Equal(3.0M, check.GPA);
    }
}

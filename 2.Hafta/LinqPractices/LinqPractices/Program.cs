using LinqPractices.DbOperations;
using LinqPractices.Entity;

DataGenerator.intialized();
LinqDbContext _context = new LinqDbContext();
var Students = _context.Students.ToList<Student>();
//Find();
Console.WriteLine("***** Find *****");
var student = _context.Students.Where(student => student.StudentID == 1).FirstOrDefault();
student = _context.Students.Find(2);
Console.WriteLine(student.Name);

// FirstOrDefault()

Console.WriteLine();
Console.WriteLine("***** FirstOrDefault *****");
student = _context.Students.Where(student => student.Surname == "Arda").FirstOrDefault();
Console.WriteLine(student.Name);

student = _context.Students.FirstOrDefault(x => x.Surname == "Arda");
Console.WriteLine(student.Name);

// SingleOrDefault()

Console.WriteLine();
Console.WriteLine("***** SingleOrDefault *****");
student = _context.Students.SingleOrDefault(x => x.Name == "Deniz");
//student = _context.Students.SingleOrDefault(x => x.SurName == "Arda"); 2 singleordefault ta hata alınır
Console.WriteLine(student.Surname);
// ToList


Console.WriteLine();
Console.WriteLine("***** ToList *****");
var studentlist = _context.Students.Where(student => student.ClassID == 2).ToList();
Console.WriteLine(studentlist.Count);

//OrderBy
Console.WriteLine();
Console.WriteLine("***** OrderBy *****");

var students = _context.Students.OrderBy(x => x.StudentID).ToList();
foreach (var st in students)
{
    Console.WriteLine(st.StudentID + " - " + st.Name + " - " + st.Surname);
}

//OrderByDescending
Console.WriteLine();
Console.WriteLine("***** OrderByDescending *****");

students = _context.Students.OrderByDescending(x => x.StudentID).ToList();
foreach (var st in students)
{
    Console.WriteLine(st.StudentID + " - " + st.Name + " - " + st.Surname);
}

// Anonymous Object Result
Console.WriteLine();
Console.WriteLine("***** Anonymous Object Result *****");

var anonymousObject = _context.Students
                    .Where(x => x.ClassID == 2)
                    .Select(x => new
                    {
                        Id = x.StudentID,
                        FullName = x.Name + " " + x.Surname
                    });

foreach (var item in anonymousObject)
{
    Console.WriteLine(item.Id + " - " + item.FullName);
}
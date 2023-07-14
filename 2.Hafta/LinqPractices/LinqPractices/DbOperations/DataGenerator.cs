using LinqPractices.Entity;

namespace LinqPractices.DbOperations
{
    public class DataGenerator
    {
        public static void intialized()
        {
            using (var context = new LinqDbContext())
            {
                if (context.Students.Any())
                {
                    return;
                }

                context.Students.AddRange
                    (
                        new Student()
                        {
                            Name = "Ayşe",
                            Surname = "Yılmaz",
                            ClassID = 1,
                        },
                        new Student()
                        {
                            Name = "Deniz",
                            Surname = "Arda",
                            ClassID = 1,
                        },
                        new Student()
                        {

                            Name = "Umut",
                            Surname = "Arda",
                            ClassID = 2,
                        },
                        new Student()
                        {

                            Name = "Merve",
                            Surname = "Çalışkan",
                            ClassID = 2,
                        }
                    );
                context.SaveChanges();
            }
        }
    }
}

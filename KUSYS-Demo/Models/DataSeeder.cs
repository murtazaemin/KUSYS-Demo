namespace KUSYS_Demo.Models
{
    public class DataSeeder
    {
        private readonly Context _context;

        public DataSeeder(Context context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Courses.Any())
            {
                // Açılışta eklenecek Course verileri
                var courses = new List<Course>()
                {
                        new Course()
                        {
                            CourseName = "Introduction to Computer Science",
                            CourseCode = "CSI101",
                            CourseId = new Guid("24bb0b88-1b75-409e-a43c-e7ca7c63bf04")
                        },
                        new Course()
                        {
                            CourseName = "Algorithms",
                            CourseCode = "CSI102",
                            CourseId = new Guid("955a79f8-5b19-4bd7-aab1-5d2271aaa16d")
                        },
                        new Course()
                        {
                            CourseName = "Calculus",
                            CourseCode = "MAT101",
                            CourseId = new Guid("a303ca7b-198a-4325-8ab1-9c1f09ac6462")
                        },
                        new Course()
                        {
                            CourseName = "Physics",
                            CourseCode = "PHY101",
                            CourseId = new Guid("8e13acbb-c81c-4c1c-9d0f-8295bf2071de")
                        }
                };

                _context.Courses.AddRange(courses);
                _context.SaveChanges();
            }
            if (!_context.Students.Any())
            {
                // Açılışta eklenecek Student verileri
                var students = new List<Student>()
                {
                    new Student()
                    {
                        FirstName = "Murtaza Emin",
                        LastName = "Alyüz",
                        BirthDate = new DateTime(1993,07,20),
                        StudentId = new Guid("c80b27d8-ee15-43a0-834a-02f25d89ce06")
                    }
                };

                _context.Students.AddRange(students);
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                // Açılışta eklenecek User verileri
                var users = new List<User>()
                {
                        new User()
                        {
                            UserName = "Murtaza",
                            Password = "123",
                            Role = "User",
                            UserId = new Guid("db4c3d37-4cee-4422-b9a0-534fecb9a0b3"),
                            StudentId = new Guid("c80b27d8-ee15-43a0-834a-02f25d89ce06")
                        },
                        new User()
                        {
                            UserName = "Admin",
                            Password = "123",
                            Role = "Admin",
                            UserId = new Guid("1f863ae2-3a7c-4def-84b4-3b90e57542b7"),
                            StudentId = new Guid("c80b27d8-ee15-43a0-834a-02f25d89ce06")
                        }
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();

            }
            
        }
    }
}

using T12V2.BusinessLogic;
using T12V2.DTO;


namespace ASPt2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            UserManager userManager = new UserManager();
            User user;

            app.MapGet("/", () => "Hello World");


            //app.MapGet("/hello", (HttpRequest request) =>
            app.MapGet("/hello", (string name, int age) =>
            {
                //string? name = request.Query["name"];
                //return $"Hello, {name}. Age = {age}";
                Person person = new Person(name,age);
                return person;
            });

            app.MapGet("/bye/{name}", (string name) =>
            {
                return $"Bye, {name}";
            });

            //querry param
            app.MapGet("/person", (string? name) =>
            {
                var list = new List<Person>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Person($"Person {i}", i * 10 + 1));
                }
                return list;
            });

            //login
            app.MapGet("/login/{name}/{password}", (string name, string password) =>
            {
                string statement="";
                user = userManager.Login(name, password);
                if (user == null)
                {
                    statement = "Login Fail";
                }
                else
                {
                    statement = " Login Success";
                }
                return statement;
            });
            //show User
            app.MapGet("/users", () =>
            {
                List<User> list = null;
                list = userManager.SelectAll();
                return list;
            });
            // show user by id
            app.MapGet("/users/{id}", (int id) =>
            {
                User user = null;
                user = userManager.SelectByID(id);                
                return user;                
            });
            //post user
            app.MapPost("/user", (string username, string fullname, string password, UserRole role) => 
            {
                user = new User(0, username ,fullname, password, role);
                int rowAffected = userManager.AddNew(user);
                return new { message = "Inserted", rowAffected };
            });
            //update user by id
            app.MapPost("/user/{id}", (int id, string field, string newString) =>
            {
                User userUpt = userManager.SelectByID(id);
                int rowAffected = userManager.Update(userUpt, field, newString );
                return new { message =" Success ", rowAffected };
            });


            app.Run();
        }
    }
}
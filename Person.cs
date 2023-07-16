namespace ASPt2
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person() { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        //public override string ToString()
        //{
        //    return string.Format($"{Name}, {Age}");
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimorfizam
{
    #region dessert
    class Dessert
    {
        private string name;
        private double weight;
        private int calories;

        public Dessert(string name, double weight, int calories)
        {
            this.name = name;
            this.weight = weight;
            this.calories = calories;
        }
        public override string ToString()
        {
            return this.name + this.Weight + this.calories;
        }
        virtual public string getDessertType()
        {
            return "dessert";
        }

        public string Name { get => name; set => name = value; }
        public double Weight { get => weight; set => weight = value; }
        public int Calories { get => calories; set => calories = value; }
    }
    class Cake : Dessert
    {
        bool containsGluten;
        string cakeType;

        public Cake(string name, double weight, int calories, bool containsGluten, string cakeType) : base(name, weight, calories)
        {
            this.ContainsGluten = containsGluten;
            this.CakeType = cakeType;
        }

        public override string getDessertType()
        {
            return cakeType + "cake";
        }
        public override string ToString()
        {
            return base.ToString() + this.containsGluten + this.cakeType;
        }

        public bool ContainsGluten { get => containsGluten; set => containsGluten = value; }
        public string CakeType { get => cakeType; set => cakeType = value; }
    }
    class IceCream : Dessert
    {
        string color, flavour;
        public IceCream(string name, double weight, int calories, string color, string flavour) : base(name, weight, calories)
        {
            this.color = color;
            this.flavour = flavour;
        }
        public override string ToString()
        {
            return base.ToString() + this.color + this.flavour;
        }

        public override string getDessertType()
        {
            return "ice cream";
        }

        public string Color { get => color; set => color = value; }
        public string Flavour { get => flavour; set => flavour = value; }
    }
    #endregion
    #region people
    class Person
    {
        string name, surname;
        int age;

        public Person(string name, string surname, int age)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
        }
        public override bool Equals(object obj)
        {
            return obj is Person person && this.name == person.name && this.surname == person.surname && this.age == person.age;
        }
        public override string ToString()
        {
            return this.name + " " + this.surname + " " + this.age;
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public int Age { get => age; set => age = value; }
    }
    class Student : Person
    {
        string studentID;
        short academicYear;

        public Student(string name, string surname, int age, string studentID, short academicYear) : base(name, surname, age)
        {
            this.studentID = studentID;
            this.academicYear = academicYear;
        }
        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   base.Equals(obj) &&
                   studentID == student.studentID;
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.studentID + " " + this.academicYear;
        }

        public override int GetHashCode()
        {
            return -1980727003 + EqualityComparer<string>.Default.GetHashCode(studentID);
        }

        public string StudentID { get => studentID; set => studentID = value; }
        public short AcademicYear { get => academicYear; set => academicYear = value; }
    }
    class Teacher : Person
    {
        string email, subject;
        double salary;

        public Teacher(string name, string surname, int age, string email, string subject, double salary) : base(name, surname, age)
        {
            this.email = email;
            this.subject = subject;
            this.salary = salary;
        }
        public override bool Equals(object obj)
        {
            return obj is Teacher teacher &&
                   base.Equals(obj) &&
                   email == teacher.email;
        }
        public override int GetHashCode()
        {
            return 848330207 + EqualityComparer<string>.Default.GetHashCode(email);
        }


        public override string ToString()
        {
            return base.ToString() + " " + this.email + " " + this.subject + " " + this.salary;
        }
        public void incraseSalary(int number)
        {
            this.salary = this.salary * ((number / 100) + 1);

        }
        public static void incraseSalary(int number, params Teacher[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].incraseSalary(number);
            }
        }

        public string Email { get => email; set => email = value; }
        public string Subject { get => subject; set => subject = value; }
        public double Salary { get => salary; set => salary = value; }
    }
    #endregion
    #region competition
    class CompetitionEntrycs
    {
        Teacher teacher;
        Dessert dessert;
        private Student[] students;
        private int[] ratings;
        private int idx;
        public CompetitionEntrycs(Teacher teacher, Dessert dessert)
        {
            this.teacher = teacher;
            this.dessert = dessert;
        }

        public bool addEntry(Student student, int number)
        {
            if (idx == 3)
            {
                return false;
            }
            foreach (Student s in students)
            {
                if (s != null && s.Equals(student))
                    return false;
            }
            Students[idx] = student;
            ratings[idx] = number;
            idx++;
            return true;
        }
        public double getRating()
        {
            if (idx == 0) return 0;

            double sum = 0;
            for (int i = 0; i < idx; i++)
                sum += ratings[i];

            return sum / idx;
        }

        internal Teacher Teacher { get => teacher; set => teacher = value; }
        internal Dessert Dessert { get => dessert; set => dessert = value; }
        public int[] Ratings { get => ratings; set => ratings = value; }
        public int Idx { get => idx; set => idx = value; }
        public Student[] Students { get => students; set => students = value; }
    }
    class UniMasterChef
    {
        private CompetitionEntrycs[] entries;
        private int idx = 0;

        public UniMasterChef(int noOfEntries)
        {
            this.entries = new CompetitionEntrycs[noOfEntries];
        }
        public bool addEntry(CompetitionEntrycs entry)
        {
            if (idx == this.entries.Length) return false;

            foreach (CompetitionEntrycs e in entries)
            {
                if (e != null && e.Equals(entry))
                    return false;
            }
            entries[idx++] = entry;
            return true;
        }
        public Dessert getBestDessert()
        {
            if (idx == 0) return null;

            double max = entries[0].getRating();
            int maxIdx = 0;

            for (int i = 0; i < idx; i++)
            {
                if (entries[i].getRating() > max)
                {
                    max = entries[i].getRating();
                    maxIdx = i;
                }
            }

            return entries[maxIdx].Dessert;
        }
        public static Person[] getInvolvedPeople(CompetitionEntrycs entry)
        {

            Person[] retVal = new Person[4];

            int idx = 0;

            retVal[idx++] = entry.Teacher;

            foreach (Student s in entry.Students)
            {
                retVal[idx++] = s;
            }

            return retVal;
        }
    }
    #endregion


    internal class Program
    {
        static void Main(string[] args)
        {
            Dessert genericDessert = new Dessert("Chocolate Mousse", 120, 300);
            Cake cake = new Cake("Raspberry chocolate cake #3", 350.5, 400, false, "birthday");
            Teacher t1 = new Teacher("Dario", "Tušek", 42, "dario.tusek@fer.hr", "OOP", 10000);
            Teacher t2 = new Teacher("Doris", "Bezmalinović", 43, "doris.bezmalinovic@fer.hr",
            "OOP", 10000);
            Student s1 = new Student("Janko", "Horvat", 18, "0036312123", (short)1);
            Student s2 = new Student("Ana", "Kovač", 19, "0036387656", (short)2);
            Student s3 = new Student("Ivana", "Stanić", 19, "0036392357", (short)1);
            UniMasterChef competition = new UniMasterChef(2);
            CompetitionEntrycs e1 = new CompetitionEntrycs(t1, genericDessert);
            competition.addEntry(e1);
            Console.WriteLine("Entry 1 rating: " + e1.getRating());
            e1.addEntry(s1, 4);
            e1.addEntry(s2, 5);
            Console.WriteLine("Entry 1 rating: " + e1.getRating());
            CompetitionEntrycs e2 = new CompetitionEntrycs(t2, cake);
            e2.addEntry(s1, 4);
            e2.addEntry(s3, 5);
            e2.addEntry(s2, 5);
            competition.addEntry(e2);
            Console.WriteLine("Entry 2 rating: " + e2.getRating());
            Console.WriteLine("Best dessert is: " + competition.getBestDessert().Name);
        }
    }
}

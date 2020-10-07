using ConsoleApp2;
using System;


namespace ConsoleApp2
{
    interface IstudentManager
    {
        bool AddNewstudent(student bk);
        bool Deletestudent(int id);
        bool Updatestudent(student bk);
        student[] GetAllstudents(string Name);
    }

    class studentManager : IstudentManager
    {

        private student[] allstudents = null;

        private bool hasstudent(int id)
        {
            foreach (student bk in allstudents)
            {
                if ((bk != null) && (bk.studentID == id))
                    return true;
            }
            return false;

        }
        //Function that is invoked when the object is created
        public studentManager(int size)
        {
            allstudents = new student[size];
        }
        public bool AddNewstudent(student bk)
        {
            bool available = hasstudent(bk.studentID);
            if (available)
                throw new Exception("student by this ID already exists");
            for (int i = 0; i < allstudents.Length; i++)
            {
                //find the first occurance of null in the array...
                if (allstudents[i] == null)
                {
                    allstudents[i] = new student();
                    allstudents[i].studentID = bk.studentID;
                    allstudents[i].Name = bk.Name;
                    allstudents[i].Address = bk.Address;
                    allstudents[i].phnumber = bk.phnumber;
                    return true;
                }
            }
            return false;
        }

        public bool Deletestudent(int id)
        {
            for (int i = 0; i < allstudents.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allstudents[i] != null) && (allstudents[i].studentID == id))
                {
                    allstudents[i] = null;
                    return true;
                }
            }
            return false;
        }

        public student[] GetAllstudents(string Name)
        {
            //create a blank array of students of the same size as the original.
            student[] copy = new student[allstudents.Length];
            //iterate thro the original to find the matching student.
            int index = 0;
            foreach (student bk in allstudents)
            {
                //if found add the student to the copy...
                if ((bk != null) && (bk.Name.Contains(Name)))
                {
                    copy[index] = bk;
                    index++;
                }
            }

            //return the copy...
            return copy;
        }

        public bool Updatestudent(student bk)
        {
            for (int i = 0; i < allstudents.Length; i++)
            {
                //find the first occurance of null in the array...
                if ((allstudents[i] != null) && (allstudents[i].studentID == bk.studentID))
                {
                    allstudents[i].Name = bk.Name;
                    allstudents[i].Address = bk.Address;
                    allstudents[i].phnumber = bk.phnumber;
                    return true;
                }
            }
            return false;
        }
    }
    class student
    {
        public int studentID
        {
            get; set;
        }
        public string Name { get; set; }
        public double phnumber { get; set; }
        public string Address { get; set; }
    }

    //Clients should not instantiate the object. they will call our factory method to get the type of the object they want...
    class studentFactoryComponent
    {
        public static IstudentManager GetComponent(int size)
        {
            return new studentManager(size);
        }
    }
    class UIClient
    {
        static string menu = string.Empty;
        static IstudentManager mgr = null;
        static void InitializeComponent()
        {
            menu = string.Format($"~~student MANAGEMENT SOFTWARE~~~~~~\nTO ADD A NEW student------------->PRESS 1\nTO UPDATE A student------------>PRESS 2\nTO DELETE A student------------PRESS 3\nTO FIND A student------------->PRESS 4\nPS:ANY OTHER KEY IS CONSIDERED AS EXIT THE APP\n");
            int size = MyConsole.getNumber("Enter the no of students U wish to store");
            mgr = studentFactoryComponent.GetComponent(size);
            mgr.AddNewstudent(new student { studentID = 1, Name = "Ganavi", Address = "mysuru", phnumber = 8967543487 });
            mgr.AddNewstudent(new student { studentID = 2, Name = "Pratheeksha", Address = "bangalore", phnumber = 9587564587 });
            mgr.AddNewstudent(new student { studentID = 3, Name = "Chandana", Address = "hydrabad", phnumber = 9856784556 });
            mgr.AddNewstudent(new student { studentID = 4, Name = "Anjali", Address = "kerala", phnumber = 8978564512 });

        }

        static void Main(string[] args)
        {
            InitializeComponent();
            bool @continue = true;
            do
            {
                string choice = MyConsole.getString(menu);
                @continue = processing(choice);
            } while (@continue);
        }

        private static bool processing(string choice)
        {
            switch (choice)
            {
                case "1":
                    addingstudentFeature();
                    break;
                case "2":
                    updatingstudentFeature();
                    break;
                case "3":
                    deletingFeature();
                    break;
                case "4":
                    readingFeature();
                    break;
                default:
                    return false;
            }
            return true;
        }

        private static void readingFeature()
        {
            string Name = MyConsole.getString("Enter the Name or part of the Name to search");
            student[] students = mgr.GetAllstudents(Name);
            foreach (var bk in students)
            {
                if (bk != null)
                    Console.WriteLine(bk.Name);
            }
        }

        private static void deletingFeature()
        {
            int id = MyConsole.getNumber("Enter the ID of the student to remove");
            if (mgr.Deletestudent(id))
                Console.WriteLine("student Deleted successfully");
            else
                Console.WriteLine("Could not find the student to delete");
        }

        private static void updatingstudentFeature()
        {
            student bk = new student();
            bk.studentID = MyConsole.getNumber("Enter the ISBN no of the student U wish to update");
            bk.Name = MyConsole.getString("Enter the new Name of this student");
            bk.Address = MyConsole.getString("Enter the new Address of this student");
            bk.phnumber = MyConsole.getDouble("Enter the new phnumber of this student");
            bool result = mgr.Updatestudent(bk);
            if (!result)
                Console.WriteLine($"No student by this id {bk.studentID} found to update");
            else
                Console.WriteLine($"student by ID {bk.studentID} is updated successfully to the database");
        }

        private static void addingstudentFeature()
        {
            student bk = new student();
            bk.studentID = MyConsole.getNumber("Enter the ID of the student");
            bk.Name = MyConsole.getString("Enter the Name of this student");
            bk.Address = MyConsole.getString("Enter the Address of this student");
            bk.phnumber = MyConsole.getDouble("Enter the phnumber of this student");
            try
            {
                bool result = mgr.AddNewstudent(bk);
                if (!result)
                    Console.WriteLine($"student by Name {bk.Name} is added successfully to the database");

                else
                    Console.WriteLine("No more students could be added");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

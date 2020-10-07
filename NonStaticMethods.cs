using System;
/*
Static methods and Non Static methods
Static methods are like singleton where they are refered using the classname. They dont have any data scoped to an instance. It is available across the application instance and will remain till the app terminates. 
They are fast in execution as no time is taken for the instantiation. 

*/
namespace ConsoleApp2
{

    class MyConsole
    {
        //members are private by default in C#
        internal static double getDouble(string question)
        {
            Console.WriteLine(question);
            return double.Parse(Console.ReadLine());
        }

        internal static string getString(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        internal static int getNumber(string question)
        {
            return int.Parse(getString(question));
        }

        internal static DateTime getDate(string question)
        {
            return DateTime.Parse(getString(question));
        }

        internal static DateTime getDate()
        {
            Console.WriteLine("Enter the date as dd/MM/yyyy");
            string value = Console.ReadLine();
            return DateTime.ParseExact(value, "dd/MM/yyyy", null);
        }
    }

    class Customer
    {
        int custID;
        string cstName, cstAddress;
        double billAmount;

        public void setDetails(int id, string name, string address)
        {
            custID = id;
            cstName = name;
            cstAddress = address;
        }

        public void addBillAmount(double amount)
        {
            billAmount += amount;
        }

        public double getBill()
        {
            return billAmount;
        }

        public void DisplayBill()
        {
            Console.WriteLine("The name:" + cstName);
            Console.WriteLine("The Address:" + cstAddress);
            Console.WriteLine("The Date:" + DateTime.Now);
            Console.WriteLine("The Total Bill: " + billAmount);
        }
    }
    class NonStaticMethods
    {
        static void Main(string[] args)
        {
            // dateTimeExample();
            exampleOnCustomer();
            exampleOnCustomer();
            exampleOnCustomer();
        }

        private static void exampleOnCustomer()
        {
            Customer cst = new Customer();
            cst.setDetails(123, "Phaniraj", "Bangalore");
            cst.addBillAmount(450);
            cst.addBillAmount(400);
            cst.addBillAmount(250);
            cst.addBillAmount(100);
            cst.addBillAmount(45);
            cst.addBillAmount(40);
            cst.DisplayBill();
        }

        private static void dateTimeExample()
        {
            /*
                DateTime is the data structure used to work with Date and Time in C#. 
                DateTime.Now property returns the current system's date and time. 
                Difference to 2 dates will return a Timespan structure. 
                Timespan contains properties that give the span of the duration in terms of days, months, minutes, Hours and so forth.
        */
            DateTime dt = MyConsole.getDate();
            Console.WriteLine("The Selected date is " + dt.ToLongDateString());
            TimeSpan span = DateTime.Now - dt;
            Console.WriteLine("The Age is " + (span.Days / 365.25));
        }
    }
}
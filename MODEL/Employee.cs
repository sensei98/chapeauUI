namespace Model
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public int Pin { get; set; }
        public int PhoneNumber { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
    public enum Role { Manager = 1, Barman, KitchenStaff, Waiter, unspecified }
}

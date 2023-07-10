namespace SipayTask1.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

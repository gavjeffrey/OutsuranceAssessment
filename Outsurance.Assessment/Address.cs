namespace Outsurance.Assessment
{
    public class Address
    {
        public Address(string Address)
        {
            var number = Address.Split(' ')[0];
            Number = number;
            Street = Address.Replace(number, "").Trim();            
        }
        public string Number { get; set; }
        public string Street { get; set; }
    }
}
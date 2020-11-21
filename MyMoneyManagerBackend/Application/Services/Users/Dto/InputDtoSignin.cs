namespace Application.Services.Users.Dto
{
    public class InputDtoSignin
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }

        public InputDtoSignin()
        {
        }
    }
}
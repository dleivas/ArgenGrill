namespace ArgenGrill.Models
{
    public class EmailViewModel
    {
        public string TextInfo { get; set; }
        public string ConfirmUrl { get; set; }
    }

    public class MyEmailViewModel
    {
        public string Email { get; set; }
        public string userID { get; set; }
    }
}
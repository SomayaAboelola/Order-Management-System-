namespace Order_Management_System.Error
{
    public class ValidationErrorResponse : ResponseApi
    {
        public ValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
        public IEnumerable<string> Errors { get; set; }
    }
}

namespace Order_Management_System.Dtos
{
    public class CustomerDto
    { 
        public int Id { get; set; } 
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}

namespace Acadimia.Infrastructure.Dtos.Auth
{
    public class LookupItemDto { public int Id { get; set; } public string Name { get; set; } }

    public class RegistrationOptionsDto
    {
        public List<LookupItemDto> Genders { get; set; } = new();
        public List<LookupItemDto> UserTypes { get; set; } = new();
    }
}
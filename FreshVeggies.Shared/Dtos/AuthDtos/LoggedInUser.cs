namespace FreshVeggies.Shared.Dtos.AuthDtos
{
    public record LoggedInUser(int userId, string firstName, string lastName, string email, string token);
}

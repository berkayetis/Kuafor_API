namespace KuaforRandevu.Application.Dtos
{
    public record CreateCustomerDto
    {
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
    }
}

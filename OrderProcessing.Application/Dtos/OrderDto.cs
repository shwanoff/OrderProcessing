namespace OrderProcessing.Application.Dtos
{
    public record OrderDto
    (
        Guid OrderNumber,
        string InvoiceAddress,
        string InvoiceEmailAddress,
        string InvoiceCreditCardNumber,
        IReadOnlyList<ProductDto> Products,
		DateTime CreatedAt
    )
    {
        public OrderDto(string InvoiceAddress, string InvoiceEmailAddress, string InvoiceCreditCardNumber, IReadOnlyList<ProductDto> Products)
            : this(Guid.Empty, InvoiceAddress, InvoiceEmailAddress, InvoiceCreditCardNumber, Products, DateTime.UtcNow)
        { 
        }

        public OrderDto()
			: this(default, default, default, default, default, default)
		{
		}
	}
}

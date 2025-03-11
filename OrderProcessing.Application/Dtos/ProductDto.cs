namespace OrderProcessing.Application.Dtos
{
    public record ProductDto(
        int ProductId,
        string ProductName,
        int ProductAmount,
        decimal ProductPrice)
	{
		public ProductDto()
			: this(default, default, default, default)
		{
		}

	}
}

namespace OrderProcessing.Domain
{
    public record Product(
		uint ProductId, 
		string Name, 
		uint Amount, 
		decimal Price)
	{
		public Product() : this(default, default, default, default) 
		{ 
		}	
	}
}

namespace OrderProcessing.Domain
{
    public record Product(
		uint Id, 
		string Name, 
		uint Amount, 
		decimal Price)
	{

	}
}

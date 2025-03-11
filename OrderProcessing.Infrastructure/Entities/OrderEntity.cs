using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OrderProcessing.Infrastructure.Entities
{
	[Table("Orders")]
	public class OrderEntity
    {
		[Key]
		public Guid Id { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(200)]
		public string Address { get; set; }

		[Required]
		[EmailAddress]
		[MinLength(3)]
		[MaxLength(200)]
		public string Email { get; set; }

		[Required]
		[CreditCard]
		[MinLength(12)]
		[MaxLength(50)]
		public string CreditCard { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public List<ProductEntity> Products { get; set; } = [];
	}
}

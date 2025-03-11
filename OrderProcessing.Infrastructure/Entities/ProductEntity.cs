using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OrderProcessing.Infrastructure.Entities
{
    public class ProductEntity
    {
		[Key]
		public Guid Id { get; set; }

		[Required]
		public uint ProductId { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public uint Amount { get; set; }

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required]
		public Guid OrderId { get; set; }
		[ForeignKey(nameof(OrderId))]
		public OrderEntity Order { get; set; }
	}
}

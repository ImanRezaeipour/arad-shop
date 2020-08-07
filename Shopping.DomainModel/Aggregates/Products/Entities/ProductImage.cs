using System;
using Shopping.Infrastructure.Core.Domain.Entities;

namespace Shopping.DomainModel.Aggregates.Products.Entities
{
	public class ProductImage : Entity
	{
		protected ProductImage() { }
		public ProductImage(Guid id, string url, int order)
		{
			Id = id;
			Url = url;
			Order = order;
		}
		public string Url { get; private set; }
		public int Order { get; private set; }
		public override void Validate()
		{
		}
	}
}
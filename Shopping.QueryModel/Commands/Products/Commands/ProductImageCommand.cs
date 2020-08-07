using System;

namespace Shopping.Commands.Commands.Products.Commands
{
	public class ProductImageCommand
	{
		public  Guid Id =>Guid.NewGuid();
		public string Url { get; set; }
		public int Order { get; set; }
	}
}
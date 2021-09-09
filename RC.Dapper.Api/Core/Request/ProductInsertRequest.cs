using System;

namespace RC.Dapper.Api.Core.Request
{
    public class ProductInsertRequest
    {
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
    }
}

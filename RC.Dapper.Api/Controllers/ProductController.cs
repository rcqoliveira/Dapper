using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RC.Dapper.Api.Core.Domains;
using RC.Dapper.Api.Core.Interface;
using RC.Dapper.Api.Core.Request;
using RC.Dapper.Api.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RC.Dapper.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        [Route("save")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainNotification))]
        public async Task Post([FromBody] ProductInsertRequest request)
        {
            await this.productService.InsertAsync(request);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainNotification))]
        public async Task Put([FromBody] ProductUpdateRequest request)
        {
            await this.productService.UpdateAsync(request);
        }

        [HttpGet]
        [Route("find/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainNotification))]
        public async Task<ProductResponse> Get(int productId)
        {
            return await this.productService.GetAsync(productId);
        }

        [HttpDelete]
        [Route("delete/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainNotification))]
        public async Task Delete(int productId)
        {
            await this.productService.DeleteRowAsync(productId);
        }

        [HttpGet]
        [Route("findAll/{pageSize:int}/{pageActual:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainNotification))] 
        public async Task<IEnumerable<ProductResponse>> Get(int pageSize, int pageActual)
        {
            return await this.productService.GetAllAsync(pageSize, pageActual);
        }
    }
}
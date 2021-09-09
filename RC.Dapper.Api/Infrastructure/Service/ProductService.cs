using Microsoft.AspNetCore.Mvc;
using RC.Dapper.Api.Core.AutoMapper;
using RC.Dapper.Api.Core.Domains;
using RC.Dapper.Api.Core.Domains.Entitie;
using RC.Dapper.Api.Core.Interface;
using RC.Dapper.Api.Core.Request;
using RC.Dapper.Api.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RC.Dapper.Api.Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task DeleteRowAsync(int id)
        {
            await this.productRepository.DeleteRowAsync(id);
        }
        
        public async Task<ProductResponse> GetAsync(int id)
        {
            var entity = await this.productRepository.GetAsync(id);
            return entity.MapTo<Product, ProductResponse>();
        }
        
        public async Task UpdateAsync(ProductUpdateRequest request)
        {
            var entity = request.MapTo<ProductUpdateRequest, Product>();
            await this.productRepository.UpdateAsync(entity);
        }
        
        public async Task InsertAsync(ProductInsertRequest request)
        {
            var entity = request.MapTo<ProductInsertRequest, Product>();
            await this.productRepository.InsertAsync(entity);
        }
        
        public async Task<int> SaveRangeAsync(IEnumerable<ProductInsertRequest> list)
        {
            var entities = list.MapTo<IEnumerable<ProductInsertRequest>, IEnumerable<Product>>();
            return await this.productRepository.SaveRangeAsync(entities);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync(int pageSize, int pageActual)
        {
            var entities = await this.productRepository.GetAllAsync(pageSize, pageActual);

            return entities.MapTo<IEnumerable<Product>, IEnumerable<ProductResponse>>();
        }
    }
}
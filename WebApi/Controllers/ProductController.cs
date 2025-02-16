using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(
            ICreateProductService createProductService,
            IDeleteProductService deleteProductService,
            IGetProductService getProductService,
            IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;

            //Guid g = Guid.NewGuid();
            //string guidString = g.ToString();
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var existingProduct = _getProductService.GetProduct(productId);
            if (existingProduct != null)
            {
                return AlreadyExist("Product");
            }

            var product = _createProductService.Create(productId, model.Name, model.Description, model.Type, model.Price);

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist("Product");
            }

            _updateProductService.Update(product, model.Name, model.Description, model.Type, model.Price, true);

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist("Product");
            }

            _deleteProductService.Delete(product);

            return Found();
        }
 
        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
                 
            if (product == null)
            {
                return DoesNotExist("Product");
            }

            return Found(new ProductData(product));
        }

        //// Note: Specifying take < 0 would return all products. It may be suitable or not depending on the requirements and database size.
        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, ProductTypes? type = null, string name = null)
        {
            var products = take < 0
                ? _getProductService.GetProducts(type, name)
                    .Skip(skip)
                    .Select(q => new ProductData(q))
                    .ToList()
                : _getProductService.GetProducts(type, name)
                    .Skip(skip).Take(take)
                    .Select(q => new ProductData(q))
                    .ToList();

            return Found(products);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllProducts()
        {
            _deleteProductService.DeleteAll();

            return Found();
        }
    }
}

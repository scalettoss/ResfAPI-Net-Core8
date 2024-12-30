using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingOrderSystem.Data.Input;
using TrackingOrderSystem.Responses;
using TrackingOrderSystem.Services;

namespace TrackingOrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getall")]

        public async Task<ActionResult> GetAllProduct()
        {
            var productList = await _productService.GetAllProductAsync();
            var successResponse = new SuccessResponse("Lấy danh sách sản phẩm thành công.", productList);
            return Ok(successResponse);
        }

        [HttpGet]
        [Route("getby/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var successResponse = new SuccessResponse("Lấy thông tin sản phẩm thành công.", product);
            return Ok(successResponse);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddProduct(ProductInput product)
        {
            await _productService.AddProductAsync(product);
            var successResponse = new SuccessResponse("Sản phẩm đã được thêm thành công.", product);
            return Ok(successResponse);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult> UpdateProduct(ProductInput product, int id)
        {
            await _productService.UpdateProductAsync(product, id);
            var successResponse = new SuccessResponse("Cập nhật thông tin sản phẩm thành công.", product);
            return Ok(successResponse);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            var successResponse = new SuccessResponse("Xóa thông tin sản phẩm thành công.");
            return Ok(successResponse);
        }
    }
}

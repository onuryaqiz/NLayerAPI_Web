using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            //var response2 = await _httpClient.GetAsync("products/GetProductsWithCategory"); // Daha önceden bu şekilde yapılıyordu . Fakat .Net 5 ile GetFromJsonASync geldi.
            //response2.Content.ReadAsStringAsync();

            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");

            return response.Data;
        }

        public async Task<ProductDto> GetByIdAsync(int id)

        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            return response.Data;

        }
        public async Task<ProductDto> SaveAsync(ProductDto newProduct)

        {

            var response = await _httpClient.PostAsJsonAsync("products", newProduct);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>(); // Başarılı , Data'yı dönüyoruz.

            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("products", newProduct);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}"); // Sileceğimiz id'yi gönderiyoruz.

            return response.IsSuccessStatusCode;
        }



    }
}

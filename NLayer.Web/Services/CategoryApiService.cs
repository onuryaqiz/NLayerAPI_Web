namespace NLayer.Web.Services
{
    public class CategoryApiService // DI Container'e eklenecek. ProductsController , Category datalarına ihtiyacı var. Katmanlı mimari kullanmayacağı için ,
                                    // CategoryApiService ve ProductApiService class'larını kullanacak.
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Hiçbir zaman httpClient'ı kendimiz üretmemeliyiz . _httpClient= new HttpClient() gibi. Bu işlemi DI Container'e bırakmalıyız.
                                      // DI Container nesne örneği vermeli. Daha az nesne örneği oluşturarak , HttpClient'ı kullanabiliriz. Scote yokluğu gibi hatalardan da korunabiliriz.
        }
    }
}

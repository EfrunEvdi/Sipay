using Restful.WebApi.Configurations;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace Restful.WebApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }

        // NotFoundException sınıfı da bir istisna sınıfıdır ve Exception sınıfından türetilmiştir. Bu sınıf, genellikle bir kaynağın bulunamaması durumunda oluşan istisna durumunu temsil etmek için kullanılır.

        // Bu sınıfın da bir kurucusu bulunur ve yine tek bir parametre olarak bir message alır.Bu parametre, istisna durumunun açıklamasını içeren bir metin veya mesajdır.Kurucu, temel Exception sınıfının kurucusunu çağırarak bu iletili istisna durumunu oluşturur.

        // Örneğin, bir istemci tarafından istenen bir kaynak bulunamadığında veya erişilemediğinde, NotFoundException kullanılabilir. Bu istisna, uygulama içinde bu tür bir hatayı belirtmek ve yakalamak için kullanılır.Daha sonra, GlobalErrorHandlingMiddleware sınıfı tarafından bu hata yakalanabilir ve uygun bir hata yanıtı döndürülebilir.
    }
}

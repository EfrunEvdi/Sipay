using Restful.WebApi.Configurations;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace Restful.WebApi.Exceptions
{
    public class UnauthorizedAccessException : Exception
    {
        public UnauthorizedAccessException(string message) : base(message)
        { }

        // UnauthorizedAccessException sınıfı da bir istisna sınıfıdır ve Exception sınıfından türetilmiştir. Bu sınıf, genellikle bir işlemin yetkilendirme gereksinimlerini karşılamadığı durumda oluşan istisna durumunu temsil etmek için kullanılır.

        // Bu sınıfın da bir kurucusu vardır ve yine tek bir parametre olarak bir message alır.Bu parametre, istisna durumunun açıklamasını içeren bir metin veya mesajdır.Kurucu, temel Exception sınıfının kurucusunu çağırarak bu iletili istisna durumunu oluşturur.

        // Örneğin, bir istemcinin belirli bir işlemi gerçekleştirmek için gerekli yetkilere sahip olmadığında veya yetkilendirme başarısız olduğunda, UnauthorizedAccessException kullanılabilir. Bu istisna, uygulama içinde bu tür bir hatayı belirtmek ve yakalamak için kullanılır.Daha sonra, GlobalErrorHandlingMiddleware sınıfı tarafından bu hata yakalanabilir ve uygun bir hata yanıtı döndürülebilir.
    }
}

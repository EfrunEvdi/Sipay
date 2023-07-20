using Restful.WebApi.Configurations;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

namespace Restful.WebApi.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }

        // BadRequestException sınıfı, özel bir istisna sınıfıdır ve Exception sınıfından türetilmiştir. Bu sınıf, genellikle HTTP talepleriyle ilgili bir istisna durumunu temsil etmek için kullanılır.

        // Bu sınıfın bir kurucusu vardır ve tek bir parametre olarak bir message alır.Bu parametre, istisna durumunun açıklamasını içeren bir metin veya mesajdir.Kurucu, temel Exception sınıfının kurucusunu çağırarak bu iletili istisna durumunu oluşturur.

        // Örneğin, bir istemci tarafından geçersiz bir istek gönderildiğinde veya işlenemeyen bir veriyle karşılaşıldığında, BadRequestException kullanılabilir. Bu istisna, uygulama içinde bu tür bir hatayı belirtmek ve yakalamak için kullanılır.Yakalanan istisna daha sonra GlobalErrorHandlingMiddleware sınıfında belirtilen hata yönetimi işlevselliği tarafından ele alınabilir ve uygun bir hata yanıtı döndürülebilir.
    }
}

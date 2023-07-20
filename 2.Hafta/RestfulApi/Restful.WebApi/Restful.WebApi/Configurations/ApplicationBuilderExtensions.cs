namespace Restful.WebApi.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();

        // Bu kod parçası, ASP.NET Core uygulamalarında hata yönetimi için bir genişletme yöntemi sağlar. AddGlobalErrorHandler yöntemi, IApplicationBuilder arayüzüne eklenen bir genişletme yöntemidir. Bu yöntem, bir GlobalErrorHandlingMiddleware ortam arabirimini kullanarak global hata yönetimi işlevselliğini uygulamaya ekler.

        // GlobalErrorHandlingMiddleware, gelen isteklerin ve çıktıların üzerinden geçen bir ara yazılım bileşenidir. Bu bileşen, uygulama seviyesinde ortaya çıkan hataları yakalar ve bunları özel bir iş mantığına göre işler. Örneğin, bir hata durumunda bir özel hata sayfası gösterebilir veya hata günlüğüne kaydedebilir.

        // Bu genişletme yöntemini kullanarak, AddGlobalErrorHandler yöntemini Startup.cs gibi başlangıç dosyalarında Configure yönteminde çağırabilirsiniz. Bu, uygulamanızın genel hata yönetimini tek bir yerden kolayca yapılandırmanıza olanak sağlar.

    }
}

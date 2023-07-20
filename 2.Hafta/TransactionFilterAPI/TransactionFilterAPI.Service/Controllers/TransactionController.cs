using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TransactionFilterAPI.Data.Domain;
using TransactionFilterAPI.Data.Repository;

namespace TransactionFilterAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }


        // API Endpoint: GET api/Transaction/GetByParameter
        // Bu API, TransactionController üzerinden çağrılır ve TransactionFilterParameters tipindeki sorgu parametresini alır.
        // TransactionFilterParameters, Transaction tablosundaki kayıtları belirli kriterlere göre filtrelemek için kullanılır.
        [HttpGet("GetByParameter")]
        public IActionResult GetByParameter([FromQuery] TransactionFilterParameters filter)
        {
            // _transactionRepository.GetByParameter fonksiyonu, veritabanında Transaction tablosunu filtrelemek için gelen kriterleri kullanır.
            // Bu fonksiyon, gelen filtreleri Transaction tablosunda uygun sorgu ifadesine dönüştürerek ilgili kayıtları getirecektir.
            var transactions = _transactionRepository.GetByParameter(filter);

            // transactions değişkenindeki sonuçlar, HTTP 200 OK cevabı ile döndürülür.
            return Ok(transactions);
        }
    }
}

# Transaction Filter API

Transaction API, belirli kriterlere göre işlemleri filtrelemek için kullanılan bir ASP.NET Core Web API projesidir.

## API Endpoint

`GET api/Transaction/GetByParameter`

Bu API, belirli filtre kriterlerine göre işlemleri sorgulamak için kullanılır.

### Filtre Kriterleri

- `AccountNumber`: Hesap numarasına göre işlemleri filtrelemek için kullanılır.
- `ReferenceNumber`: Referans numarasına göre işlemleri filtrelemek için kullanılır.
- `MinAmountCredit`: Kredi işlemleri için asgari tutarı belirlemek için kullanılır.
- `MaxAmountCredit`: Kredi işlemleri için azami tutarı belirlemek için kullanılır.
- `MinAmountDebit`: Borç işlemleri için asgari tutarı belirlemek için kullanılır.
- `MaxAmountDebit`: Borç işlemleri için azami tutarı belirlemek için kullanılır.
- `Description`: İşlem açıklamasına göre işlemleri filtrelemek için kullanılır.
- `BeginDate`: İşlem tarihinin başlangıç tarihini belirlemek için kullanılır.
- `EndDate`: İşlem tarihinin bitiş tarihini belirlemek için kullanılır.

### Örnek Kullanım

```http
GET /api/Transaction/GetByParameter?AccountNumber=100000&ReferenceNumber=1&MinAmountCredit=500&MaxAmountCredit=1000&MinAmountDebit=5000&MaxAmountDebit=7000&Description=Efrun&BeginDate=2023-07-18&EndDate=2023-07-20
```
Örnek olarak, hesap numarası 100000 olan, referans numarası 1 olan, kredi işlemleri için tutarı 500 ile 1000 arasında, borç işlemleri için tutarı 5000 ile 7000 arasında, açıklamasında "Efrun" metni bulunan ve işlem tarihi 18 Temmuz 2023 ile 20 Temmuz 2023 arasında olan işlemleri getirecektir.

### IGenericRepository
```C#
 public interface IGenericRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);
    }
```
`IGenericRepository` arayüzü, belirli bir varlık türü için veri erişim katmanını soyutlamak için kullanılır. Bu arayüz, LINQ ifadelerini temel alan sorguları yürütmek için bir `Where` fonksiyonu içerir.

### GenericRepository
```C#
  public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
    {
        private readonly TransactionFilterAPIDBContext _context;

        public GenericRepository(TransactionFilterAPIDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
        {
            return _context.Set<Entity>().Where(expression).ToList();
        }
    }
```
`GenericRepository` sınıfı, farklı varlık türleri için tekrar kullanılabilir bir veri erişim katmanıdır. IGenericRepository arayüzünden türetilir ve veritabanı bağlantısı için TransactionFilterAPIDBContext kullanır. Where metodu, LINQ ifadelerine göre veritabanında filtreleme işlemlerini gerçekleştirir.

### ITransactionRepository
```C#
 public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        IEnumerable<Transaction> GetByParameter(TransactionFilterParameters filter);
    }
```
`ITransactionRepository`, `IGenericRepository` arayüzünden türetilir ve `Transaction` varlık türü için işlem metotlarını tanımlar. `GetByParameter` metodu, belirli filtre kriterlerine göre işlemleri getirmek için kullanılır.

### TransactionRepository
```C#
public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly TransactionFilterAPIDBContext _context;

        public TransactionRepository(TransactionFilterAPIDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetByParameter(TransactionFilterParameters filter)
        {
            Expression<Func<Transaction, bool>> expression = transaction =>
                (filter.AccountNumber == null || transaction.AccountNumber == filter.AccountNumber) && // Eğer 0 olarak girilirse hesap numarasına göre filtrelemeye dahil edilmez, aksi halde hesap numarası ile eşleşen Transactionları filtreler.
                (filter.ReferenceNumber == null || transaction.ReferenceNumber == filter.ReferenceNumber) && // Girilen referans numarasına göre Transactionları filtreler.
                (filter.MinAmountCredit == null || transaction.CreditAmount >= filter.MinAmountCredit) &&
                (filter.MaxAmountCredit == null || transaction.CreditAmount <= filter.MaxAmountCredit) && // Kredi miktarı aralığına göre Transactionları filtreler.
                (filter.MinAmountDebit == null || transaction.DebitAmount >= filter.MinAmountDebit) &&
                (filter.MaxAmountDebit == null || transaction.DebitAmount <= filter.MaxAmountDebit) && // Borç miktarı aralığına göre Transactionları filtreler.
                (filter.Description == null || transaction.Description.Contains(filter.Description)) && // Girilen metni içeren Transactionları filtreler.
                (filter.BeginDate == null || transaction.TransactionDate >= filter.BeginDate) &&
                (filter.EndDate == null || transaction.TransactionDate <= filter.EndDate); // Transaction tarihini belirtilen tarih aralığına göre filtreler.


            return Where(expression);
        }
    }
```
`TransactionRepository` sınıfı, `GenericRepository` sınıfından türetilir ve `ITransactionRepository` arayüzündeki metotları uygular. `GetByParameter` metodu, filtre kriterlerine göre işlemleri veritabanında sorgular ve sonuçları döndürür.

### TransactionController
```C#
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
```
`TransactionController`, Web API'nin HTTP taleplerini yöneten ana denetleyicisidir. `ITransactionRepository` arayüzündeki `GetByParameter` metodu ile veritabanında filtreleme işlemlerini gerçekleştirir. Gelen sonuçlar HTTP 200 OK cevabıyla döndürülür.

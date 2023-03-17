using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBTest.Models;
using MongoDBTest.MongoDB;
using MongoDBTest.Services;

namespace MongoDBTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly CRUDServices<Transaction> crudServices;

        public TransactionController(CRUDServices<Transaction> crudServices)
        {
            this.crudServices = crudServices;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTransactions()
        {
            return Ok(await crudServices.LoadRecordAsync(CollectionsList.Transaction));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertTransaction(string action)
        {
            await crudServices.InsertRecordAsync(CollectionsList.Transaction, new Transaction
            {
                TransactionAction = action
            });

            return Ok(new { Success = "Insert transaction done." });
        }

    }
}

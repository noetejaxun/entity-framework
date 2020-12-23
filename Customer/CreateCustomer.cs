using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServerlessSQLDemo.Customer
{
    public class CreateCustomer
    {
        private readonly ICustomerService _customerService;

        public CreateCustomer(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [FunctionName("CreateCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var vm = JsonConvert.DeserializeObject<CustomerViewModel>(requestBody);

            var result = await _customerService.CreateAsync(vm);

            return new CreatedResult(result.Id.ToString(), result);
        }
    }
}

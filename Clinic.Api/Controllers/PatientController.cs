using System.Threading.Tasks;
using Clinic.Api.Core;
using Clinic.Api.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Patient")]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork work = null;
        public PatientController(IUnitOfWork work)
        {
            this.work = work;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resut = await work.PatientRepository.Get();
            work.Complete();
            return Ok(resut);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {            
            await work.PatientRepository.Add(patient);
            work.Complete();
            return Ok();
        }
    }
}
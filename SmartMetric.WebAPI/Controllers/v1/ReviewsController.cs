using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Core.ServicesContracts.Adders;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ReviewsController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;
        private readonly IReviewAdderService _reviewAdderService;
        private readonly ApplicationDbContext _context;

        public ReviewsController(ISmartTimeService smartTimeService, ApplicationDbContext context, IReviewAdderService reviewAdderService)
        {
            _smartTimeService = smartTimeService;
            _context = context;
            _reviewAdderService = reviewAdderService;
        }


        //TESTE de paginação
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetAllEmployees([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var totalCount = await _context.Funcionarios.CountAsync();

            var employees = await _context.Funcionarios
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Add("X-Total-Count", totalCount.ToString());

            return employees;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewDTOAddRequest request)
        {
           var response = await _reviewAdderService.AddReview(request); 
            if(response.Data != null)
            return Ok(response);

            return BadRequest(response);
        }
    }
}

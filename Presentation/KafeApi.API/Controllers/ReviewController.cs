using KafeApi.Application.Dtos.ReviewDtos;
using KafeApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllReviews();
            return CreateResponse(result);
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reviewService.GetReviewById(id);
            return CreateResponse(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto createReviewDto)
        {
            var result = await _reviewService.CreateReview(createReviewDto);
            return CreateResponse(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateReviewDto updateReviewDto)
        {
            var result = await _reviewService.UpdateReview(updateReviewDto);
            return CreateResponse(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reviewService.DeleteReview(id);
            return CreateResponse(result);
        }

    }
}

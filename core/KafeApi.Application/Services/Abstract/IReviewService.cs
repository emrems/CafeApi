using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Abstract
{
    public interface IReviewService
    {
        Task<ResponseDto<List<DetailReviewDto>>> GetAllReviews();
        Task<ResponseDto<DetailReviewDto>> GetReviewById(int id);
        Task<ResponseDto<object>> CreateReview(CreateReviewDto createReviewDto);
        Task<ResponseDto<object>> UpdateReview(UpdateReviewDto updateReviewDto);
        Task<ResponseDto<object>> DeleteReview(int id);

    }
}

using AutoMapper;
using FluentValidation;
using KafeApi.Application.Dtos.ResponseDtos;
using KafeApi.Application.Dtos.ReviewDtos;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Services.Abstract;
using KafeApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Application.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewDto> _createReviewDtoValidator;
        private readonly IValidator<UpdateReviewDto> _updateReviewDtoValidator;

        public ReviewService(IGenericRepository<Review> reviewRepository, IMapper mapper, IValidator<CreateReviewDto> createReviewDtoValidator = null, IValidator<UpdateReviewDto> updateReviewDtoValidator = null)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _createReviewDtoValidator = createReviewDtoValidator;
            _updateReviewDtoValidator = updateReviewDtoValidator;
        }

        public async Task<ResponseDto<object>> CreateReview(CreateReviewDto createReviewDto)
        {
            try
            {
                var reviewValidator = await _createReviewDtoValidator.ValidateAsync(createReviewDto);
                if (!reviewValidator.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = reviewValidator.Errors.Select(e => e.ErrorMessage).FirstOrDefault(),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }

                var review = _mapper.Map<Review>(createReviewDto);
                await _reviewRepository.AddAsync(review);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Review başarıyla oluşturuldu.",
                    Data = null,
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Hata oluştu.",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Review bulunamadı.",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _reviewRepository.DeleteAsync(review);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Review başarıyla silindi.",
                    Data = null,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Hata oluştu.",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<List<DetailReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                if (reviews == null || !reviews.Any())
                {
                    return new ResponseDto<List<DetailReviewDto>>
                    {
                        Success = false,
                        Message = "Review bulunamadı.",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }

                var reviewDtos = _mapper.Map<List<DetailReviewDto>>(reviews);
                return new ResponseDto<List<DetailReviewDto>>
                {
                    Success = true,
                    Message = "Reviewler başarıyla getirildi.",
                    Data = reviewDtos,
                    ErrorCode = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<DetailReviewDto>>
                {
                    Success = false,
                    Message = "Hata oluştu.",
                    ErrorCode = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<DetailReviewDto>> GetReviewById(int id)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<DetailReviewDto>
                    {
                        Success = false,
                        Message = "Review bulunamadı.",
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var reviewDto = _mapper.Map<DetailReviewDto>(review);
                return new ResponseDto<DetailReviewDto>
                {
                    Success = true,
                    Message = "Review başarıyla getirildi.",
                    Data = reviewDto,
                    ErrorCode = null
                };

            }
            catch (Exception)
            {

                return new ResponseDto<DetailReviewDto>
                {
                    Success = false,
                    Message = "Hata oluştu.",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto updateReviewDto)
        {
            try
            {
                var resultValidator = await _updateReviewDtoValidator.ValidateAsync(updateReviewDto);
                if (!resultValidator.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = resultValidator.Errors.Select(e => e.ErrorMessage).FirstOrDefault(),
                        ErrorCode = ErrorCodes.ValidationError,
                        Data = null
                    };
                }
                var review = _mapper.Map<Review>(updateReviewDto);
                await _reviewRepository.UpdateAsync(review);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Review başarıyla güncellendi.",
                    Data = null,
                    ErrorCode = null
                };
            }
            catch (Exception)
            {

                return new ResponseDto<object>
                {

                    Success = false,
                    Message = "Hata oluştu.",
                    ErrorCode = ErrorCodes.Exception,
                    Data = null
                };
            }
        }
    }
}

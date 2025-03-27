//using Lotto.Data.UnitOfWorks;
//using Lotto.Domain.Entities.Games;
//using Lotto.Service.Configurations;
//using Lotto.Service.Exceptions;
//using Lotto.Service.Extensions;
//using Lotto.Service.Helpers;
//using Microsoft.EntityFrameworkCore;

//public class NumberService(IUnitOfWork unitOfWork, ZipService zipService) : INumberService
//{
//    private readonly ZipService _zipService = zipService;
//    public async ValueTask<Number> CreateAsync(Number number)
//    {
//        var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
//        await unitOfWork.SaveAsync();

//        _zipService.CreateWinningNumbersArchive(createdNumber.Id, createdNumber.WinningNumbers);

//        return createdNumber;
//    }
    

//    public async ValueTask<Number> UpdateAsync(long id, Number number)
//    {
//        var existingNumber = await unitOfWork.NumberRepository
//            .SelectAsync(n => n.Id == id)
//            ?? throw new NotFoundException($"This Number is not found with ID={id}");

//        // Faqat qiymat berilgan bo‘lsa o‘zgartiramiz
//        existingNumber.Deadline = number.Deadline != default ? number.Deadline : existingNumber.Deadline;
//        existingNumber.Amount = number.Amount != default ? number.Amount : existingNumber.Amount;

//        if (DateTime.UtcNow >= existingNumber.Deadline)
//        {
//            existingNumber.IsCompleted = true;
//        }

//        var updatedNumber = await unitOfWork.NumberRepository.UpdateAsync(existingNumber);
//        await unitOfWork.SaveAsync();
//        return updatedNumber;
//    }


//    public async ValueTask<bool> DeleteAsync(long id)
//    {
//        var existingNumber = await unitOfWork.NumberRepository
//            .SelectAsync(n => n.Id == id)
//            ?? throw new NotFoundException($"This Number is not found with ID={id}");

//        await unitOfWork.NumberRepository.DeleteAsync(existingNumber);
//        await unitOfWork.SaveAsync();
//        return true;
//    }

//    public async ValueTask<Number> GetByIdAsync(long id)
//    {
//        return await unitOfWork.NumberRepository
//            .SelectAsync(n => n.Id == id)
//            ?? throw new NotFoundException($"This Number is not found with ID={id}");
//    }

//    public async ValueTask<IEnumerable<Number>> GetAllAsync(PaginationParams @params, Filter filter)
//    {
//        var numbers = unitOfWork.NumberRepository
//            .SelectAsQueryable()
//            .OrderBy(filter); 

//        // 📌 Paginatsiya
//        return await numbers
//            .ToPaginateAsQueryable(@params) 
//            .ToListAsync();
//    }

//    public async ValueTask<IEnumerable<Number>> GetAllAsync()
//    {
//        return await unitOfWork.NumberRepository.SelectAsEnumerableAsync();
//    }
//}

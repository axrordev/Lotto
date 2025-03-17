using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using System;

public class NumberService(IUnitOfWork unitOfWork) : INumberService
{
    public async ValueTask<Number> CreateAsync(Number number)
    {
        // 1️⃣ Shu `Deadline` sanasi hali kelmagan bo‘lsa, o‘yinni yaratishga ruxsat beramiz
        if (number.Deadline <= DateTime.UtcNow)
            throw new ArgumentException("Deadline must be a future date.");

        var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
        await unitOfWork.SaveAsync();

        return createdNumber;
    }


    public async ValueTask<Number> UpdateAsync(long id, Number number)
    {
        var existingNumber = await unitOfWork.NumberRepository
            .SelectAsync(n => n.Id == id)
            ?? throw new NotFoundException($"This Number is not found with ID={id}");

        existingNumber.Deadline = number.Deadline;
        existingNumber.Amount = number.Amount;

        if (DateTime.UtcNow >= existingNumber.Deadline)
        {
            existingNumber.IsCompleted = true;
        }

        var updatedNumber = await unitOfWork.NumberRepository.UpdateAsync(existingNumber);
        await unitOfWork.SaveAsync();
        return updatedNumber;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<Number> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IEnumerable<Number>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<Number>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}

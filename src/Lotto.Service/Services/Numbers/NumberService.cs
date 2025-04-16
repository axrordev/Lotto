using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


public class NumberService(IUnitOfWork unitOfWork) : INumberService
{
    public async ValueTask<Number> CreateAsync(Number number)
        {
            try
            {
                if (number.Deadline <= DateTime.UtcNow)
                    throw new ArgumentException("Deadline must be in the future.");
                if (number.Amount <= 0)
                    throw new ArgumentException("Amount must be greater than 0.");

                number.IsCompleted = false;

                var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
                await unitOfWork.SaveAsync();

                return createdNumber;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create game: {ex.Message}", ex);
            }
        }

    public async ValueTask<PlayNumber> PlayAsync(PlayNumber model)
        {
            try
            {
                var number = await unitOfWork.NumberRepository.SelectAsync(n => n.Id == model.NumberId);
                if (number == null)
                    throw new NotFoundException("Game not found.");

                if (DateTime.UtcNow > number.Deadline)
                    throw new Exception("Game is already completed. You cannot participate.");

                if (model.SelectedNumbers == null || model.SelectedNumbers.Length != 5)
                    throw new ArgumentException("You must select exactly 5 numbers.");

                foreach (var num in model.SelectedNumbers)
                {
                    if (num < 1 || num > 45)
                        throw new ArgumentException($"All numbers must be between 1 and 45. Invalid number: {num}");
                }

                if (model.SelectedNumbers.Distinct().Count() != model.SelectedNumbers.Length)
                    throw new ArgumentException("Numbers must be unique.");

                var playNumber = new PlayNumber
                {
                    UserId = model.UserId,
                    NumberId = model.NumberId,
                    SelectedNumbers = model.SelectedNumbers,
                    IsWinner = false
                };

                var createdPlayNumber = await unitOfWork.PlayNumberRepository.InsertAsync(playNumber);
                await unitOfWork.SaveAsync();

                return createdPlayNumber;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to play game: {ex.Message}", ex);
            }
        }

    public async ValueTask<List<PlayNumber>> GetUserPlaysAsync(long userId)
        {
            try
            {
                var plays = await unitOfWork.PlayNumberRepository.SelectAsEnumerableAsync(
                    pn => pn.UserId == userId,
                    new[] { nameof(PlayNumber.Number) });

                return plays.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user plays: {ex.Message}", ex);
            }
        }

    public async ValueTask SetWinningNumbersAsync(long numberId, int[] winningNumbers)
        {
            try
            {
                var number = await unitOfWork.NumberRepository.SelectAsync(n => n.Id == numberId);
                if (number == null)
                    throw new NotFoundException("Game not found.");

                if (DateTime.UtcNow < number.Deadline)
                    throw new Exception("Game is not yet completed. Please wait until the deadline.");

                if (number.IsCompleted)
                    throw new Exception("Winning numbers have already been set for this game.");

                if (winningNumbers == null || winningNumbers.Length != 5)
                    throw new ArgumentException("You must provide exactly 5 winning numbers.");

                foreach (var num in winningNumbers)
                {
                    if (num < 1 || num > 45)
                        throw new ArgumentException($"All winning numbers must be between 1 and 45. Invalid number: {num}");
                }

                if (winningNumbers.Distinct().Count() != winningNumbers.Length)
                    throw new ArgumentException("Winning numbers must be unique.");

                number.WinningNumbers = winningNumbers;
                number.IsCompleted = true;

                await unitOfWork.NumberRepository.UpdateAsync(number);
                await unitOfWork.SaveAsync();

                var playNumbers = await unitOfWork.PlayNumberRepository.SelectAsEnumerableAsync(
                    pn => pn.NumberId == numberId);

                foreach (var playNumber in playNumbers)
                {
                    playNumber.IsWinner = playNumber.SelectedNumbers.SequenceEqual(winningNumbers);
                    await unitOfWork.PlayNumberRepository.UpdateAsync(playNumber);
                }

                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error setting winning numbers: {ex.Message}", ex);
            }
        }

    public async ValueTask<Number> UpdateAsync(long id, Number number)
    {
        var existingNumber = await unitOfWork.NumberRepository
            .SelectAsync(n => n.Id == id)
            ?? throw new NotFoundException($"This Number is not found with ID={id}");

        // Faqat qiymat berilgan bo‘lsa o‘zgartiramiz
        existingNumber.Deadline = number.Deadline != default ? number.Deadline : existingNumber.Deadline;
        existingNumber.Amount = number.Amount != default ? number.Amount : existingNumber.Amount;

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
        var existingNumber = await unitOfWork.NumberRepository
            .SelectAsync(n => n.Id == id)
            ?? throw new NotFoundException($"This Number is not found with ID={id}");

        await unitOfWork.NumberRepository.DeleteAsync(existingNumber);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Number> GetByIdAsync(long id)
    {
        return await unitOfWork.NumberRepository
            .SelectAsync(n => n.Id == id)
            ?? throw new NotFoundException($"This Number is not found with ID={id}");
    }

    public async ValueTask<IEnumerable<Number>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var numbers = unitOfWork.NumberRepository
            .SelectAsQueryable()
            .OrderBy(filter);

        // 📌 Paginatsiya
        return await numbers
            .ToPaginateAsQueryable(@params)
            .ToListAsync();
    }

    public async ValueTask<IEnumerable<Number>> GetAllAsync()
    {
        return await unitOfWork.NumberRepository.SelectAsEnumerableAsync();
    }
}

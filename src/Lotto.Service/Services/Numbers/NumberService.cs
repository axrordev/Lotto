using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Helpers;
using Microsoft.EntityFrameworkCore;


public class NumberService(IUnitOfWork unitOfWork) : INumberService
{
    private readonly string wwwRootPath;
    //public async ValueTask<Number> CreateAsync(Number number)
    //{
    //    // Create the number
    //    var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
    //    await unitOfWork.SaveAsync();

    //    // Generate the zip file
    //    string zipPath = ZipService.CreateWinningNumbersArchive(createdNumber.Id, createdNumber.WinningNumbers);

    //    // Create an announcement
    //    var announcement = new Announcement
    //    {
    //        Title = "New Winning Numbers Available",
    //        Message = $"The winning numbers for game {createdNumber.Id} are available for download. [Download Here]({zipPath})",
    //        ExpiryDate = DateTime.UtcNow.AddDays(7), // Set expiry date for the announcement
    //        IsActive = true
    //    };

    //    await unitOfWork.AnnouncementRepository.InsertAsync(announcement);
    //    await unitOfWork.SaveAsync();

    //    return createdNumber;
    //}
    
    public async ValueTask<Number> CreateAsync(Number number, int[] winningNumbers)
    {
        //try
        //{
        //    // WinningNumbers ni tekshirish
        //    if (winningNumbers == null || winningNumbers.Length != 5)
        //        throw new ArgumentException("Winning numbers must contain exactly 5 numbers.");

        //    var number = new Number
        //    {
        //        Deadline = model.Deadline,
        //        Amount = model.Amount,
        //        IsCompleted = false
        //    };

        //    // WinningNumbers ni hash qilish
        //    number.WinningNumbersHash = HashService.GenerateHash(winningNumbers);

        //    // WinningNumbers ni shifrlash
        //    number.EncryptedWinningNumbers = EncryptionService.Encrypt(winningNumbers);

        //    // Number ni saqlash
        //    var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
        //    await unitOfWork.SaveAsync();

        //    // E'lon yaratish
        //    var announcement = new Announcement
        //    {
        //        Title = "New Game Created",
        //        Message = $"Game {createdNumber.Id} hash: {createdNumber.WinningNumbersHash}. Deadline: {createdNumber.Deadline}.",
        //        ExpiryDate = DateTime.UtcNow.AddDays(7),
        //        IsActive = true
        //    };

        //    await unitOfWork.AnnouncementRepository.InsertAsync(announcement);
        //    await unitOfWork.SaveAsync();

        //    return createdNumber;
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception($"Failed to create number: {ex.Message}", ex);
        //}
         number.WinningNumbersHash = HashService.GenerateHash(winningNumbers);
     number.EncryptedWinningNumbers = EncryptionService.Encrypt(winningNumbers);
     var createdNumber = await unitOfWork.NumberRepository.InsertAsync(number);
     await unitOfWork.SaveAsync();

     var announcement = new Announcement
     {
         Title = "New Game Created",
         Message = $"Game {createdNumber.Id} hash: {createdNumber.WinningNumbersHash}. Deadline: {createdNumber.Deadline}.",
         ExpiryDate = DateTime.UtcNow.AddDays(7),
         IsActive = true
     };
     await unitOfWork.AnnouncementRepository.InsertAsync(announcement);
     await unitOfWork.SaveAsync();

     return createdNumber;
    }

    public async ValueTask<PlayNumber> PlayAsync(PlayNumber model)
    {
        try
        {
            // Number ni tekshirish
            var number = await unitOfWork.NumberRepository.SelectAsync(n => n.Id == model.NumberId);
            if (number == null)
                throw new NotFoundException("Game not found.");

            if (DateTime.UtcNow > number.Deadline)
                throw new Exception("Game is already completed. You cannot participate.");

            // SelectedNumbers ni tekshirish
            if (model.SelectedNumbers == null || model.SelectedNumbers.Length != 5)
                throw new ArgumentException("You must select exactly 5 numbers.");


            var playNumber = new PlayNumber
            {
                UserId = model.UserId,
                NumberId = model.NumberId,
                SelectedNumbers = model.SelectedNumbers,
                IsWinner = false
            };

            // PlayNumber ni saqlash
            var createdPlayNumber = await unitOfWork.PlayNumberRepository.InsertAsync(playNumber);
            await unitOfWork.SaveAsync();

            return createdPlayNumber;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to play game: {ex.Message}", ex);
        }
    }

    public async ValueTask AnnounceResultsAsync(long numberId)
    {
        try
        {
            // Number ni olish
            var number = await unitOfWork.NumberRepository.SelectAsync(n => n.Id == numberId);
            if (number == null)
                throw new NotFoundException("Game not found.");

            // Deadline ni tekshirish
            if (DateTime.UtcNow < number.Deadline)
                throw new Exception("Game is not yet completed. Please wait until the deadline.");

            // IsCompleted ni yangilash
            if (!number.IsCompleted)
            {
                number.IsCompleted = true;
                await unitOfWork.NumberRepository.UpdateAsync(number);
                await unitOfWork.SaveAsync();
            }

            // Shifrlangan WinningNumbers ni ochish
            var winningNumbers = EncryptionService.Decrypt(number.EncryptedWinningNumbers);

            // Hash ni tekshirish (qo'shimcha xavfsizlik uchun)
            if (!HashService.VerifyHash(winningNumbers, number.WinningNumbersHash))
                throw new Exception("Decrypted winning numbers do not match the original hash.");

            // Barcha PlayNumber larni olish va IsWinner ni yangilash
            var playNumbers = await unitOfWork.PlayNumberRepository.SelectAsEnumerableAsync(
                pn => pn.NumberId == numberId);

            foreach (var playNumber in playNumbers)
            {
                playNumber.IsWinner = playNumber.SelectedNumbers.SequenceEqual(winningNumbers);
                await unitOfWork.PlayNumberRepository.UpdateAsync(playNumber);
            }

            await unitOfWork.SaveAsync();

            // Natijani e'lon qilish
            var winners = playNumbers.Where(pn => pn.IsWinner).Select(pn => pn.UserId).ToList();
            var winnersMessage = winners.Any()
                ? $"Winners: {string.Join(", ", winners)}"
                : "No winners this time.";

            var announcement = new Announcement
            {
                Title = $"Game {numberId} Results",
                Message = $"Winning numbers: {string.Join(", ", winningNumbers)}. {winnersMessage}",
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsActive = true
            };

            await unitOfWork.AnnouncementRepository.InsertAsync(announcement);
            await unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error announcing results: {ex.Message}", ex);
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

using FreshVeggies.Api.Domain.Models;
using FreshVeggies.Api.Persistence;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreshVeggies.Api.Services
{
    public class UserService(
        ApplicationDbContext context,
        IPasswordHasher<User> passwordHasher
        ) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public async Task<ApiResult> CreateAddressAsync(AddressDto addressDto, int userId)
        {
            Address? address = null;
            if (addressDto.Id == 0)
            {
                address = new Address
                {
                    AddressName = addressDto.Address,
                    FullAddress = addressDto.Name,
                    IsDefault = addressDto.IsDefault,
                    UserId = userId
                };

                _context.Addresses.Add(address);
            }
            else
            {
                address = await _context.Addresses.FindAsync(addressDto.Id);
                if (address is null)
                {
                    return ApiResult.Failure("Address not found");
                }
                address.FullAddress = addressDto.Address;
                address.AddressName = addressDto.Name;
                address.IsDefault = addressDto.IsDefault;

                _context.Addresses.Update(address);
            }

            try
            {
                // Check if the address is set as default and if so, set other addresses to not default
                if (addressDto.IsDefault)
                {
                    var defaultAddress = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault && a.Id != addressDto.Id);

                    if (defaultAddress is not null)
                    {
                        defaultAddress.IsDefault = false;
                    }
                }

                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Failure($"An error occurred while creating the address: {ex.Message}");

            }
        }

        public async Task<AddressDto[]> GetUserAddressesAsync(int userId)
        {
            // Get all addresses for the user
            var addresses = await _context.Addresses
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Select(a => new AddressDto
                {
                    Id = a.Id,
                    Address = a.FullAddress,
                    Name = a.AddressName,
                    IsDefault = a.IsDefault
                })
                .ToArrayAsync();
            return addresses;
        }

        // add a change password method
        public async Task<ApiResult> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user is null)
                {
                    return ApiResult.Failure("User not found");
                }

                var verifyPassword = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, changePasswordDto.CurrerntPassword);

                if (verifyPassword != PasswordVerificationResult.Success)
                {
                    return ApiResult.Failure("Current password is incorrect");
                }

                // Generate new password hash
                user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Failure($"An error occurred while changing the password: {ex.Message}");
            }
        }
    }
}

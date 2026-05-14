using System;
using System.Collections.Generic;
using System.Linq;
using Portfolio_Tracker.Models;

namespace Portfolio_Tracker.Services
{
    public static class AuthService
    {
        private const string UsersPath = "Data/users.json";

        public static (bool ok, string message, User user) Register(string username, string password, string fullName)
        {
            username = (username ?? string.Empty).Trim();
            fullName = (fullName ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(username))
                return (false, "Username is required.", null);
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return (false, "Password must be at least 6 characters.", null);

            var users = JsonService.LoadUsers<List<User>>() ?? new List<User>();

            if (users.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)))
                return (false, "Username already exists.", null);

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                PasswordHash = hash,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow,
                IsGuest = false
            };

            users.Add(user);
            JsonService.SaveUsers(users);

            return (true, "Registered", user);
        }

        public static (bool ok, string message, User user) Login(string username, string password)
        {
            username = (username ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return (false, "Enter username and password.", null);

            var users = JsonService.LoadUsers<List<User>>() ?? new List<User>();

            var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                return (false, "Invalid credentials.", null);

            try
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                    return (true, "OK", user);
                else
                    return (false, "Invalid credentials.", null);
            }
            catch
            {
                return (false, "Invalid credentials.", null);
            }
        }

        public static User Guest()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "guest",
                FullName = "Guest",
                IsGuest = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
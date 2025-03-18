using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    /// <returns>Returns the first(earliest) desktop Session</returns>
    public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions.Where(session => session.DeviceType == DeviceType.Desktop).OrderBy(session => session.StartedAtUTC).FirstAsync();
    }
    
    /// <returns>Returns only Sessions from Active users that were ended before 2025</returns>
    public async Task<List<Session>> GetSessionsAsync()
    {
        return await _dbContext.Sessions.Where(session=>session.EndedAtUTC.Year<2025&&session.User.Status==UserStatus.Active).ToListAsync();
    }
}

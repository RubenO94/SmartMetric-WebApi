using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class SmartTimeRepository : ISmartTimeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SmartTimeRepository> _logger;

        public SmartTimeRepository(ApplicationDbContext context, ILogger<SmartTimeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Funcionario>> GetAllEmployees()
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllEmployees)} foi iniciado");

            return await _context.Funcionarios.ToListAsync();
        }

        public async Task<Utilizador?> GetUser(int userId)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetUser)} foi iniciado");

            return await _context.Utilizadores.FirstOrDefaultAsync(temp => temp.Idutilizador == userId);
        }

        public async Task<Utilizador?> UpdateUser(Utilizador utilizador)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(UpdateUser)} foi iniciado");

            var user = await _context.Utilizadores.FindAsync(utilizador.Idutilizador);

            if (user == null)
            {
                return null;
            }

            user.RefreshTokenExpiration = utilizador.RefreshTokenExpiration;
            user.RefreshToken = utilizador.RefreshToken;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}

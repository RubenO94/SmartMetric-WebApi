using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;

namespace SmartMetric.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<QuestionRepository> _logger;

        public QuestionRepository(ApplicationDbContext dbContext, ILogger<QuestionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<Question> AddQuestion(Question question)
        {
            _logger.LogInformation($"{nameof(QuestionRepository)}.{nameof(AddQuestion)} foi iniciado");

            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }

        #endregion

        #region Getters

        public async Task<List<Question>> GetAllQuestion()
        {
            _logger.LogInformation($"{nameof(QuestionRepository)}.{nameof(GetAllQuestion)} foi iniciado");
            return await _dbContext.Questions.ToListAsync();
        }

        public async Task<List<Question>?> GetQuestionByFormTemplateId(Guid formTemplateId)
        {
            _logger.LogInformation($"{nameof(QuestionRepository)}.{nameof(GetQuestionByFormTemplateId)} foi iniciado");

            return await _dbContext.Questions
                .Include(temp => temp.Translations)
                .Include(temp => temp.SingleChoiceOptions)!.ThenInclude(temp =>  temp.Translations)
                .Include(temp => temp.RatingOptions)!.ThenInclude(temp => temp.Translations)
                .Where(temp => temp.FormTemplateId == formTemplateId).ToListAsync();
        }

        public async Task<Question?> GetQuestionById(Guid questionId)
        {
            _logger.LogInformation($"{nameof(QuestionRepository)}.{nameof(GetQuestionById)} foi iniciado");

            return await _dbContext.Questions.Include(temp => temp.Translations).FirstOrDefaultAsync(trans => trans.QuestionId == questionId);
        }

        #endregion
    }
}

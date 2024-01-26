using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface IQuestionRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona uma questão na base de dados
        /// </summary>
        /// <param name="question"></param>
        /// <returns>Retorna um objeto do tipo Question, após este ser inserido com sucesso na base de dados</returns>
        Task<Question> AddQuestion(Question question);

        #endregion

        #region Getters

        /// <summary>
        /// Função que retorna todas as questões que existem na base de dados
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo Question, caso não exista nenhum objeto retorna uma lista vazia</returns>
        Task<List<Question>> GetAllQuestion();

        /// <summary>
        /// Função que procura através do Id passado por parâmetro, uma questão associada a este Id na base de dados 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna um objeto do tipo Question</returns>
        Task<Question?> GetQuestionById(Guid? questionId);

        /// <summary>
        /// Função que procura através do Id passado por parâmetro, todas as questões associadas ao Template requerido por parâmetro, na base de dados
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <returns>Retorna uma lista de objetos do tipo Question</returns>
        Task<List<Question>?> GetQuestionByFormTemplateId(Guid formTemplateId);

        /// <summary>
        /// Função que procura através do Id passado pro parâmetro, todas as questões associadas à revisão requerido por parâmetro
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        Task<List<Question>?> GetQuestionByReviewId(Guid reviewId);

        #endregion
    }
}

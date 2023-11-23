using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface que define operações de acesso a dados para submissões de avaliações.
    /// </summary>
    public interface ISubmissionRepository
    {
        /// <summary>
        /// Obtém uma lista paginada de todas as submissões.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Número de itens por página.</param>
        /// <returns>Uma lista paginada de submissões.</returns>
        Task<List<Submission>> GetAllSubmissions(int page = 1, int pageSize = 20);

        /// <summary>
        /// Obtém uma lista de submissões associadas a uma revisão específica.
        /// </summary>
        /// <param name="reviewId">Identificador único da revisão.</param>
        /// <returns>Uma lista de submissões associadas à revisão.</returns>
        Task<List<Submission>> GetAllSubmissionsByReviewId(Guid reviewId);

        /// <summary>
        /// Obtém uma submissão com base no seu identificador único.
        /// </summary>
        /// <param name="submissionId">Identificador único da submissão.</param>
        /// <returns>A submissão correspondente ao identificador único fornecido.</returns>
        Task<Submission?> GetSubmissionById(Guid submissionId);

        /// <summary>
        /// Adiciona uma nova submissão.
        /// </summary>
        /// <param name="submission">A submissão a ser adicionada.</param>
        /// <returns>Um valor indicando se a adição foi bem-sucedida.</returns>
        Task<bool> AddSubmission(Submission submission);
    }

}

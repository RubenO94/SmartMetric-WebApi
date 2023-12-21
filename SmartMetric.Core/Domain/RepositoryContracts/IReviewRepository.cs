using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface que define as operações de leitura e escrita para revisões.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Obtém todas as revisões com suporte para paginação.
        /// </summary>
        /// <param name="page">Número da página a ser recuperada.</param>
        /// <param name="pageSize">Número de revisões por página.</param>
        /// <returns>Uma lista de revisões.</returns>
        Task<List<Review>> GetAllReviews(int page = 1, int pageSize = 20, string? language = null);

        /// <summary>
        /// Obtém uma revisão com base no seu identificador único.
        /// </summary>
        /// <param name="reviewId">Identificador único da revisão.</param>
        /// <returns>A revisão correspondente ao identificador fornecido.</returns>
        Task<Review?> GetReviewById(Guid reviewId);

        /// <summary>
        /// Adiciona uma nova revisão.
        /// </summary>
        /// <param name="review">A revisão a ser adicionada.</param>
        /// <returns>True se a adição for bem-sucedida; False, caso contrário.</returns>
        Task<bool> AddReview(Review review);

        /// <summary>
        /// Remove uma revisão com base no seu identificador único.
        /// </summary>
        /// <param name="reviewId">Identificador único da revisão a ser removida.</param>
        /// <returns>True se a remoção for bem-sucedida; False, caso contrário.</returns>
        Task<bool> DeleteReview(Guid reviewId);

        /// <summary>
        /// Atualiza uma revisão existente.
        /// </summary>
        /// <param name="review">A revisão atualizada.</param>
        /// <returns>True se a atualização for bem-sucedida; False, caso contrário.</returns>
        Task<bool> UpdateReview(Review review);

        Task<int> GetTotalRecords(Expression<Func<Review, bool>>? filter = null);

        Task<bool> UpdateReviewStatus(Guid reviewId, ReviewDTOUpdateStatus review);
    }

}

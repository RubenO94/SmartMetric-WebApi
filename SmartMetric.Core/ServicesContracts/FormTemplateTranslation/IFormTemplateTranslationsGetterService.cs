﻿using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.FormTemplateTranslations
{
    /// <summary>
    /// Representa a lógica de negócio para a obtenção de traduções de modelos de formulário.
    /// </summary>
    public interface IFormTemplateTranslationsGetterService
    {
        /// <summary>
        /// Obtém todas as traduções dos modelos de formulário.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="TranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<TranslationDTOResponse>>> GetAllFormTemplateTranslations();

        /// <summary>
        /// Obtém uma tradução de um modelo de formulário com base no ID da tradução fornecido.
        /// </summary>
        /// <param name="formTemplateTranslationId">ID da tradução para pesquisa.</param>
        /// <returns>A tradução correspondente, ou null se não encontrada.</returns>
        Task<ApiResponse<TranslationDTOResponse?>> GetFormTemplateTranslationById(Guid? formTemplateTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID do modelo de formulário fornecido.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário a ser pesquisado.</param>
        /// <returns>Uma lista de objetos <see cref="TranslationDTOResponse"/> ou null se não houver traduções.</returns>
        Task<ApiResponse<List<TranslationDTOResponse>?>> GetTranslationsByFormTemplateId(Guid? formTemplateId);
    }


}


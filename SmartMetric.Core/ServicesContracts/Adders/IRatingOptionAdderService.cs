﻿using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    /// <summary>
    /// Define o serviço para adição de opções de resposta de classificação.
    /// </summary>
    public interface IRatingOptionAdderService
    {
        /// <summary>
        /// Adiciona uma opção de resposta de classificação a uma questão e numa nova linha na tabela RatingOption.
        /// </summary>
        /// <param name="request">Os detalhes da opção de resposta de classificação a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto RatingOptionDTOResponse em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<RatingOptionDTOResponse?>> AddRatingOption(Guid? questionId, RatingOptionDTOAddRequest? request);
    }

}

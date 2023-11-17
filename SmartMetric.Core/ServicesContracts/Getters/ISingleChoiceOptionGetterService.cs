﻿using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    public interface ISingleChoiceOptionGetterService
    {
        /// <summary>
        /// Procura todas as opções de resposta de escolhaúnica de todas as questões
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOptionDTOResponse</returns>
        Task<List<SingleChoiceOptionDTOResponse>> GetAllSingleChoiceOption();

        /// <summary>
        /// Procura por uma opção de resposta de escolha única através do seu Id passado por parâmetro
        /// </summary>
        /// <param name="singleChoiceOptionId"></param>
        /// <returns>Retorna um objeto do tipo SingleChoiceOptionDTOResponse</returns>
        Task<SingleChoiceOptionDTOResponse?> GetSingleChoiceOptionById(Guid? singleChoiceOptionId);

        /// <summary>
        /// Procura por todas as opções de resposta de escolha unica associadas à questão pretendida
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOptionDTOResponse</returns>
        Task<List<SingleChoiceOptionDTOResponse>?> GetSingleChoiceOptionByQuestionId (Guid? questionId);
    }
}
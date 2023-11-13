﻿using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de modelos de formulário.
    /// </summary>
    public interface IFormTemplatesGetterService
    {
        /// <summary>
        /// Obtém todos os modelos de formulário existentes.
        /// </summary>
        /// <returns>Uma lista de objetos FormTemplateDTOResponse, ou null se nenhum modelo de formulário estiver disponível.</returns>
        Task<List<FormTemplateDTOResponse?>> GetAllFormTemplates();

        /// <summary>
        /// Obtém um modelo de formulário com base no seu identificador único (GUID).
        /// </summary>
        /// <param name="formTemplateId">O identificador único (GUID) do modelo de formulário desejado.</param>
        /// <returns>O objeto FormTemplateDTOResponse correspondente ao identificador fornecido, ou null se nenhum modelo for encontrado.</returns>
        Task<FormTemplateDTOResponse?> GetFormTemplateById(Guid formTemplateId);
    }

}
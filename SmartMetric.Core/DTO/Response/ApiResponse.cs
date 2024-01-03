using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    /// <summary>
    /// Representa uma resposta padronizada para operações da API, contendo um status code, uma mensagem e dados associados.
    /// </summary>
    /// <typeparam name="T">O tipo de objeto associado à resposta.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Obtém ou define o status code da resposta.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Obtém ou define a mensagem descritiva da resposta.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Obtém ou define o total de registos da entidade associada, caso esta seja em lista.
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// Obtém ou define os dados associados à resposta.
        /// </summary>
        public T? Data { get; set; }

    }

}

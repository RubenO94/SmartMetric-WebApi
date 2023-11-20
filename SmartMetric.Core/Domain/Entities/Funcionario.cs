using System;
using System.Collections.Generic;

namespace SmartMetric.Core.Domain.Entities;

public partial class Funcionario
{
    public int Idfuncionario { get; set; }

    public string? Numero { get; set; }

    public string? Nome { get; set; }

    public bool? Activo { get; set; }

    public string? Morada { get; set; }

    public string? Morada2 { get; set; }

    public string? CodigoPostal { get; set; }

    public string? Localidade { get; set; }

    public int? Iddepartamento { get; set; }

    public string? Telefone { get; set; }

    public string? Telemovel { get; set; }

    public byte[]? Fotografia { get; set; }

    public string? Email { get; set; }

    public string? Notas { get; set; }

    public string? Cartao { get; set; }

    public string? CartaoAlternativo { get; set; }

    public string? Pin { get; set; }

    public DateTime? DataAdmissao { get; set; }

    public DateTime? DataDemissao { get; set; }

    public DateTime? DataNascimento { get; set; }

    public string? CentroCusto { get; set; }

    public string? NomeAbreviado { get; set; }

    public string? Password { get; set; }

    public int? IdplanoHorarios { get; set; }

    public int? Idmunicipio { get; set; }

    public DateTime? HorasSemanais { get; set; }

    public int? PerfilAcesso { get; set; }

    public int? Identidade { get; set; }

    public bool? AdministradorTerminais { get; set; }

    public bool? AcessoPortalWeb { get; set; }

    public string? LoginLdap { get; set; }

    public DateTime? UltimoAcessoSmartTime { get; set; }

    public bool? SuperiorHierarquico { get; set; }

    public int? Idperfil { get; set; }

    public int? NumeroFilhos { get; set; }

    public decimal? ValorHora { get; set; }

    public decimal? ValorPremioAssiduidade { get; set; }

    public string? SubAlimentacao { get; set; }

    public bool? NaoExportar { get; set; }

    public decimal? ValorBase { get; set; }

    public bool? NaoGerarSubTurno { get; set; }

    public DateTime? HorasMensais { get; set; }

    public string? DeviceId { get; set; }

    public int? Idgrupo { get; set; }

    public bool? MarcarSemBiometria { get; set; }

    public decimal? ValorSubsidio { get; set; }

    public bool? CstFullControl { get; set; }

    public DateTime? DataExpiracao { get; set; }

    public string? Pinapk { get; set; }

    public string? Pinapp { get; set; }

    public string? Local { get; set; }

    public bool? PermitirFeriasAnoAnterior { get; set; }

    public bool? Alarme { get; set; }

    public int? TentativasAcesso { get; set; }

    public bool? Gdpr { get; set; }

    public string? Idioma { get; set; }

    public bool? IgnorarPrazoAntecedenciaExtras { get; set; }

    public bool? PrimeiroAcesso { get; set; }

    public bool? SuperiorHierarquicoPorDelegacao { get; set; }

    public bool? SoAcessos { get; set; }

    public string? EmailEquipa { get; set; }

    public int? IdperfilEquipa { get; set; }

    public int? IdperfilEquipaAcesso { get; set; }

    public decimal? ValorDiario { get; set; }

    public bool? Temporario { get; set; }

    public string? CartaoAlternativo1 { get; set; }

    public string? Sexo { get; set; }

    public string? EstadoCivil { get; set; }

    public byte[]? Assinatura { get; set; }

    public int? IdperfilSuperiorHierarquico { get; set; }

    public int? PlanosHorariosLinha { get; set; }

    public bool? OcultarBh { get; set; }

    public bool? NaoGerarSubAlimentacao { get; set; }

    public bool? EnviarOffline { get; set; }

    public decimal? ValorHoraFds { get; set; }

    public bool? NaoValidarDocumentos { get; set; }

    public bool? IgnorarLimites { get; set; }

    public string? Folha { get; set; }

    public bool? CartaConducao { get; set; }

    public Guid? IdpersonApp { get; set; }

    public bool? Seguranca { get; set; }

    public bool? Visitado { get; set; }
}

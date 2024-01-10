/// <summary>
/// Enumeração que representa os tipos de permissões numa janela do módulo SmartMetric.
/// </summary>
public enum PermissionType
{
    /// <summary>
    /// Permissão para criar (criar novos itens ou registos).
    /// </summary>
    Create = 1,

    /// <summary>
    /// Permissão para ler (visualizar informações existentes).
    /// </summary>
    Read,

    /// <summary>
    /// Permissão para atualizar (modificar informações existentes).
    /// </summary>
    Update,

    /// <summary>
    /// Permissão para excluir (remover itens ou registos existentes).
    /// </summary>
    Delete,

    /// <summary>
    /// Permissão para aplicar atualizações parciais (patch) em informações existentes.
    /// </summary>
    Patch
}

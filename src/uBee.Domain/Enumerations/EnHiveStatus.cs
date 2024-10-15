namespace uBee.Domain.Enumerations
{
    public enum EnHiveStatus
    {
        Available,          // Disponível para aluguel
        InUse,              // Atualmente em uso por um agricultor
        UnderMaintenance,   // Em manutenção e não disponível
        Decommissioned      // Desativada ou removida do sistema
    }
}

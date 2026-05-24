using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Accounting
{
    /// <summary>
    /// Datos crudos de mecánico + configuración de pago, sin procesamiento de negocio.
    /// El servicio construye el DTO final a partir de este.
    /// </summary>
    public class MechanicWithSettingsRawDto
    {
        public int SettingId { get; set; }
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = string.Empty;
        public string? PaymentType { get; set; }
        public decimal? Value { get; set; }
    }

    /// <summary>
    /// Datos crudos de órdenes de trabajo para el cálculo de resumen de ventas.
    /// </summary>
    public class WorkOrderSalesRawDto
    {
        public int WorkOrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal DownPayment { get; set; }
        public List<WorkOrderPartRawDto> Parts { get; set; } = new();
        public List<WorkOrderServiceRawDto> Services { get; set; } = new();
    }

    public class WorkOrderPartRawDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class WorkOrderServiceRawDto
    {
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Datos crudos de servicio pendiente de pago a mecánico, sin comisión calculada.
    /// El servicio aplica la lógica de cálculo según el tipo de pago.
    /// </summary>
    public class PendingServiceRawDto
    {
        public int ServiceId { get; set; }
        public int WorkOrderId { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } = string.Empty;
        public decimal ServicePrice { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string PaymentType { get; set; } = "Porcentaje";
        public decimal ConfiguredValue { get; set; }
    }
}

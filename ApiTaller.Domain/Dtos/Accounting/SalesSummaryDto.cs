using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class SalesSummaryDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalServices { get; set; }
        public decimal TotalParts { get; set; }
        public decimal TotalDownPayments { get; set; }
        public int OrdersCount { get; set; }

        // Nuevos campos de refinamiento
        public decimal NetProfit { get; set; } // Ganancia neta real (Mano de obra neta + Repuestos netos)
        public decimal InStockPartsSales { get; set; } // Repuestos de inventario (con stock)
        public decimal OutOfStockPartsSales { get; set; } // Repuestos bajo pedido (sin stock)
        public decimal ExternalQuotesSales { get; set; } // Cotizaciones externas
        public decimal PartsCost { get; set; } // Costo total de adquisición
        public decimal PartsNetProfit { get; set; } // Ganancia neta en repuestos (Venta - Costo)
        public decimal MechanicPayout { get; set; } // Pago / Comisiones estimadas a mecánicos
        public decimal LaborNetProfit { get; set; } // Ganancia neta de mano de obra (Servicios - Comisión)
        public decimal MotoSales { get; set; }
        public decimal CarSales { get; set; }

        // Control de Caja vs Bancos / Transferencias
        public decimal CashSales { get; set; }
        public decimal BankTransferSales { get; set; }
        public Dictionary<string, decimal> SalesByPaymentMethod { get; set; } = new Dictionary<string, decimal>();
    }
}

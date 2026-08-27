namespace ApiTaller.Domain.Constants
{
    public static class ModuleConstants
    {
        // Módulos de Operación y Negocio
        public const string Customers = "Clientes";
        public const string Vehicles = "Vehiculos";
        public const string WorkOrders = "Ordenes Trabajo";
        public const string Quotations = "Cotizaciones";
        public const string Pos = "Punto de Venta";
        public const string Inventory = "Inventario";
        public const string Agenda = "Agenda";
        public const string Accounting = "Contabilidad";

        // Módulos de Catálogos y Configuración del Taller
        public const string Brands = "Marcas";
        public const string Models = "Modelos";
        public const string References = "Referencias";
        public const string Cylinders = "Cilindros";
        public const string ProductTypes = "Tipos Productos";
        public const string Products = "Productos";
        public const string Units = "Unidades";
        public const string PaymentMethods = "Metodos Pago";
        public const string Suppliers = "Proveedores";
        public const string ServiceTypes = "Tipos Servicio";
        public const string ServiceCatalogs = "Catalogos Servicio";
        public const string ServicePrices = "Precios Servicio";
        public const string WorkshopLogo = "Logo del Taller";
        public const string EmailSettings = "Envío Correo";
        public const string CustomerPortal = "Portal Cliente";
        public const string ControlCenter = "Centro de Control";

        // Módulos Exclusivos de Plataforma / SuperAdmin (no seleccionables por taller)
        public const string Roles = "Roles";
        public const string RoleConfiguration = "Configuracion Roles";
        public const string Modules = "Modulos";
        public const string Operations = "Operaciones";
        public const string Actions = "Acciones";
        public const string Users = "Usuarios";
        public const string IdentificationTypes = "Tipos Identificacion";
        public const string VehicleMode = "Modo Vehicular";
        public const string SaasManagement = "Gestión SaaS";

        public static readonly string[] SuperAdminReservedModules = new[]
        {
            Roles,
            RoleConfiguration,
            Modules,
            Operations,
            Actions,
            IdentificationTypes,
            VehicleMode,
            SaasManagement
        };
    }
}

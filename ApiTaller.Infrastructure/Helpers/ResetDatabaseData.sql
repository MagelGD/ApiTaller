-- ==============================================================================
-- SCRIPT DE RESETEO TOTAL Y LIMPIEZA ABSOLUTA DE BASE DE DATOS (DROP ALL TABLES)
-- Base de Datos: db_acd660_tallerpwa (TallerMotoCar Multi-Tenant SaaS)
-- Propósito: Eliminar el 100% de las tablas existentes SIN borrar la base de datos.
--            Cada sentencia DROP TABLE es individual para evitar el Error 1066 (Not unique table/alias).
--            Al terminar, la base de datos queda con 0 tablas y todos los AUTO_INCREMENT iniciarán en 1.
-- ==============================================================================

USE `db_acd660_tallerpwa`;

-- 1. Desactivar temporalmente verificación de llaves foráneas y modo seguro
SET FOREIGN_KEY_CHECKS = 0;
SET SQL_SAFE_UPDATES = 0;

-- ==============================================================================
-- ELIMINACIÓN DE TABLAS (Sentencias Individuales para evitar Error 1066)
-- ==============================================================================

-- 1. Cotizaciones
DROP TABLE IF EXISTS `quotationattachment`;
DROP TABLE IF EXISTS `QuotationAttachment`;
DROP TABLE IF EXISTS `quotation_attachment`;
DROP TABLE IF EXISTS `quotationdetail`;
DROP TABLE IF EXISTS `QuotationDetail`;
DROP TABLE IF EXISTS `quotation_detail`;
DROP TABLE IF EXISTS `quotation`;
DROP TABLE IF EXISTS `Quotation`;

-- 2. Parque Automotor y Clientes
DROP TABLE IF EXISTS `brandmodels`;
DROP TABLE IF EXISTS `BrandModels`;
DROP TABLE IF EXISTS `brand_models`;
DROP TABLE IF EXISTS `brandmodelversion`;
DROP TABLE IF EXISTS `BrandModelVersion`;
DROP TABLE IF EXISTS `brand_model_version`;
DROP TABLE IF EXISTS `brand`;
DROP TABLE IF EXISTS `Brand`;
DROP TABLE IF EXISTS `vehicle`;
DROP TABLE IF EXISTS `Vehicle`;
DROP TABLE IF EXISTS `customer`;
DROP TABLE IF EXISTS `Customer`;

-- 3. Órdenes de Trabajo y Evidencias
DROP TABLE IF EXISTS `work_order_evidence`;
DROP TABLE IF EXISTS `workorderevidence`;
DROP TABLE IF EXISTS `WorkOrderEvidence`;
DROP TABLE IF EXISTS `work_order_history`;
DROP TABLE IF EXISTS `workorderhistory`;
DROP TABLE IF EXISTS `WorkOrderHistory`;
DROP TABLE IF EXISTS `work_order_part`;
DROP TABLE IF EXISTS `workorderpart`;
DROP TABLE IF EXISTS `WorkOrderPart`;
DROP TABLE IF EXISTS `work_order_service`;
DROP TABLE IF EXISTS `workorderservice`;
DROP TABLE IF EXISTS `WorkOrderService`;
DROP TABLE IF EXISTS `work_order`;
DROP TABLE IF EXISTS `workorder`;
DROP TABLE IF EXISTS `WorkOrder`;

-- 4. Ventas, POS y Facturación
DROP TABLE IF EXISTS `sale_payment`;
DROP TABLE IF EXISTS `salepayment`;
DROP TABLE IF EXISTS `SalePayment`;
DROP TABLE IF EXISTS `sale_detail`;
DROP TABLE IF EXISTS `saledetail`;
DROP TABLE IF EXISTS `SaleDetail`;
DROP TABLE IF EXISTS `sale`;
DROP TABLE IF EXISTS `Sale`;

-- 5. Inventario, Compras y Kardex
DROP TABLE IF EXISTS `inventory_reception_detail`;
DROP TABLE IF EXISTS `inventoryreceptiondetail`;
DROP TABLE IF EXISTS `InventoryReceptionDetail`;
DROP TABLE IF EXISTS `inventory_reception`;
DROP TABLE IF EXISTS `inventoryreception`;
DROP TABLE IF EXISTS `InventoryReception`;
DROP TABLE IF EXISTS `inventory_history`;
DROP TABLE IF EXISTS `inventoryhistory`;
DROP TABLE IF EXISTS `InventoryHistory`;
DROP TABLE IF EXISTS `inventory`;
DROP TABLE IF EXISTS `Inventory`;

-- 6. Agenda y Citas
DROP TABLE IF EXISTS `appointment`;
DROP TABLE IF EXISTS `Appointment`;
DROP TABLE IF EXISTS `agenda_block`;
DROP TABLE IF EXISTS `agendablock`;
DROP TABLE IF EXISTS `AgendaBlock`;
DROP TABLE IF EXISTS `agenda_day_config`;
DROP TABLE IF EXISTS `agendadayconfig`;
DROP TABLE IF EXISTS `AgendaDayConfig`;
DROP TABLE IF EXISTS `agenda_settings`;
DROP TABLE IF EXISTS `agendasettings`;
DROP TABLE IF EXISTS `AgendaSettings`;

-- 7. Contabilidad y Liquidación de Mecánicos
DROP TABLE IF EXISTS `mechanic_payment_settlement`;
DROP TABLE IF EXISTS `mechanicpaymentsettlement`;
DROP TABLE IF EXISTS `MechanicPaymentSettlement`;
DROP TABLE IF EXISTS `mechanic_payment_settings`;
DROP TABLE IF EXISTS `mechanicpaymentsettings`;
DROP TABLE IF EXISTS `MechanicPaymentSettings`;

-- 8. Catálogos, Servicios y Productos
DROP TABLE IF EXISTS `service_price_by_version`;
DROP TABLE IF EXISTS `servicepricebyversion`;
DROP TABLE IF EXISTS `ServicePriceByVersion`;
DROP TABLE IF EXISTS `service_catalog`;
DROP TABLE IF EXISTS `servicecatalog`;
DROP TABLE IF EXISTS `ServiceCatalog`;
DROP TABLE IF EXISTS `service_type`;
DROP TABLE IF EXISTS `servicetype`;
DROP TABLE IF EXISTS `ServiceType`;
DROP TABLE IF EXISTS `product`;
DROP TABLE IF EXISTS `Product`;
DROP TABLE IF EXISTS `product_type`;
DROP TABLE IF EXISTS `producttype`;
DROP TABLE IF EXISTS `ProductType`;
DROP TABLE IF EXISTS `supplier`;
DROP TABLE IF EXISTS `Supplier`;
DROP TABLE IF EXISTS `payment_method`;
DROP TABLE IF EXISTS `paymentmethod`;
DROP TABLE IF EXISTS `PaymentMethod`;

-- 9. Seguridad, Roles, Permisos y Módulos
DROP TABLE IF EXISTS `role_actions`;
DROP TABLE IF EXISTS `role_action`;
DROP TABLE IF EXISTS `roleaction`;
DROP TABLE IF EXISTS `roleactions`;
DROP TABLE IF EXISTS `RoleAction`;
DROP TABLE IF EXISTS `user_role_module`;
DROP TABLE IF EXISTS `userrolemodule`;
DROP TABLE IF EXISTS `UserRoleModule`;
DROP TABLE IF EXISTS `workshop_module`;
DROP TABLE IF EXISTS `workshopmodule`;
DROP TABLE IF EXISTS `WorkshopModule`;
DROP TABLE IF EXISTS `action`;
DROP TABLE IF EXISTS `Action`;
DROP TABLE IF EXISTS `module`;
DROP TABLE IF EXISTS `Module`;
DROP TABLE IF EXISTS `operation`;
DROP TABLE IF EXISTS `Operation`;
DROP TABLE IF EXISTS `password_reset_token`;
DROP TABLE IF EXISTS `passwordresettoken`;
DROP TABLE IF EXISTS `PasswordResetToken`;
DROP TABLE IF EXISTS `login`;
DROP TABLE IF EXISTS `Login`;
DROP TABLE IF EXISTS `user`;
DROP TABLE IF EXISTS `User`;
DROP TABLE IF EXISTS `user_role`;
DROP TABLE IF EXISTS `userrole`;
DROP TABLE IF EXISTS `UserRole`;
DROP TABLE IF EXISTS `identification_type`;
DROP TABLE IF EXISTS `identificationtype`;
DROP TABLE IF EXISTS `IdentificationType`;

-- 10. SaaS y Configuración del Taller
DROP TABLE IF EXISTS `email_settings`;
DROP TABLE IF EXISTS `emailsettings`;
DROP TABLE IF EXISTS `EmailSettings`;
DROP TABLE IF EXISTS `workshop_settings`;
DROP TABLE IF EXISTS `workshopsettings`;
DROP TABLE IF EXISTS `WorkshopSettings`;
DROP TABLE IF EXISTS `workshop`;
DROP TABLE IF EXISTS `Workshop`;

-- 11. Control de Migraciones EF Core
DROP TABLE IF EXISTS `__EFMigrationsHistory`;

-- 2. Reactivar la verificación de llaves foráneas y modo seguro
SET FOREIGN_KEY_CHECKS = 1;
SET SQL_SAFE_UPDATES = 1;

-- Mensaje de confirmación en consola SQL
SELECT CONCAT('Base de datos "', DATABASE(), '" vaciada completamente con exito. 0 tablas restantes. Lista para correr migraciones con autoincrementales desde 1.') AS Resultado;

-- ==============================================================================
-- SCRIPT DE RESETEO Y VACIADO COMPLETO DE BASE DE DATOS (DATA TRUNCATE)
-- Sistema: TallerMotoCar Multi-Tenant SaaS
-- Propósito: Vaciar todos los datos de las tablas SIN borrar la base de datos (DROP DATABASE)
--            Reinicia los contadores AUTO_INCREMENT a 1 y deja la base de datos
--            lista para ejecutar inmediatamente 'SeedInitialComplete.sql'.
-- ==============================================================================

USE TallerMotoCar;

-- 1. Desactivar temporalmente la verificación de llaves foráneas para permitir TRUNCATE sin bloqueos
SET FOREIGN_KEY_CHECKS = 0;

-- ==============================================================================
-- DOMINIO 1: AUDITORÍA, HISTÓRICOS Y RELACIONES TRANSACCIONALES HIJAS
-- ==============================================================================
TRUNCATE TABLE IF EXISTS roleaction;
TRUNCATE TABLE IF EXISTS user_role_module;
TRUNCATE TABLE IF EXISTS password_reset_token;
TRUNCATE TABLE IF EXISTS login;
TRUNCATE TABLE IF EXISTS work_order_evidence;
TRUNCATE TABLE IF EXISTS work_order_history;
TRUNCATE TABLE IF EXISTS work_order_part;
TRUNCATE TABLE IF EXISTS work_order_service;
TRUNCATE TABLE IF EXISTS sale_payment;
TRUNCATE TABLE IF EXISTS sale_detail;
TRUNCATE TABLE IF EXISTS inventory_reception_detail;
TRUNCATE TABLE IF EXISTS inventory_history;

-- ==============================================================================
-- DOMINIO 2: OPERACIONES, FACTURACIÓN, AGENDA Y CONTABILIDAD
-- ==============================================================================
TRUNCATE TABLE IF EXISTS work_order;
TRUNCATE TABLE IF EXISTS sale;
TRUNCATE TABLE IF EXISTS appointment;
TRUNCATE TABLE IF EXISTS agenda_block;
TRUNCATE TABLE IF EXISTS agenda_day_config;
TRUNCATE TABLE IF EXISTS agenda_settings;
TRUNCATE TABLE IF EXISTS mechanic_payment_settlement;
TRUNCATE TABLE IF EXISTS mechanic_payment_settings;
TRUNCATE TABLE IF EXISTS inventory_reception;
TRUNCATE TABLE IF EXISTS inventory;

-- ==============================================================================
-- DOMINIO 3: CATÁLOGOS DE SERVICIOS Y TARIFAS
-- ==============================================================================
TRUNCATE TABLE IF EXISTS service_price_by_version;
TRUNCATE TABLE IF EXISTS service_catalog;
TRUNCATE TABLE IF EXISTS service_type;

-- ==============================================================================
-- DOMINIO 4: CATÁLOGO DE PRODUCTOS Y PROVEEDORES
-- ==============================================================================
TRUNCATE TABLE IF EXISTS product;
TRUNCATE TABLE IF EXISTS product_type;
TRUNCATE TABLE IF EXISTS supplier;
TRUNCATE TABLE IF EXISTS payment_method;

-- ==============================================================================
-- DOMINIO 5: PARQUE AUTOMOTOR Y CLIENTES
-- ==============================================================================
TRUNCATE TABLE IF EXISTS vehicle;
TRUNCATE TABLE IF EXISTS brand_model_version;
TRUNCATE TABLE IF EXISTS brand_models;
TRUNCATE TABLE IF EXISTS brand;
TRUNCATE TABLE IF EXISTS customer;

-- ==============================================================================
-- DOMINIO 6: SEGURIDAD, ROLES, ACCIONES Y USUARIOS
-- ==============================================================================
TRUNCATE TABLE IF EXISTS action;
TRUNCATE TABLE IF EXISTS module;
TRUNCATE TABLE IF EXISTS operation;
TRUNCATE TABLE IF EXISTS user;
TRUNCATE TABLE IF EXISTS userrole;
TRUNCATE TABLE IF EXISTS identification_type;

-- ==============================================================================
-- DOMINIO 7: CONFIGURACIONES SAAS MULTI-TENANT
-- ==============================================================================
TRUNCATE TABLE IF EXISTS workshop_settings;
TRUNCATE TABLE IF EXISTS email_settings;
TRUNCATE TABLE IF EXISTS workshop;

-- 2. Reactivar la verificación de llaves foráneas
SET FOREIGN_KEY_CHECKS = 1;

-- Mensaje de confirmación en consola SQL
SELECT 'Base de datos TallerMotoCar vaciada exitosamente. Lista para ejecutar SeedInitialComplete.sql' AS Resultado;

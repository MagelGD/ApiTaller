USE TallerMotoCar;

-- ==============================================================================
-- SCRIPT MAESTRO DE INICIALIZACIÓN COMPLETA (SEED DATA MULTI-MODAL)
-- Sistema: TallerMotoCar Multi-Tenant SaaS
-- Modalidades Soportadas: Motos, Carros, Lubricentros y Talleres Mixtos
-- Propósito: Despliegue en producción o nueva instancia de desarrollo desde cero
-- ==============================================================================

-- 1. Desactivar temporalmente la verificación de llaves foráneas
SET FOREIGN_KEY_CHECKS = 0;

-- ==============================================================================
-- PASO 1: LIMPIEZA Y RESTRICCIONES DE INTEGRIDAD
-- ==============================================================================
TRUNCATE TABLE roleaction;
TRUNCATE TABLE user_role_module;
TRUNCATE TABLE action;
TRUNCATE TABLE module;
TRUNCATE TABLE operation;

-- Índices únicos para evitar duplicados en re-ejecuciones
ALTER TABLE operation ADD UNIQUE INDEX IF NOT EXISTS idx_unique_operation (name);
ALTER TABLE module ADD UNIQUE INDEX IF NOT EXISTS idx_unique_module (name);
ALTER TABLE action ADD UNIQUE INDEX IF NOT EXISTS idx_unique_slug (slug);
ALTER TABLE userrole ADD UNIQUE INDEX IF NOT EXISTS idx_unique_role (role, workshop_id);
ALTER TABLE workshop_settings DROP INDEX IF EXISTS UQ_WORKSHOP_SETTINGS_KEY;
ALTER TABLE workshop_settings ADD UNIQUE INDEX IF NOT EXISTS UQ_WORKSHOP_SETTINGS_TENANT_KEY (workshop_id, setting_key);

-- ==============================================================================
-- PASO 2: SAAS TENANT RAÍZ (Taller Maestro / Plantilla Base Universal)
-- ==============================================================================
INSERT IGNORE INTO workshop (
    id, name, slug, owner_email, phone, address, city, 
    workshop_type, plan, is_active, trial_ends_at, created_at, updated_at
) VALUES (
    1, 'Taller MotoCar Principal', 'taller-principal', 'admin@taller.com', '3001234567', 
    'Calle Principal #10-20', 'Bogotá', 'multi', 'basic', 1, DATE_ADD(NOW(), INTERVAL 365 DAY), NOW(), NOW()
);

-- ==============================================================================
-- PASO 3: TIPOS DE IDENTIFICACIÓN (Alcance Global / Multi-Tenant)
-- ==============================================================================
INSERT IGNORE INTO identification_type (id, identification, workshop_id, is_active, created_at, updated_at, responsabilidad_user_id) VALUES 
(1, 'CC', NULL, 1, NOW(), NOW(), 1),
(2, 'NIT', NULL, 1, NOW(), NOW(), 1),
(3, 'CE', NULL, 1, NOW(), NOW(), 1),
(4, 'Pasaporte', NULL, 1, NOW(), NOW(), 1),
(5, 'TI', NULL, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 4: ROLES DE USUARIO
-- ==============================================================================
INSERT IGNORE INTO userrole (id, role, workshop_id, is_active, created_at, update_at, responsible_user_id) VALUES 
(1, 'SuperAdmin', NULL, 1, NOW(), NOW(), 1),
(2, 'Administrador', 1, 1, NOW(), NOW(), 1),
(3, 'Mecanico', 1, 1, NOW(), NOW(), 1),
(4, 'Cliente', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 5: USUARIO SUPERADMINISTRADOR
-- ==============================================================================
INSERT IGNORE INTO user (
    id, workshop_id, user_role_id, identification_type_id, identification_number, 
    first_name, middle_name, first_surname, second_last_name, 
    full_name, username, password, email, is_active, must_change_password, created_at, updated_at
) 
VALUES (
    1, NULL, 1, 1, '123456789', 
    'Magel', '', 'Admin', '', 
    'Magel Admin', 'admin', 
    '$2a$11$RJ7pgtRSpt1H/g6ryQ6k1.lL2N8tsoNaP3xs.bS7tAeneyn/2L1Am', 
    'admin@taller.com', 1, 0, NOW(), NOW()
);

-- ==============================================================================
-- PASO 6: OPERACIONES DEL SISTEMA
-- ==============================================================================
INSERT IGNORE INTO operation (id, name, is_active, created_at, updated_at, responsible_user_id) VALUES 
(1, 'Ver', 1, NOW(), NOW(), 1),
(2, 'Guardar', 1, NOW(), NOW(), 1),
(3, 'Editar', 1, NOW(), NOW(), 1),
(4, 'Inactivar', 1, NOW(), NOW(), 1),
(5, 'Cambiar_Estado', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 7: MÓDULOS DEL SISTEMA (31 Módulos Completos)
-- ==============================================================================
INSERT IGNORE INTO module (id, name, is_active, created_at, update_at, responsible_user_id) VALUES 
(1, 'Roles', 1, NOW(), NOW(), 1),
(2, 'Configuracion Roles', 1, NOW(), NOW(), 1),
(3, 'Modulos', 1, NOW(), NOW(), 1),
(4, 'Operaciones', 1, NOW(), NOW(), 1),
(5, 'Acciones', 1, NOW(), NOW(), 1),
(6, 'Usuarios', 1, NOW(), NOW(), 1),
(7, 'Tipos Identificacion', 1, NOW(), NOW(), 1),
(8, 'Marcas', 1, NOW(), NOW(), 1),
(9, 'Modelos', 1, NOW(), NOW(), 1),
(10, 'Referencias', 1, NOW(), NOW(), 1),
(11, 'Cilindros', 1, NOW(), NOW(), 1),
(12, 'Tipos Productos', 1, NOW(), NOW(), 1),
(13, 'Productos', 1, NOW(), NOW(), 1),
(14, 'Unidades', 1, NOW(), NOW(), 1),
(15, 'Metodos Pago', 1, NOW(), NOW(), 1),
(16, 'Proveedores', 1, NOW(), NOW(), 1),
(17, 'Clientes', 1, NOW(), NOW(), 1),
(18, 'Vehiculos', 1, NOW(), NOW(), 1),
(19, 'Ordenes Trabajo', 1, NOW(), NOW(), 1),
(20, 'Tipos Servicio', 1, NOW(), NOW(), 1),
(21, 'Catalogos Servicio', 1, NOW(), NOW(), 1),
(22, 'Precios Servicio', 1, NOW(), NOW(), 1),
(23, 'Inventario', 1, NOW(), NOW(), 1),
(24, 'Logo del Taller', 1, NOW(), NOW(), 1),
(25, 'Portal Cliente', 1, NOW(), NOW(), 1),
(26, 'Envío Correo', 1, NOW(), NOW(), 1),
(27, 'Modo Vehicular', 1, NOW(), NOW(), 1),
(28, 'Agenda', 1, NOW(), NOW(), 1),
(29, 'Centro de Control', 1, NOW(), NOW(), 1),
(30, 'Contabilidad', 1, NOW(), NOW(), 1),
(31, 'Gestión SaaS', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 8: ACCIONES Y PERMISOS (Slugs Sincronizados con Frontend y API)
-- ==============================================================================
INSERT IGNORE INTO action (module_id, operation_id, name, slug, is_active, created_at, updated_at, responsible_user_id) VALUES
-- 1. ROLES
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Roles', 'Ver_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Roles', 'Guardar_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Roles', 'Editar_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Roles', 'Inactivar_Roles', 1, NOW(), NOW(), 1),

-- 2. CONFIGURACIÓN ROLES
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Config Roles', 'Ver_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Config Roles', 'Guardar_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Config Roles', 'Editar_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Config Roles', 'Inactivar_Configuracion_Roles', 1, NOW(), NOW(), 1),

-- 3. MÓDULOS
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Modulos', 'Ver_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Modulos', 'Guardar_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Modulos', 'Editar_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Modulos', 'Inactivar_Modulos', 1, NOW(), NOW(), 1),

-- 4. OPERACIONES
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Operaciones', 'Ver_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Operaciones', 'Guardar_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Operaciones', 'Editar_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Operaciones', 'Inactivar_Operaciones', 1, NOW(), NOW(), 1),

-- 5. ACCIONES
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Acciones', 'Ver_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Acciones', 'Guardar_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Acciones', 'Editar_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Acciones', 'Inactivar_Acciones', 1, NOW(), NOW(), 1),

-- 6. USUARIOS
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Usuarios', 'Ver_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Usuarios', 'Guardar_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Usuarios', 'Editar_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Usuarios', 'Inactivar_Usuarios', 1, NOW(), NOW(), 1),

-- 7. TIPOS IDENTIFICACIÓN
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos ID', 'Ver_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos ID', 'Guardar_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos ID', 'Editar_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos ID', 'Inactivar_Tipos_Identificacion', 1, NOW(), NOW(), 1),

-- 8. MARCAS
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Marcas', 'Ver_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Marcas', 'Guardar_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Marcas', 'Editar_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Marcas', 'Inactivar_Marcas', 1, NOW(), NOW(), 1),

-- 9. MODELOS
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Modelos', 'Ver_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Modelos', 'Guardar_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Modelos', 'Editar_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Modelos', 'Inactivar_Modelos', 1, NOW(), NOW(), 1),

-- 10. REFERENCIAS
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Referencias', 'Ver_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Referencias', 'Guardar_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Referencias', 'Editar_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Referencias', 'Inactivar_Referencias', 1, NOW(), NOW(), 1),

-- 11. CILINDROS
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Cilindros', 'workshop-cylinders-view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Cilindros', 'workshop-cylinders-create', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Cilindros', 'workshop-cylinders-edit', 1, NOW(), NOW(), 1),

-- 12. TIPOS PRODUCTOS
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Prod', 'Ver_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Prod', 'Guardar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Prod', 'Editar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Prod', 'Inactivar_Tipos_Productos', 1, NOW(), NOW(), 1),

-- 13. PRODUCTOS
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Productos', 'Ver_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Productos', 'Guardar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Productos', 'Editar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Productos', 'Inactivar_Productos', 1, NOW(), NOW(), 1),

-- 14. UNIDADES
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Unidades', 'workshop-units-view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Unidades', 'workshop-units-create', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Unidades', 'workshop-units-edit', 1, NOW(), NOW(), 1),

-- 15. MÉTODOS PAGO
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Metodos Pago', 'Ver_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Metodos Pago', 'Guardar_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Metodos Pago', 'Editar_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Metodos Pago', 'Inactivar_Metodos_Pago', 1, NOW(), NOW(), 1),

-- 16. PROVEEDORES
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Proveedores', 'Ver_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Proveedores', 'Guardar_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Proveedores', 'Editar_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Proveedores', 'Inactivar_Proveedores', 1, NOW(), NOW(), 1),

-- 17. CLIENTES
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Clientes', 'Ver_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Clientes', 'Guardar_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Clientes', 'Editar_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Clientes', 'Inactivar_Clientes', 1, NOW(), NOW(), 1),

-- 18. VEHÍCULOS
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Vehiculos', 'Ver_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Vehiculos', 'Guardar_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Vehiculos', 'Editar_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Vehiculos', 'Inactivar_Vehiculos', 1, NOW(), NOW(), 1),

-- 19. ÓRDENES DE TRABAJO
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Ordenes Trabajo', 'Ver_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Ordenes Trabajo', 'Guardar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Ordenes Trabajo', 'Editar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Ordenes Trabajo', 'Inactivar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Cambiar Estado Ordenes', 'Cambiar_Estado_Orden_Trabajo', 1, NOW(), NOW(), 1),

-- 20. TIPOS SERVICIO
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Servicio', 'Ver_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Servicio', 'Guardar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Servicio', 'Editar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Servicio', 'Inactivar_Tipos_Servicio', 1, NOW(), NOW(), 1),

-- 21. CATÁLOGOS SERVICIO
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Catalogos Servicio', 'Ver_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Catalogos Servicio', 'Guardar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Catalogos Servicio', 'Editar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Catalogos Servicio', 'Inactivar_Catalogos_Servicio', 1, NOW(), NOW(), 1),

-- 22. PRECIOS SERVICIO
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Precios Servicio', 'Ver_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Precios Servicio', 'Guardar_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Precios Servicio', 'Editar_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Precios Servicio', 'Inactivar_Precios_Servicio', 1, NOW(), NOW(), 1),

-- 23. INVENTARIO
((SELECT id FROM module WHERE name = 'Inventario'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Inventario', 'Ver_Inventario', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Inventario'), (SELECT id FROM operation WHERE name = 'Editar'), 'Ajustar Inventario', 'Ajustar_Inventario', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Inventario'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Historial Inventario', 'Ver_Historial_Inventario', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Inventario'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Recepcion Masiva Inventario', 'Recepcion_Masiva_Inventario', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Inventario'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Facturas Inventario', 'Ver_Facturas_Inventario', 1, NOW(), NOW(), 1),

-- 24. LOGO DEL TALLER
((SELECT id FROM module WHERE name = 'Logo del Taller'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Logo Taller', 'config.logo.view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Logo del Taller'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Logo Taller', 'config.logo.save', 1, NOW(), NOW(), 1),

-- 25. PORTAL CLIENTE
((SELECT id FROM module WHERE name = 'Portal Cliente'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Portal Cliente', 'Ver_Portal_Cliente', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Portal Cliente'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Aprobar Cotizacion Cliente', 'Aprobar_Portal_Cliente', 1, NOW(), NOW(), 1),

-- 26. ENVÍO CORREO
((SELECT id FROM module WHERE name = 'Envío Correo'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Configuración Correo', 'config.email.view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Envío Correo'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Configuración Correo', 'config.email.save', 1, NOW(), NOW(), 1),

-- 27. MODO VEHICULAR
((SELECT id FROM module WHERE name = 'Modo Vehicular'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Configuración Modo Vehicular', 'config.vehicle_mode.view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modo Vehicular'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Configuración Modo Vehicular', 'config.vehicle_mode.save', 1, NOW(), NOW(), 1),

-- 28. AGENDA
((SELECT id FROM module WHERE name = 'Agenda'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Agenda', 'Ver_Agenda', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Agenda'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Agenda', 'Guardar_Agenda', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Agenda'), (SELECT id FROM operation WHERE name = 'Editar'), 'Configuracion Agenda', 'Configuracion_Agenda', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Agenda'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Convertir OT', 'Convertir_OT_Agenda', 1, NOW(), NOW(), 1),

-- 29. CENTRO DE CONTROL
((SELECT id FROM module WHERE name = 'Centro de Control'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Dashboard Administrativo', 'dashboard.admin.view', 1, NOW(), NOW(), 1),

-- 30. CONTABILIDAD
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Contabilidad', 'Ver_Contabilidad', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Pagos Mecanicos', 'Ver_Pagos_Mecanicos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Pagos Mecanicos', 'Guardar_Pagos_Mecanicos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Control Ventas', 'Ver_Control_Ventas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Liquidacion Mecanicos', 'Ver_Liquidacion_Mecanicos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Contabilidad'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Liquidacion Mecanicos', 'Guardar_Liquidacion_Mecanicos', 1, NOW(), NOW(), 1),

-- 31. GESTIÓN SAAS (Plataforma Global)
((SELECT id FROM module WHERE name = 'Gestión SaaS'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Gestión SaaS', 'Ver_Gestion_Saas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Gestión SaaS'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Gestión SaaS', 'Guardar_Gestion_Saas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Gestión SaaS'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Gestión SaaS', 'Editar_Gestion_Saas', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 9: ASIGNACIÓN DE MÓDULOS Y ACCIONES A ROLES
-- ==============================================================================

-- A. Asignar TODOS los módulos al Rol SuperAdmin (Plataforma Global)
INSERT INTO user_role_module (user_role_id, module_role_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, m.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN module m
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM user_role_module urm 
      WHERE urm.user_role_id = ur.id AND urm.module_role_id = m.id
  );

-- B. Asignar TODAS las acciones al Rol SuperAdmin
INSERT INTO roleaction (role_id, action_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, a.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN action a
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM roleaction ra 
      WHERE ra.role_id = ur.id AND ra.action_id = a.id
  );

-- C. Asignar módulos operativos y de configuración al Rol Administrador del Taller 1
INSERT INTO user_role_module (user_role_id, module_role_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, m.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN module m
WHERE ur.role = 'Administrador' AND ur.workshop_id = 1
  AND m.name NOT IN ('Roles', 'Configuracion Roles', 'Modulos', 'Operaciones', 'Acciones', 'Gestión SaaS')
  AND NOT EXISTS (
      SELECT 1 FROM user_role_module urm 
      WHERE urm.user_role_id = ur.id AND urm.module_role_id = m.id
  );

-- D. Asignar acciones al Rol Administrador del Taller 1
INSERT INTO roleaction (role_id, action_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, a.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN action a
JOIN module m ON a.module_id = m.id
WHERE ur.role = 'Administrador' AND ur.workshop_id = 1
  AND m.name NOT IN ('Roles', 'Configuracion Roles', 'Modulos', 'Operaciones', 'Acciones', 'Gestión SaaS')
  AND NOT EXISTS (
      SELECT 1 FROM roleaction ra 
      WHERE ra.role_id = ur.id AND ra.action_id = a.id
  );

-- ==============================================================================
-- PASO 10: MÉTODOS DE PAGO (Taller 1)
-- ==============================================================================
INSERT IGNORE INTO payment_method (name, icon, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
('Efectivo', 'payments', 1, 1, NOW(), NOW(), 1),
('Nequi', 'smartphone', 1, 1, NOW(), NOW(), 1),
('Daviplata', 'mobile_friendly', 1, 1, NOW(), NOW(), 1),
('Código QR', 'qr_code_2', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 11: TIPOS DE SERVICIO Y CATÁLOGO DE SERVICIOS (Motos + Carros + Lubricentro)
-- ==============================================================================
INSERT IGNORE INTO service_type (id, name, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
(1, 'Mantenimiento Preventivo', 1, 1, NOW(), NOW(), 1),
(2, 'Mantenimiento Correctivo', 1, 1, NOW(), NOW(), 1),
(3, 'Garantía', 1, 1, NOW(), NOW(), 1),
(4, 'Revisión General / Viaje', 1, 1, NOW(), NOW(), 1),
(5, 'Modificaciones / Personalización', 1, 1, NOW(), NOW(), 1),
(6, 'Lavado y Detallado', 1, 1, NOW(), NOW(), 1),
(7, 'Diagnóstico Escáner', 1, 1, NOW(), NOW(), 1),
(8, 'Lubricentro Rápido', 1, 1, NOW(), NOW(), 1),
(9, 'Alineación y Balanceo', 1, 1, NOW(), NOW(), 1),
(10, 'Aire Acondicionado', 1, 1, NOW(), NOW(), 1);

INSERT IGNORE INTO service_catalog (service_type_id, name, description, default_minutes, default_price, time_unit, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
-- Mantenimiento Preventivo Motos y Carros
(1, 'Cambio de Aceite y Filtro (Moto)', 'Drenaje de aceite, cambio de filtro y revisión de niveles en motocicletas', 30, 25000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Cambio de Aceite y Filtro de Motor (Carro / Camioneta)', 'Drenaje de aceite de motor, cambio de filtro de aceite y revisión de 10 puntos clave', 45, 35000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Lubricación y Ajuste de Cadena (Moto)', 'Limpieza, lubricación con producto especializado y tensión de cadena', 15, 15000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Sincronización Básica de Moto', 'Limpieza de cuerpo de aceleración/carburador y calibración de bujía', 60, 45000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Sincronización Electrónica de Carro', 'Limpieza de inyectores por ultrasonido, cuerpo de aceleración, bujías y escaneo', 120, 140000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Mantenimiento General Estándar Moto', 'Revisión de frenos, niveles, luces, llantas y ajuste general de tornillería', 120, 80000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Mantenimiento Preventivo 10.000 KM Carro', 'Inspección de 25 puntos: frenos, suspensión, niveles de fluidos, luces y filtros', 90, 110000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),

-- Mantenimiento Correctivo Motos y Carros
(2, 'Reparación de Fuga de Aceite (Motor)', 'Desmonte, cambio de retenedores, empaques y sellado de cárter/tapa válvulas', 180, 120000, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Pastillas de Freno (Moto)', 'Desmonte de mordaza, limpieza, lubricación de pasadores y pastillas nuevas', 30, 20000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Pastillas y Discos de Freno Delanteros (Carro)', 'Desmonte de mordazas, rectificación o cambio de discos, pastillas y engrase de guías', 60, 60000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(2, 'Mantenimiento y Purga de Frenos Traseros (Bandas y Tambor Carro)', 'Limpieza de zapatas, regulación de freno de mano y purga de líquido', 60, 55000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Kit de Distribución / Tiempos (Carro)', 'Cambio de correa dentada de repartición, poleas tensoras y bomba de agua', 240, 220000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Kit de Embrague / Clutch (Carro)', 'Desmonte de caja de cambios, cambio de prensa, disco y balinera/collarín', 300, 280000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Amortiguadores Delanteros (Carro)', 'Desmonte de torre mcpherson, compresión de espiral y montaje de amortiguador a gas', 120, 100000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(2, 'Cambio de Kit de Arrastre (Moto)', 'Desmonte de ruedas, cambio de piñones de ataque, catalina y cadena', 60, 40000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),

-- Garantía
(3, 'Revisión por Garantía (Motor)', 'Inspección técnica detallada para solicitud de garantía de fábrica', 60, 0, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),
(3, 'Revisión por Garantía (Eléctrica)', 'Revisión de componentes eléctricos y diagnóstico bajo cobertura', 60, 0, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),

-- Revisión Pre-Viaje
(4, 'Revisión Pre-Viaje Carro (Inspección 30 Puntos)', 'Inspección profunda de frenos, llantas, refrigeración, suspensión y luces', 90, 85000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(4, 'Revisión Pre-Viaje Moto (Inspección 20 Puntos)', 'Inspección de kit de arrastre, frenos, llantas, cables y batería', 60, 50000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),

-- Modificaciones / Personalización
(5, 'Instalación de Exploradoras LED (Carro/Moto)', 'Montaje de exploradoras, cableado con relay y switch independiente', 90, 60000, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),
(5, 'Instalación de Alarma / GPS con Cortacorriente', 'Conexión eléctrica de seguridad satelital', 120, 80000, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),

-- Lavado y Detallado
(6, 'Lavado General de Moto con Cera', 'Lavado profundo de motor, chasis y detallado con protectores plásticos', 60, 25000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),
(6, 'Lavado Especializado de Motor y Chasis (Carro)', 'Desengrase a vapor/químico y aplicación de silicona hidrofóbica en motor', 60, 55000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),

-- Diagnóstico Escáner
(7, 'Escaneo Computarizado OBD2 (Carro / Inyección)', 'Lectura y borrado de códigos de falla DTC y prueba de actuadores en vivo', 30, 45000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Escaneo Computarizado Inyección Moto', 'Lectura de parámetros de sensores, TPS, O2 y reseteo de servicio', 20, 35000, 'Minutos', 'moto', 1, 1, NOW(), NOW(), 1),

-- Lubricentro Rápido
(8, 'Cambio de Aceite de Transmisión Manual / Caja', 'Drenaje y llenado de valvulina sintética 75W-90 / 80W-90', 30, 30000, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),
(8, 'Cambio de Filtro de Cabina / Polen (A/C Carro)', 'Desmonte de guantera y reemplazo de filtro de aire acondicionado', 20, 20000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(8, 'Flush / Enjuague Interno de Motor (Carro/Moto)', 'Aplicación de químico limpiador interno previo al drenaje de aceite viejo', 20, 25000, 'Minutos', 'both', 1, 1, NOW(), NOW(), 1),

-- Alineación y Balanceo
(9, 'Alineación 3D Computarizada (Eje Delantero Carro)', 'Calibración de convergencia, divergencia y caída según especificaciones OEM', 40, 50000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),
(9, 'Balanceo de Ruedas por Computadora (Juego x4)', 'Balanceo dinámico con pesas de plomo de alta precisión', 40, 40000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1),

-- Aire Acondicionado
(10, 'Carga de Gas Refrigerante R134a para A/C', 'Vacío del circuito con bomba, prueba de fugas y recarga con aceite PAG y gas R134a', 60, 120000, 'Minutos', 'car', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 12: TIPOS DE PRODUCTOS Y REPUESTOS (Motos + Carros)
-- ==============================================================================
INSERT IGNORE INTO product_type (id, type, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
(1, 'Aceites de Motor', 1, 1, NOW(), NOW(), 1),
(2, 'Líquidos de Freno y Embrague', 1, 1, NOW(), NOW(), 1),
(3, 'Refrigerantes y Anticongelantes', 1, 1, NOW(), NOW(), 1),
(4, 'Llantas Delanteras', 1, 1, NOW(), NOW(), 1),
(5, 'Llantas Traseras', 1, 1, NOW(), NOW(), 1),
(6, 'Neumáticos y Válvulas', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno', 1, 1, NOW(), NOW(), 1),
(8, 'Bandas y Discos de Freno', 1, 1, NOW(), NOW(), 1),
(9, 'Filtros de Aceite', 1, 1, NOW(), NOW(), 1),
(10, 'Filtros de Aire', 1, 1, NOW(), NOW(), 1),
(11, 'Filtros de Gasolina', 1, 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre (Motos)', 1, 1, NOW(), NOW(), 1),
(13, 'Baterías', 1, 1, NOW(), NOW(), 1),
(14, 'Bujías', 1, 1, NOW(), NOW(), 1),
(15, 'Sistema Eléctrico y Sensores', 1, 1, NOW(), NOW(), 1),
(16, 'Suspensión y Amortiguadores', 1, 1, NOW(), NOW(), 1),
(17, 'Rodamientos, Cunas y Bocines', 1, 1, NOW(), NOW(), 1),
(18, 'Guayas y Mandos', 1, 1, NOW(), NOW(), 1),
(19, 'Bombillería y Exploradoras', 1, 1, NOW(), NOW(), 1),
(20, 'Carenajes, Tapas y Plumillas', 1, 1, NOW(), NOW(), 1),
(21, 'Químicos y Aditivos', 1, 1, NOW(), NOW(), 1),
(22, 'Herramientas y Accesorios', 1, 1, NOW(), NOW(), 1),
(23, 'Empaques y Retenedores de Motor', 1, 1, NOW(), NOW(), 1),
(24, 'Partes de Motor y Distribución', 1, 1, NOW(), NOW(), 1),
(25, 'Kits de Embrague (Clutch)', 1, 1, NOW(), NOW(), 1),
(26, 'Dirección y Terminales', 1, 1, NOW(), NOW(), 1),
(27, 'Filtros de Cabina / Polen (A/C)', 1, 1, NOW(), NOW(), 1),
(28, 'Fluidos de Transmisión (ATF/Valvulina)', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 13: PRODUCTOS, REPUESTOS E INSUMOS SEMILLA (Motos + Carros)
-- ==============================================================================
INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ------------------------------------------------------------------------------
-- 1. ACEITES DE MOTOR (Motos, Carros y Lubricentros)
-- ------------------------------------------------------------------------------
-- Aceites para Motos
(1, 'Aceite Liqui Moly Motorbike 4T 10W-40 Street Semi Sintético 1L', 48000, 65000, 'LM-10W40-STR-SS', '10W-40 4T', 'Aceite semisintético de alto rendimiento para motos', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 7100 4T 10W-40 Full Sintético 1L', 68000, 88000, 'MT-7100-10W40', '10W-40 4T Ester', 'Aceite 100% sintético con tecnología Ester para motos de alto desempeño', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Yamalube 4T 10W-40 Semi Sintético 1L', 35000, 48000, 'YAM-10W40-SS', '10W-40 Moto', 'Aceite original Yamaha para FZ, MT03, R3, NMAX', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil Super Moto 4T 20W-50 Mineral 1L', 23000, 32000, 'MB-SM-20W50-M', '20W-50 Moto', 'Aceite mineral de alta rotación para motos de trabajo', 'moto', 1, 1, NOW(), NOW(), 1),

-- Aceites para Carros y Camionetas
(1, 'Aceite Mobil 1 Advanced Full Synthetic 5W-30 1 Galón (3.78L)', 150000, 195000, 'MB1-5W30-GAL', '5W-30 Dexos1 Gen3', 'Aceite 100% sintético premium para Chevrolet Onix, Tracker, Mazda, Toyota, Kia', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil 1 Advanced Full Synthetic 0W-20 1 Galón (3.78L)', 165000, 215000, 'MB1-0W20-GAL', '0W-20 API SP', 'Aceite ultra sintético de baja fricción para Toyota Corolla, RAV4, Honda, Nissan', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Magnatec 10W-40 Semi Sintético 1 Galón', 110000, 145000, 'CST-MAG-10W40-GAL', '10W-40 Gasolina', 'Moléculas inteligentes de adhesión continua para Renault Sandero, Duster, Gol, Aveo', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol GTX 20W-50 Mineral 1 Galón (3.78L)', 85000, 115000, 'CST-GTX-20W50-GAL', '20W-50 Anti-Sludge', 'Aceite mineral premium para motores con más de 100.000 km', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 8100 X-cess Gen2 5W-40 100% Sintético 5L', 210000, 275000, 'MT-8100-5W40-5L', '5W-40 Full Synth', 'Aceite sintético homologado para Volkswagen, BMW, Renault, Mercedes-Benz', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Shell Helix HX7 10W-30 Semi Sintético 1 Galón', 98000, 130000, 'SHL-HX7-10W30-GAL', '10W-30 Flexi Molecule', 'Excelente relación costo/beneficio para flotas y autos modernos', 'car', 1, 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil Delvac Modern 15W-40 Super Defense 1 Galón', 95000, 125000, 'MB-DELV-15W40-GAL', '15W-40 Diésel CI-4', 'Aceite diésel de trabajo pesado para Toyota Hilux, D-Max, Nissan Frontier, Ranger', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 2. FLUIDOS DE TRANSMISIÓN (Cajas Automáticas y Manuales)
-- ------------------------------------------------------------------------------
(28, 'Aceite para Transmisión Automática Mobil ATF D/M 1QT', 32000, 45000, 'MB-ATF-DM', 'Dexron III / Mercon', 'Fluido para cajas automáticas convencionales y dirección hidráulica', 'car', 1, 1, NOW(), NOW(), 1),
(28, 'Aceite Transmisión Automática Motul ATF VI Full Sintético 1L', 58000, 78000, 'MT-ATF-VI', 'Dexron VI / Mercon LV', 'Fluido 100% sintético de baja viscosidad para transmisiones modernas de 6+ velocidades', 'car', 1, 1, NOW(), NOW(), 1),
(28, 'Valvulina para Transmisión Manual Liqui Moly 75W-90 GL-4+ 1L', 65000, 88000, 'LM-75W90-GL4', '75W-90 Full Synth', 'Aceite sintético para cajas de cambio manuales de alta suavidad', 'both', 1, 1, NOW(), NOW(), 1),
(28, 'Aceite para Diferencial y Corona Valvoline 80W-90 GL-5 1QT', 28000, 40000, 'VAL-80W90-GL5', '80W-90 Eje Trasero', 'Aceite de extrema presión para diferenciales y 4x4 (Hilux, Duster 4WD)', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 3. FILTROS DE ACEITE (Motos y Carros)
-- ------------------------------------------------------------------------------
(9, 'Filtro de Aceite Original Yamaha FZ 2.0 / FZ25', 26000, 35000, 'YAM-FIL-FZ', 'Yamaha FZ', 'Elemento filtrante OEM Yamaha', 'moto', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Original Bajaj Pulsar NS 200 / Dominar', 18000, 25000, 'BAJ-FIL-NS', 'Pulsar NS200', 'Filtro de aceite original Bajaj', 'moto', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Blindado Mann Filter W 68/3', 25000, 38000, 'MANN-W683', 'W 68/3 Rosca 3/4-16', 'Filtro para Chevrolet Spark, Spark GT, Sail, Beat, Suzuki Swift, Alto', 'car', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Blindado Bosch 0986AF0059', 24000, 36000, 'BOS-0986AF0059', 'Rosca M20x1.5', 'Filtro de aceite para Renault Sandero, Logan, Stepway, Duster 1.6/2.0, Clio, Kangoo', 'car', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Ecológico de Cartucho Mann Filter HU 7029 z', 32000, 48000, 'MANN-HU7029Z', 'Cartucho Toyota', 'Filtro ecológico para Toyota Hilux Revo, Fortuner, Prado TXL Diésel (1GD / 2GD)', 'car', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Blindado Motorcraft FL-910S', 30000, 45000, 'MTC-FL910S', 'Ford OEM', 'Filtro original para Ford Fiesta, EcoSport, Escape, Focus, Fusion', 'car', 1, 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Blindado Mann Filter W 67/1', 26000, 38000, 'MANN-W671', 'W 67/1 Mazda/Kia', 'Filtro para Mazda 2, Mazda 3 SkyActiv, CX-30, CX-5, Kia Picanto, Rio, Hyundai i10', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 4. FILTROS DE AIRE Y CABINA (Carros)
-- ------------------------------------------------------------------------------
(10, 'Filtro de Aire de Motor Mann Filter C 25 008 (Renault Duster / Sandero)', 30000, 45000, 'MANN-C25008', 'Sandero/Logan/Duster', 'Filtro de aire de alta retención para motores Renault K4M/H4M', 'car', 1, 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire de Motor Bosch (Chevrolet Sail 1.4L)', 25000, 38000, 'BOS-AIR-SAIL', 'Sail 1.4L 16V', 'Filtro de microfibra de encaje exacto para Chevrolet Sail', 'car', 1, 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire de Motor Mann Filter C 26 018 (Kia Picanto Ion / Grand i10)', 24000, 36000, 'MANN-C26018', 'Picanto/i10', 'Elemento filtrante de aire para motores Kappa 1.0L / 1.25L', 'car', 1, 1, NOW(), NOW(), 1),
(27, 'Filtro de Cabina / A/C Polen Carbón Activado (Renault Duster / Logan / Sandero)', 28000, 45000, 'CAB-FIL-REN-CA', 'Cabina Duster/Sandero', 'Filtro purificador con carbón activado para sistema de aire acondicionado', 'car', 1, 1, NOW(), NOW(), 1),
(27, 'Filtro de Cabina / A/C Polen (Chevrolet Spark GT / Beat)', 22000, 35000, 'CAB-FIL-SPKGT', 'Cabina Spark GT', 'Filtro antipolen antibacterial para habitáculo', 'car', 1, 1, NOW(), NOW(), 1),
(27, 'Filtro de Cabina / A/C Polen (Mazda 2 SkyActiv / CX-3 / CX-30)', 32000, 50000, 'CAB-FIL-MZ2', 'Cabina Mazda 2/CX30', 'Filtro de habitáculo de alta retención de micropartículas', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 5. PASTILLAS Y DISCOS DE FRENO (Carros)
-- ------------------------------------------------------------------------------
(7, 'Pastillas de Freno Delanteras Cerámicas Bosch Chevrolet Spark GT / Beat', 55000, 80000, 'BOS-BP-SPKGT', 'Spark GT / Beat 1.2L', 'Juego de 4 pastillas cerámicas delanteras con láminas antirruido', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Delanteras Incolbest Renault Sandero / Logan / Duster', 58000, 85000, 'INC-BP-REN16', 'Sandero/Logan/Duster', 'Pastillas delanteras de equipo original de alto coeficiente de fricción', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Delanteras Cerámicas Brembo Mazda 2 / CX-30 SkyActiv', 95000, 135000, 'BRM-BP-MZ2CX30', 'Mazda 2 / CX-30', 'Pastillas premium cerámicas de bajo desgaste de disco y gran frenado', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Delanteras Semimetálicas Wagner Toyota Hilux / Fortuner / Prado', 90000, 130000, 'WAG-BP-TOYHLX', 'Hilux / Fortuner / Prado', 'Pastillas para trabajo pesado y vehículos 4x4', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Delanteras Bosch Chevrolet Sail 1.4L', 52000, 75000, 'BOS-BP-SAIL', 'Sail 1.4L', 'Pastillas de freno libres de asbesto para uso urbano', 'car', 1, 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Delanteras Incolbest Kia Picanto Ion / Grand i10', 48000, 70000, 'INC-BP-PIC10', 'Picanto Ion / i10', 'Excelente duración y tacto de frenado suave', 'car', 1, 1, NOW(), NOW(), 1),
(8, 'Discos de Freno Delanteros Ventilados Fremax (Par) Chevrolet Sail', 140000, 195000, 'FRX-BD-SAIL', 'Sail Ventilado', 'Discos con tecnología Carbon+ con pintura anticorrosiva', 'car', 1, 1, NOW(), NOW(), 1),
(8, 'Discos de Freno Delanteros Ventilados Fremax (Par) Renault Sandero / Logan 1.6', 155000, 210000, 'FRX-BD-REN16', 'Sandero 1.6', 'Discos de freno delanteros tratados térmicamente para evitar alabeo', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 6. SUSPENSIÓN, DIRECCIÓN Y RODAMIENTOS (Carros)
-- ------------------------------------------------------------------------------
(16, 'Amortiguador Delantero a Gas Monroe OESpectrum Chevrolet Sail (Unidad)', 135000, 185000, 'MNR-AM-SAIL-DEL', 'Sail Delantero', 'Amortiguador presurizado con gas nitrógeno para confort y estabilidad', 'car', 1, 1, NOW(), NOW(), 1),
(16, 'Amortiguador Delantero a Gas KYB Excel-G Renault Sandero / Logan / Stepway', 145000, 198000, 'KYB-AM-REN-DEL', 'Sandero Delantero', 'Amortiguador japonés doble tubo de máxima durabilidad', 'car', 1, 1, NOW(), NOW(), 1),
(16, 'Amortiguador Trasero a Gas Gabriel Ultra Renault Duster 4x2 / 4x4', 110000, 155000, 'GAB-AM-DUS-TRA', 'Duster Trasero', 'Amortiguador reforzado para carretera destapada y carga', 'car', 1, 1, NOW(), NOW(), 1),
(26, 'Terminal de Dirección Exterior CTR (Par) Chevrolet Spark GT / Sail', 45000, 68000, 'CTR-TER-SPKGT', 'Terminal Spark/Sail', 'Terminales forjados de alta resistencia con guardapolvo de cloropreno', 'car', 1, 1, NOW(), NOW(), 1),
(26, 'Rótula de Suspensión Inferior 555 Japón Toyota Hilux / Fortuner', 65000, 95000, '555-ROT-TOYHLX', 'Hilux Inferior', 'Rótula japonesa para trabajo pesado en trocha y minería', 'car', 1, 1, NOW(), NOW(), 1),
(26, 'Bieletas de Barra Estabilizadora Delantera (Par) Mazda 2 / CX-30 / CX-5', 55000, 80000, 'BIE-MZ2-PAR', 'Bieletas Mazda', 'Bieletas metálicas con bujes reforzados para evitar ruidos de suspensión', 'car', 1, 1, NOW(), NOW(), 1),
(17, 'Bocín / Rodamiento de Rueda Delantero con Sensor ABS SKF Chevrolet Onix / Tracker', 120000, 170000, 'SKF-BRG-ONIX', 'Onix/Tracker ABS', 'Rodamiento doble sellado con pista magnética para sensor ABS', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 7. KITS DE EMBRAGUE / CLUTCH (Carros)
-- ------------------------------------------------------------------------------
(25, 'Kit de Embrague / Clutch Completo LuK Chevrolet Spark GT / Beat (Prensa+Disco+Balinera)', 210000, 285000, 'LUK-KC-SPKGT', 'Spark GT 1.2L', 'Kit original LuK de 3 piezas para máxima suavidad de pedal', 'car', 1, 1, NOW(), NOW(), 1),
(25, 'Kit de Embrague / Clutch Completo Valeo Renault Sandero / Logan / Duster 1.6L', 240000, 320000, 'VAL-KC-REN16', 'Renault 1.6L K7M/K4M', 'Kit de embrague equipo original francés con collarín mecánico', 'car', 1, 1, NOW(), NOW(), 1),
(25, 'Kit de Embrague / Clutch Completo LuK Chevrolet Sail 1.4L', 230000, 310000, 'LUK-KC-SAIL', 'Sail 1.4L', 'Kit reforzado LuK RepSet de alto rendimiento para servicio urbano', 'car', 1, 1, NOW(), NOW(), 1),
(25, 'Kit de Embrague / Clutch Completo Valeo Kia Picanto Ion / Morning / Grand i10', 195000, 265000, 'VAL-KC-PIC10', 'Picanto/i10 1.0/1.25L', 'Kit de embrague coreano de ajuste suave y duradero', 'car', 1, 1, NOW(), NOW(), 1),
(25, 'Kit de Embrague Completo Aisin Toyota Hilux 2.4 / 2.8 Diésel (1GD / 2GD)', 480000, 620000, 'AIS-KC-TOYHLX', 'Hilux Revo D-4D', 'Kit de embrague japonés para trabajo pesado y arrastre', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 8. KITS DE DISTRIBUCIÓN / REPARTICIÓN Y MOTOR (Carros)
-- ------------------------------------------------------------------------------
(24, 'Kit de Distribución Completo Gates Chevrolet Aveo 1.4/1.6 / Sail 1.4 (Correa+Tensor+Bomba)', 220000, 295000, 'GAT-KD-AVEO16', 'Aveo/Sail 16V', 'Kit Gates PowerGrip con correa HNBR, tensor automático y bomba de agua con turbina metálica', 'car', 1, 1, NOW(), NOW(), 1),
(24, 'Kit de Repartición Completo INA Renault Sandero / Logan / Duster 1.6 8V/16V con Bomba', 250000, 340000, 'INA-KD-REN16', 'Motores K7M / K4M', 'Kit de distribución alemán con bomba de agua de equipo original', 'car', 1, 1, NOW(), NOW(), 1),
(24, 'Kit de Distribución Continental ContiTech Volkswagen Gol / Voyage 1.6L', 180000, 250000, 'CT-KD-VWGOL', 'Gol/Voyage 1.6L 8V', 'Correa de repartición reforzada y tensor alemán', 'car', 1, 1, NOW(), NOW(), 1),
(24, 'Bomba de Gasolina Eléctrica Universal Bosch 3.8 Bar (Pila/Cartucho)', 75000, 110000, 'BOS-FP-UNI38', 'Inyección 3.8 Bar', 'Pila de combustible de alta presión para Sail, Aveo, Sandero, Picanto, Gol', 'car', 1, 1, NOW(), NOW(), 1),
(23, 'Empaque de Culata Multilámina Victor Reinz Renault Sandero / Logan / Duster 1.6L', 65000, 95000, 'VR-HG-REN16', 'K4M 16V MLS', 'Junta de culata metálica de alta estanqueidad y resistencia térmica', 'car', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- 9. ELECTRICIDAD, BATERÍAS, BUJÍAS Y ACCESORIOS (Carros)
-- ------------------------------------------------------------------------------
(13, 'Batería Automotriz MAC Gold Plus 42-IST (12V 55Ah Libre Mantenimiento)', 260000, 340000, 'MAC-BAT-42IST', '42Ah/55Ah Bornes Delgados', 'Batería sellada de alta potencia para Spark GT, Picanto, Grand i10, March, Swift', 'car', 1, 1, NOW(), NOW(), 1),
(13, 'Batería Automotriz Bosch S4 65D26L (12V 65Ah Bornes Gruesos)', 310000, 410000, 'BOS-BAT-S4-65', '65Ah Eje Izquierdo', 'Batería libre de mantenimiento para Mazda 2/3, Sail, Sandero, Duster, D-Max', 'car', 1, 1, NOW(), NOW(), 1),
(13, 'Batería Automotriz Varta Blue Dynamic 75D31L (12V 75Ah Camionetas / Diésel)', 380000, 490000, 'VAR-BAT-75D31L', '75Ah Heavy Duty', 'Batería alemana de alto arranque para Toyota Hilux, Prado, Frontier, Fortuner', 'car', 1, 1, NOW(), NOW(), 1),
(14, 'Bujías NGK Láser Iridium IFR6E11 (Juego x4) Chevrolet / Suzuki', 140000, 190000, 'NGK-IFR6E11-K4', 'Iridium Carro', 'Juego de 4 bujías de ultra larga duración (80.000 a 100.000 km)', 'car', 1, 1, NOW(), NOW(), 1),
(14, 'Bujías Bosch Doble Platino FR7DPP33X (Juego x4) Renault / VW', 95000, 135000, 'BOS-FR7DPP-K4', 'Platino Carro', 'Electrodo fino de platino para encendido rápido y ahorro de combustible', 'car', 1, 1, NOW(), NOW(), 1),
(15, 'Bobina de Encendido Delphi tipo Lápiz (Unidad) Renault Logan / Sandero / Duster 1.6 16V', 65000, 95000, 'DEL-COIL-REN16', 'Bobina K4M Lápiz', 'Bobina individual de encendido original Delphi', 'car', 1, 1, NOW(), NOW(), 1),
(15, 'Sensor de Oxígeno / Sonda Lambda Delantero Bosch Chevrolet Sail / Aveo', 95000, 140000, 'BOS-O2S-SAIL', '4 Cables con Conector', 'Sensor de mezcla aire/combustible para reducción de emisiones y consumo', 'car', 1, 1, NOW(), NOW(), 1),
(19, 'Bombillo Halógeno Osram Night Breaker 200 H4 (Par 12V 60/55W)', 75000, 105000, 'OSR-H4-NB200', 'H4 +200% Luz', 'Bombillos de alta visibilidad para farolas principales de carro', 'car', 1, 1, NOW(), NOW(), 1),
(19, 'Bombillo LED Automotriz Philips Ultinon Essential H7 (Par)', 140000, 195000, 'PHI-LED-H7', 'H7 6500K Blanco', 'Kit de bombillos LED con corte de luz antideslumbrante para luces bajas/altas', 'car', 1, 1, NOW(), NOW(), 1),
(20, 'Plumillas Limpiaparabrisas Bosch Aerotwin Tipo Banana (Par 24" y 16")', 45000, 68000, 'BOS-AERO-2416', 'Aerotwin 24"/16"', 'Juego de plumillas aerodinámicas de silicona para barrido silencioso', 'car', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 14: MARCAS, MODELOS Y REFERENCIAS (Motos + Carros)
-- ==============================================================================

-- A. Marcas de Motos y Carros (brand)
INSERT IGNORE INTO brand (id, name, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
-- Marcas de Motos (1 al 30)
(1, 'Yamaha', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 'Honda', 'moto', 1, 1, NOW(), NOW(), 1),
(3, 'Suzuki', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 'Bajaj', 'moto', 1, 1, NOW(), NOW(), 1),
(5, 'AKT', 'moto', 1, 1, NOW(), NOW(), 1),
(6, 'TVS', 'moto', 1, 1, NOW(), NOW(), 1),
(7, 'KTM', 'moto', 1, 1, NOW(), NOW(), 1),
(8, 'Hero', 'moto', 1, 1, NOW(), NOW(), 1),
(9, 'Husqvarna', 'moto', 1, 1, NOW(), NOW(), 1),
(10, 'Royal Enfield', 'moto', 1, 1, NOW(), NOW(), 1),
(11, 'Kymco', 'moto', 1, 1, NOW(), NOW(), 1),
(12, 'SYM', 'moto', 1, 1, NOW(), NOW(), 1),
(13, 'Benelli', 'moto', 1, 1, NOW(), NOW(), 1),
(14, 'BMW (Motos)', 'moto', 1, 1, NOW(), NOW(), 1),
(15, 'Ducati', 'moto', 1, 1, NOW(), NOW(), 1),
(16, 'Kawasaki', 'moto', 1, 1, NOW(), NOW(), 1),
(17, 'Triumph', 'moto', 1, 1, NOW(), NOW(), 1),
(18, 'Harley-Davidson', 'moto', 1, 1, NOW(), NOW(), 1),
(19, 'CFMoto', 'moto', 1, 1, NOW(), NOW(), 1),
(20, 'Victory', 'moto', 1, 1, NOW(), NOW(), 1),
(21, 'Auteco Mobility', 'moto', 1, 1, NOW(), NOW(), 1),
(22, 'Vespa', 'moto', 1, 1, NOW(), NOW(), 1),
(23, 'Piaggio', 'moto', 1, 1, NOW(), NOW(), 1),
(24, 'Aprilia', 'moto', 1, 1, NOW(), NOW(), 1),
(25, 'Zontes', 'moto', 1, 1, NOW(), NOW(), 1),
(26, 'Macbor', 'moto', 1, 1, NOW(), NOW(), 1),
(27, 'Voge', 'moto', 1, 1, NOW(), NOW(), 1),
(28, 'QJ Motor', 'moto', 1, 1, NOW(), NOW(), 1),
(29, 'NIU (Eléctricas)', 'moto', 1, 1, NOW(), NOW(), 1),
(30, 'Super Soco (Eléctricas)', 'moto', 1, 1, NOW(), NOW(), 1),

-- Marcas de Carros y Camionetas (31 al 50)
(31, 'Chevrolet', 'car', 1, 1, NOW(), NOW(), 1),
(32, 'Renault', 'car', 1, 1, NOW(), NOW(), 1),
(33, 'Toyota', 'car', 1, 1, NOW(), NOW(), 1),
(34, 'Mazda', 'car', 1, 1, NOW(), NOW(), 1),
(35, 'Nissan', 'car', 1, 1, NOW(), NOW(), 1),
(36, 'Kia', 'car', 1, 1, NOW(), NOW(), 1),
(37, 'Hyundai', 'car', 1, 1, NOW(), NOW(), 1),
(38, 'Ford', 'car', 1, 1, NOW(), NOW(), 1),
(39, 'Volkswagen', 'car', 1, 1, NOW(), NOW(), 1),
(40, 'Suzuki (Autos)', 'car', 1, 1, NOW(), NOW(), 1),
(41, 'Honda (Autos)', 'car', 1, 1, NOW(), NOW(), 1),
(42, 'Mitsubishi', 'car', 1, 1, NOW(), NOW(), 1),
(43, 'BMW (Autos)', 'car', 1, 1, NOW(), NOW(), 1),
(44, 'Mercedes-Benz', 'car', 1, 1, NOW(), NOW(), 1),
(45, 'Audi', 'car', 1, 1, NOW(), NOW(), 1),
(46, 'BYD (Eléctricos/Híbridos)', 'car', 1, 1, NOW(), NOW(), 1),
(47, 'JAC', 'car', 1, 1, NOW(), NOW(), 1),
(48, 'Foton', 'car', 1, 1, NOW(), NOW(), 1),
(49, 'Chery', 'car', 1, 1, NOW(), NOW(), 1),
(50, 'Great Wall', 'car', 1, 1, NOW(), NOW(), 1);

-- B. Modelos / Años (brandmodels: 2000 al 2027)
INSERT IGNORE INTO brandmodels (id, models, vehicle_type, workshop_id, is_active, created_at, updated_at, ResponsibleUserId) VALUES
(1, '2000', 'both', 1, 1, NOW(), NOW(), 1),
(2, '2001', 'both', 1, 1, NOW(), NOW(), 1),
(3, '2002', 'both', 1, 1, NOW(), NOW(), 1),
(4, '2003', 'both', 1, 1, NOW(), NOW(), 1),
(5, '2004', 'both', 1, 1, NOW(), NOW(), 1),
(6, '2005', 'both', 1, 1, NOW(), NOW(), 1),
(7, '2006', 'both', 1, 1, NOW(), NOW(), 1),
(8, '2007', 'both', 1, 1, NOW(), NOW(), 1),
(9, '2008', 'both', 1, 1, NOW(), NOW(), 1),
(10, '2009', 'both', 1, 1, NOW(), NOW(), 1),
(11, '2010', 'both', 1, 1, NOW(), NOW(), 1),
(12, '2011', 'both', 1, 1, NOW(), NOW(), 1),
(13, '2012', 'both', 1, 1, NOW(), NOW(), 1),
(14, '2013', 'both', 1, 1, NOW(), NOW(), 1),
(15, '2014', 'both', 1, 1, NOW(), NOW(), 1),
(16, '2015', 'both', 1, 1, NOW(), NOW(), 1),
(17, '2016', 'both', 1, 1, NOW(), NOW(), 1),
(18, '2017', 'both', 1, 1, NOW(), NOW(), 1),
(19, '2018', 'both', 1, 1, NOW(), NOW(), 1),
(20, '2019', 'both', 1, 1, NOW(), NOW(), 1),
(21, '2020', 'both', 1, 1, NOW(), NOW(), 1),
(22, '2021', 'both', 1, 1, NOW(), NOW(), 1),
(23, '2022', 'both', 1, 1, NOW(), NOW(), 1),
(24, '2023', 'both', 1, 1, NOW(), NOW(), 1),
(25, '2024', 'both', 1, 1, NOW(), NOW(), 1),
(26, '2025', 'both', 1, 1, NOW(), NOW(), 1),
(27, '2026', 'both', 1, 1, NOW(), NOW(), 1),
(28, '2027', 'both', 1, 1, NOW(), NOW(), 1);

-- C. Referencias / Versiones de Motos y Carros (brandmodelversion)
INSERT IGNORE INTO brandmodelversion (BrandId, ModelId, version, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ------------------------------------------------------------------------------
-- MOTOCICLETAS (BrandIds 1 al 14)
-- ------------------------------------------------------------------------------
-- 1. YAMAHA
(1, 1, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 2, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 3, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 4, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 5, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 6, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 7, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 8, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1), (1, 9, 'RX 115', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 10, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1), (1, 11, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1), (1, 12, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 13, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1), (1, 14, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1), (1, 15, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1), (1, 16, 'FZ 16', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 17, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 18, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 19, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 20, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 21, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 22, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 23, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 24, 'FZ 2.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 25, 'FZ 3.0', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 26, 'FZ 3.0', 'moto', 1, 1, NOW(), NOW(), 1), (1, 27, 'FZ 3.0', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 17, 'NMAX 155', 'moto', 1, 1, NOW(), NOW(), 1), (1, 18, 'NMAX 155', 'moto', 1, 1, NOW(), NOW(), 1), (1, 19, 'NMAX 155', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 20, 'NMAX 155', 'moto', 1, 1, NOW(), NOW(), 1), (1, 21, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1), (1, 22, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 23, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1), (1, 24, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1), (1, 25, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 26, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1), (1, 27, 'NMAX Connected', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 16, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 17, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 18, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 19, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 20, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 21, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 22, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 23, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 24, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 25, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 26, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1), (1, 27, 'MT-09', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 20, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1), (1, 21, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1), (1, 22, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1),
(1, 23, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1), (1, 24, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1), (1, 25, 'XTZ 150', 'moto', 1, 1, NOW(), NOW(), 1),

-- 2. HONDA (Motos)
(2, 16, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 17, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 18, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 19, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 20, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 21, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 22, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 23, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 24, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 25, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 26, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1), (2, 27, 'XR 150L', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 18, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 19, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 20, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 21, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 22, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 23, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 24, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 25, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 26, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1), (2, 27, 'CB 190R', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 19, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1), (2, 20, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1), (2, 21, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1),
(2, 22, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1), (2, 23, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1), (2, 24, 'Wave 110S', 'moto', 1, 1, NOW(), NOW(), 1),

-- 3. SUZUKI (Motos)
(3, 1, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1), (3, 5, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1), (3, 10, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1),
(3, 15, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1), (3, 20, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1), (3, 25, 'GN 125', 'moto', 1, 1, NOW(), NOW(), 1),
(3, 17, 'Gixxer 150', 'moto', 1, 1, NOW(), NOW(), 1), (3, 18, 'Gixxer 150', 'moto', 1, 1, NOW(), NOW(), 1), (3, 19, 'Gixxer 150', 'moto', 1, 1, NOW(), NOW(), 1),
(3, 20, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1), (3, 21, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1), (3, 22, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1),
(3, 23, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1), (3, 24, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1), (3, 25, 'Gixxer 150 FI', 'moto', 1, 1, NOW(), NOW(), 1),

-- 4. BAJAJ
(4, 10, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1), (4, 15, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1), (4, 20, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 25, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1), (4, 26, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1), (4, 27, 'Boxer CT 100', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 14, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1), (4, 15, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1), (4, 16, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 17, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1), (4, 18, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1), (4, 19, 'Pulsar NS 200', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 20, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1), (4, 21, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1), (4, 22, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 23, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1), (4, 24, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1), (4, 25, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 26, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1), (4, 27, 'Pulsar NS 200 FI', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 19, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1), (4, 20, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1), (4, 21, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1),
(4, 22, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1), (4, 23, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1), (4, 24, 'Dominar 400', 'moto', 1, 1, NOW(), NOW(), 1),

-- 5. AKT
(5, 10, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 15, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 20, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1),
(5, 22, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 23, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 24, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1),
(5, 25, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 26, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1), (5, 27, 'NKD 125', 'moto', 1, 1, NOW(), NOW(), 1),
(5, 19, 'CR4 162', 'moto', 1, 1, NOW(), NOW(), 1), (5, 20, 'CR4 162', 'moto', 1, 1, NOW(), NOW(), 1), (5, 21, 'CR4 162', 'moto', 1, 1, NOW(), NOW(), 1),

-- ------------------------------------------------------------------------------
-- AUTOMÓVILES Y CAMIONETAS (BrandIds 31 al 40)
-- ------------------------------------------------------------------------------

-- 31. CHEVROLET (Autos)
(31, 8, 'Spark 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (31, 9, 'Spark 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (31, 10, 'Spark 1.0L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 11, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 12, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 13, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 14, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 15, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 16, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 17, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 18, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 19, 'Spark GT 1.2L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 20, 'Beat 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 21, 'Beat 1.2L', 'car', 1, 1, NOW(), NOW(), 1), (31, 22, 'Beat 1.2L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 8, 'Aveo Family 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (31, 9, 'Aveo Family 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (31, 10, 'Aveo Emotion 1.6L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 11, 'Aveo Emotion 1.6L', 'car', 1, 1, NOW(), NOW(), 1), (31, 12, 'Aveo Emotion 1.6L', 'car', 1, 1, NOW(), NOW(), 1), (31, 13, 'Aveo Emotion 1.6L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 13, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 14, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 15, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 16, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 17, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 18, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 19, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 20, 'Sail 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 17, 'Onix 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 18, 'Onix 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (31, 19, 'Onix 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 20, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1), (31, 21, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1), (31, 22, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1),
(31, 23, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1), (31, 24, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1), (31, 25, 'Onix Turbo 1.0T', 'car', 1, 1, NOW(), NOW(), 1),
(31, 14, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1), (31, 15, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1), (31, 16, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 17, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1), (31, 18, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1), (31, 19, 'Tracker 1.8L', 'car', 1, 1, NOW(), NOW(), 1),
(31, 21, 'Tracker Turbo 1.2T', 'car', 1, 1, NOW(), NOW(), 1), (31, 22, 'Tracker Turbo 1.2T', 'car', 1, 1, NOW(), NOW(), 1), (31, 23, 'Tracker Turbo 1.2T', 'car', 1, 1, NOW(), NOW(), 1),
(31, 24, 'Tracker Turbo 1.2T', 'car', 1, 1, NOW(), NOW(), 1), (31, 25, 'Tracker Turbo 1.2T', 'car', 1, 1, NOW(), NOW(), 1),
(31, 12, 'D-Max 3.0 Diésel', 'car', 1, 1, NOW(), NOW(), 1), (31, 15, 'D-Max 2.5 Diésel', 'car', 1, 1, NOW(), NOW(), 1), (31, 20, 'D-Max 3.0 Diésel', 'car', 1, 1, NOW(), NOW(), 1),
(31, 23, 'D-Max 3.0 Diésel', 'car', 1, 1, NOW(), NOW(), 1), (31, 25, 'D-Max 3.0 Diésel', 'car', 1, 1, NOW(), NOW(), 1),

-- 32. RENAULT (Autos)
(32, 9, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1), (32, 10, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1), (32, 11, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1),
(32, 12, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1), (32, 13, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1), (32, 14, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1),
(32, 15, 'Sandero 1.6L 8V', 'car', 1, 1, NOW(), NOW(), 1), (32, 16, 'Sandero II 1.6L', 'car', 1, 1, NOW(), NOW(), 1), (32, 17, 'Sandero II 1.6L', 'car', 1, 1, NOW(), NOW(), 1),
(32, 18, 'Sandero II 1.6L', 'car', 1, 1, NOW(), NOW(), 1), (32, 19, 'Sandero II 1.6L', 'car', 1, 1, NOW(), NOW(), 1), (32, 20, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1),
(32, 21, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1), (32, 22, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1), (32, 23, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1),
(32, 24, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1), (32, 25, 'Sandero 1.6L 16V', 'car', 1, 1, NOW(), NOW(), 1),
(32, 12, 'Duster 1.6L 4x2', 'car', 1, 1, NOW(), NOW(), 1), (32, 13, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1), (32, 14, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1),
(32, 15, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1), (32, 16, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1), (32, 17, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1),
(32, 18, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1), (32, 19, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1), (32, 20, 'Duster 2.0L 4x4', 'car', 1, 1, NOW(), NOW(), 1),
(32, 21, 'Duster Turbo 1.3T', 'car', 1, 1, NOW(), NOW(), 1), (32, 22, 'Duster Turbo 1.3T', 'car', 1, 1, NOW(), NOW(), 1), (32, 23, 'Duster Turbo 1.3T', 'car', 1, 1, NOW(), NOW(), 1),
(32, 24, 'Duster Turbo 1.3T', 'car', 1, 1, NOW(), NOW(), 1), (32, 25, 'Duster Turbo 1.3T', 'car', 1, 1, NOW(), NOW(), 1),
(32, 19, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (32, 20, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (32, 21, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1),
(32, 22, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (32, 23, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (32, 24, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1),
(32, 25, 'Kwid 1.0L', 'car', 1, 1, NOW(), NOW(), 1),

-- 33. TOYOTA (Autos)
(33, 10, 'Hilux 2.5 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 12, 'Hilux 2.5 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 15, 'Hilux 2.5 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 16, 'Hilux Revo 2.4 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 17, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 18, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 19, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 20, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 21, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 22, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 23, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 24, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 25, 'Hilux Revo 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 10, 'Prado TX 3.0 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 14, 'Prado TXL 3.0 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 18, 'Prado TXL 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 20, 'Prado TXL 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 22, 'Prado TXL 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 24, 'Prado TXL 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),
(33, 21, 'Corolla Cross 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (33, 22, 'Corolla Cross 1.8 Hybrid', 'car', 1, 1, NOW(), NOW(), 1), (33, 23, 'Corolla Cross 1.8 Hybrid', 'car', 1, 1, NOW(), NOW(), 1),
(33, 24, 'Corolla Cross 1.8 Hybrid', 'car', 1, 1, NOW(), NOW(), 1), (33, 25, 'Corolla Cross 1.8 Hybrid', 'car', 1, 1, NOW(), NOW(), 1),
(33, 14, 'Fortuner 2.7 Gasolina', 'car', 1, 1, NOW(), NOW(), 1), (33, 17, 'Fortuner 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1), (33, 21, 'Fortuner 2.8 D-4D', 'car', 1, 1, NOW(), NOW(), 1),

-- 34. MAZDA (Autos)
(34, 11, 'Mazda 2 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 12, 'Mazda 2 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 13, 'Mazda 2 1.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 14, 'Mazda 2 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 15, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 16, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 17, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 18, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 19, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 20, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 21, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 22, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 23, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 24, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 25, 'Mazda 2 SkyActiv 1.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 15, 'Mazda 3 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 16, 'Mazda 3 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 17, 'Mazda 3 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 18, 'Mazda 3 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 19, 'Mazda 3 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 20, 'Mazda 3 SkyActiv 2.5L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 20, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 21, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 22, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 23, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 24, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 25, 'CX-30 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1),
(34, 16, 'CX-5 SkyActiv 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (34, 18, 'CX-5 SkyActiv 2.5L', 'car', 1, 1, NOW(), NOW(), 1), (34, 22, 'CX-5 SkyActiv 2.5L', 'car', 1, 1, NOW(), NOW(), 1),

-- 36. KIA (Autos)
(36, 12, 'Picanto Ion 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (36, 13, 'Picanto Ion 1.0L', 'car', 1, 1, NOW(), NOW(), 1), (36, 14, 'Picanto Ion 1.0L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 15, 'Picanto Ion 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 16, 'Picanto Ion 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 17, 'Picanto Ion 1.25L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 18, 'Picanto All New 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 19, 'Picanto All New 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 20, 'Picanto All New 1.25L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 21, 'Picanto GT Line 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 22, 'Picanto GT Line 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 23, 'Picanto GT Line 1.25L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 24, 'Picanto GT Line 1.25L', 'car', 1, 1, NOW(), NOW(), 1), (36, 25, 'Picanto GT Line 1.25L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 13, 'Rio Spice 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (36, 15, 'Rio Spice 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (36, 18, 'Rio All New 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 20, 'Rio All New 1.4L', 'car', 1, 1, NOW(), NOW(), 1), (36, 22, 'Rio All New 1.4L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 14, 'Sportage Revolution 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (36, 17, 'Sportage QL 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (36, 21, 'Sportage QL 2.0L', 'car', 1, 1, NOW(), NOW(), 1),
(36, 23, 'Sportage NQ5 2.0L', 'car', 1, 1, NOW(), NOW(), 1), (36, 25, 'Sportage NQ5 2.0L', 'car', 1, 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 15: CONFIGURACIONES INICIALES DEL TALLER (Taller 1)
-- ==============================================================================

-- A. Configuración de Modo Vehicular (workshop_settings)
INSERT IGNORE INTO workshop_settings (setting_key, setting_value, description, workshop_id, is_active, created_at, updated_at, responsible_user_id) VALUES
('vehicle_mode', 'multi', 'Modo de operación del taller (moto, car, multi)', 1, 1, NOW(), NOW(), 1),
('logo', '', 'Logo comercial del taller en formato Base64', 1, 1, NOW(), NOW(), 1);

-- B. Configuración de Agenda (agenda_settings)
INSERT IGNORE INTO agenda_settings (
    weeks_to_open, daily_slots, business_hours_start, business_hours_end, 
    start_date, working_days, workshop_id, is_active, created_at, updated_at, responsible_user_id
) VALUES (
    4, 8, '08:00:00', '18:00:00', 
    CURRENT_DATE(), '1,2,3,4,5,6', 1, 1, NOW(), NOW(), 1
);

-- ==============================================================================
-- PASO 16: PROCEDIMIENTO ALMACENADO PARA CLONACIÓN SAAS INTELIGENTE
-- Clona automáticamente catálogos según el tipo de taller registrado ('moto', 'car', 'multi')
-- ==============================================================================
DROP PROCEDURE IF EXISTS sp_SeedWorkshopCatalogs;

DELIMITER //
CREATE PROCEDURE sp_SeedWorkshopCatalogs(IN p_NewWorkshopId INT)
BEGIN
    DECLARE v_WorkshopType VARCHAR(20) DEFAULT 'moto';

    -- Obtener el tipo de negocio del nuevo taller ('moto', 'car', 'multi')
    SELECT IFNULL(workshop_type, 'moto') INTO v_WorkshopType 
    FROM workshop 
    WHERE id = p_NewWorkshopId;

    -- 1. Clonar Marcas (Filtrado por tipo de vehículo del taller)
    INSERT INTO brand (name, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT name, vehicle_type, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM brand 
    WHERE workshop_id = 1 
      AND (v_WorkshopType = 'multi' OR vehicle_type = v_WorkshopType OR vehicle_type = 'both');

    -- 2. Clonar Modelos
    INSERT INTO brandmodels (models, vehicle_type, workshop_id, is_active, created_at, updated_at, ResponsibleUserId)
    SELECT models, vehicle_type, p_NewWorkshopId, is_active, NOW(), NOW(), ResponsibleUserId
    FROM brandmodels 
    WHERE workshop_id = 1
      AND (v_WorkshopType = 'multi' OR vehicle_type = v_WorkshopType OR vehicle_type = 'both');

    -- 3. Clonar Versiones (Join por Nombre para mapear nuevos IDs)
    INSERT INTO brandmodelversion (BrandId, ModelId, version, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT 
        b_new.id, 
        m_new.id, 
        v_old.version, 
        v_old.vehicle_type,
        p_NewWorkshopId, 
        v_old.is_active, 
        NOW(), 
        NOW(), 
        v_old.responsible_user_id
    FROM brandmodelversion v_old
    JOIN brand b_old ON v_old.BrandId = b_old.id AND b_old.workshop_id = 1
    JOIN brand b_new ON b_old.name = b_new.name AND b_new.workshop_id = p_NewWorkshopId
    JOIN brandmodels m_old ON v_old.ModelId = m_old.id AND m_old.workshop_id = 1
    JOIN brandmodels m_new ON m_old.models = m_new.models AND m_new.workshop_id = p_NewWorkshopId
    WHERE v_old.workshop_id = 1
      AND (v_WorkshopType = 'multi' OR v_old.vehicle_type = v_WorkshopType OR v_old.vehicle_type = 'both');

    -- 4. Clonar Métodos de Pago
    INSERT INTO payment_method (name, icon, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT name, icon, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM payment_method WHERE workshop_id = 1;

    -- 5. Clonar Tipos de Producto
    INSERT INTO product_type (type, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT type, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM product_type WHERE workshop_id = 1;

    -- 6. Clonar Productos y Repuestos (Filtrado por tipo de vehículo del taller)
    INSERT INTO product (product_type_id, product_name, price, sale_price, code, reference, description, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT 
        pt_new.id,
        p_old.product_name,
        p_old.price,
        p_old.sale_price,
        p_old.code,
        p_old.reference,
        p_old.description,
        p_old.vehicle_type,
        p_NewWorkshopId,
        p_old.is_active,
        NOW(),
        NOW(),
        p_old.responsible_user_id
    FROM product p_old
    JOIN product_type pt_old ON p_old.product_type_id = pt_old.id AND pt_old.workshop_id = 1
    JOIN product_type pt_new ON pt_old.type = pt_new.type AND pt_new.workshop_id = p_NewWorkshopId
    WHERE p_old.workshop_id = 1
      AND (v_WorkshopType = 'multi' OR p_old.vehicle_type = v_WorkshopType OR p_old.vehicle_type = 'both');

    -- 7. Clonar Tipos de Servicio
    INSERT INTO service_type (name, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT name, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM service_type WHERE workshop_id = 1;

    -- 8. Clonar Catálogos de Servicio (Join por Tipo de Servicio y Filtrado por Tipo de Vehículo)
    INSERT INTO service_catalog (service_type_id, name, description, default_minutes, default_price, time_unit, vehicle_type, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT 
        st_new.id,
        sc_old.name,
        sc_old.description,
        sc_old.default_minutes,
        sc_old.default_price,
        sc_old.time_unit,
        sc_old.vehicle_type,
        p_NewWorkshopId,
        sc_old.is_active,
        NOW(),
        NOW(),
        sc_old.responsible_user_id
    FROM service_catalog sc_old
    JOIN service_type st_old ON sc_old.service_type_id = st_old.id AND st_old.workshop_id = 1
    JOIN service_type st_new ON st_old.name = st_new.name AND st_new.workshop_id = p_NewWorkshopId
    WHERE sc_old.workshop_id = 1
      AND (v_WorkshopType = 'multi' OR sc_old.vehicle_type = v_WorkshopType OR sc_old.vehicle_type = 'both');

    -- 9. Clonar Configuraciones Básicas (Modo Vehicular y Agenda)
    INSERT INTO workshop_settings (setting_key, setting_value, description, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT setting_key, IF(setting_key = 'vehicle_mode', v_WorkshopType, setting_value), description, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM workshop_settings WHERE workshop_id = 1;

    INSERT INTO agenda_settings (weeks_to_open, daily_slots, business_hours_start, business_hours_end, start_date, working_days, workshop_id, is_active, created_at, updated_at, responsible_user_id)
    SELECT weeks_to_open, daily_slots, business_hours_start, business_hours_end, CURRENT_DATE(), working_days, p_NewWorkshopId, is_active, NOW(), NOW(), responsible_user_id
    FROM agenda_settings WHERE workshop_id = 1;

END //
DELIMITER ;

-- ==============================================================================
-- FINALIZACIÓN: Reactivar validación de llaves foráneas
-- ==============================================================================
SET FOREIGN_KEY_CHECKS = 1;

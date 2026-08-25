-- ==============================================================================
-- SCRIPT DE ACTUALIZACIÓN INCREMENTAL Y SINCRONIZACIÓN DE PERMISOS (SEGURO)
-- Sistema: TallerMotoCar Multi-Tenant SaaS
-- Compatibilidad: MySQL 8.x / MariaDB / phpMyAdmin / Hosting en Producción
-- Propósito: Actualizar o agregar nuevos módulos y permisos sin alterar ni borrar datos.
-- CARACTERÍSTICA: ¡CERO COMANDOS TRUNCATE! 100% SEGURO PARA ENTORNOS CON DATOS EN VIVO.
-- ==============================================================================

SET FOREIGN_KEY_CHECKS = 0;
SET SQL_SAFE_UPDATES = 0;

-- ==============================================================================
-- PASO 1: OPERACIONES BASE DEL SISTEMA (Idempotente)
-- ==============================================================================
INSERT IGNORE INTO operation (id, name, is_active, created_at, updated_at, responsible_user_id) VALUES 
(1, 'Ver', 1, NOW(), NOW(), 1),
(2, 'Guardar', 1, NOW(), NOW(), 1),
(3, 'Editar', 1, NOW(), NOW(), 1),
(4, 'Inactivar', 1, NOW(), NOW(), 1),
(5, 'Cambiar_Estado', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 2: CATÁLOGO DE MÓDULOS DEL SISTEMA (1 al 33) (Idempotente)
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
(31, 'Gestión SaaS', 1, NOW(), NOW(), 1),
(32, 'Punto de Venta', 1, NOW(), NOW(), 1),
(33, 'Cotizaciones', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 3: CATÁLOGO DE ACCIONES Y SLUGS RBAC (Idempotente)
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
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Productos', 'Ver_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Productos', 'Guardar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Productos', 'Editar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Productos', 'Inactivar_Tipos_Productos', 1, NOW(), NOW(), 1),

-- 13. PRODUCTOS
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Productos', 'Ver_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Productos', 'Guardar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Productos', 'Editar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Productos', 'Inactivar_Productos', 1, NOW(), NOW(), 1),

-- 14. UNIDADES
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Unidades', 'workshop-units-view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Unidades', 'workshop-units-create', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Unidades', 'workshop-units-edit', 1, NOW(), NOW(), 1),

-- 15. MÉTODOS DE PAGO
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
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Cambiar Estado OT', 'Cambiar_Estado_Orden_Trabajo', 1, NOW(), NOW(), 1),

-- 20. TIPOS DE SERVICIO
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Servicio', 'Ver_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Servicio', 'Guardar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Servicio', 'Editar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Servicio', 'Inactivar_Tipos_Servicio', 1, NOW(), NOW(), 1),

-- 21. CATÁLOGOS DE SERVICIO
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Catalogos Servicio', 'Ver_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Catalogos Servicio', 'Guardar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Catalogos Servicio', 'Editar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Catalogos Servicio', 'Inactivar_Catalogos_Servicio', 1, NOW(), NOW(), 1),

-- 22. PRECIOS DE SERVICIO
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
((SELECT id FROM module WHERE name = 'Portal Cliente'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Aprobar Portal Cliente', 'Aprobar_Portal_Cliente', 1, NOW(), NOW(), 1),

-- 26. ENVÍO CORREO
((SELECT id FROM module WHERE name = 'Envío Correo'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Config Email', 'config.email.view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Envío Correo'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Config Email', 'config.email.save', 1, NOW(), NOW(), 1),

-- 27. MODO VEHICULAR
((SELECT id FROM module WHERE name = 'Modo Vehicular'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Modo Vehicular', 'config.vehicle_mode.view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modo Vehicular'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Modo Vehicular', 'config.vehicle_mode.save', 1, NOW(), NOW(), 1),

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
((SELECT id FROM module WHERE name = 'Gestión SaaS'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Gestión SaaS', 'Editar_Gestion_Saas', 1, NOW(), NOW(), 1),

-- 32. PUNTO DE VENTA (POS)
((SELECT id FROM module WHERE name = 'Punto de Venta'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Punto de Venta', 'Ver_Punto_Venta', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Punto de Venta'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Procesar Venta Directa', 'Procesar_Venta_Directa', 1, NOW(), NOW(), 1),

-- 33. COTIZACIONES Y PRESUPUESTOS
((SELECT id FROM module WHERE name = 'Cotizaciones'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Cotizaciones', 'Ver_Cotizaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cotizaciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Cotizaciones', 'Guardar_Cotizaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cotizaciones'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Cotizaciones', 'Editar_Cotizaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cotizaciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Enviar Cotizaciones Email', 'Enviar_Cotizaciones_Email', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cotizaciones'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Convertir Cotizaciones', 'Convertir_Cotizaciones', 1, NOW(), NOW(), 1);

-- ==============================================================================
-- PASO 4: SINCRONIZACIÓN DE ROLES (Multi-Taller / Idempotente)
-- ==============================================================================

-- A. Asignar CUALQUIER módulo faltante al Rol SuperAdmin
INSERT INTO user_role_module (user_role_id, module_role_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, m.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN module m
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM user_role_module urm 
      WHERE urm.user_role_id = ur.id AND urm.module_role_id = m.id
  );

-- B. Asignar CUALQUIER acción faltante al Rol SuperAdmin
INSERT INTO roleaction (role_id, action_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, a.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN action a
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM roleaction ra 
      WHERE ra.role_id = ur.id AND ra.action_id = a.id
  );

-- C. Asignar módulos operativos y de configuración a TODOS los Roles Administrador de TODOS los talleres
INSERT INTO user_role_module (user_role_id, module_role_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, m.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN module m
WHERE ur.role = 'Administrador'
  AND m.name NOT IN ('Roles', 'Configuracion Roles', 'Modulos', 'Operaciones', 'Acciones', 'Gestión SaaS')
  AND NOT EXISTS (
      SELECT 1 FROM user_role_module urm 
      WHERE urm.user_role_id = ur.id AND urm.module_role_id = m.id
  );

-- D. Asignar acciones operativas a TODOS los Roles Administrador de TODOS los talleres
INSERT INTO roleaction (role_id, action_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, a.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN action a
JOIN module m ON a.module_id = m.id
WHERE ur.role = 'Administrador'
  AND m.name NOT IN ('Roles', 'Configuracion Roles', 'Modulos', 'Operaciones', 'Acciones', 'Gestión SaaS')
  AND NOT EXISTS (
      SELECT 1 FROM roleaction ra 
      WHERE ra.role_id = ur.id AND ra.action_id = a.id
  );

SET FOREIGN_KEY_CHECKS = 1;
SET SQL_SAFE_UPDATES = 1;

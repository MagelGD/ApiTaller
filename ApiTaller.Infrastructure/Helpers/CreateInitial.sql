USE tallermoto;

-- Bloquear temporalmente las relaciones de llaves foráneas
SET FOREIGN_KEY_CHECKS = 0;

-- ==============================================================================
-- PASO 1: LIMPIEZA Y BLINDAJE DE LA BASE DE DATOS
-- ==============================================================================
-- 1. Limpiamos las tablas afectadas para eliminar los duplicados y los "0"
TRUNCATE TABLE roleaction;
TRUNCATE TABLE user_role_module;
TRUNCATE TABLE action;
TRUNCATE TABLE module;
TRUNCATE TABLE operation;

-- 2. Agregamos restricciones UNIQUE para que NUNCA MÁS se duplique un registro.
-- Con esto, el INSERT IGNORE funcionará perfectamente de ahora en adelante.
ALTER TABLE operation ADD UNIQUE INDEX idx_unique_operation (name);
ALTER TABLE module ADD UNIQUE INDEX idx_unique_module (name);
ALTER TABLE action ADD UNIQUE INDEX idx_unique_slug (slug);
ALTER TABLE userrole ADD UNIQUE INDEX idx_unique_role (role);

-- ==============================================================================
-- PASO 2: INSERCIÓN DE DATOS (TU SCRIPT)
-- ==============================================================================

-- 1. Crear Tipo de Identificación (CC)
INSERT IGNORE INTO identification_type (id, identification, is_active, created_at, updated_at, responsabilidad_user_id) 
VALUES (1, 'CC', 1, NOW(), NOW(), 1);

-- 2. Crear Roles (SuperAdmin, Mecanico, Cliente)
INSERT IGNORE INTO userrole (id, role, is_active, created_at, update_at, responsible_user_id) VALUES 
(1, 'SuperAdmin', 1, NOW(), NOW(), 1),
(2, 'Mecanico', 1, NOW(), NOW(), 1),
(3, 'Cliente', 1, NOW(), NOW(), 1);

-- 3. Crear Usuario Administrador (Magel) FORZANDO el id = 1
INSERT IGNORE INTO user (
    id, user_role_id, identification_type_id, identification_number, 
    first_name, middle_name, first_surname, second_last_name, 
    full_name, username, password, email, is_active, created_at, updated_at
) 
VALUES (
    1, 1, 1, '123456789', 
    'Magel', '', 'Admin', '', 
    'Magel Admin', 'admin', 
    '$2a$11$RJ7pgtRSpt1H/g6ryQ6k1.lL2N8tsoNaP3xs.bS7tAeneyn/2L1Am', 
    'admin@taller.com', 1, NOW(), NOW()
);

-- 4. Crear Operaciones
INSERT IGNORE INTO operation (name, is_active, created_at, updated_at, responsible_user_id) VALUES 
('Ver', 1, NOW(), NOW(), 1),
('Guardar', 1, NOW(), NOW(), 1),
('Editar', 1, NOW(), NOW(), 1),
('Inactivar', 1, NOW(), NOW(), 1),
('Cambiar_Estado', 1, NOW(), NOW(), 1);

-- 5. Crear Módulos en Español
INSERT IGNORE INTO module (name, is_active, created_at, update_at, responsible_user_id) VALUES 
('Roles', 1, NOW(), NOW(), 1),
('Configuracion Roles', 1, NOW(), NOW(), 1),
('Modulos', 1, NOW(), NOW(), 1),
('Operaciones', 1, NOW(), NOW(), 1),
('Acciones', 1, NOW(), NOW(), 1),
('Usuarios', 1, NOW(), NOW(), 1),
('Tipos Identificacion', 1, NOW(), NOW(), 1),
('Marcas', 1, NOW(), NOW(), 1),
('Modelos', 1, NOW(), NOW(), 1),
('Referencias', 1, NOW(), NOW(), 1),
('Cilindros', 1, NOW(), NOW(), 1),
('Tipos Productos', 1, NOW(), NOW(), 1),
('Productos', 1, NOW(), NOW(), 1),
('Unidades', 1, NOW(), NOW(), 1),
('Metodos Pago', 1, NOW(), NOW(), 1),
('Proveedores', 1, NOW(), NOW(), 1),
('Clientes', 1, NOW(), NOW(), 1),
('Vehiculos', 1, NOW(), NOW(), 1),
('Ordenes Trabajo', 1, NOW(), NOW(), 1),
('Tipos Servicio', 1, NOW(), NOW(), 1),
('Catalogos Servicio', 1, NOW(), NOW(), 1),
('Precios Servicio', 1, NOW(), NOW(), 1);

-- 6. Crear Acciones
INSERT IGNORE INTO action (module_id, operation_id, name, slug, is_active, created_at, updated_at, responsible_user_id) VALUES
-- MÓDULO: ROLES
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Roles', 'Ver_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Roles', 'Guardar_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Roles', 'Editar_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Roles'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Roles', 'Inactivar_Roles', 1, NOW(), NOW(), 1),

-- MÓDULO: CONFIG
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Config Roles', 'Ver_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Config Roles', 'Guardar_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Config Roles', 'Editar_Configuracion_Roles', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Configuracion Roles'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Config Roles', 'Inactivar_Configuracion_Roles', 1, NOW(), NOW(), 1),

-- MÓDULO: MODULES
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Modulos', 'Ver_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Modulos', 'Guardar_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Modulos', 'Editar_Modulos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modulos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Modulos', 'Inactivar_Modulos', 1, NOW(), NOW(), 1),

-- MÓDULO: OPERATIONS
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Operaciones', 'Ver_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Operaciones', 'Guardar_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Operaciones', 'Editar_Operaciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Operaciones'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Operaciones', 'Inactivar_Operaciones', 1, NOW(), NOW(), 1),

-- MÓDULO: ACTIONS
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Acciones', 'Ver_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Acciones', 'Guardar_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Acciones', 'Editar_Acciones', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Acciones'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Acciones', 'Inactivar_Acciones', 1, NOW(), NOW(), 1),

-- MÓDULO: USERS
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Usuarios', 'Ver_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Usuarios', 'Guardar_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Usuarios', 'Editar_Usuarios', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Usuarios'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Usuarios', 'Inactivar_Usuarios', 1, NOW(), NOW(), 1),

-- MÓDULO: ID_TYPES
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos ID', 'Ver_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos ID', 'Guardar_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos ID', 'Editar_Tipos_Identificacion', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Identificacion'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos ID', 'Inactivar_Tipos_Identificacion', 1, NOW(), NOW(), 1),

-- MÓDULO: BRANDS
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Marcas', 'Ver_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Marcas', 'Guardar_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Marcas', 'Editar_Marcas', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Marcas'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Marcas', 'Inactivar_Marcas', 1, NOW(), NOW(), 1),

-- MÓDULO: MODELS
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Modelos', 'Ver_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Modelos', 'Guardar_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Modelos', 'Editar_Modelos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Modelos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Modelos', 'Inactivar_Modelos', 1, NOW(), NOW(), 1),

-- MÓDULO: REFERENCES
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Referencias', 'Ver_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Referencias', 'Guardar_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Referencias', 'Editar_Referencias', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Referencias'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Referencias', 'Inactivar_Referencias', 1, NOW(), NOW(), 1),

-- MÓDULO: CYLINDERS
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Cilindros', 'workshop-cylinders-view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Cilindros', 'workshop-cylinders-create', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Cilindros'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Cilindros', 'workshop-cylinders-edit', 1, NOW(), NOW(), 1),

-- MÓDULO: PRODUCT_TYPES
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Prod', 'Ver_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Prod', 'Guardar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Prod', 'Editar_Tipos_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Prod', 'Inactivar_Tipos_Productos', 1, NOW(), NOW(), 1),

-- MÓDULO: PRODUCTS
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Productos', 'Ver_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Productos', 'Guardar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Productos', 'Editar_Productos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Productos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Productos', 'Inactivar_Productos', 1, NOW(), NOW(), 1),

-- MÓDULO: UNITS
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Unidades', 'workshop-units-view', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Unidades', 'workshop-units-create', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Unidades'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Unidades', 'workshop-units-edit', 1, NOW(), NOW(), 1),

-- MÓDULO: PAYMENT_METHODS
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Metodos Pago', 'Ver_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Metodos Pago', 'Guardar_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Metodos Pago', 'Editar_Metodos_Pago', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Metodos Pago'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Metodos Pago', 'Inactivar_Metodos_Pago', 1, NOW(), NOW(), 1),

-- MÓDULO: SUPPLIERS
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Proveedores', 'Ver_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Proveedores', 'Guardar_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Proveedores', 'Editar_Proveedores', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Proveedores'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Proveedores', 'Inactivar_Proveedores', 1, NOW(), NOW(), 1),

-- MÓDULO: CUSTOMERS
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Clientes', 'Ver_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Clientes', 'Guardar_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Clientes', 'Editar_Clientes', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Clientes'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Clientes', 'Inactivar_Clientes', 1, NOW(), NOW(), 1),

-- MÓDULO: VEHICLES
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Vehiculos', 'Ver_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Vehiculos', 'Guardar_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Vehiculos', 'Editar_Vehiculos', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Vehiculos'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Vehiculos', 'Inactivar_Vehiculos', 1, NOW(), NOW(), 1),

-- MÓDULO: WORK_ORDERS
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Ordenes Trabajo', 'Ver_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Ordenes Trabajo', 'Guardar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Ordenes Trabajo', 'Editar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Ordenes Trabajo', 'Inactivar_Ordenes_Trabajo', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Ordenes Trabajo'), (SELECT id FROM operation WHERE name = 'Cambiar_Estado'), 'Cambiar Estado Ordenes', 'Cambiar_Estado_Orden_Trabajo', 1, NOW(), NOW(), 1),

-- MÓDULO: SERVICE_TYPES
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Tipos Servicio', 'Ver_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Tipos Servicio', 'Guardar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Tipos Servicio', 'Editar_Tipos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Tipos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Tipos Servicio', 'Inactivar_Tipos_Servicio', 1, NOW(), NOW(), 1),

-- MÓDULO: SERVICE_CATALOGS
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Catalogos Servicio', 'Ver_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Catalogos Servicio', 'Guardar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Catalogos Servicio', 'Editar_Catalogos_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Catalogos Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Catalogos Servicio', 'Inactivar_Catalogos_Servicio', 1, NOW(), NOW(), 1),

-- MÓDULO: SERVICE_PRICES
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Ver'), 'Ver Precios Servicio', 'Ver_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Guardar'), 'Guardar Precios Servicio', 'Guardar_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Editar'), 'Editar Precios Servicio', 'Editar_Precios_Servicio', 1, NOW(), NOW(), 1),
((SELECT id FROM module WHERE name = 'Precios Servicio'), (SELECT id FROM operation WHERE name = 'Inactivar'), 'Inactivar Precios Servicio', 'Inactivar_Precios_Servicio', 1, NOW(), NOW(), 1);

-- 7. ASIGNAR MÓDULOS AL ROL SUPERADMIN
INSERT INTO user_role_module (user_role_id, module_role_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, m.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN module m
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM user_role_module urm 
      WHERE urm.user_role_id = ur.id AND urm.module_role_id = m.id
  );

-- 8. ASIGNAR ACCIONES (PERMISOS) AL ROL SUPERADMIN
INSERT INTO roleaction (role_id, action_id, is_active, created_at, updated_at, responsible_user_id)
SELECT ur.id, a.id, 1, NOW(), NOW(), 1
FROM userrole ur
CROSS JOIN action a
WHERE ur.role = 'SuperAdmin'
  AND NOT EXISTS (
      SELECT 1 FROM roleaction ra 
      WHERE ra.role_id = ur.id AND ra.action_id = a.id
  );

-- Reactivar validación de llaves foráneas
SET FOREIGN_KEY_CHECKS = 1;
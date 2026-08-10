USE TallerMotoCar;

-- -------------------------------------------------------------------------
-- SCRIPT PARA LIMPIEZA DE TALLERES SAAS DE PRUEBA
-- Este script elimina todos los talleres creados, sus roles y sus usuarios,
-- dejando únicamente al SuperAdmin global y los roles base.
-- -------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;
SET SQL_SAFE_UPDATES = 0;

-- 1. Eliminar Permisos y Módulos asociados a roles de Talleres (Tenant Admins)
-- Conservamos únicamente los permisos del SuperAdmin (role_id = 1)
DELETE FROM user_role_module WHERE user_role_id > 1;
DELETE FROM roleaction WHERE role_id > 1;

-- 2. Eliminar Usuarios (Administradores locales, mecánicos, cajeros, etc.)
-- Conservamos únicamente al SuperAdmin global (id = 1)
DELETE FROM user WHERE id > 1;

-- 3. Eliminar Roles Locales de los talleres
-- Conservamos los roles por defecto del sistema (1: SuperAdmin, 2: Mecanico, 3: Cliente)
DELETE FROM userrole WHERE id > 3;

-- 4. Eliminar los Talleres (Workshops)
-- Esto eliminará todas las empresas SaaS creadas. Si hay tablas dependientes
-- como clientes, citas, o inventario sin ON DELETE CASCADE, también deberían limpiarse.
DELETE FROM workshop WHERE id > 0;

-- (Opcional) Si tu base de datos no tiene CASCADE para el catálogo, limpia las tablas transaccionales:
-- DELETE FROM product WHERE workshop_id IS NOT NULL;
-- DELETE FROM customer WHERE workshop_id IS NOT NULL;
-- DELETE FROM work_order WHERE workshop_id IS NOT NULL;

SET SQL_SAFE_UPDATES = 1;
SET FOREIGN_KEY_CHECKS = 1;

-- -------------------------------------------------------------------------
-- VERIFICACIÓN
-- -------------------------------------------------------------------------
SELECT 'Usuarios Restantes' as Result, COUNT(*) as Total FROM user;
SELECT 'Talleres Restantes' as Result, COUNT(*) as Total FROM workshop;

-- ==============================================================================
-- SCRIPT DE MIGRACIÓN: Parametrización y Feature Toggling de Módulos por Taller
-- Sistema: TallerMotoCar Multi-Tenant SaaS
-- Compatibilidad: MySQL 8.x / MariaDB
-- ==============================================================================

SET FOREIGN_KEY_CHECKS = 0;
SET SQL_SAFE_UPDATES = 0;

-- 1. Crear tabla relacional workshop_module
CREATE TABLE IF NOT EXISTS workshop_module (
    id INT AUTO_INCREMENT PRIMARY KEY,
    workshop_id INT NOT NULL,
    module_id INT NOT NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    responsible_user_id INT NULL,
    CONSTRAINT FK_workshop_module_workshop FOREIGN KEY (workshop_id) REFERENCES workshop(id) ON DELETE CASCADE,
    CONSTRAINT FK_workshop_module_module FOREIGN KEY (module_id) REFERENCES module(id) ON DELETE RESTRICT,
    UNIQUE KEY UQ_workshop_module (workshop_id, module_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 2. Backfill para talleres existentes: Asignar todos los módulos de negocio activos
INSERT IGNORE INTO workshop_module (workshop_id, module_id, is_active, created_at, updated_at, responsible_user_id)
SELECT w.id, m.id, 1, NOW(), NOW(), 1
FROM workshop w
CROSS JOIN module m
WHERE m.is_active = 1
  AND m.name NOT IN ('Roles', 'Configuracion Roles', 'Modulos', 'Operaciones', 'Acciones', 'Tipos Identificacion', 'Modo Vehicular');

SET FOREIGN_KEY_CHECKS = 1;
SET SQL_SAFE_UPDATES = 1;

-- ==============================================================================
-- SCRIPT DE MIGRACIÓN: Fotos en Productos y Combos/Kits de Productos (BOM)
-- Sistema: TallerMotoCar Multi-Tenant SaaS
-- Compatibilidad: MySQL 8.x / MariaDB
-- ==============================================================================

SET FOREIGN_KEY_CHECKS = 0;
SET SQL_SAFE_UPDATES = 0;

-- 1. Agregar columnas image_base64 e is_combo a la tabla product si no existen
SET @dbname = DATABASE();
SET @tablename = "product";
SET @columnname = "image_base64";
SET @preparedStatement = (SELECT IF(
  (
    SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
    WHERE
      (table_name = @tablename)
      AND (table_schema = @dbname)
      AND (column_name = @columnname)
  ) > 0,
  "SELECT 1",
  CONCAT("ALTER TABLE ", @tablename, " ADD COLUMN `image_base64` LONGTEXT NULL;")
));
PREPARE alterIfNotExists FROM @preparedStatement;
EXECUTE alterIfNotExists;
DEALLOCATE PREPARE alterIfNotExists;

SET @columnname = "is_combo";
SET @preparedStatement = (SELECT IF(
  (
    SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
    WHERE
      (table_name = @tablename)
      AND (table_schema = @dbname)
      AND (column_name = @columnname)
  ) > 0,
  "SELECT 1",
  CONCAT("ALTER TABLE ", @tablename, " ADD COLUMN `is_combo` TINYINT(1) NOT NULL DEFAULT 0;")
));
PREPARE alterIfNotExists FROM @preparedStatement;
EXECUTE alterIfNotExists;
DEALLOCATE PREPARE alterIfNotExists;

-- 2. Crear tabla relacional product_combo_item
CREATE TABLE IF NOT EXISTS `product_combo_item` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `parent_product_id` INT NOT NULL,
    `child_product_id` INT NOT NULL,
    `quantity` INT NOT NULL DEFAULT 1,
    `workshop_id` INT NOT NULL,
    `is_active` TINYINT(1) NOT NULL DEFAULT 1,
    `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `responsible_user_id` INT NULL,
    CONSTRAINT `FK_COMBO_PARENT_PRODUCT` FOREIGN KEY (`parent_product_id`) REFERENCES `product` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_COMBO_CHILD_PRODUCT` FOREIGN KEY (`child_product_id`) REFERENCES `product` (`id`) ON DELETE RESTRICT,
    INDEX `FK_COMBO_PARENT_PRODUCT` (`parent_product_id`),
    INDEX `FK_COMBO_CHILD_PRODUCT` (`child_product_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;
SET SQL_SAFE_UPDATES = 1;

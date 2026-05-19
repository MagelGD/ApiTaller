USE tallermoto;

-- ==============================================================================
-- SCRIPT 1: CARGA MASIVA DE MARCAS (brand)
-- ==============================================================================

INSERT IGNORE INTO brand (id, name, is_active, created_at, updated_at, responsible_user_id) VALUES
(1, 'Yamaha', 1, NOW(), NOW(), 1),
(2, 'Honda', 1, NOW(), NOW(), 1),
(3, 'Suzuki', 1, NOW(), NOW(), 1),
(4, 'Bajaj', 1, NOW(), NOW(), 1),
(5, 'AKT', 1, NOW(), NOW(), 1),
(6, 'TVS', 1, NOW(), NOW(), 1),
(7, 'KTM', 1, NOW(), NOW(), 1),
(8, 'Hero', 1, NOW(), NOW(), 1),
(9, 'Husqvarna', 1, NOW(), NOW(), 1),
(10, 'Royal Enfield', 1, NOW(), NOW(), 1),
(11, 'Kymco', 1, NOW(), NOW(), 1),
(12, 'SYM', 1, NOW(), NOW(), 1),
(13, 'Benelli', 1, NOW(), NOW(), 1),
(14, 'BMW', 1, NOW(), NOW(), 1),
(15, 'Ducati', 1, NOW(), NOW(), 1),
(16, 'Kawasaki', 1, NOW(), NOW(), 1),
(17, 'Triumph', 1, NOW(), NOW(), 1),
(18, 'Harley-Davidson', 1, NOW(), NOW(), 1),
(19, 'CFMoto', 1, NOW(), NOW(), 1),
(20, 'Victory', 1, NOW(), NOW(), 1),
(21, 'Auteco Mobility', 1, NOW(), NOW(), 1),
(22, 'Vespa', 1, NOW(), NOW(), 1),
(23, 'Piaggio', 1, NOW(), NOW(), 1),
(24, 'Aprilia', 1, NOW(), NOW(), 1),
(25, 'Zontes', 1, NOW(), NOW(), 1),
(26, 'Macbor', 1, NOW(), NOW(), 1),
(27, 'Voge', 1, NOW(), NOW(), 1),
(28, 'QJ Motor', 1, NOW(), NOW(), 1),
(29, 'NIU (Eléctricas)', 1, NOW(), NOW(), 1),
(30, 'Super Soco (Eléctricas)', 1, NOW(), NOW(), 1);
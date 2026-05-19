USE tallermoto;

-- ==============================================================================
-- SCRIPT 1: CARGA DE TIPOS DE PRODUCTOS (product_type)
-- ==============================================================================

INSERT IGNORE INTO product_type (id, type, is_active, created_at, updated_at, responsible_user_id) VALUES
(1, 'Aceites de Motor', 1, NOW(), NOW(), 1),
(2, 'Líquidos de Freno y Embrague', 1, NOW(), NOW(), 1),
(3, 'Refrigerantes y Anticongelantes', 1, NOW(), NOW(), 1),
(4, 'Llantas Delanteras', 1, NOW(), NOW(), 1),
(5, 'Llantas Traseras', 1, NOW(), NOW(), 1),
(6, 'Neumáticos y Válvulas', 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno', 1, NOW(), NOW(), 1),
(9, 'Filtros de Aceite', 1, NOW(), NOW(), 1),
(10, 'Filtros de Aire', 1, NOW(), NOW(), 1),
(11, 'Filtros de Gasolina', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre (Cadenas, Piñones, Platos)', 1, NOW(), NOW(), 1),
(13, 'Baterías', 1, NOW(), NOW(), 1),
(14, 'Bujías', 1, NOW(), NOW(), 1),
(15, 'Sistema Eléctrico (CDI, Bobinas, Reguladores)', 1, NOW(), NOW(), 1),
(16, 'Suspensión (Retenedores, Aceites de Barra)', 1, NOW(), NOW(), 1),
(17, 'Rodamientos, Cunas y Balineras', 1, NOW(), NOW(), 1),
(18, 'Guayas (Clutch, Acelerador, Choke)', 1, NOW(), NOW(), 1),
(19, 'Bombillería y Exploradoras', 1, NOW(), NOW(), 1),
(20, 'Carenajes, Tapas y Espejos', 1, NOW(), NOW(), 1),
(21, 'Químicos (Desengrasantes, Lub. Cadena)', 1, NOW(), NOW(), 1),
(22, 'Herramientas y Accesorios', 1, NOW(), NOW(), 1),
(23, 'Empaques y Retenedores de Motor', 1, NOW(), NOW(), 1),
(24, 'Partes de Motor (Pistones, Anillos, Válvulas)', 1, NOW(), NOW(), 1),
(25, 'Tornillería y Varios', 1, NOW(), NOW(), 1);
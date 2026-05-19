USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 1: ACEITES DE MOTOR
-- MARCA: LIQUI MOLY (SOLO ACEITES)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- Línea Street & Race (4 Tiempos)
(1, 'Aceite Liqui Moly Motorbike 4T 10W-40 Street Semi Sintético 1L', 48000, 65000, 'LM-10W40-STR-SS', '10W-40', 'Aceite semisintético de alto rendimiento, excelente limpieza del motor', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 10W-40 Synth Street Race 1L', 72000, 95000, 'LM-10W40-RACE-FS', '10W-40', 'Aceite 100% sintético para máxima potencia y protección extrema', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 15W-50 Street Semi Sintético 1L', 48000, 65000, 'LM-15W50-STR-SS', '15W-50', 'Aceite semisintético, ideal para embragues multidisco bañados en aceite', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 15W-50 Synth Street Race 1L', 75000, 98000, 'LM-15W50-RACE-FS', '15W-50', 'Aceite 100% sintético para condiciones de carrera', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 20W-50 Street Mineral 1L', 42000, 58000, 'LM-20W50-STR-M', '20W-50', 'Aceite mineral premium para motores de 4 tiempos sometidos a cargas normales', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 5W-40 Synth Street Race 1L', 78000, 102000, 'LM-5W40-RACE-FS', '5W-40', 'Aceite 100% sintético para superbikes y altas revoluciones', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike HD Classic SAE 50 Street 1L', 45000, 62000, 'LM-SAE50-HD', 'SAE 50', 'Aceite monogrado especial para motos clásicas tipo Harley-Davidson', 1, NOW(), NOW(), 1),

-- Línea Offroad (4 Tiempos)
(1, 'Aceite Liqui Moly Motorbike 4T 10W-40 Offroad Semi Sintético 1L', 52000, 70000, 'LM-10W40-OFF-SS', '10W-40', 'Aceite semisintético formulado para tierra y enduro', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 10W-50 Synth Offroad Race 1L', 78000, 105000, 'LM-10W50-OFF-FS', '10W-50', 'Aceite 100% sintético para motocross y enduro de alta exigencia', 1, NOW(), NOW(), 1),

-- Línea Scooter (Automáticas)
(1, 'Aceite Liqui Moly Motorbike 4T 10W-40 Scooter Semi Sintético 1L', 46000, 62000, 'LM-10W40-SCO-SS', '10W-40 MB', 'Aceite semisintético específico para scooters (JASO MB)', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 4T 5W-40 Scooter Synth 1L', 68000, 90000, 'LM-5W40-SCO-FS', '5W-40', 'Aceite sintético para maxi-scooters de alto rendimiento', 1, NOW(), NOW(), 1),

-- Línea 2 Tiempos
(1, 'Aceite Liqui Moly Motorbike 2T Street Semi Sintético 1L', 45000, 60000, 'LM-2T-STR-SS', '2T', 'Aceite semisintético para motores de 2 tiempos, baja emisión de humos', 1, NOW(), NOW(), 1),
(1, 'Aceite Liqui Moly Motorbike 2T Synth Offroad Race 1L', 75000, 98000, 'LM-2T-OFF-FS', '2T', 'Aceite 100% sintético para competición 2T (Enduro/Motocross)', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 1: ACEITES DE MOTOR
-- MARCA: MOTUL (EXCLUSIVO)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- Línea 3000 (Mineral)
(1, 'Aceite Motul 3000 4T 20W-50 Mineral 1L', 28000, 38000, 'MT-3000-20W50', '20W-50', 'Aceite mineral con tecnología HC-Tech para protección del motor y caja', 1, NOW(), NOW(), 1),

-- Línea 5100 (Semi Sintético - Technosynthese)
(1, 'Aceite Motul 5100 4T 10W-40 Semi Sintético 1L', 44000, 57000, 'MT-5100-10W40', '10W-40', 'Aceite semisintético con Ester, ideal para uso urbano y carretera', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 5100 4T 15W-50 Semi Sintético 1L', 42000, 55000, 'MT-5100-15W50', '15W-50', 'Aceite semisintético con Ester, el más vendido para motos de media cilindrada', 1, NOW(), NOW(), 1),

-- Línea 7100 (100% Sintético)
(1, 'Aceite Motul 7100 4T 10W-40 Full Sintético 1L', 68000, 88000, 'MT-7100-10W40', '10W-40', 'Aceite 100% sintético con Ester, máxima protección para alto cilindraje', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 7100 4T 10W-50 Full Sintético 1L', 69000, 89000, 'MT-7100-10W50', '10W-50', 'Aceite 100% sintético con Ester, excelente resistencia a altas temperaturas', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 7100 4T 15W-50 Full Sintético 1L', 66000, 86000, 'MT-7100-15W50', '15W-50', 'Aceite 100% sintético con Ester, protección extrema de la caja de cambios', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 7100 4T 20W-50 Full Sintético 1L', 65000, 85000, 'MT-7100-20W50', '20W-50', 'Aceite 100% sintético con Ester, reduce el consumo de aceite en motores grandes', 1, NOW(), NOW(), 1),

-- Línea 300V (Factory Line - Competición)
(1, 'Aceite Motul 300V Factory Line 4T 10W-40 1L', 110000, 145000, 'MT-300V-10W40', '10W-40', 'Aceite 100% sintético con tecnología ESTER Core para máxima potencia', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 300V Factory Line 4T 15W-50 1L', 110000, 145000, 'MT-300V-15W50', '15W-50', 'Aceite 100% sintético ESTER Core para resistencia extrema en resistencia/enduro', 1, NOW(), NOW(), 1),

-- Línea Scooter
(1, 'Aceite Motul Scooter Expert 4T 10W-40 Semi Sintético 1L', 40000, 52000, 'MT-SCO-EXP-10W40', '10W-40 MB', 'Aceite semisintético para scooters automáticas (JASO MB)', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul Scooter Power 4T 5W-40 Full Sintético 1L', 62000, 82000, 'MT-SCO-POW-5W40', '5W-40 MB', 'Aceite 100% sintético para maxi-scooters', 1, NOW(), NOW(), 1),

-- Línea 2 Tiempos
(1, 'Aceite Motul 510 2T Technosynthese 1L', 45000, 60000, 'MT-510-2T', '2T', 'Aceite semisintético para motos 2T, anti-humo', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 710 2T Full Sintético 1L', 65000, 85000, 'MT-710-2T', '2T', 'Aceite 100% sintético con Ester para motores 2T de alto rendimiento', 1, NOW(), NOW(), 1),
(1, 'Aceite Motul 800 2T Factory Line Off Road 1L', 95000, 125000, 'MT-800-2T-OFF', '2T', 'Aceite 100% sintético ESTER Core para motocross/enduro (solo premezcla)', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 1: ACEITES DE MOTOR
-- MARCAS: CASTROL Y MOBIL (EXCLUSIVO)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CASTROL (Líneas Power 1, Actevo, Go! y Scooter)
-- ==========================================
-- Línea Power 1 (Sintéticos y Semi Sintéticos)
(1, 'Aceite Castrol Power 1 Ultimate 10W-40 Full Sintético 1L', 55000, 75000, 'CST-PW1U-10W40', '10W-40', 'Aceite 100% sintético, máxima aceleración y rendimiento extremo', 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Power 1 4T 15W-50 Semi Sintético 1L', 38000, 50000, 'CST-PW1-15W50', '15W-50', 'Fórmula Power Release para optimizar la fricción y proteger el embrague', 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Power 1 4T 10W-40 Semi Sintético 1L', 38000, 50000, 'CST-PW1-10W40', '10W-40', 'Aceite semisintético avanzado para motos de 4 tiempos', 1, NOW(), NOW(), 1),

-- Línea Actevo (Semi Sintético y Mineral)
(1, 'Aceite Castrol Actevo 4T 20W-50 Semi Sintético 1L', 28000, 38000, 'CST-ACT-20W50-SS', '20W-50', 'Moléculas Actibond que se adhieren al motor protegiendo continuamente', 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Actevo 4T 10W-30 Semi Sintético 1L', 28000, 38000, 'CST-ACT-10W30-SS', '10W-30', 'Protección continua ideal para motos Honda y de baja cilindrada', 1, NOW(), NOW(), 1),

-- Línea Go! (Mineral)
(1, 'Aceite Castrol Go! 4T 20W-50 Mineral 1L', 22000, 30000, 'CST-GO-20W50', '20W-50', 'Aceite mineral con Trizone Technology para un manejo sin problemas y urbano', 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Go! 4T 25W-60 Mineral 1L', 23000, 32000, 'CST-GO-25W60', '25W-60', 'Aceite mineral de alta viscosidad ideal para motores con alto kilometraje o desgaste', 1, NOW(), NOW(), 1),

-- Línea Scooter y 2T Castrol
(1, 'Aceite Castrol Power 1 Scooter 4T 10W-40 Semi Sintético 1L', 39000, 52000, 'CST-SCO-10W40', '10W-40 MB', 'Especialmente diseñado para scooters con tecnología Scootek (Baja fricción)', 1, NOW(), NOW(), 1),
(1, 'Aceite Castrol Go! 2T Mineral 1L', 18000, 25000, 'CST-GO-2T', '2T', 'Aceite para motores de 2 tiempos de uso diario', 1, NOW(), NOW(), 1),

-- ==========================================
-- MOBIL (Líneas Mobil 1 Racing y Super Moto)
-- ==========================================
-- Línea Mobil 1 Racing (Full Sintético)
(1, 'Aceite Mobil 1 Racing 4T 10W-40 Full Sintético 1L', 65000, 85000, 'MB-1R-10W40', '10W-40', 'Tecnología sintética avanzada para motos deportivas y altas RPM', 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil 1 Racing 4T 15W-50 Full Sintético 1L', 65000, 85000, 'MB-1R-15W50', '15W-50', 'Máxima protección térmica y resistencia de la película para motores de alto desempeño', 1, NOW(), NOW(), 1),

-- Línea Mobil Super Moto (Semi Sintético y Mineral)
(1, 'Aceite Mobil Super Moto 4T 10W-40 Semi Sintético 1L', 32000, 44000, 'MB-SM-10W40-SS', '10W-40', 'Aceite semisintético que prolonga la vida útil del motor y caja', 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil Super Moto 4T 20W-50 Mineral 1L', 23000, 32000, 'MB-SM-20W50-M', '20W-50', 'Aceite mineral estándar, excelente protección contra el desgaste (Muy popular en motos de trabajo)', 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil Super Moto 4T 10W-30 Mineral 1L', 24000, 33000, 'MB-SM-10W30-M', '10W-30', 'Aceite mineral de baja fricción recomendado para uso urbano y motores modernos', 1, NOW(), NOW(), 1),

-- Línea Scooter y 2T Mobil
(1, 'Aceite Mobil Super Moto Scooter 10W-30 1L', 25000, 35000, 'MB-SCO-10W30', '10W-30 MB', 'Aceite exclusivo para scooters automáticas que reduce la fricción', 1, NOW(), NOW(), 1),
(1, 'Aceite Mobil Super Moto 2T Mineral 1L', 17000, 24000, 'MB-SM-2T', '2T', 'Aceite con tecnología mineral pre-diluida para 2 tiempos', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 1: ACEITES DE MOTOR
-- MARCAS: IPONE Y ORIGINALES OEM (YAMAHA, HONDA, SUZUKI, BAJAJ)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- IPONE (Alta competencia y especialidades)
-- ==========================================
(1, 'Aceite Ipone Katana 10W-40 Full Sintético 1L', 75000, 98000, 'IP-KAT-10W40', '10W-40', 'Aceite con ester optimizado para caja de cambios (Speed & Easy Shift)', 1, NOW(), NOW(), 1),
(1, 'Aceite Ipone R4000 RS 10W-40 Semi Sintético 1L', 52000, 68000, 'IP-R4000-10W40', '10W-40', 'Aceite semisintético de alto rendimiento, excelente protección', 1, NOW(), NOW(), 1),
(1, 'Aceite Ipone 10.4 10W-40 Semi Sintético 1L', 40000, 52000, 'IP-104-10W40', '10W-40', 'Aceite semisintético para uso diario y urbano', 1, NOW(), NOW(), 1),
(1, 'Aceite Ipone Scoot 4 10W-40 Semi Sintético 1L', 42000, 55000, 'IP-SCO4-10W40', '10W-40 MB', 'Fórmula específica para scooters de 4 tiempos', 1, NOW(), NOW(), 1),
(1, 'Aceite Ipone Samourai Racing 2T 1L (Aroma a Fresa)', 85000, 115000, 'IP-SAM-2T', '2T', 'Aceite 100% sintético para motos 2 tiempos de competición con dosificador', 1, NOW(), NOW(), 1),

-- ==========================================
-- MARCAS ORIGINALES OEM (Yamalube, Honda, Suzuki, Bajaj)
-- ==========================================

-- YAMALUBE (Yamaha)
(1, 'Aceite Yamalube 4T 20W-50 Mineral 1L', 24000, 34000, 'YAM-20W50-M', '20W-50', 'Aceite original Yamaha mineral para trabajo pesado', 1, NOW(), NOW(), 1),
(1, 'Aceite Yamalube 4T 10W-40 Semi Sintético 1L', 35000, 48000, 'YAM-10W40-SS', '10W-40', 'Aceite original Yamaha semisintético (Recomendado FZ/MT)', 1, NOW(), NOW(), 1),
(1, 'Aceite Yamalube 4T 10W-40 Full Sintético 1L', 58000, 78000, 'YAM-10W40-FS', '10W-40', 'Aceite original 100% sintético para alta gama Yamaha', 1, NOW(), NOW(), 1),
(1, 'Aceite Yamalube 2T Mineral 1L', 22000, 32000, 'YAM-2T-M', '2T', 'Aceite original Yamaha para motores de 2 tiempos', 1, NOW(), NOW(), 1),

-- HONDA (Genuine Oil)
(1, 'Aceite Honda Genuine 10W-30 Mineral 1L', 22000, 32000, 'HON-10W30-M', '10W-30', 'Aceite original Honda para motores de baja fricción', 1, NOW(), NOW(), 1),
(1, 'Aceite Honda Genuine 10W-30 Semi Sintético 1L', 32000, 45000, 'HON-10W30-SS', '10W-30', 'Aceite original Honda con mayor durabilidad y protección térmica', 1, NOW(), NOW(), 1),
(1, 'Aceite Honda Genuine 10W-30 Full Sintético 1L', 55000, 75000, 'HON-10W30-FS', '10W-30', 'Máxima protección para motores Honda de alto desempeño', 1, NOW(), NOW(), 1),

-- SUZUKI (Ecstar)
(1, 'Aceite Suzuki Ecstar R7000 10W-40 Semi Sintético 1L', 34000, 46000, 'SUZ-ECS-10W40-SS', '10W-40', 'Aceite original recomendado para Gixxer y V-Strom', 1, NOW(), NOW(), 1),
(1, 'Aceite Suzuki Ecstar R9000 10W-40 Full Sintético 1L', 62000, 82000, 'SUZ-ECS-10W40-FS', '10W-40', 'Aceite sintético de máximo rendimiento para motores Suzuki', 1, NOW(), NOW(), 1),

-- BAJAJ (DTS-i)
(1, 'Aceite Bajaj DTS-i 20W-50 Mineral 1L', 26000, 36000, 'BAJ-DTS-20W50', '20W-50', 'Aceite original formulado para máxima protección en motores Pulsar y Dominar', 1, NOW(), NOW(), 1),
(1, 'Aceite Bajaj DTS-i 10W-30 Mineral 1L', 24000, 34000, 'BAJ-DTS-10W30', '10W-30', 'Aceite original recomendado para Boxer y Discover', 1, NOW(), NOW(), 1);




USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 2: LÍQUIDOS DE FRENO Y EMBRAGUE
-- TODAS LAS MARCAS INTEGRADAS
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- MOTUL (Gama completa DOT y Racing)
-- ==========================================
(2, 'Líquido de Frenos Motul DOT 3 & 4 500ml', 32000, 45000, 'MT-DOT4-500', 'DOT 4', 'Fluido 100% sintético para circuitos de freno y embrague', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Motul DOT 5.1 500ml', 45000, 62000, 'MT-DOT51-500', 'DOT 5.1', 'Fluido de larga duración, ideal para sistemas ABS', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Motul RBF 600 Factory Line 500ml', 75000, 98000, 'MT-RBF600', 'RBF 600', 'Líquido de frenos de competición, punto de ebullición ultra alto (312°C)', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Motul RBF 660 Factory Line 500ml', 85000, 115000, 'MT-RBF660', 'RBF 660', 'Líquido de frenos extremo para competición (325°C)', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Motul RBF 700 Factory Line 500ml', 110000, 145000, 'MT-RBF700', 'RBF 700', 'El punto de ebullición más alto del mercado (336°C) para MotoGP/WSBK', 1, NOW(), NOW(), 1),

-- ==========================================
-- LIQUI MOLY (Tecnología Alemana)
-- ==========================================
(2, 'Líquido de Frenos Liqui Moly DOT 4 250ml', 22000, 32000, 'LM-DOT4-250', 'DOT 4', 'Fluido sintético alemán de alto rendimiento', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Liqui Moly DOT 4 500ml', 38000, 52000, 'LM-DOT4-500', 'DOT 4', 'Líquido de frenos excelente protección contra bloqueo por vapor', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Liqui Moly DOT 5.1 250ml', 28000, 38000, 'LM-DOT51-250', 'DOT 5.1', 'Líquido de frenos especial para sistemas ABS modernos', 1, NOW(), NOW(), 1),

-- ==========================================
-- BREMBO (Especialistas en frenado)
-- ==========================================
(2, 'Líquido de Frenos Brembo Premium DOT 4 500ml', 42000, 58000, 'BRM-DOT4-500', 'DOT 4', 'Fluido de frenos premium Brembo, baja viscosidad', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Brembo Premium DOT 5.1 500ml', 55000, 75000, 'BRM-DOT51-500', 'DOT 5.1', 'Ideal para motocicletas de alto rendimiento y uso en pista', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Brembo LCF 600 Plus Racing 500ml', 95000, 130000, 'BRM-LCF600', 'LCF 600+', 'Líquido exclusivo para competición y trackdays', 1, NOW(), NOW(), 1),

-- ==========================================
-- BOSCH (Líder en sistemas ABS y confiabilidad)
-- ==========================================
(2, 'Líquido de Frenos Bosch DOT 3 250ml', 12000, 18000, 'BOS-DOT3-250', 'DOT 3', 'Fluido convencional confiable para motos de baja cilindrada', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Bosch DOT 4 250ml', 16000, 24000, 'BOS-DOT4-250', 'DOT 4', 'Líquido sintético, excelente relación calidad-precio', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Bosch DOT 4 500ml', 25000, 35000, 'BOS-DOT4-500', 'DOT 4', 'Envase grande para purgado completo de frenos delanteros y traseros', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Bosch ENV6 (DOT 4 / 5.1 Compatible) 500ml', 45000, 65000, 'BOS-ENV6-500', 'ENV6', 'Tecnología avanzada que reemplaza DOT 3, 4 y 5.1', 1, NOW(), NOW(), 1),

-- ==========================================
-- CASTROL & IPONE
-- ==========================================
(2, 'Líquido de Frenos Castrol React Performance DOT 4 500ml', 35000, 48000, 'CST-DOT4-500', 'DOT 4', 'Líquido sintético de alta temperatura formulado para motos deportivas', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Ipone Brake Fluid DOT 4 500ml', 38000, 52000, 'IP-DOT4-500', 'DOT 4', 'Fluido 100% sintético para frenos y embragues hidráulicos', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Ipone Brake 300 Racing 500ml', 78000, 105000, 'IP-BR300-500', 'Racing', 'Líquido de competición, punto de ebullición seco muy alto', 1, NOW(), NOW(), 1),

-- ==========================================
-- REPSOL & EBC
-- ==========================================
(2, 'Líquido de Frenos Repsol Moto DOT 4 500ml', 28000, 38000, 'REP-DOT4-500', 'DOT 4', 'Máximo rendimiento y estabilidad térmica de Repsol', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos EBC Brakes BF307+ DOT 4 500ml', 45000, 65000, 'EBC-BF307-500', 'DOT 4+', 'Líquido de alto rendimiento diseñado para trabajar con pastillas EBC', 1, NOW(), NOW(), 1),

-- ==========================================
-- MARCAS ORIGINALES OEM
-- ==========================================
(2, 'Líquido de Frenos Yamalube DOT 4 500ml', 32000, 45000, 'YAM-DOT4-500', 'DOT 4', 'Fluido original de fábrica para motocicletas Yamaha', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Honda Genuine DOT 4 250ml', 25000, 35000, 'HON-DOT4-250', 'DOT 4', 'Fluido original recomendado para sistemas Honda', 1, NOW(), NOW(), 1),

-- ==========================================
-- MARCAS DE ENTRADA / ECONÓMICAS
-- ==========================================
(2, 'Líquido de Frenos Simoniz DOT 3 200ml', 8000, 13000, 'SIM-DOT3-200', 'DOT 3', 'Líquido económico ideal para motos de trabajo (Tambor delantero / Disco sencillo)', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Wagner DOT 4 250ml', 14000, 20000, 'WAG-DOT4-250', 'DOT 4', 'Calidad de equipo original a bajo costo', 1, NOW(), NOW(), 1),
(2, 'Líquido de Frenos Qualid DOT 3 300ml', 6000, 10000, 'QLD-DOT3-300', 'DOT 3', 'Opción económica de entrada para mantenimientos de bajo presupuesto', 1, NOW(), NOW(), 1);



USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 3: REFRIGERANTES Y ANTICONGELANTES
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- MOTUL (Líder en refrigerantes de alto rendimiento)
-- ==========================================
(3, 'Refrigerante Motul Motocool Expert 1L (Amarillo)', 42000, 58000, 'MT-COOL-EXP-1L', 'Híbrido', 'Refrigerante anticorrosión y anticongelante listo para usar (hasta -37°C / 135°C)', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Motul Motocool Factory Line 1L (Rojo)', 55000, 75000, 'MT-COOL-FL-1L', 'Orgánico OAT', 'Refrigerante de competición, reduce la temperatura del motor eficientemente', 1, NOW(), NOW(), 1),
(3, 'Aditivo Refrigerante Motul MoCool 500ml', 65000, 85000, 'MT-MOCOOL-500', 'Aditivo', 'Aditivo concentrado para reducir la temperatura del sistema de refrigeración hasta 15°C', 1, NOW(), NOW(), 1),

-- ==========================================
-- LIQUI MOLY (Tecnología Alemana)
-- ==========================================
(3, 'Refrigerante Liqui Moly Motorbike Coolant Ready Mix 1L', 45000, 62000, 'LM-COOL-RM-1L', 'Ready Mix', 'Refrigerante listo para usar, excelente disipación térmica para motos', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Liqui Moly G12 Plus 1L (Rojo)', 46000, 64000, 'LM-G12-1L', 'G12+ Orgánico', 'Anticongelante/Refrigerante de larga duración (OAT)', 1, NOW(), NOW(), 1),
(3, 'Limpiador de Radiador Liqui Moly Radiator Cleaner 150ml', 25000, 35000, 'LM-RAD-CLEAN', 'Limpiador', 'Elimina depósitos calcáreos y aceite del sistema de refrigeración', 1, NOW(), NOW(), 1),

-- ==========================================
-- IPONE Y CASTROL
-- ==========================================
(3, 'Refrigerante Ipone Radiator Liquid 1L (Azul)', 44000, 60000, 'IP-RAD-LIQ-1L', 'Radiator Liquid', 'Refrigerante listo para usar, garantiza una refrigeración óptima del motor', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Castrol Radicool SF Premix 1L (Rosa)', 38000, 52000, 'CST-RAD-SF-1L', 'Radicool SF', 'Refrigerante de larga vida útil basado en tecnología de ácidos orgánicos', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Castrol Radicool Motorcycle 1L', 35000, 48000, 'CST-RAD-MOTO-1L', 'Motorcycle', 'Fluido refrigerante premezclado especial para motos', 1, NOW(), NOW(), 1),

-- ==========================================
-- MARCAS ORIGINALES OEM (Yamaha, Honda, KTM)
-- ==========================================
(3, 'Refrigerante Yamalube Pre-Mixed 1L', 32000, 45000, 'YAM-COOL-1L', 'Original', 'Refrigerante original de fábrica para motocicletas Yamaha (NMAX, MT, R3)', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Honda Genuine Type 2 (Azul) 1L', 35000, 48000, 'HON-COOL-T2', 'Type 2', 'Refrigerante original de larga duración para motos Honda', 1, NOW(), NOW(), 1),
(3, 'Refrigerante KTM Motorex Coolant M3.0 1L (Rosa)', 58000, 80000, 'KTM-MOT-M30', 'M3.0', 'Refrigerante original recomendado para motocicletas KTM y Husqvarna', 1, NOW(), NOW(), 1),

-- ==========================================
-- REPSOL Y BOSCH
-- ==========================================
(3, 'Refrigerante Repsol Moto Coolant 50% 1L', 30000, 42000, 'REP-COOL-50-1L', '50% Orgánico', 'Refrigerante de uso directo formulado para sistemas de circuito cerrado', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Bosch Coolant Orgánico 1L (Rojo)', 28000, 38000, 'BOS-COOL-ORG', 'Orgánico', 'Protección eficaz contra la corrosión y el sobrecalentamiento', 1, NOW(), NOW(), 1),

-- ==========================================
-- MARCAS DE ENTRADA / ECONÓMICAS (Alta rotación)
-- ==========================================
(3, 'Refrigerante Qualid Verde 1L', 10000, 16000, 'QLD-REF-V-1L', 'Estándar', 'Refrigerante económico de uso general para motos de trabajo', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Qualid Rojo (Larga Vida) 1L', 12000, 18000, 'QLD-REF-R-1L', 'Larga Vida', 'Refrigerante económico con mejoradores de protección', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Simoniz Moto Coolant Verde 1L', 14000, 22000, 'SIM-COOL-V-1L', 'Moto Coolant', 'Evita la oxidación y previene el recalentamiento', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Simoniz Moto Coolant Rojo 1L', 15000, 24000, 'SIM-COOL-R-1L', 'Moto Coolant', 'Refrigerante formulado con tecnología OAT', 1, NOW(), NOW(), 1),
(3, 'Refrigerante Freezetone Rojo 1 Galón (3.78L)', 25000, 38000, 'FREEZ-ROJO-GAL', 'Galón', 'Formato económico en galón para purgas completas de radiador', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 4: LLANTAS DELANTERAS
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- MICHELIN (Alta Gama Pistera y Adventure)
-- ==========================================
(4, 'Llanta Michelin Pilot Street 2 90/90-17 (Delantera)', 175000, 230000, 'MIC-PS2-909017', '90/90-17', 'Uso urbano/calle, excelente evacuación de agua y agarre en mojado', 1, NOW(), NOW(), 1),
(4, 'Llanta Michelin Pilot Street 2 110/70-17 (Delantera)', 220000, 290000, 'MIC-PS2-1107017', '110/70-17', 'Uso urbano/calle deportivo, medida ideal para Yamaha MT03 o FZ25', 1, NOW(), NOW(), 1),
(4, 'Llanta Michelin Road 5 120/70-ZR17 (Delantera)', 480000, 620000, 'MIC-RD5-1207017', '120/70-ZR17', 'Llanta radial Sport Touring de alto rendimiento (Bi-compuesto)', 1, NOW(), NOW(), 1),
(4, 'Llanta Michelin Anakee Adventure 90/90-21 (Delantera)', 450000, 580000, 'MIC-ANA-909021', '90/90-21', 'Llanta 80% Asfalto / 20% Off-Road para motos Trail y Adventure', 1, NOW(), NOW(), 1),
(4, 'Llanta Michelin City Grip 2 110/70-13 (Delantera)', 210000, 275000, 'MIC-CG2-1107013', '110/70-13', 'La mejor llanta para Scooters (NMAX), sílice para clima húmedo', 1, NOW(), NOW(), 1),

-- ==========================================
-- PIRELLI (Deportividad y Durabilidad)
-- ==========================================
(4, 'Llanta Pirelli Diablo Rosso III 110/70-R17 (Delantera)', 290000, 380000, 'PIR-DR3-1107017', '110/70-R17', 'Llanta radial deportiva, máxima adherencia y agilidad en curvas', 1, NOW(), NOW(), 1),
(4, 'Llanta Pirelli Diablo Rosso Sport 100/80-17 (Delantera)', 210000, 280000, 'PIR-DRS-1008017', '100/80-17', 'Deportividad para motos de media y baja cilindrada (Pulsar NS, FZ)', 1, NOW(), NOW(), 1),
(4, 'Llanta Pirelli Angel City 90/90-17 (Delantera)', 180000, 240000, 'PIR-ANG-909017', '90/90-17', 'Alta durabilidad para uso diario en ciudad, resistente a pinchazos', 1, NOW(), NOW(), 1),
(4, 'Llanta Pirelli Scorpion Trail II 110/80-R19 (Delantera)', 460000, 600000, 'PIR-SCO-1108019', '110/80-R19', 'Doble propósito premium, referencia estándar para V-Strom o BMW GS', 1, NOW(), NOW(), 1),

-- ==========================================
-- METZELER (Alemana, Especialistas en motos)
-- ==========================================
(4, 'Llanta Metzeler Tourance 110/80-19 (Delantera)', 350000, 460000, 'MET-TOU-1108019', '110/80-19', 'Doble propósito legendaria, excelente kilometraje y estabilidad', 1, NOW(), NOW(), 1),
(4, 'Llanta Metzeler Tourance 90/90-21 (Delantera)', 320000, 420000, 'MET-TOU-909021', '90/90-21', 'Doble propósito para rines 21, excelente agarre en asfalto y destapado', 1, NOW(), NOW(), 1),
(4, 'Llanta Metzeler Sportec M5 110/70-17 (Delantera)', 270000, 350000, 'MET-M5-1107017', '110/70-17', 'Llanta deportiva con tecnología Interact para múltiples zonas de tensión', 1, NOW(), NOW(), 1),

-- ==========================================
-- TIMSUN (Excelente relación Calidad/Precio)
-- ==========================================
(4, 'Llanta Timsun TS-822 90/90-19 (Delantera)', 145000, 185000, 'TIM-822-909019', '90/90-19', 'Doble propósito 50/50, labrado ideal para Honda XR 150L y Yamaha XTZ', 1, NOW(), NOW(), 1),
(4, 'Llanta Timsun TS-659 110/70-17 (Delantera)', 160000, 210000, 'TIM-659-1107017', '110/70-17', 'Llanta pistera de compuesto medio/blando, excelente agarre', 1, NOW(), NOW(), 1),
(4, 'Llanta Timsun TS-823 2.75-18 (Delantera)', 110000, 145000, 'TIM-823-27518', '2.75-18', 'Llanta de trabajo y rural agresiva para vías destapadas', 1, NOW(), NOW(), 1),

-- ==========================================
-- DUNLOP & KENDA
-- ==========================================
(4, 'Llanta Dunlop Arrowmax GT601 110/70-17 (Delantera)', 240000, 310000, 'DUN-GT601-1107017', '110/70-17', 'Pistera duradera con gran estabilidad a altas velocidades', 1, NOW(), NOW(), 1),
(4, 'Llanta Dunlop D605 2.75-21 (Delantera)', 180000, 240000, 'DUN-D605-27521', '2.75-21', 'Enduro homologada para calle (XTZ 125, DR 150)', 1, NOW(), NOW(), 1),
(4, 'Llanta Kenda K761 90/90-21 (Delantera)', 130000, 175000, 'KEN-K761-909021', '90/90-21', 'Llanta doble propósito Off-Road/On-Road económica', 1, NOW(), NOW(), 1),
(4, 'Llanta Kenda K784F Big Block 110/80-19 (Delantera)', 260000, 340000, 'KEN-K784-1108019', '110/80-19', 'Tacos agresivos para aventura extrema (Big Trail)', 1, NOW(), NOW(), 1),

-- ==========================================
-- MRF & CORSA (Alta rotación motos de trabajo/urbano)
-- ==========================================
(4, 'Llanta MRF Zapper FS 2.75-18 (Delantera)', 85000, 115000, 'MRF-ZAP-27518', '2.75-18', 'Llanta de trabajo urbano de larga duración (NKD, Boxer, YBR)', 1, NOW(), NOW(), 1),
(4, 'Llanta MRF Nylogrip Zapper 90/90-17 (Delantera)', 95000, 130000, 'MRF-NYL-909017', '90/90-17', 'Diseño de líneas rectas, alta dirección en calle', 1, NOW(), NOW(), 1),
(4, 'Llanta Corsa Platinum S123 90/90-17 (Delantera)', 105000, 140000, 'COR-S123-909017', '90/90-17', 'Compuesto blando urbano para excelente frenado', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 5: LLANTAS TRASERAS
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- MICHELIN (Alta Gama Pistera y Adventure)
-- ==========================================
(5, 'Llanta Michelin Pilot Street 2 130/70-17 (Trasera)', 240000, 310000, 'MIC-PS2-1307017', '130/70-17', 'Excelente duración y agarre en mojado (Ideal FZ, Gixxer, NS200)', 1, NOW(), NOW(), 1),
(5, 'Llanta Michelin Pilot Street 2 140/70-17 (Trasera)', 255000, 330000, 'MIC-PS2-1407017', '140/70-17', 'Perfil más ancho para mayor estabilidad (MT03, R3, Ninja 400)', 1, NOW(), NOW(), 1),
(5, 'Llanta Michelin Road 5 180/55-ZR17 (Trasera)', 680000, 850000, 'MIC-RD5-1805517', '180/55-ZR17', 'Radial Sport Touring, bi-compuesto para alta cilindrada (MT09, Z900)', 1, NOW(), NOW(), 1),
(5, 'Llanta Michelin Anakee Adventure 150/70-R17 (Trasera)', 650000, 820000, 'MIC-ANA-1507017', '150/70-R17', 'Doble propósito premium 80% calle / 20% off-road', 1, NOW(), NOW(), 1),
(5, 'Llanta Michelin City Grip 2 130/70-13 (Trasera)', 230000, 290000, 'MIC-CG2-1307013', '130/70-13', 'La llanta referente para Scooters tipo Yamaha NMAX', 1, NOW(), NOW(), 1),

-- ==========================================
-- PIRELLI (Deportividad y Durabilidad)
-- ==========================================
(5, 'Llanta Pirelli Diablo Rosso III 150/60-R17 (Trasera)', 340000, 450000, 'PIR-DR3-1506017', '150/60-R17', 'Llanta radial deportiva, excelente inclinación y agarre', 1, NOW(), NOW(), 1),
(5, 'Llanta Pirelli Diablo Rosso Sport 130/70-17 (Trasera)', 230000, 295000, 'PIR-DRS-1307017', '130/70-17', 'Perfil deportivo para motos de baja/media cilindrada', 1, NOW(), NOW(), 1),
(5, 'Llanta Pirelli Angel City 130/70-17 (Trasera)', 190000, 250000, 'PIR-ANG-1307017', '130/70-17', 'Alta durabilidad y resistencia a pinchazos para uso urbano', 1, NOW(), NOW(), 1),
(5, 'Llanta Pirelli Scorpion Trail II 150/70-R17 (Trasera)', 620000, 790000, 'PIR-SCO-1507017', '150/70-R17', 'Referencia estándar para motos Adventure (V-Strom, BMW GS)', 1, NOW(), NOW(), 1),

-- ==========================================
-- METZELER (Alemana, Especialistas en motos)
-- ==========================================
(5, 'Llanta Metzeler Tourance 150/70-R17 (Trasera)', 480000, 620000, 'MET-TOU-1507017', '150/70-R17', 'Doble propósito legendaria, excelente kilometraje y resistencia', 1, NOW(), NOW(), 1),
(5, 'Llanta Metzeler Tourance 130/80-17 (Trasera)', 320000, 410000, 'MET-TOU-1308017', '130/80-17', 'Medida ideal para motos doble propósito de cilindraje medio', 1, NOW(), NOW(), 1),
(5, 'Llanta Metzeler Sportec M5 150/60-17 (Trasera)', 330000, 430000, 'MET-M5-1506017', '150/60-17', 'Tecnología Interact, agarre superior en asfalto seco y mojado', 1, NOW(), NOW(), 1),

-- ==========================================
-- TIMSUN (Excelente relación Calidad/Precio)
-- ==========================================
(5, 'Llanta Timsun TS-822 110/90-17 (Trasera)', 165000, 215000, 'TIM-822-1109017', '110/90-17', 'Doble propósito 50/50, labrado profundo (XR 150L, XTZ 150)', 1, NOW(), NOW(), 1),
(5, 'Llanta Timsun TS-659 130/70-17 (Trasera)', 180000, 235000, 'TIM-659-1307017', '130/70-17', 'Compuesto medio/blando urbano, diseño pistero', 1, NOW(), NOW(), 1),
(5, 'Llanta Timsun TS-823 90/90-18 (Trasera)', 110000, 145000, 'TIM-823-909018', '90/90-18', 'Labrado de trabajo y rural para vías destapadas', 1, NOW(), NOW(), 1),

-- ==========================================
-- DUNLOP & KENDA
-- ==========================================
(5, 'Llanta Dunlop Arrowmax GT601 130/70-17 (Trasera)', 260000, 340000, 'DUN-GT601-1307017', '130/70-17', 'Diseño clásico pistero, gran kilometraje', 1, NOW(), NOW(), 1),
(5, 'Llanta Dunlop D605 4.10-18 (Trasera)', 210000, 275000, 'DUN-D605-41018', '4.10-18', 'Enduro homologada para asfalto, tracción en todo terreno', 1, NOW(), NOW(), 1),
(5, 'Llanta Kenda K761 130/80-17 (Trasera)', 175000, 230000, 'KEN-K761-1308017', '130/80-17', 'Doble propósito económica y versátil', 1, NOW(), NOW(), 1),
(5, 'Llanta Kenda K784 Big Block 150/70-17 (Trasera)', 320000, 410000, 'KEN-K784-1507017', '150/70-17', 'Tacos agresivos, orientada 60% Off-Road / 40% Asfalto', 1, NOW(), NOW(), 1),

-- ==========================================
-- MRF & CORSA (Alta rotación motos de trabajo/urbano)
-- ==========================================
(5, 'Llanta MRF Zapper 90/90-18 (Trasera)', 95000, 130000, 'MRF-ZAP-909018', '90/90-18', 'Llanta de trabajo urbano de larga duración (NKD, YBR, CB125F)', 1, NOW(), NOW(), 1),
(5, 'Llanta MRF Zapper C 130/70-17 (Trasera)', 150000, 195000, 'MRF-ZAPC-1307017', '130/70-17', 'Llanta pistera económica para motos de 150cc a 200cc', 1, NOW(), NOW(), 1),
(5, 'Llanta Corsa Platinum S123 130/70-17 (Trasera)', 160000, 210000, 'COR-S123-1307017', '130/70-17', 'Compuesto blando urbano para excelente evacuación de agua', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 6: NEUMÁTICOS Y VÁLVULAS
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- MICHELIN (Airstop - Premium)
-- ==========================================
(6, 'Neumático Michelin Airstop 17" (2.75/3.00-17)', 35000, 48000, 'MIC-AIR-17', '17 Pulgadas', 'Neumático premium de butilo, máxima retención de aire', 1, NOW(), NOW(), 1),
(6, 'Neumático Michelin Airstop 18" (2.75/3.00-18)', 36000, 49000, 'MIC-AIR-18', '18 Pulgadas', 'Alta resistencia a pinchazos para motos de trabajo y calle', 1, NOW(), NOW(), 1),
(6, 'Neumático Michelin Airstop 21" (2.75/3.00-21)', 38000, 52000, 'MIC-AIR-21', '21 Pulgadas', 'Neumático reforzado para llanta delantera Off-Road/Enduro', 1, NOW(), NOW(), 1),

-- ==========================================
-- IRC (Heavy Duty - Alta Resistencia Japonesa)
-- ==========================================
(6, 'Neumático IRC Heavy Duty 17" (130/70-17 / 140/70-17)', 45000, 60000, 'IRC-HD-17', '17 Ancho', 'Neumático de 3mm de grosor para llantas anchas pisteras', 1, NOW(), NOW(), 1),
(6, 'Neumático IRC Heavy Duty 18" (4.10/4.60-18)', 48000, 65000, 'IRC-HD-18-ANC', '18 Ancho', 'Grosor extra para motos de enduro y trocha pesada', 1, NOW(), NOW(), 1),
(6, 'Neumático IRC Standard 19" (90/90-19)', 32000, 45000, 'IRC-STD-19', '19 Pulgadas', 'Calidad original para llanta delantera (XR150, XTZ)', 1, NOW(), NOW(), 1),

-- ==========================================
-- KENDA (Tuff Tube y Standard)
-- ==========================================
(6, 'Neumático Kenda Tuff Tube 21" (80/100-21)', 42000, 58000, 'KEN-TUFF-21', '21 Pulgadas', 'Tubo extra grueso resistente a pellizcos para motocross', 1, NOW(), NOW(), 1),
(6, 'Neumático Kenda Tuff Tube 18" (100/100-18)', 45000, 60000, 'KEN-TUFF-18', '18 Pulgadas', 'Tubo de competición off-road', 1, NOW(), NOW(), 1),
(6, 'Neumático Kenda Standard 17" (2.75/3.00-17)', 22000, 32000, 'KEN-STD-17', '17 Pulgadas', 'Excelente calidad a precio moderado', 1, NOW(), NOW(), 1),

-- ==========================================
-- MRF & TIMSUN (Alta rotación motos de trabajo y calle)
-- ==========================================
(6, 'Neumático MRF 18" (2.75/3.00-18)', 18000, 25000, 'MRF-NEU-18', '18 Pulgadas', 'El neumático más usado en motos de trabajo (Boxer CT100, NKD 125)', 1, NOW(), NOW(), 1),
(6, 'Neumático MRF 17" (2.75/3.00-17)', 18000, 25000, 'MRF-NEU-17', '17 Pulgadas', 'Neumático duradero para trabajo urbano', 1, NOW(), NOW(), 1),
(6, 'Neumático Timsun 19" (90/90-19)', 24000, 34000, 'TIM-NEU-19', '19 Pulgadas', 'Alta elasticidad y retención de aire', 1, NOW(), NOW(), 1),
(6, 'Neumático Timsun 17" (110/90-17)', 25000, 35000, 'TIM-NEU-17-ANC', '17 Ancho', 'Ideal para llantas traseras doble propósito', 1, NOW(), NOW(), 1),

-- ==========================================
-- VÁLVULAS (Tubeless y Accesorios)
-- ==========================================
(6, 'Válvula Tubeless TR412 (Corta) para Moto', 3000, 8000, 'VAL-TR412', 'TR412', 'Válvula de caucho estándar para llantas sellomatic (Scooters y pisteras)', 1, NOW(), NOW(), 1),
(6, 'Válvula Tubeless TR413 (Larga) para Moto', 3500, 8000, 'VAL-TR413', 'TR413', 'Válvula estándar de caucho de caña larga', 1, NOW(), NOW(), 1),
(6, 'Válvula Tubeless Aluminio CNC a 90 Grados (Par)', 25000, 45000, 'VAL-CNC-90', '90 Grados', 'Válvulas anguladas de aluminio anodizado para facilitar el inflado en discos grandes', 1, NOW(), NOW(), 1),
(6, 'Válvula Tubeless Aluminio CNC a 90 Grados (Unidad)', 13000, 25000, 'VAL-CNC-90-UNI', '90 Grados', 'Válvula angulada para rin delantero o trasero', 1, NOW(), NOW(), 1),
(6, 'Cerraja / Gusanillo para Válvula (Paquete x10)', 5000, 15000, 'VAL-GUS-10', 'Gusanillo', 'Núcleo de retención de aire de repuesto', 1, NOW(), NOW(), 1),
(6, 'Tapa Válvula de Aluminio CNC de Lujo (Par)', 5000, 12000, 'VAL-TAP-CNC', 'Tapas CNC', 'Tapas metálicas de lujo con o-ring interno para proteger el gusanillo', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 7 Y 8: PASTILLAS Y BANDAS DE FRENO
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 7: PASTILLAS DE FRENO
-- ==========================================
-- BREMBO (Alta Gama y Pista)
(7, 'Pastillas de Freno Brembo Carbon Cerámica (Delanteras)', 95000, 135000, 'BRM-CC-DEL', 'Carbon Ceramic', 'Frenado progresivo y gran durabilidad para motos de calle', 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Brembo Sinterizadas SA (Delanteras)', 140000, 185000, 'BRM-SA-DEL', 'Sinterizada SA', 'Alto coeficiente de fricción para motos deportivas y alto cilindraje', 1, NOW(), NOW(), 1),
(7, 'Pastillas de Freno Brembo Sinterizadas SP (Traseras)', 120000, 160000, 'BRM-SP-TRA', 'Sinterizada SP', 'Compuesto específico para el freno trasero, excelente estabilidad', 1, NOW(), NOW(), 1),

-- EBC BRAKES (Rendimiento Premium)
(7, 'Pastillas EBC FA213 Orgánicas / Kevlar', 55000, 75000, 'EBC-FA213-ORG', 'FA213 Orgánica', 'Pastillas en Kevlar/Orgánicas, excelente tacto y no rayan el disco', 1, NOW(), NOW(), 1),
(7, 'Pastillas EBC FA213HH Sinterizadas Doble H', 115000, 155000, 'EBC-FA213-HH', 'FA213 Sinterizada', 'Pastillas Doble H de ultra alta fricción, frenado extremo', 1, NOW(), NOW(), 1),
(7, 'Pastillas EBC FA142HH Sinterizadas Doble H', 110000, 150000, 'EBC-FA142-HH', 'FA142 Sinterizada', 'Ideales para Yamaha y motos pisteras de media cilindrada', 1, NOW(), NOW(), 1),

-- ICHIBAN (Tecnología Cerámica - Excelente Costo/Beneficio)
(7, 'Pastillas Ichiban Cerámica FZ 2.0 / Gixxer (Delanteras)', 22000, 35000, 'ICH-FZ2-CER', 'Cerámica', 'Compuesto cerámico sin asbesto, cuidan el disco y no hacen ruido', 1, NOW(), NOW(), 1),
(7, 'Pastillas Ichiban Cerámica Pulsar NS 200 (Delanteras)', 24000, 38000, 'ICH-NS200-DEL-CER', 'Cerámica', 'Alta resistencia a la temperatura para manejo urbano agresivo', 1, NOW(), NOW(), 1),
(7, 'Pastillas Ichiban Cerámica NMAX 155 (Traseras)', 23000, 36000, 'ICH-NMAX-TRA-CER', 'Cerámica', 'Pastilla de larga vida útil para scooters de alto desempeño', 1, NOW(), NOW(), 1),

-- ORIGINALES OEM (Yamaha, Honda, Bajaj)
(7, 'Pastillas Originales Yamaha NMAX (Delanteras)', 45000, 65000, 'YAM-NMAX-DEL', 'Original', 'Pastillas OEM Yamaha, máxima garantía de frenado', 1, NOW(), NOW(), 1),
(7, 'Pastillas Originales Yamaha FZ25 (Delanteras)', 48000, 68000, 'YAM-FZ25-DEL', 'Original', 'Compuesto original de fábrica para la serie FZ', 1, NOW(), NOW(), 1),
(7, 'Pastillas Originales Honda CB190R (Delanteras)', 42000, 60000, 'HON-CB190-DEL', 'Original', 'Pastillas Nissin originales de equipo de fábrica', 1, NOW(), NOW(), 1),
(7, 'Pastillas Originales Bajaj Pulsar NS 200 (Delanteras Bybre)', 35000, 50000, 'BAJ-NS200-DEL', 'Original', 'Pastillas Bybre de fábrica para máxima compatibilidad', 1, NOW(), NOW(), 1),

-- REVO & MARCAS DE ENTRADA
(7, 'Pastillas Revo Orgánicas NS 200 / Duke 200 (Delanteras)', 18000, 28000, 'REV-NS200-OD', 'Orgánica', 'Económicas, duraderas y de uso urbano diario', 1, NOW(), NOW(), 1),
(7, 'Pastillas Revo Orgánicas AKT CR4 / NKD (Delanteras)', 15000, 24000, 'REV-CR4-DEL', 'Orgánica', 'Pastillas de entrada con gran relación precio/duración', 1, NOW(), NOW(), 1),


-- ==========================================
-- CATEGORÍA 8: BANDAS DE FRENO (Frenos de Tambor)
-- ==========================================
-- INCOLBEST (Líder en Colombia)
(8, 'Bandas de Freno Incolbest Honda Wave / C100', 18000, 26000, 'INC-BAN-WAV', 'Premium', 'Bandas con asbesto libre, alto coeficiente de fricción', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Incolbest AKT NKD 125 / Boxer CT 100', 19000, 28000, 'INC-BAN-NKD', 'Premium', 'La banda de mayor duración y frenado para motos de trabajo', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Incolbest Yamaha BWS 125 (Traseras)', 22000, 32000, 'INC-BAN-BWS', 'Premium', 'Ajuste perfecto y frenado seguro para scooters', 1, NOW(), NOW(), 1),

-- ORIGINALES OEM
(8, 'Bandas de Freno Originales Yamaha FZ16 (Traseras)', 35000, 50000, 'YAM-BAN-FZ', 'Original', 'Zapatas originales Yamaha con resortes incluidos', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Originales Honda XR 150L (Traseras)', 32000, 45000, 'HON-BAN-XR', 'Original', 'Zapatas OEM para óptimo frenado en destapado', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Originales Suzuki GN 125 (Traseras)', 28000, 40000, 'SUZ-BAN-GN', 'Original', 'Reemplazo original de fábrica para zapatas traseras', 1, NOW(), NOW(), 1),

-- REVO & ECONÓMICAS
(8, 'Bandas de Freno Revo AKT NKD / Evo 125', 12000, 20000, 'REV-BAN-NKD', '130mm', 'Banda estándar de campana, solución económica y efectiva', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Revo Boxer CT 100 / Platina', 12000, 20000, 'REV-BAN-BOX', '110mm', 'Zapatas económicas de uso diario masivo', 1, NOW(), NOW(), 1),
(8, 'Bandas de Freno Qualid Universales 130mm', 10000, 16000, 'QLD-BAN-130', '130mm Estándar', 'La opción más accesible para recambio rápido', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 9, 10 y 11: SISTEMA DE FILTRACIÓN
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 9: FILTROS DE ACEITE
-- ==========================================
-- K&N y HIFLOFILTRO (Alto Rendimiento)
(9, 'Filtro de Aceite K&N KN-155 (KTM / Bajaj)', 65000, 85000, 'KN-155', 'KN155', 'Filtro de alto flujo con tuerca soldada, ideal para Duke y NS 200', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite K&N KN-204 (Yamaha / Honda)', 68000, 88000, 'KN-204', 'KN204', 'Filtro premium para alto cilindraje (MT09, R6, CB500)', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Hiflofiltro HF112 (Honda)', 25000, 35000, 'HF-112', 'HF112', 'Filtro de papel de alta calidad certificado por TÜV', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Hiflofiltro HF138 (Suzuki)', 38000, 52000, 'HF-138', 'HF138', 'Filtro blindado para V-Strom y Gixxer 250', 1, NOW(), NOW(), 1),

-- ORIGINALES OEM
(9, 'Filtro de Aceite Original Yamaha FZ25 / FZ 2.0', 26000, 35000, 'YAM-FIL-FZ', 'Original', 'Elemento filtrante OEM Yamaha de máxima retención', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Original Honda CB190R / XR150L', 15000, 22000, 'HON-FIL-CB', 'Original', 'Cartucho OEM Honda', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Original Bajaj Pulsar NS 200 / Dominar', 18000, 25000, 'BAJ-FIL-NS', 'Original', 'Filtro de aceite original de fábrica Bajaj', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Original Suzuki Gixxer 150', 22000, 32000, 'SUZ-FIL-GIX', 'Original', 'Filtro de aceite OEM Suzuki', 1, NOW(), NOW(), 1),

-- REVO E ICHIBAN (Alternativos / Económicos)
(9, 'Filtro de Aceite Revo Pulsar NS 200', 8000, 15000, 'REV-FIL-NS', 'Papel', 'Filtro económico de papel, ideal para mantenimientos de bajo costo', 1, NOW(), NOW(), 1),
(9, 'Filtro de Aceite Ichiban Yamaha FZ 2.0', 10000, 18000, 'ICH-FIL-FZ', 'Celulosa', 'Excelente relación costo/beneficio con filtración eficiente', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 10: FILTROS DE AIRE
-- ==========================================
-- K&N (Lavables de Alto Flujo)
(10, 'Filtro de Aire K&N Alto Flujo Yamaha MT-09', 320000, 420000, 'KN-AIR-MT09', 'Lavable', 'Filtro de algodón lavable, aumenta caballos de fuerza y aceleración', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire K&N Alto Flujo Kawasaki Ninja 400', 310000, 400000, 'KN-AIR-NIN400', 'Lavable', 'Filtro de aire reutilizable, dura toda la vida de la moto', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire K&N Alto Flujo KTM Duke 200/390', 280000, 380000, 'KN-AIR-DUKE', 'Lavable', 'Diseñado para mejorar la respuesta del acelerador', 1, NOW(), NOW(), 1),

-- ORIGINALES OEM
(10, 'Filtro de Aire Original Yamaha NMAX 155', 45000, 65000, 'YAM-AIR-NMAX', 'Original', 'Filtro de papel plisado OEM Yamaha, máxima protección del motor', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Original Honda XR 150L', 38000, 52000, 'HON-AIR-XR150', 'Original', 'Filtro de espuma viscosa original de fábrica', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Original Suzuki Gixxer 150', 42000, 58000, 'SUZ-AIR-GIX', 'Original', 'Elemento de filtración de aire original Suzuki', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Original Bajaj Pulsar NS 200', 35000, 50000, 'BAJ-AIR-NS', 'Original', 'Filtro de aire de papel original Bajaj', 1, NOW(), NOW(), 1),

-- HIBARI Y REVO (Alternativos / Rotación Masiva)
(10, 'Filtro de Aire Hibari Yamaha FZ 2.0', 22000, 35000, 'HIB-AIR-FZ', 'Papel', 'Filtro de papel de excelente calidad para reemplazo', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Hibari Yamaha BWS 125', 18000, 28000, 'HIB-AIR-BWS', 'Papel', 'Filtro alternativo de alta durabilidad', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Revo AKT NKD 125', 12000, 20000, 'REV-AIR-NKD', 'Espuma', 'Elemento de espuma económico para motos de trabajo', 1, NOW(), NOW(), 1),
(10, 'Filtro de Aire Revo Honda Wave 110', 14000, 22000, 'REV-AIR-WAV', 'Papel', 'Reemplazo directo y económico', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 11: FILTROS DE GASOLINA
-- ==========================================
-- INYECCIÓN (Alta Presión)
(11, 'Filtro de Gasolina Inyección Universal (Pila/Bomba)', 18000, 30000, 'GEN-GAS-FI', 'Alta Presión', 'Filtro o cedazo para bombas de inyección electrónica', 1, NOW(), NOW(), 1),
(11, 'Filtro de Gasolina Original Yamaha (Inyección)', 45000, 65000, 'YAM-GAS-FI', 'Original', 'Elemento filtrante de combustible OEM para sistema FI', 1, NOW(), NOW(), 1),
(11, 'Filtro de Gasolina Mahle KL 315 (BMW / KTM)', 65000, 85000, 'MAH-KL315', 'KL 315', 'Filtro de aluminio para líneas de combustible de alta presión', 1, NOW(), NOW(), 1),

-- CARBURADOR (Baja Presión / Universales)
(11, 'Filtro de Gasolina Genérico Universal (Plástico Pequeño)', 3000, 6000, 'GEN-GAS-01', 'Universal P', 'Filtro de línea de combustible económico, ideal para NKD/Boxer', 1, NOW(), NOW(), 1),
(11, 'Filtro de Gasolina Universal con Imán', 5000, 10000, 'GEN-GAS-IMAN', 'Universal I', 'Filtro plástico con núcleo magnético para retener partículas metálicas', 1, NOW(), NOW(), 1),
(11, 'Filtro de Gasolina Universal Lavable (Vidrio/Aluminio)', 15000, 25000, 'GEN-GAS-LAV', 'Lavable', 'Filtro de combustible desarmable de lujo y alto flujo', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍA 12: KIT DE ARRASTRE
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CASSARELLA (La marca de mayor rotación y equilibrio calidad/precio)
-- ==========================================
(12, 'Kit de Arrastre Cassarella Pulsar NS 200 O-Ring', 145000, 195000, 'CAS-NS200-OR', 'Paso 520', 'Kit completo con cadena reforzada de O-rings para mayor duración y menor ruido', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Cassarella Yamaha FZ 2.0 / FZ 16', 110000, 155000, 'CAS-FZ2-428', 'Paso 428', 'Plato, piñón y cadena reforzada dorada, ajuste perfecto', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Cassarella Honda XR 150L', 115000, 160000, 'CAS-XR150', 'Paso 428', 'Kit de acero de alta resistencia para uso mixto (calle y destapado)', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Cassarella Suzuki Gixxer 150', 120000, 165000, 'CAS-GIX150', 'Paso 428', 'Durabilidad garantizada para uso urbano intenso', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Cassarella Yamaha XTZ 125', 105000, 145000, 'CAS-XTZ125', 'Paso 428', 'Kit reforzado para trabajo en terrenos irregulares', 1, NOW(), NOW(), 1),

-- ==========================================
-- DID (Cadenas Japonesas de Alta Gama y Competición)
-- ==========================================
(12, 'Cadena DID 520 VO O-Ring (120 Eslabones)', 220000, 290000, 'DID-520VO', '520 O-Ring', 'Durabilidad extrema japonesa con sellos O-Ring, ideal para media cilindrada', 1, NOW(), NOW(), 1),
(12, 'Cadena DID 520 VX3 X-Ring (120 Eslabones)', 380000, 480000, 'DID-520VX3', '520 X-Ring', 'Cadena premium para alto cilindraje y máxima retención de lubricante', 1, NOW(), NOW(), 1),
(12, 'Cadena DID 428 HD Reforzada (132 Eslabones)', 95000, 130000, 'DID-428HD', '428 Reforzada', 'Cadena sin oring de alta tensión para motos de trabajo y enduro ligero', 1, NOW(), NOW(), 1),

-- ==========================================
-- CHOHO (Calidad OEM, muy buscada para ensamble)
-- ==========================================
(12, 'Kit de Arrastre Choho Honda CB 125F', 75000, 105000, 'CHO-CB125', 'Paso 428', 'Kit completo calidad original de fábrica', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Choho Yamaha YBR 125', 72000, 100000, 'CHO-YBR125', 'Paso 428', 'Acero tratado térmicamente para evitar estiramiento y desgaste prematuro', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Choho AKT CR4 162', 80000, 115000, 'CHO-CR4162', 'Paso 428', 'Excelente recambio para garantizar suavidad en la transmisión', 1, NOW(), NOW(), 1),

-- ==========================================
-- REVO (Rotación masiva, económica y motos de trabajo)
-- ==========================================
(12, 'Kit de Arrastre Revo AKT NKD 125', 45000, 65000, 'REV-NKD-428', 'Paso 428', 'Kit económico de acero 1045, ideal para recambio frecuente en ciudad', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Revo Bajaj Boxer CT 100', 42000, 60000, 'REV-BOX-428', 'Paso 428', 'El más vendido para motos de trabajo y mensajería', 1, NOW(), NOW(), 1),
(12, 'Kit de Arrastre Revo Yamaha Crypton 115', 38000, 55000, 'REV-CRY115', 'Paso 428', 'Solución económica y súper confiable para uso diario', 1, NOW(), NOW(), 1),

-- ==========================================
-- JT SPROCKETS (Piñones y Platos Premium)
-- ==========================================
(12, 'Piñón de Salida JT Sprockets 14T (Paso 520)', 35000, 50000, 'JT-14T-520', '14T 520', 'Acero de aleación cromada superior, alta resistencia al desgaste', 1, NOW(), NOW(), 1),
(12, 'Piñón de Salida JT Sprockets 15T (Paso 428)', 28000, 40000, 'JT-15T-428', '15T 428', 'Fabricación de altísima precisión para reducir vibraciones', 1, NOW(), NOW(), 1),
(12, 'Plato Trasero JT Sprockets 45T (Paso 520)', 85000, 115000, 'JT-45T-520', '45T 520', 'Acero alto carbono C49, durabilidad extrema para exigencia en ruta', 1, NOW(), NOW(), 1),
(12, 'Plato Trasero JT Sprockets 42T (Paso 428)', 70000, 95000, 'JT-42T-428', '42T 428', 'Acero templado, ideal para combinar con cadenas premium como DID', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 13 Y 14: BATERÍAS Y BUJÍAS
-- TODAS LAS MARCAS INTEGRADAS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 13: BATERÍAS
-- ==========================================
-- YUASA (Líder Mundial - AGM Libre de mantenimiento)
(13, 'Batería Yuasa YTX9-BS', 185000, 245000, 'YUA-YTX9BS', '12V 8Ah', 'Batería AGM libre de mantenimiento, alta resistencia a vibraciones', 1, NOW(), NOW(), 1),
(13, 'Batería Yuasa YTZ10S', 320000, 410000, 'YUA-YTZ10S', '12V 8.6Ah', 'Batería de alto rendimiento, ideal para MT-09, CBR y R6', 1, NOW(), NOW(), 1),
(13, 'Batería Yuasa YTX7L-BS', 170000, 220000, 'YUA-YTX7LBS', '12V 6Ah', 'Excelente potencia de arranque en frío, libre de mantenimiento', 1, NOW(), NOW(), 1),

-- MAGNA Y BS BATTERY (Tecnología Gel y SLA)
(13, 'Batería Magna YTX7L-BS Gel', 85000, 120000, 'MAG-7LBS-GEL', '12V 6Ah', 'Batería tecnología Gel, mayor vida útil y no derrama ácidos', 1, NOW(), NOW(), 1),
(13, 'Batería Magna YTX4L-BS Gel', 65000, 90000, 'MAG-4LBS-GEL', '12V 3Ah', 'Batería de gel para scooters, moped y motos semiautomáticas', 1, NOW(), NOW(), 1),
(13, 'Batería BS Battery BTX7A-BS (SLA)', 115000, 160000, 'BS-BTX7A', '12V 6Ah', 'Batería activada de fábrica (SLA), sellada y lista para usar', 1, NOW(), NOW(), 1),

-- BOSCH Y MEGABAT (Alternativas Confiables y de Trabajo)
(13, 'Batería Bosch M6 AGM YTX7A-BS', 110000, 150000, 'BOS-YTX7A', '12V 6Ah', 'Batería sellada Bosch con tecnología AGM', 1, NOW(), NOW(), 1),
(13, 'Batería Megabat 12N5-3B (Ácido)', 55000, 80000, 'MEG-12N5', '12V 5Ah', 'Batería convencional de ácido, la más usada en motos de trabajo (RX, DT, AX)', 1, NOW(), NOW(), 1),
(13, 'Batería Megabat YTZ7S Gel', 75000, 105000, 'MEG-YTZ7S', '12V 6Ah', 'Alternativa económica en Gel para alta rotación urbana', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 14: BUJÍAS
-- ==========================================
-- NGK (Estándar de Cobre)
(14, 'Bujía NGK Cobre CPR8EA-9', 14000, 22000, 'NGK-CPR8', 'Cobre', 'Bujía original OEM estándar (Yamaha FZ, Honda XR)', 1, NOW(), NOW(), 1),
(14, 'Bujía NGK Cobre D8EA', 12000, 18000, 'NGK-D8EA', 'Cobre', 'Bujía para motores CG (AKT NKD, Boxer CT)', 1, NOW(), NOW(), 1),
(14, 'Bujía NGK Cobre CR8E', 15000, 24000, 'NGK-CR8E', 'Cobre', 'Bujía estándar para Bajaj Pulsar y motos multipropósito', 1, NOW(), NOW(), 1),

-- NGK (Iridium - Alto Rendimiento)
(14, 'Bujía NGK Iridium CR8EIX', 48000, 65000, 'NGK-CR8EIX', 'Iridium', 'Mejor ignición, ahorro combustible y aceleración más suave', 1, NOW(), NOW(), 1),
(14, 'Bujía NGK Iridium CPR8EAIX-9', 52000, 70000, 'NGK-CPR8EIX', 'Iridium', 'Bujía Iridium para Yamaha/Honda, vida útil extendida', 1, NOW(), NOW(), 1),
(14, 'Bujía NGK Laser Iridium LMAR8A-9', 65000, 85000, 'NGK-LMAR8A9', 'Laser Iridium', 'Bujía premium de equipo original para MT-09 y alta gama', 1, NOW(), NOW(), 1),

-- DENSO Y BOSCH (Especialistas en Encendido)
(14, 'Bujía Denso Iridium Power IU24', 55000, 75000, 'DEN-IU24', 'Iridium Power', 'Electrodo de iridio de 0.4mm, la chispa más potente del mercado', 1, NOW(), NOW(), 1),
(14, 'Bujía Bosch UR4DC Cobre', 13000, 20000, 'BOS-UR4DC', 'Cobre Super', 'Alternativa alemana de encendido confiable y económica', 1, NOW(), NOW(), 1),

-- ACCESORIOS DE ENCENDIDO
(14, 'Capuchón de Bujía NGK Resistivo (Rojo)', 25000, 35000, 'NGK-CAP-R', 'Resistivo', 'Aislante de silicona de alta calidad, resistente al agua y salto de chispa', 1, NOW(), NOW(), 1),
(14, 'Capuchón de Bujía Genérico Negro', 5000, 10000, 'GEN-CAP-B', 'Estándar', 'Capuchón económico para recambio rápido', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 15, 16 Y 17
-- SISTEMA ELÉCTRICO, SUSPENSIÓN Y RODAMIENTOS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 15: SISTEMA ELÉCTRICO (CDI, Bobinas, Reguladores)
-- ==========================================
-- CDI y Módulos de Encendido
(15, 'CDI Pietcard Racing sin limitador (Pulsar NS 200)', 140000, 185000, 'PIE-CDI-NS200R', 'Racing', 'CDI argentino de alto rendimiento, elimina el corte de RPM', 1, NOW(), NOW(), 1),
(15, 'CDI Revo Corriente Alterna (AKT NKD / Evo 125)', 25000, 40000, 'REV-CDI-NKD', '6 Pines', 'Módulo de encendido económico y confiable para motos de trabajo', 1, NOW(), NOW(), 1),
(15, 'TCI Original Yamaha (FZ 2.0 Inyección)', 280000, 360000, 'YAM-TCI-FZ2', 'Original', 'Unidad de control de encendido original de fábrica', 1, NOW(), NOW(), 1),

-- Reguladores y Estatores
(15, 'Regulador de Corriente Promoto (Honda XR 150L)', 45000, 65000, 'PRO-REG-XR15', '5 Pines', 'Regulador rectificador de voltaje de alta disipación térmica', 1, NOW(), NOW(), 1),
(15, 'Regulador de Corriente Revo (Yamaha FZ16)', 38000, 55000, 'REV-REG-FZ16', '4 Pines', 'Regulador de reemplazo exacto', 1, NOW(), NOW(), 1),
(15, 'Estator / Plato de Bobinas Original Bajaj (Dominar 400)', 180000, 240000, 'BAJ-EST-DOM4', 'Original', 'Generador de corriente original trifásico', 1, NOW(), NOW(), 1),

-- Bobinas y Componentes de Arranque
(15, 'Bobina de Alta Promoto Universal (Motos Carburadas)', 18000, 28000, 'PRO-BOB-UNI', '1 Salida', 'Bobina de encendido de reemplazo universal', 1, NOW(), NOW(), 1),
(15, 'Relay de Arranque / Marranita Universal Revo', 12000, 20000, 'REV-REL-UNI', 'Universal', 'Relé de arranque para motos de 125cc a 200cc', 1, NOW(), NOW(), 1),
(15, 'Motor de Arranque Original TVS (Apache 200)', 150000, 210000, 'TVS-ARR-AP200', 'Original', 'Motor de partida completo', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 16: SUSPENSIÓN (Aceites, Retenedores)
-- ==========================================
-- Aceites de Suspensión (Fork Oil)
(16, 'Aceite Suspensión Motul Fork Oil Expert 10W (Medium) 1L', 45000, 60000, 'MT-FORK-10W-EXP', '10W Medium', 'Aceite semisintético para horquillas telescópicas estándar', 1, NOW(), NOW(), 1),
(16, 'Aceite Suspensión Motul Fork Oil Factory Line 5W (Light) 1L', 65000, 85000, 'MT-FORK-5W-FL', '5W Light', 'Aceite 100% sintético para horquillas invertidas (alto desempeño)', 1, NOW(), NOW(), 1),
(16, 'Aceite Suspensión Motul Fork Oil Expert 15W (Heavy) 1L', 45000, 60000, 'MT-FORK-15W-EXP', '15W Heavy', 'Ideal para endurecer la suspensión en motos de trabajo o carga', 1, NOW(), NOW(), 1),
(16, 'Aceite Suspensión Liqui Moly Motorbike Fork Oil 10W 500ml', 38000, 52000, 'LM-FORK-10W-500', '10W Medium', 'Fluido alemán para amortiguadores, evita la formación de espuma', 1, NOW(), NOW(), 1),

-- Retenedores y Guardapolvos
(16, 'Retenedores de Suspensión ARI 37x50x11 (Par)', 48000, 65000, 'ARI-375011', '37mm', 'Retenedores italianos de alta resistencia (Pulsar NS200, Ninja 300)', 1, NOW(), NOW(), 1),
(16, 'Retenedores de Suspensión NOK Originales 41mm (Par)', 65000, 90000, 'NOK-41MM', '41mm', 'Retenedores japoneses equipo original (Yamaha MT07, R3)', 1, NOW(), NOW(), 1),
(16, 'Kit Retenedores y Guardapolvos Revo (Yamaha FZ 2.0 / FZ16)', 22000, 35000, 'REV-RET-FZ', '41mm Kit', 'Kit económico completo para barras de 41mm', 1, NOW(), NOW(), 1),
(16, 'Guardapolvos de Suspensión Originales Honda (XR 150L)', 35000, 50000, 'HON-GUA-XR', 'Original', 'Protege el retenedor del polvo y barro', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 17: RODAMIENTOS Y CUNAS
-- ==========================================
-- Rodamientos / Balineras (Alta Calidad Japonesa y Europea)
(17, 'Rodamiento SKF 6202-2RS (Rueda / Alta Velocidad)', 18000, 25000, 'SKF-6202-2RS', '6202-2RS', 'Balinera sellada sueca, ideal para ruedas delanteras y traseras', 1, NOW(), NOW(), 1),
(17, 'Rodamiento SKF 6301-2RS (Rueda delantera)', 19000, 26000, 'SKF-6301-2RS', '6301-2RS', 'Balinera sellada de alta precisión para rueda', 1, NOW(), NOW(), 1),
(17, 'Rodamiento NTN 6204-2RS (Porta Sprocket)', 24000, 34000, 'NTN-6204-2RS', '6204-2RS', 'Rodamiento japonés de alta carga para el porta catarina', 1, NOW(), NOW(), 1),
(17, 'Rodamiento Koyo 6004-2RS (Eje trasero)', 22000, 32000, 'KOYO-6004-2RS', '6004-2RS', 'Balinera japonesa sellada', 1, NOW(), NOW(), 1),
(17, 'Rodamiento KML 6202-ZZ (Económico)', 8000, 14000, 'KML-6202-ZZ', '6202-ZZ', 'Balinera económica con sello metálico', 1, NOW(), NOW(), 1),

-- Cunas de Dirección
(17, 'Cunas de Dirección Originales Yamaha (NMAX / Aerox)', 110000, 150000, 'YAM-CUN-NMAX', 'Original', 'Kit de cunas (pistas y balines) de dirección original', 1, NOW(), NOW(), 1),
(17, 'Cunas de Dirección Revo de Rodillos (AKT NKD / Honda CB)', 28000, 45000, 'REV-CUN-ROD', 'Rodillos', 'Kit de cunas cónicas de rodillos, mayor durabilidad que los balines', 1, NOW(), NOW(), 1),
(17, 'Cunas de Dirección Promoto (Bajaj Pulsar NS 200)', 35000, 50000, 'PRO-CUN-NS200', 'Estándar', 'Kit completo de pistas para dirección', 1, NOW(), NOW(), 1);

USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 18, 19 Y 20
-- GUAYAS, BOMBILLERÍA/EXPLORADORAS Y CARENAJES/ESPEJOS (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 18: GUAYAS (Clutch, Acelerador, Choke, Freno)
-- ==========================================
-- Guayas Originales (OEM)
(18, 'Guaya de Clutch Original Yamaha FZ 2.0 / FZ25', 25000, 35000, 'YAM-GUA-CLU-FZ', 'Original', 'Cable de embrague original de fábrica, máxima suavidad', 1, NOW(), NOW(), 1),
(18, 'Guaya de Acelerador Original Honda XR 150L (Doble)', 32000, 45000, 'HON-GUA-ACE-XR', 'Original', 'Kit de guayas de aceleración (A y B) originales', 1, NOW(), NOW(), 1),
(18, 'Guaya de Freno Trasero Original Yamaha NMAX 155', 38000, 52000, 'YAM-GUA-FRE-NMAX', 'Original', 'Guaya de freno blindada original', 1, NOW(), NOW(), 1),

-- Guayas Revo y Promoto (Económicas y de alta rotación)
(18, 'Guaya de Clutch Revo Bajaj Pulsar NS 200', 12000, 18000, 'REV-GUA-CLU-NS', 'Estándar', 'Guaya de embrague trenzada, excelente relación calidad-precio', 1, NOW(), NOW(), 1),
(18, 'Guaya de Acelerador Revo AKT NKD 125', 8000, 14000, 'REV-GUA-ACE-NKD', 'Estándar', 'Guaya de aceleración para motos de trabajo', 1, NOW(), NOW(), 1),
(18, 'Guaya de Clutch Promoto Yamaha YBR 125', 10000, 16000, 'PRO-GUA-CLU-YBR', 'Estándar', 'Funda recubierta en teflón para mayor duración', 1, NOW(), NOW(), 1),
(18, 'Guaya de Choke / Ahogador Promoto Honda CB 125F', 9000, 15000, 'PRO-GUA-CHO-CB', 'Estándar', 'Guaya para el sistema de estrangulador del carburador', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 19: BOMBILLERÍA Y EXPLORADORAS
-- ==========================================
-- Bombillos Farola Principal (Halógenos y LED)
(19, 'Bombillo Halógeno Osram Night Racer 50 H4 (12V 35/35W)', 22000, 32000, 'OSR-H4-NR50', 'H4 Halógeno', 'Hasta 50% más de luz, ideal para farolas de motos AC/DC', 1, NOW(), NOW(), 1),
(19, 'Bombillo LED Philips Ultinon Moto H4 / HS1', 45000, 65000, 'PHI-LED-H4', 'H4 LED', 'Luz blanca brillante 6000K, excelente corte de luz que no encandila', 1, NOW(), NOW(), 1),
(19, 'Bombillo LED M5 / H6 (Motos de Trabajo - Corriente Alterna)', 15000, 25000, 'GEN-LED-M5', 'M5 LED', 'LED de 3 caras para motos como NKD, Boxer o Eco Deluxe', 1, NOW(), NOW(), 1),

-- Exploradoras (Accesorios muy populares)
(19, 'Exploradoras Mini Láser Bicolor (Luz Blanca/Amarilla) Par', 65000, 95000, 'EXP-MINI-LASER', 'Bicolor', 'Mini exploradoras LED con función de luz blanca (bajas) y amarilla (altas)', 1, NOW(), NOW(), 1),
(19, 'Exploradoras LED Cuadradas 16 Leds (Par) 48W', 40000, 60000, 'EXP-CUA-16LED', '48W', 'Exploradoras de expansión, ideales para trabajo o trocha', 1, NOW(), NOW(), 1),
(19, 'Switch / Interruptor Impermeable para Exploradoras', 8000, 15000, 'SWI-EXP-IMP', 'Universal', 'Botón On/Off de manubrio resistente a la lluvia', 1, NOW(), NOW(), 1),

-- Direccionales y Stop
(19, 'Bombillo Stop Trasero LED (Doble Contacto 1157)', 8000, 14000, 'LED-STOP-1157', '1157', 'Bombillo de freno estroboscópico de gel', 1, NOW(), NOW(), 1),
(19, 'Bombillo Direccional Halógeno (Pata de pescado/T10) Naranja x4', 5000, 10000, 'HAL-DIR-T10', 'T10', 'Pillitos naranjas para tableros o direccionales', 1, NOW(), NOW(), 1),
(19, 'Direccionales LED Secuenciales Universales (Par)', 25000, 38000, 'DIR-LED-SEC', 'Secuencial', 'Direccionales tipo flecha dinámica', 1, NOW(), NOW(), 1),
(19, 'Flasher Electrónico Regulable para Direccionales LED', 12000, 20000, 'FLA-LED-REG', '2 Pines', 'Evita el parpadeo rápido al instalar direccionales LED', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 20: CARENAJES, TAPAS Y ESPEJOS
-- ==========================================
-- Espejos Retrovisores
(20, 'Espejos Originales Yamaha NMAX (Par)', 55000, 80000, 'YAM-ESP-NMAX', 'Original', 'Espejos de equipo original, excelente visibilidad y sin vibración', 1, NOW(), NOW(), 1),
(20, 'Espejos Originales Honda XR 150L / CB190R (Par)', 45000, 65000, 'HON-ESP-XR', 'Original', 'Espejos redondos/ovalados de fábrica rosca Honda', 1, NOW(), NOW(), 1),
(20, 'Espejos Tipo Rizoma Tomok (Universales Aluminio)', 35000, 55000, 'ACC-ESP-TOMOK', 'Rizoma Genérico', 'Espejos deportivos de lujo en aluminio CNC con adaptadores', 1, NOW(), NOW(), 1),
(20, 'Espejos Universales Tipo FZ para motos de trabajo (Par)', 15000, 25000, 'GEN-ESP-FZ', 'Estándar', 'Espejos plásticos económicos de diseño romboide', 1, NOW(), NOW(), 1),

-- Carenajes, Tapas y Cúpulas
(20, 'Tapas Laterales AKT NKD 125 Negras (Par)', 25000, 40000, 'AKT-TAP-NKD-N', 'Plástico', 'Tapas laterales de batería y filtro', 1, NOW(), NOW(), 1),
(20, 'Guardabarros Delantero Pulsar NS 200 (Negro Brillante)', 45000, 65000, 'BAJ-GUA-NS200', 'Original', 'Pieza plástica original de recambio', 1, NOW(), NOW(), 1),
(20, 'Cúpula / Visor Deportivo Pulsar NS 200 (Ahumado oscuro)', 35000, 55000, 'ACC-CUP-NS200', 'Acrílico', 'Visor cortavientos de accesorio', 1, NOW(), NOW(), 1),
(20, 'Protector de Exosto / Mofle Honda Wave 110', 22000, 35000, 'HON-PRO-WAV', 'Genérico', 'Tapa antiquemaduras para el tubo de escape', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- CARGA MASIVA DE PRODUCTOS - CATEGORÍAS 22, 23, 24 Y 25
-- HERRAMIENTAS, EMPAQUES, PARTES DE MOTOR Y TORNILLERÍA (MERCADO COLOMBIA)
-- ==============================================================================

INSERT IGNORE INTO product (product_type_id, product_name, price, sale_price, code, reference, description, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- CATEGORÍA 22: HERRAMIENTAS Y ACCESORIOS (Para venta o uso del taller)
-- ==========================================
(22, 'Extractor de Volante Universal (Motos de Trabajo)', 35000, 50000, 'HER-EXT-VOL', 'Universal', 'Extractor de volante magnético rosca doble', 1, NOW(), NOW(), 1),
(22, 'Medidor de Presión de Llantas Análogo', 15000, 25000, 'HER-MED-PRE', 'Análogo', 'Calibrador de presión de aire tipo lapicero en metal', 1, NOW(), NOW(), 1),
(22, 'Kit Parcha Llantas Sellomatic (Mechas y Agujas)', 12000, 20000, 'HER-PAR-SEL', 'Kit Básico', 'Kit de despinche rápido para llantas tubeless', 1, NOW(), NOW(), 1),
(22, 'Malla Pulpo Elástica para Carga (Casco)', 8000, 15000, 'ACC-MAL-PUL', 'Malla', 'Malla reflectiva con ganchos plásticos para asegurar carga', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 23: EMPAQUES Y RETENEDORES DE MOTOR
-- ==========================================
-- Vedamotors (Athena) y Originales
(23, 'Kit de Empaques Completo Vedamotors (Yamaha FZ16 / FZ 2.0)', 35000, 55000, 'EMP-VED-FZ16', 'Full Kit', 'Empaquetadura completa de motor sin asbesto de alta temperatura', 1, NOW(), NOW(), 1),
(23, 'Kit de Empaques Completo Revo (AKT NKD 125)', 18000, 28000, 'EMP-REV-NKD', 'Full Kit', 'Juego de juntas completo para motor CG 125', 1, NOW(), NOW(), 1),
(23, 'Empaque de Culata Original Bajaj (Pulsar NS 200)', 25000, 40000, 'EMP-CUL-NS200', 'Original', 'Empaque de culata metálico laminado (evita fugas de compresión)', 1, NOW(), NOW(), 1),
(23, 'Kit Retenedores de Motor Revo (Boxer CT 100)', 15000, 25000, 'RET-MOT-BOX', 'Motor', 'Juego de retenedores de eje de cambios, cran y piñón', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 24: PARTES DE MOTOR (Pistones, Anillos, Válvulas)
-- ==========================================
-- Kits de Cilindro y Partes Mayores
(24, 'Kit de Cilindro Completo NPC (Honda CB 125F)', 120000, 175000, 'CIL-NPC-CB125', 'Kit Cilindro', 'Incluye cilindro, pistón, anillos, bulón y pines', 1, NOW(), NOW(), 1),
(24, 'Kit de Cilindro Completo Revo (AKT NKD / Evo 125)', 85000, 125000, 'CIL-REV-NKD', 'Kit Cilindro', 'Kit de motor completo, listo para instalar (Medida Standard)', 1, NOW(), NOW(), 1),

-- Anillos y Pistones
(24, 'Kit de Anillos RIK Standard (Yamaha NMAX 155)', 65000, 95000, 'ANI-RIK-NMAX-STD', 'Standard', 'Anillos japoneses RIK de altísima resistencia al desgaste', 1, NOW(), NOW(), 1),
(24, 'Pistón y Anillos Revo Standard (Yamaha BWS 125)', 45000, 68000, 'PIS-REV-BWS-STD', 'Standard', 'Kit de pistón de repuesto', 1, NOW(), NOW(), 1),

-- Válvulas y Cadenillas
(24, 'Válvulas de Motor Originales Yamaha (FZ 2.0) Par', 85000, 120000, 'VAL-YAM-FZ2', 'Original', 'Válvula de admisión y escape originales de fábrica', 1, NOW(), NOW(), 1),
(24, 'Válvulas de Motor Ichiban (Pulsar NS 200) Kit x4', 55000, 85000, 'VAL-ICH-NS200', 'Titanio/Acero', 'Kit de 4 válvulas de repuesto', 1, NOW(), NOW(), 1),
(24, 'Cadenilla de Distribución DID (Pulsar NS 200)', 45000, 65000, 'CAD-DIS-DID-NS', 'Original DID', 'Cadena de tiempos japonesa silenciosa y reforzada', 1, NOW(), NOW(), 1),

-- ==========================================
-- CATEGORÍA 25: TORNILLERÍA Y VARIOS
-- ==========================================
(25, 'Tuerca Eje Trasero Autoasegurante (Universal 14mm)', 2000, 5000, 'TOR-TUE-14MM', '14mm', 'Tuerca de seguridad con nylon interno', 1, NOW(), NOW(), 1),
(25, 'Kit Tornillería de Carenaje Universal (Clips y Tornillos)', 15000, 25000, 'TOR-KIT-CAR', 'Grapas', 'Juego de grapas plásticas y tornillos bristol para tapas', 1, NOW(), NOW(), 1),
(25, 'Resorte de Gato Central (Universal motos de trabajo)', 3000, 8000, 'VAR-RES-GAT', 'Acero', 'Resorte templado de alta tensión para parador central', 1, NOW(), NOW(), 1),
(25, 'Chaveta / Pin de Seguridad Universal (Paquete x10)', 2000, 6000, 'VAR-CHA-UNI', 'Pines', 'Chavetas metálicas para pedales de freno y ejes', 1, NOW(), NOW(), 1);
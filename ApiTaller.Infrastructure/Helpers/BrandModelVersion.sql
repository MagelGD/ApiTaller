USE TallerMotoCar;

-- ==============================================================================
-- SCRIPT 3: CARGA MASIVA DE REFERENCIAS (brandmodelversion)
-- ==============================================================================

INSERT IGNORE INTO brandmodelversion (BrandId, ModelId, version, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- 1. YAMAHA (BrandId = 1)
-- ==========================================
-- RX 115 (Años 2000 al 2008 -> ModelIds 1 al 9)
(1, 1, 'RX 115', 1, NOW(), NOW(), 1), (1, 2, 'RX 115', 1, NOW(), NOW(), 1), (1, 3, 'RX 115', 1, NOW(), NOW(), 1),
(1, 4, 'RX 115', 1, NOW(), NOW(), 1), (1, 5, 'RX 115', 1, NOW(), NOW(), 1), (1, 6, 'RX 115', 1, NOW(), NOW(), 1),
(1, 7, 'RX 115', 1, NOW(), NOW(), 1), (1, 8, 'RX 115', 1, NOW(), NOW(), 1), (1, 9, 'RX 115', 1, NOW(), NOW(), 1),

-- FZ 16 Carburada (Años 2009 al 2015 -> ModelIds 10 al 16)
(1, 10, 'FZ 16', 1, NOW(), NOW(), 1), (1, 11, 'FZ 16', 1, NOW(), NOW(), 1), (1, 12, 'FZ 16', 1, NOW(), NOW(), 1),
(1, 13, 'FZ 16', 1, NOW(), NOW(), 1), (1, 14, 'FZ 16', 1, NOW(), NOW(), 1), (1, 15, 'FZ 16', 1, NOW(), NOW(), 1),
(1, 16, 'FZ 16', 1, NOW(), NOW(), 1),

-- FZ 2.0 (Años 2016 al 2023 -> ModelIds 17 al 24)
(1, 17, 'FZ 2.0', 1, NOW(), NOW(), 1), (1, 18, 'FZ 2.0', 1, NOW(), NOW(), 1), (1, 19, 'FZ 2.0', 1, NOW(), NOW(), 1),
(1, 20, 'FZ 2.0', 1, NOW(), NOW(), 1), (1, 21, 'FZ 2.0', 1, NOW(), NOW(), 1), (1, 22, 'FZ 2.0', 1, NOW(), NOW(), 1),
(1, 23, 'FZ 2.0', 1, NOW(), NOW(), 1), (1, 24, 'FZ 2.0', 1, NOW(), NOW(), 1),

-- FZ 3.0 (Años 2024 al 2026 -> ModelIds 25 al 27)
(1, 25, 'FZ 3.0', 1, NOW(), NOW(), 1), (1, 26, 'FZ 3.0', 1, NOW(), NOW(), 1), (1, 27, 'FZ 3.0', 1, NOW(), NOW(), 1),

-- NMAX / NMAX Connected (Años 2016 al 2026 -> ModelIds 17 al 27)
(1, 17, 'NMAX 155', 1, NOW(), NOW(), 1), (1, 18, 'NMAX 155', 1, NOW(), NOW(), 1), (1, 19, 'NMAX 155', 1, NOW(), NOW(), 1),
(1, 20, 'NMAX 155', 1, NOW(), NOW(), 1), (1, 21, 'NMAX Connected', 1, NOW(), NOW(), 1), (1, 22, 'NMAX Connected', 1, NOW(), NOW(), 1),
(1, 23, 'NMAX Connected', 1, NOW(), NOW(), 1), (1, 24, 'NMAX Connected', 1, NOW(), NOW(), 1), (1, 25, 'NMAX Connected', 1, NOW(), NOW(), 1),
(1, 26, 'NMAX Connected', 1, NOW(), NOW(), 1), (1, 27, 'NMAX Connected', 1, NOW(), NOW(), 1),

-- MT-09 (Años 2015 al 2026 -> ModelIds 16 al 27)
(1, 16, 'MT-09', 1, NOW(), NOW(), 1), (1, 17, 'MT-09', 1, NOW(), NOW(), 1), (1, 18, 'MT-09', 1, NOW(), NOW(), 1),
(1, 19, 'MT-09', 1, NOW(), NOW(), 1), (1, 20, 'MT-09', 1, NOW(), NOW(), 1), (1, 21, 'MT-09', 1, NOW(), NOW(), 1),
(1, 22, 'MT-09', 1, NOW(), NOW(), 1), (1, 23, 'MT-09', 1, NOW(), NOW(), 1), (1, 24, 'MT-09', 1, NOW(), NOW(), 1),
(1, 25, 'MT-09', 1, NOW(), NOW(), 1), (1, 26, 'MT-09', 1, NOW(), NOW(), 1), (1, 27, 'MT-09', 1, NOW(), NOW(), 1),

-- XTZ 150 (Años 2019 al 2026 -> ModelIds 20 al 27)
(1, 20, 'XTZ 150', 1, NOW(), NOW(), 1), (1, 21, 'XTZ 150', 1, NOW(), NOW(), 1), (1, 22, 'XTZ 150', 1, NOW(), NOW(), 1),
(1, 23, 'XTZ 150', 1, NOW(), NOW(), 1), (1, 24, 'XTZ 150', 1, NOW(), NOW(), 1), (1, 25, 'XTZ 150', 1, NOW(), NOW(), 1),
(1, 26, 'XTZ 150', 1, NOW(), NOW(), 1), (1, 27, 'XTZ 150', 1, NOW(), NOW(), 1),

-- ==========================================
-- 2. HONDA (BrandId = 2)
-- ==========================================
-- XR 150L (Años 2015 al 2026 -> ModelIds 16 al 27)
(2, 16, 'XR 150L', 1, NOW(), NOW(), 1), (2, 17, 'XR 150L', 1, NOW(), NOW(), 1), (2, 18, 'XR 150L', 1, NOW(), NOW(), 1),
(2, 19, 'XR 150L', 1, NOW(), NOW(), 1), (2, 20, 'XR 150L', 1, NOW(), NOW(), 1), (2, 21, 'XR 150L', 1, NOW(), NOW(), 1),
(2, 22, 'XR 150L', 1, NOW(), NOW(), 1), (2, 23, 'XR 150L', 1, NOW(), NOW(), 1), (2, 24, 'XR 150L', 1, NOW(), NOW(), 1),
(2, 25, 'XR 150L', 1, NOW(), NOW(), 1), (2, 26, 'XR 150L', 1, NOW(), NOW(), 1), (2, 27, 'XR 150L', 1, NOW(), NOW(), 1),

-- CB 190R (Años 2017 al 2026 -> ModelIds 18 al 27)
(2, 18, 'CB 190R', 1, NOW(), NOW(), 1), (2, 19, 'CB 190R', 1, NOW(), NOW(), 1), (2, 20, 'CB 190R', 1, NOW(), NOW(), 1),
(2, 21, 'CB 190R', 1, NOW(), NOW(), 1), (2, 22, 'CB 190R', 1, NOW(), NOW(), 1), (2, 23, 'CB 190R', 1, NOW(), NOW(), 1),
(2, 24, 'CB 190R', 1, NOW(), NOW(), 1), (2, 25, 'CB 190R', 1, NOW(), NOW(), 1), (2, 26, 'CB 190R', 1, NOW(), NOW(), 1),
(2, 27, 'CB 190R', 1, NOW(), NOW(), 1),

-- Wave 110S (Años 2018 al 2026 -> ModelIds 19 al 27)
(2, 19, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 20, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 21, 'Wave 110S', 1, NOW(), NOW(), 1),
(2, 22, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 23, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 24, 'Wave 110S', 1, NOW(), NOW(), 1),
(2, 25, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 26, 'Wave 110S', 1, NOW(), NOW(), 1), (2, 27, 'Wave 110S', 1, NOW(), NOW(), 1),

-- ==========================================
-- 3. SUZUKI (BrandId = 3)
-- ==========================================
-- GN 125 (¡La incombustible! Años 2000 al 2026 -> ModelIds 1 al 27)
(3, 1, 'GN 125', 1, NOW(), NOW(), 1), (3, 2, 'GN 125', 1, NOW(), NOW(), 1), (3, 3, 'GN 125', 1, NOW(), NOW(), 1),
(3, 4, 'GN 125', 1, NOW(), NOW(), 1), (3, 5, 'GN 125', 1, NOW(), NOW(), 1), (3, 6, 'GN 125', 1, NOW(), NOW(), 1),
(3, 7, 'GN 125', 1, NOW(), NOW(), 1), (3, 8, 'GN 125', 1, NOW(), NOW(), 1), (3, 9, 'GN 125', 1, NOW(), NOW(), 1),
(3, 10, 'GN 125', 1, NOW(), NOW(), 1), (3, 11, 'GN 125', 1, NOW(), NOW(), 1), (3, 12, 'GN 125', 1, NOW(), NOW(), 1),
(3, 13, 'GN 125', 1, NOW(), NOW(), 1), (3, 14, 'GN 125', 1, NOW(), NOW(), 1), (3, 15, 'GN 125', 1, NOW(), NOW(), 1),
(3, 16, 'GN 125', 1, NOW(), NOW(), 1), (3, 17, 'GN 125', 1, NOW(), NOW(), 1), (3, 18, 'GN 125', 1, NOW(), NOW(), 1),
(3, 19, 'GN 125', 1, NOW(), NOW(), 1), (3, 20, 'GN 125', 1, NOW(), NOW(), 1), (3, 21, 'GN 125', 1, NOW(), NOW(), 1),
(3, 22, 'GN 125', 1, NOW(), NOW(), 1), (3, 23, 'GN 125', 1, NOW(), NOW(), 1), (3, 24, 'GN 125', 1, NOW(), NOW(), 1),
(3, 25, 'GN 125', 1, NOW(), NOW(), 1), (3, 26, 'GN 125', 1, NOW(), NOW(), 1), (3, 27, 'GN 125', 1, NOW(), NOW(), 1),

-- Gixxer 150 / 150 FI (Años 2016 al 2026 -> ModelIds 17 al 27)
(3, 17, 'Gixxer 150', 1, NOW(), NOW(), 1), (3, 18, 'Gixxer 150', 1, NOW(), NOW(), 1), (3, 19, 'Gixxer 150', 1, NOW(), NOW(), 1),
(3, 20, 'Gixxer 150 FI', 1, NOW(), NOW(), 1), (3, 21, 'Gixxer 150 FI', 1, NOW(), NOW(), 1), (3, 22, 'Gixxer 150 FI', 1, NOW(), NOW(), 1),
(3, 23, 'Gixxer 150 FI', 1, NOW(), NOW(), 1), (3, 24, 'Gixxer 150 FI', 1, NOW(), NOW(), 1), (3, 25, 'Gixxer 150 FI', 1, NOW(), NOW(), 1),
(3, 26, 'Gixxer 150 FI', 1, NOW(), NOW(), 1), (3, 27, 'Gixxer 150 FI', 1, NOW(), NOW(), 1),

-- ==========================================
-- 4. BAJAJ (BrandId = 4)
-- ==========================================
-- Boxer CT 100 (Años 2005 al 2026 -> ModelIds 6 al 27)
(4, 6, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 7, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 8, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 9, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 10, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 11, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 12, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 13, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 14, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 15, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 16, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 17, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 18, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 19, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 20, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 21, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 22, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 23, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 24, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 25, 'Boxer CT 100', 1, NOW(), NOW(), 1), (4, 26, 'Boxer CT 100', 1, NOW(), NOW(), 1),
(4, 27, 'Boxer CT 100', 1, NOW(), NOW(), 1),

-- Pulsar NS 200 / FI (Años 2013 al 2026 -> ModelIds 14 al 27)
(4, 14, 'Pulsar NS 200', 1, NOW(), NOW(), 1), (4, 15, 'Pulsar NS 200', 1, NOW(), NOW(), 1), (4, 16, 'Pulsar NS 200', 1, NOW(), NOW(), 1),
(4, 17, 'Pulsar NS 200', 1, NOW(), NOW(), 1), (4, 18, 'Pulsar NS 200', 1, NOW(), NOW(), 1), (4, 19, 'Pulsar NS 200', 1, NOW(), NOW(), 1),
(4, 20, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1), (4, 21, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1), (4, 22, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1),
(4, 23, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1), (4, 24, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1), (4, 25, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1),
(4, 26, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1), (4, 27, 'Pulsar NS 200 FI', 1, NOW(), NOW(), 1),

-- Dominar 400 (Años 2018 al 2026 -> ModelIds 19 al 27)
(4, 19, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 20, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 21, 'Dominar 400', 1, NOW(), NOW(), 1),
(4, 22, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 23, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 24, 'Dominar 400', 1, NOW(), NOW(), 1),
(4, 25, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 26, 'Dominar 400', 1, NOW(), NOW(), 1), (4, 27, 'Dominar 400', 1, NOW(), NOW(), 1),

-- ==========================================
-- 5. AKT (BrandId = 5)
-- ==========================================
-- NKD 125 (Años 2006 al 2026 -> ModelIds 7 al 27)
(5, 7, 'NKD 125', 1, NOW(), NOW(), 1), (5, 8, 'NKD 125', 1, NOW(), NOW(), 1), (5, 9, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 10, 'NKD 125', 1, NOW(), NOW(), 1), (5, 11, 'NKD 125', 1, NOW(), NOW(), 1), (5, 12, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 13, 'NKD 125', 1, NOW(), NOW(), 1), (5, 14, 'NKD 125', 1, NOW(), NOW(), 1), (5, 15, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 16, 'NKD 125', 1, NOW(), NOW(), 1), (5, 17, 'NKD 125', 1, NOW(), NOW(), 1), (5, 18, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 19, 'NKD 125', 1, NOW(), NOW(), 1), (5, 20, 'NKD 125', 1, NOW(), NOW(), 1), (5, 21, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 22, 'NKD 125', 1, NOW(), NOW(), 1), (5, 23, 'NKD 125', 1, NOW(), NOW(), 1), (5, 24, 'NKD 125', 1, NOW(), NOW(), 1),
(5, 25, 'NKD 125', 1, NOW(), NOW(), 1), (5, 26, 'NKD 125', 1, NOW(), NOW(), 1), (5, 27, 'NKD 125', 1, NOW(), NOW(), 1),

-- CR4 162 (Años 2018 al 2026 -> ModelIds 19 al 27)
(5, 19, 'CR4 162', 1, NOW(), NOW(), 1), (5, 20, 'CR4 162', 1, NOW(), NOW(), 1), (5, 21, 'CR4 162', 1, NOW(), NOW(), 1),
(5, 22, 'CR4 162', 1, NOW(), NOW(), 1), (5, 23, 'CR4 162', 1, NOW(), NOW(), 1), (5, 24, 'CR4 162', 1, NOW(), NOW(), 1),
(5, 25, 'CR4 162', 1, NOW(), NOW(), 1), (5, 26, 'CR4 162', 1, NOW(), NOW(), 1), (5, 27, 'CR4 162', 1, NOW(), NOW(), 1),

-- ==========================================
-- 6. TVS (BrandId = 6)
-- ==========================================
-- Apache RTR 200 4V (Años 2017 al 2026 -> ModelIds 18 al 27)
(6, 18, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 19, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 20, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1),
(6, 21, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 22, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 23, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1),
(6, 24, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 25, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1), (6, 26, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1),
(6, 27, 'Apache RTR 200 4V', 1, NOW(), NOW(), 1),

-- Raider 125 (Años 2022 al 2026 -> ModelIds 23 al 27)
(6, 23, 'Raider 125', 1, NOW(), NOW(), 1), (6, 24, 'Raider 125', 1, NOW(), NOW(), 1), (6, 25, 'Raider 125', 1, NOW(), NOW(), 1),
(6, 26, 'Raider 125', 1, NOW(), NOW(), 1), (6, 27, 'Raider 125', 1, NOW(), NOW(), 1),

-- ==========================================
-- 7. KTM (BrandId = 7)
-- ==========================================
-- Duke 200 / NG (Años 2013 al 2026 -> ModelIds 14 al 27)
(7, 14, 'Duke 200', 1, NOW(), NOW(), 1), (7, 15, 'Duke 200', 1, NOW(), NOW(), 1), (7, 16, 'Duke 200', 1, NOW(), NOW(), 1),
(7, 17, 'Duke 200', 1, NOW(), NOW(), 1), (7, 18, 'Duke 200', 1, NOW(), NOW(), 1), (7, 19, 'Duke 200', 1, NOW(), NOW(), 1),
(7, 20, 'Duke 200', 1, NOW(), NOW(), 1), (7, 21, 'Duke 200 NG', 1, NOW(), NOW(), 1), (7, 22, 'Duke 200 NG', 1, NOW(), NOW(), 1),
(7, 23, 'Duke 200 NG', 1, NOW(), NOW(), 1), (7, 24, 'Duke 200 NG', 1, NOW(), NOW(), 1), (7, 25, 'Duke 200 NG', 1, NOW(), NOW(), 1),
(7, 26, 'Duke 200 NG', 1, NOW(), NOW(), 1), (7, 27, 'Duke 200 NG', 1, NOW(), NOW(), 1),

-- ==========================================
-- 8. HERO (BrandId = 8)
-- ==========================================
-- Eco Deluxe (Años 2015 al 2026 -> ModelIds 16 al 27)
(8, 16, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 17, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 18, 'Eco Deluxe', 1, NOW(), NOW(), 1),
(8, 19, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 20, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 21, 'Eco Deluxe', 1, NOW(), NOW(), 1),
(8, 22, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 23, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 24, 'Eco Deluxe', 1, NOW(), NOW(), 1),
(8, 25, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 26, 'Eco Deluxe', 1, NOW(), NOW(), 1), (8, 27, 'Eco Deluxe', 1, NOW(), NOW(), 1),

-- ==========================================
-- 10. ROYAL ENFIELD (BrandId = 10)
-- ==========================================
-- Himalayan 411 (Años 2018 al 2025 -> ModelIds 19 al 26)
(10, 19, 'Himalayan 411', 1, NOW(), NOW(), 1), (10, 20, 'Himalayan 411', 1, NOW(), NOW(), 1), (10, 21, 'Himalayan 411', 1, NOW(), NOW(), 1),
(10, 22, 'Himalayan 411', 1, NOW(), NOW(), 1), (10, 23, 'Himalayan 411', 1, NOW(), NOW(), 1), (10, 24, 'Himalayan 411', 1, NOW(), NOW(), 1),
(10, 25, 'Himalayan 411', 1, NOW(), NOW(), 1), (10, 26, 'Himalayan 411', 1, NOW(), NOW(), 1),

-- ==========================================
-- 14. BMW (BrandId = 14)
-- ==========================================
-- R1200 GS / R1250 GS (Años 2010 al 2026 -> ModelIds 11 al 27)
(14, 11, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 12, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 13, 'R1200 GS', 1, NOW(), NOW(), 1),
(14, 14, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 15, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 16, 'R1200 GS', 1, NOW(), NOW(), 1),
(14, 17, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 18, 'R1200 GS', 1, NOW(), NOW(), 1), (14, 19, 'R1250 GS', 1, NOW(), NOW(), 1),
(14, 20, 'R1250 GS', 1, NOW(), NOW(), 1), (14, 21, 'R1250 GS', 1, NOW(), NOW(), 1), (14, 22, 'R1250 GS', 1, NOW(), NOW(), 1),
(14, 23, 'R1250 GS', 1, NOW(), NOW(), 1), (14, 24, 'R1250 GS', 1, NOW(), NOW(), 1), (14, 25, 'R1250 GS', 1, NOW(), NOW(), 1),
(14, 26, 'R1300 GS', 1, NOW(), NOW(), 1), (14, 27, 'R1300 GS', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- SCRIPT 3 (PARTE 2): CONTINUACIÓN DE CARGA MASIVA DE REFERENCIAS
-- ==============================================================================

INSERT IGNORE INTO brandmodelversion (BrandId, ModelId, version, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- 9. HUSQVARNA (BrandId = 9)
-- ==========================================
-- Svartpilen 200 (Años 2020 al 2026 -> ModelIds 21 al 27)
(9, 21, 'Svartpilen 200', 1, NOW(), NOW(), 1), (9, 22, 'Svartpilen 200', 1, NOW(), NOW(), 1), (9, 23, 'Svartpilen 200', 1, NOW(), NOW(), 1),
(9, 24, 'Svartpilen 200', 1, NOW(), NOW(), 1), (9, 25, 'Svartpilen 200', 1, NOW(), NOW(), 1), (9, 26, 'Svartpilen 200', 1, NOW(), NOW(), 1),
(9, 27, 'Svartpilen 200', 1, NOW(), NOW(), 1),

-- Svartpilen 401 (Años 2018 al 2026 -> ModelIds 19 al 27)
(9, 19, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 20, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 21, 'Svartpilen 401', 1, NOW(), NOW(), 1),
(9, 22, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 23, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 24, 'Svartpilen 401', 1, NOW(), NOW(), 1),
(9, 25, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 26, 'Svartpilen 401', 1, NOW(), NOW(), 1), (9, 27, 'Svartpilen 401', 1, NOW(), NOW(), 1),

-- ==========================================
-- 11. KYMCO (BrandId = 11)
-- ==========================================
-- Agility 125 (¡La reina de las automáticas! Años 2010 al 2026 -> ModelIds 11 al 27)
(11, 11, 'Agility 125', 1, NOW(), NOW(), 1), (11, 12, 'Agility 125', 1, NOW(), NOW(), 1), (11, 13, 'Agility 125', 1, NOW(), NOW(), 1),
(11, 14, 'Agility 125', 1, NOW(), NOW(), 1), (11, 15, 'Agility 125', 1, NOW(), NOW(), 1), (11, 16, 'Agility 125', 1, NOW(), NOW(), 1),
(11, 17, 'Agility 125', 1, NOW(), NOW(), 1), (11, 18, 'Agility 125', 1, NOW(), NOW(), 1), (11, 19, 'Agility 125', 1, NOW(), NOW(), 1),
(11, 20, 'Agility 125', 1, NOW(), NOW(), 1), (11, 21, 'Agility 125', 1, NOW(), NOW(), 1), (11, 22, 'Agility 125', 1, NOW(), NOW(), 1),
(11, 23, 'Agility 125', 1, NOW(), NOW(), 1), (11, 24, 'Agility 125', 1, NOW(), NOW(), 1), (11, 25, 'Agility 125', 1, NOW(), NOW(), 1),
(11, 26, 'Agility 125', 1, NOW(), NOW(), 1), (11, 27, 'Agility 125', 1, NOW(), NOW(), 1),

-- Agility City 150 (Años 2012 al 2026 -> ModelIds 13 al 27)
(11, 13, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 14, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 15, 'Agility City 150', 1, NOW(), NOW(), 1),
(11, 16, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 17, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 18, 'Agility City 150', 1, NOW(), NOW(), 1),
(11, 19, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 20, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 21, 'Agility City 150', 1, NOW(), NOW(), 1),
(11, 22, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 23, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 24, 'Agility City 150', 1, NOW(), NOW(), 1),
(11, 25, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 26, 'Agility City 150', 1, NOW(), NOW(), 1), (11, 27, 'Agility City 150', 1, NOW(), NOW(), 1),

-- ==========================================
-- 12. SYM (BrandId = 12)
-- ==========================================
-- Crox R 125 (Años 2017 al 2026 -> ModelIds 18 al 27)
(12, 18, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 19, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 20, 'Crox R 125', 1, NOW(), NOW(), 1),
(12, 21, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 22, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 23, 'Crox R 125', 1, NOW(), NOW(), 1),
(12, 24, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 25, 'Crox R 125', 1, NOW(), NOW(), 1), (12, 26, 'Crox R 125', 1, NOW(), NOW(), 1),
(12, 27, 'Crox R 125', 1, NOW(), NOW(), 1),

-- ==========================================
-- 13. BENELLI (BrandId = 13)
-- ==========================================
-- TRK 502 / 502X (Años 2018 al 2026 -> ModelIds 19 al 27)
(13, 19, 'TRK 502', 1, NOW(), NOW(), 1), (13, 20, 'TRK 502', 1, NOW(), NOW(), 1), (13, 21, 'TRK 502', 1, NOW(), NOW(), 1),
(13, 22, 'TRK 502', 1, NOW(), NOW(), 1), (13, 23, 'TRK 502', 1, NOW(), NOW(), 1), (13, 24, 'TRK 502', 1, NOW(), NOW(), 1),
(13, 25, 'TRK 502', 1, NOW(), NOW(), 1), (13, 26, 'TRK 502', 1, NOW(), NOW(), 1), (13, 27, 'TRK 502', 1, NOW(), NOW(), 1),

-- Leoncino 500 (Años 2018 al 2026 -> ModelIds 19 al 27)
(13, 19, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 20, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 21, 'Leoncino 500', 1, NOW(), NOW(), 1),
(13, 22, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 23, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 24, 'Leoncino 500', 1, NOW(), NOW(), 1),
(13, 25, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 26, 'Leoncino 500', 1, NOW(), NOW(), 1), (13, 27, 'Leoncino 500', 1, NOW(), NOW(), 1),

-- ==========================================
-- 15. DUCATI (BrandId = 15)
-- ==========================================
-- Scrambler Icon (Años 2015 al 2026 -> ModelIds 16 al 27)
(15, 16, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 17, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 18, 'Scrambler Icon', 1, NOW(), NOW(), 1),
(15, 19, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 20, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 21, 'Scrambler Icon', 1, NOW(), NOW(), 1),
(15, 22, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 23, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 24, 'Scrambler Icon', 1, NOW(), NOW(), 1),
(15, 25, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 26, 'Scrambler Icon', 1, NOW(), NOW(), 1), (15, 27, 'Scrambler Icon', 1, NOW(), NOW(), 1),

-- Multistrada V4 (Años 2021 al 2026 -> ModelIds 22 al 27)
(15, 22, 'Multistrada V4', 1, NOW(), NOW(), 1), (15, 23, 'Multistrada V4', 1, NOW(), NOW(), 1), (15, 24, 'Multistrada V4', 1, NOW(), NOW(), 1),
(15, 25, 'Multistrada V4', 1, NOW(), NOW(), 1), (15, 26, 'Multistrada V4', 1, NOW(), NOW(), 1), (15, 27, 'Multistrada V4', 1, NOW(), NOW(), 1),

-- ==========================================
-- 16. KAWASAKI (BrandId = 16)
-- ==========================================
-- Ninja 300 (Años 2013 al 2023 -> ModelIds 14 al 24)
(16, 14, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 15, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 16, 'Ninja 300', 1, NOW(), NOW(), 1),
(16, 17, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 18, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 19, 'Ninja 300', 1, NOW(), NOW(), 1),
(16, 20, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 21, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 22, 'Ninja 300', 1, NOW(), NOW(), 1),
(16, 23, 'Ninja 300', 1, NOW(), NOW(), 1), (16, 24, 'Ninja 300', 1, NOW(), NOW(), 1),

-- Ninja 400 (Años 2018 al 2026 -> ModelIds 19 al 27)
(16, 19, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 20, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 21, 'Ninja 400', 1, NOW(), NOW(), 1),
(16, 22, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 23, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 24, 'Ninja 400', 1, NOW(), NOW(), 1),
(16, 25, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 26, 'Ninja 400', 1, NOW(), NOW(), 1), (16, 27, 'Ninja 400', 1, NOW(), NOW(), 1),

-- KLR 650 (Histórica y el regreso: 2000-2018 y 2022-2026)
(16, 1, 'KLR 650', 1, NOW(), NOW(), 1), (16, 2, 'KLR 650', 1, NOW(), NOW(), 1), (16, 3, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 4, 'KLR 650', 1, NOW(), NOW(), 1), (16, 5, 'KLR 650', 1, NOW(), NOW(), 1), (16, 6, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 7, 'KLR 650', 1, NOW(), NOW(), 1), (16, 8, 'KLR 650', 1, NOW(), NOW(), 1), (16, 9, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 10, 'KLR 650', 1, NOW(), NOW(), 1), (16, 11, 'KLR 650', 1, NOW(), NOW(), 1), (16, 12, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 13, 'KLR 650', 1, NOW(), NOW(), 1), (16, 14, 'KLR 650', 1, NOW(), NOW(), 1), (16, 15, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 16, 'KLR 650', 1, NOW(), NOW(), 1), (16, 17, 'KLR 650', 1, NOW(), NOW(), 1), (16, 18, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 23, 'KLR 650', 1, NOW(), NOW(), 1), (16, 24, 'KLR 650', 1, NOW(), NOW(), 1), (16, 25, 'KLR 650', 1, NOW(), NOW(), 1),
(16, 26, 'KLR 650', 1, NOW(), NOW(), 1), (16, 27, 'KLR 650', 1, NOW(), NOW(), 1),

-- ==========================================
-- 20. VICTORY (BrandId = 20)
-- ==========================================
-- MRX 125 / MRX 150 (Años 2020 al 2026 -> ModelIds 21 al 27)
(20, 21, 'MRX 125', 1, NOW(), NOW(), 1), (20, 22, 'MRX 125', 1, NOW(), NOW(), 1), (20, 23, 'MRX 125', 1, NOW(), NOW(), 1),
(20, 24, 'MRX 125', 1, NOW(), NOW(), 1), (20, 25, 'MRX 125', 1, NOW(), NOW(), 1), (20, 26, 'MRX 125', 1, NOW(), NOW(), 1),
(20, 27, 'MRX 125', 1, NOW(), NOW(), 1),
(20, 21, 'MRX 150', 1, NOW(), NOW(), 1), (20, 22, 'MRX 150', 1, NOW(), NOW(), 1), (20, 23, 'MRX 150', 1, NOW(), NOW(), 1),
(20, 24, 'MRX 150', 1, NOW(), NOW(), 1), (20, 25, 'MRX 150', 1, NOW(), NOW(), 1), (20, 26, 'MRX 150', 1, NOW(), NOW(), 1),
(20, 27, 'MRX 150', 1, NOW(), NOW(), 1),

-- Blackline 250 (Años 2022 al 2026 -> ModelIds 23 al 27)
(20, 23, 'Blackline 250', 1, NOW(), NOW(), 1), (20, 24, 'Blackline 250', 1, NOW(), NOW(), 1), (20, 25, 'Blackline 250', 1, NOW(), NOW(), 1),
(20, 26, 'Blackline 250', 1, NOW(), NOW(), 1), (20, 27, 'Blackline 250', 1, NOW(), NOW(), 1);


USE tallermoto;

-- ==============================================================================
-- SCRIPT 3 (PARTE 3 FINAL): CONTINUACIÓN Y CIERRE DE REFERENCIAS
-- ==============================================================================

INSERT IGNORE INTO brandmodelversion (BrandId, ModelId, version, is_active, created_at, updated_at, responsible_user_id) VALUES

-- ==========================================
-- 17. TRIUMPH (BrandId = 17)
-- ==========================================
-- Tiger 800 (Años 2011 al 2020 -> ModelIds 12 al 21)
(17, 12, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 13, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 14, 'Tiger 800', 1, NOW(), NOW(), 1),
(17, 15, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 16, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 17, 'Tiger 800', 1, NOW(), NOW(), 1),
(17, 18, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 19, 'Tiger 800', 1, NOW(), NOW(), 1), (17, 20, 'Tiger 800', 1, NOW(), NOW(), 1),
(17, 21, 'Tiger 800', 1, NOW(), NOW(), 1),

-- Tiger 900 (Años 2020 al 2026 -> ModelIds 21 al 27)
(17, 21, 'Tiger 900', 1, NOW(), NOW(), 1), (17, 22, 'Tiger 900', 1, NOW(), NOW(), 1), (17, 23, 'Tiger 900', 1, NOW(), NOW(), 1),
(17, 24, 'Tiger 900', 1, NOW(), NOW(), 1), (17, 25, 'Tiger 900', 1, NOW(), NOW(), 1), (17, 26, 'Tiger 900', 1, NOW(), NOW(), 1),
(17, 27, 'Tiger 900', 1, NOW(), NOW(), 1),

-- ==========================================
-- 18. HARLEY-DAVIDSON (BrandId = 18)
-- ==========================================
-- Iron 883 (Años 2010 al 2022 -> ModelIds 11 al 23)
(18, 11, 'Iron 883', 1, NOW(), NOW(), 1), (18, 12, 'Iron 883', 1, NOW(), NOW(), 1), (18, 13, 'Iron 883', 1, NOW(), NOW(), 1),
(18, 14, 'Iron 883', 1, NOW(), NOW(), 1), (18, 15, 'Iron 883', 1, NOW(), NOW(), 1), (18, 16, 'Iron 883', 1, NOW(), NOW(), 1),
(18, 17, 'Iron 883', 1, NOW(), NOW(), 1), (18, 18, 'Iron 883', 1, NOW(), NOW(), 1), (18, 19, 'Iron 883', 1, NOW(), NOW(), 1),
(18, 20, 'Iron 883', 1, NOW(), NOW(), 1), (18, 21, 'Iron 883', 1, NOW(), NOW(), 1), (18, 22, 'Iron 883', 1, NOW(), NOW(), 1),
(18, 23, 'Iron 883', 1, NOW(), NOW(), 1),

-- ==========================================
-- 19. CFMOTO (BrandId = 19)
-- ==========================================
-- 250 SR (Años 2021 al 2026 -> ModelIds 22 al 27)
(19, 22, '250 SR', 1, NOW(), NOW(), 1), (19, 23, '250 SR', 1, NOW(), NOW(), 1), (19, 24, '250 SR', 1, NOW(), NOW(), 1),
(19, 25, '250 SR', 1, NOW(), NOW(), 1), (19, 26, '250 SR', 1, NOW(), NOW(), 1), (19, 27, '250 SR', 1, NOW(), NOW(), 1),

-- 400 NK (Años 2018 al 2026 -> ModelIds 19 al 27)
(19, 19, '400 NK', 1, NOW(), NOW(), 1), (19, 20, '400 NK', 1, NOW(), NOW(), 1), (19, 21, '400 NK', 1, NOW(), NOW(), 1),
(19, 22, '400 NK', 1, NOW(), NOW(), 1), (19, 23, '400 NK', 1, NOW(), NOW(), 1), (19, 24, '400 NK', 1, NOW(), NOW(), 1),
(19, 25, '400 NK', 1, NOW(), NOW(), 1), (19, 26, '400 NK', 1, NOW(), NOW(), 1), (19, 27, '400 NK', 1, NOW(), NOW(), 1),

-- ==========================================
-- 21. AUTECO MOBILITY (BrandId = 21)
-- ==========================================
-- Combat 125 (Años 2022 al 2026 -> ModelIds 23 al 27)
(21, 23, 'Combat 125', 1, NOW(), NOW(), 1), (21, 24, 'Combat 125', 1, NOW(), NOW(), 1), (21, 25, 'Combat 125', 1, NOW(), NOW(), 1),
(21, 26, 'Combat 125', 1, NOW(), NOW(), 1), (21, 27, 'Combat 125', 1, NOW(), NOW(), 1),

-- ==========================================
-- 22. VESPA (BrandId = 22)
-- ==========================================
-- Primavera 150 (Años 2015 al 2026 -> ModelIds 16 al 27)
(22, 16, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 17, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 18, 'Primavera 150', 1, NOW(), NOW(), 1),
(22, 19, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 20, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 21, 'Primavera 150', 1, NOW(), NOW(), 1),
(22, 22, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 23, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 24, 'Primavera 150', 1, NOW(), NOW(), 1),
(22, 25, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 26, 'Primavera 150', 1, NOW(), NOW(), 1), (22, 27, 'Primavera 150', 1, NOW(), NOW(), 1),

-- ==========================================
-- 24. APRILIA (BrandId = 24)
-- ==========================================
-- SR 160 (Años 2021 al 2026 -> ModelIds 22 al 27)
(24, 22, 'SR 160', 1, NOW(), NOW(), 1), (24, 23, 'SR 160', 1, NOW(), NOW(), 1), (24, 24, 'SR 160', 1, NOW(), NOW(), 1),
(24, 25, 'SR 160', 1, NOW(), NOW(), 1), (24, 26, 'SR 160', 1, NOW(), NOW(), 1), (24, 27, 'SR 160', 1, NOW(), NOW(), 1),

-- ==========================================
-- 25. ZONTES (BrandId = 25)
-- ==========================================
-- T310 / T350 (Años 2020 al 2026 -> ModelIds 21 al 27)
(25, 21, 'T310', 1, NOW(), NOW(), 1), (25, 22, 'T310', 1, NOW(), NOW(), 1), (25, 23, 'T310', 1, NOW(), NOW(), 1),
(25, 24, 'T350', 1, NOW(), NOW(), 1), (25, 25, 'T350', 1, NOW(), NOW(), 1), (25, 26, 'T350', 1, NOW(), NOW(), 1),
(25, 27, 'T350', 1, NOW(), NOW(), 1),

-- ==========================================
-- 27. VOGE (BrandId = 27)
-- ==========================================
-- 300 DS (Años 2021 al 2026 -> ModelIds 22 al 27)
(27, 22, '300 DS', 1, NOW(), NOW(), 1), (27, 23, '300 DS', 1, NOW(), NOW(), 1), (27, 24, '300 DS', 1, NOW(), NOW(), 1),
(27, 25, '300 DS', 1, NOW(), NOW(), 1), (27, 26, '300 DS', 1, NOW(), NOW(), 1), (27, 27, '300 DS', 1, NOW(), NOW(), 1),

-- ==========================================
-- 29. NIU (ELÉCTRICAS) (BrandId = 29)
-- ==========================================
-- NQi Sport (Años 2021 al 2026 -> ModelIds 22 al 27)
(29, 22, 'NQi Sport', 1, NOW(), NOW(), 1), (29, 23, 'NQi Sport', 1, NOW(), NOW(), 1), (29, 24, 'NQi Sport', 1, NOW(), NOW(), 1),
(29, 25, 'NQi Sport', 1, NOW(), NOW(), 1), (29, 26, 'NQi Sport', 1, NOW(), NOW(), 1), (29, 27, 'NQi Sport', 1, NOW(), NOW(), 1);
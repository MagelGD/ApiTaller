USE tallermoto;

-- ==============================================================================
-- CARGA DE CATÁLOGO DE SERVICIOS (service_catalog)
-- Relacionado con: service_type (id)
-- ==============================================================================

INSERT IGNORE INTO service_catalog (service_type_id, name, description, default_minutes, default_price, time_unit, is_active, created_at, updated_at, responsible_user_id) VALUES

-- 1. Mantenimiento Preventivo (service_type_id = 1)
(1, 'Cambio de Aceite y Filtro', 'Drenaje de aceite, cambio de filtro y revisión de niveles', 30, 25000, 'Minutos', 1, NOW(), NOW(), 1),
(1, 'Lubricación y Ajuste de Cadena', 'Limpieza, lubricación con producto especializado y tensión de cadena', 15, 15000, 'Minutos', 1, NOW(), NOW(), 1),
(1, 'Sincronización Básica', 'Limpieza de cuerpo de aceleración/carburador y revisión de bujía', 60, 45000, 'Minutos', 1, NOW(), NOW(), 1),
(1, 'Mantenimiento General Estándar', 'Revisión de frenos, niveles, luces, llantas y ajuste general', 120, 80000, 'Minutos', 1, NOW(), NOW(), 1),

-- 2. Mantenimiento Correctivo (service_type_id = 2)
(2, 'Reparación de Fuga de Aceite', 'Cambio de retenedores y empaques en motor', 180, 120000, 'Minutos', 1, NOW(), NOW(), 1),
(2, 'Cambio de Pastillas de Freno', 'Desmonte de mordaza, limpieza y cambio de pastillas', 30, 20000, 'Minutos', 1, NOW(), NOW(), 1),
(2, 'Reparación de Sistema Eléctrico', 'Localización de cortos o falla en carga/luces', 90, 60000, 'Minutos', 1, NOW(), NOW(), 1),
(2, 'Cambio de Kit de Arrastre', 'Desmonte de ruedas, cambio de piñones y cadena', 60, 40000, 'Minutos', 1, NOW(), NOW(), 1),

-- 3. Garantía (service_type_id = 3)
(3, 'Revisión por Garantía (Motor)', 'Inspección técnica para solicitud de garantía de fábrica', 60, 0, 'Minutos', 1, NOW(), NOW(), 1),
(3, 'Revisión por Garantía (Eléctrica)', 'Revisión de componentes eléctricos bajo cobertura', 60, 0, 'Minutos', 1, NOW(), NOW(), 1),

-- 4. Revisión General / Viaje (service_type_id = 4)
(4, 'Revisión Pre-Viaje (Completa)', 'Inspección profunda de 20 puntos clave antes de salir a carretera', 150, 95000, 'Minutos', 1, NOW(), NOW(), 1),

-- 5. Modificaciones / Personalización (service_type_id = 5)
(5, 'Instalación de Exploradoras', 'Montaje de exploradoras LED, cableado y switch', 90, 50000, 'Minutos', 1, NOW(), NOW(), 1),
(5, 'Instalación de Alarmas/GPS', 'Conexión eléctrica de seguridad', 120, 70000, 'Minutos', 1, NOW(), NOW(), 1),
(5, 'Instalación de Accesorios (Slider, Cúpulas)', 'Montaje de piezas estéticas o de protección', 45, 30000, 'Minutos', 1, NOW(), NOW(), 1),

-- 6. Lavado y Detallado (service_type_id = 6)
(6, 'Lavado General con Cera', 'Lavado profundo de motor, chasis y detallado final', 60, 25000, 'Minutos', 1, NOW(), NOW(), 1),
(6, 'Detallado de Motor y Partes Negras', 'Aplicación de renovadores de plástico y partes del motor', 30, 15000, 'Minutos', 1, NOW(), NOW(), 1),

-- 7. Diagnóstico Escáner (service_type_id = 7)
(7, 'Escaneo Computarizado (Inyección)', 'Lectura de códigos de error y parámetros en tiempo real', 20, 35000, 'Minutos', 1, NOW(), NOW(), 1);
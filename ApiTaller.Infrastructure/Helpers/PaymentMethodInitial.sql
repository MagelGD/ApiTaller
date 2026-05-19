USE tallermoto;

-- ==============================================================================
-- CARGA DE MÉTODOS DE PAGO - COMPATIBLES CON ANGULAR MATERIAL
-- ==============================================================================

INSERT IGNORE INTO payment_method (name, icon, is_active, created_at, updated_at, responsible_user_id) VALUES
('Efectivo', 'payments', 1, NOW(), NOW(), 1),
('Nequi', 'smartphone', 1, NOW(), NOW(), 1),
('Daviplata', 'mobile_friendly', 1, NOW(), NOW(), 1),
('Código QR', 'qr_code_2', 1, NOW(), NOW(), 1),
('PSE / Transferencia', 'account_balance', 1, NOW(), NOW(), 1),
('Tarjeta de Crédito', 'credit_card', 1, NOW(), NOW(), 1);
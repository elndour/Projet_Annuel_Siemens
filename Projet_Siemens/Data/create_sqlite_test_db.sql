-- ========================================
-- SCRIPT SQLITE - CRÉATION DE LA BASE MES DE TEST
-- Compatible SQLite (pas d'installation serveur requise)
-- ========================================

-- 1. Table des ordres de production
CREATE TABLE IF NOT EXISTS production_orders (
    order_id INTEGER PRIMARY KEY,
    product_id TEXT,
    product_name TEXT,
    quantity_planned INTEGER,
    quantity_produced INTEGER,
    start_time TEXT,
    end_time TEXT,
    status TEXT,
    efficiency_rate REAL
);

-- 2. Table des logs d'erreurs
CREATE TABLE IF NOT EXISTS error_log (
    error_id INTEGER PRIMARY KEY,
    error_code TEXT,
    error_message TEXT,
    error_timestamp TEXT,
    module_name TEXT,
    severity_level TEXT,
    stack_trace TEXT
);

-- 3. Table des tâches MES
CREATE TABLE IF NOT EXISTS mes_tasks (
    task_id INTEGER PRIMARY KEY,
    task_name TEXT,
    task_type TEXT,
    task_status TEXT,
    assigned_machine TEXT,
    assigned_user TEXT,
    start_time TEXT,
    estimated_end_time TEXT,
    completion_percentage REAL,
    priority_level INTEGER
);

-- 4. Table des temps d'arrêt
CREATE TABLE IF NOT EXISTS downtime_log (
    downtime_id INTEGER PRIMARY KEY,
    machine_id TEXT,
    machine_name TEXT,
    downtime_reason TEXT,
    downtime_category TEXT,
    downtime_start TEXT,
    downtime_end TEXT,
    duration_minutes INTEGER,
    reported_by TEXT
);

-- 5. Table des opérations
CREATE TABLE IF NOT EXISTS operations_log (
    operation_id INTEGER PRIMARY KEY,
    machine_id TEXT,
    machine_name TEXT,
    operation_date TEXT,
    processing_time INTEGER,
    status TEXT
);

-- 6. Table de contrôle qualité
CREATE TABLE IF NOT EXISTS quality_control (
    inspection_id INTEGER PRIMARY KEY,
    product_id TEXT,
    product_name TEXT,
    inspection_date TEXT,
    quality_status TEXT,
    quality_score REAL
);

-- 7. Table d'inventaire
CREATE TABLE IF NOT EXISTS inventory (
    material_id TEXT PRIMARY KEY,
    material_name TEXT,
    material_type TEXT,
    current_quantity INTEGER,
    min_quantity INTEGER,
    max_quantity INTEGER,
    unit_of_measure TEXT,
    last_updated TEXT,
    warehouse_location TEXT
);

-- 8. Table des activités utilisateur
CREATE TABLE IF NOT EXISTS user_activity_log (
    activity_id INTEGER PRIMARY KEY,
    user_id TEXT,
    username TEXT,
    action_type TEXT,
    action_description TEXT,
    action_timestamp TEXT,
    affected_entity TEXT,
    ip_address TEXT
);

-- 9. Table des indicateurs de santé
CREATE TABLE IF NOT EXISTS system_health_indicators (
    indicator_id INTEGER PRIMARY KEY,
    indicator_name TEXT,
    indicator_value REAL,
    indicator_unit TEXT,
    status TEXT,
    measurement_time TEXT,
    threshold_warning REAL,
    threshold_critical REAL
);

-- 10. Table de production par batch
CREATE TABLE IF NOT EXISTS batch_production (
    batch_id INTEGER PRIMARY KEY,
    batch_number TEXT,
    product_id TEXT,
    product_name TEXT,
    batch_start_time TEXT,
    batch_end_time TEXT,
    total_quantity INTEGER,
    good_quantity INTEGER,
    rejected_quantity INTEGER,
    batch_status TEXT,
    yield_percentage REAL
);

-- ========================================
-- INSERTION DE DONNÉES DE TEST
-- ========================================

-- Ordres de production
INSERT INTO production_orders VALUES (1, 'PROD-001', 'Widget A', 1000, 987, '2025-01-08 08:00:00', '2025-01-08 16:30:00', 'COMPLETED', 98.7);
INSERT INTO production_orders VALUES (2, 'PROD-002', 'Widget B', 500, 495, '2025-01-07 09:00:00', '2025-01-07 15:00:00', 'COMPLETED', 99.0);
INSERT INTO production_orders VALUES (3, 'PROD-003', 'Gadget X', 2000, 1850, '2025-01-06 07:00:00', '2025-01-06 18:00:00', 'COMPLETED', 92.5);
INSERT INTO production_orders VALUES (4, 'PROD-004', 'Component Y', 1500, 1500, '2025-01-05 08:30:00', '2025-01-05 17:00:00', 'COMPLETED', 100.0);
INSERT INTO production_orders VALUES (5, 'PROD-001', 'Widget A', 1200, 1050, '2025-01-04 08:00:00', '2025-01-04 19:30:00', 'COMPLETED', 87.5);

-- Logs d'erreurs
INSERT INTO error_log VALUES (1, 'ERR-500', 'Database connection timeout', '2025-01-08 14:23:00', 'DatabaseModule', 'CRITICAL', 'Exception at line 245');
INSERT INTO error_log VALUES (2, 'ERR-404', 'Resource not found', '2025-01-08 10:15:00', 'FileHandler', 'ERROR', 'FileNotFoundException');
INSERT INTO error_log VALUES (3, 'ERR-301', 'Machine communication failed', '2025-01-07 16:40:00', 'PLCInterface', 'CRITICAL', 'TimeoutException');
INSERT INTO error_log VALUES (4, 'ERR-200', 'Invalid parameter', '2025-01-07 11:30:00', 'ValidationModule', 'ERROR', 'ArgumentException');
INSERT INTO error_log VALUES (5, 'ERR-101', 'Low memory warning', '2025-01-06 09:20:00', 'SystemMonitor', 'WARNING', 'MemoryException');

-- Tâches MES
INSERT INTO mes_tasks VALUES (1, 'Quality Inspection Batch-001', 'INSPECTION', 'RUNNING', 'QC-STATION-01', 'john.doe', '2025-01-09 08:00:00', '2025-01-09 12:00:00', 45.5, 1);
INSERT INTO mes_tasks VALUES (2, 'Machine Calibration', 'MAINTENANCE', 'PENDING', 'MACHINE-05', 'tech.support', '2025-01-09 14:00:00', '2025-01-09 16:00:00', 0, 3);
INSERT INTO mes_tasks VALUES (3, 'Production Line Cleanup', 'CLEANING', 'ERROR', 'LINE-A', 'maintenance.crew', '2025-01-09 06:00:00', '2025-01-09 08:00:00', 75.0, 2);

-- Temps d'arrêt
INSERT INTO downtime_log VALUES (1, 'MACHINE-01', 'CNC Milling Machine', 'Tool breakage', 'MECHANICAL', '2025-01-08 10:30:00', '2025-01-08 11:15:00', 45, 'operator.smith');
INSERT INTO downtime_log VALUES (2, 'MACHINE-03', 'Assembly Robot', 'Software error', 'ELECTRICAL', '2025-01-07 14:00:00', '2025-01-07 14:20:00', 20, 'tech.jones');
INSERT INTO downtime_log VALUES (3, 'LINE-B', 'Packaging Line', 'Material shortage', 'MATERIAL', '2025-01-06 09:00:00', '2025-01-06 10:30:00', 90, 'supervisor.brown');

-- Opérations (20 exemples)
INSERT INTO operations_log (operation_id, machine_id, machine_name, operation_date, processing_time, status)
SELECT 
    seq.num as operation_id,
    'MACHINE-' || (seq.num % 5) as machine_id,
    'Machine ' || (seq.num % 5) as machine_name,
    date('now', '-' || (seq.num % 7) || ' days') as operation_date,
    50 + (seq.num * 13 % 100) as processing_time,
    CASE 
        WHEN seq.num % 4 = 0 THEN 'FAILED'
        WHEN seq.num % 7 = 0 THEN 'WARNING'
        ELSE 'SUCCESS'
    END as status
FROM (
    SELECT 1 as num UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5
    UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10
    UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15
    UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20
) seq;

-- Inventaire
INSERT INTO inventory VALUES ('MAT-001', 'Steel Sheet 2mm', 'RAW_MATERIAL', 50, 100, 500, 'KG', datetime('now'), 'WAREHOUSE-A');
INSERT INTO inventory VALUES ('MAT-002', 'Aluminum Rod 10mm', 'RAW_MATERIAL', 80, 150, 600, 'METERS', datetime('now'), 'WAREHOUSE-B');
INSERT INTO inventory VALUES ('MAT-003', 'Plastic Pellets', 'RAW_MATERIAL', 120, 100, 400, 'KG', datetime('now'), 'WAREHOUSE-A');

-- Production par batch
INSERT INTO batch_production VALUES (1, 'BATCH-2025-001', 'PROD-001', 'Widget A', '2025-01-08 06:00:00', '2025-01-08 18:00:00', 1000, 987, 13, 'COMPLETED', 98.7);
INSERT INTO batch_production VALUES (2, 'BATCH-2025-002', 'PROD-002', 'Widget B', '2025-01-07 07:00:00', '2025-01-07 15:00:00', 500, 495, 5, 'COMPLETED', 99.0);

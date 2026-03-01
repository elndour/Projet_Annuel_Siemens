-- ========================================
-- SCRIPT DE CRÉATION DE TABLES DE TEST MES
-- Pour Oracle XE / PostgreSQL
-- ========================================

-- 1. Table des ordres de production
CREATE TABLE production_orders (
    order_id NUMBER PRIMARY KEY,
    product_id VARCHAR2(50),
    product_name VARCHAR2(100),
    quantity_planned NUMBER,
    quantity_produced NUMBER,
    start_time TIMESTAMP,
    end_time TIMESTAMP,
    status VARCHAR2(20),
    efficiency_rate NUMBER(5,2)
);

-- 2. Table des logs d'erreurs
CREATE TABLE error_log (
    error_id NUMBER PRIMARY KEY,
    error_code VARCHAR2(20),
    error_message VARCHAR2(500),
    error_timestamp TIMESTAMP,
    module_name VARCHAR2(100),
    severity_level VARCHAR2(20),
    stack_trace VARCHAR2(2000)
);

-- 3. Table des tâches MES
CREATE TABLE mes_tasks (
    task_id NUMBER PRIMARY KEY,
    task_name VARCHAR2(100),
    task_type VARCHAR2(50),
    task_status VARCHAR2(20),
    assigned_machine VARCHAR2(50),
    assigned_user VARCHAR2(50),
    start_time TIMESTAMP,
    estimated_end_time TIMESTAMP,
    completion_percentage NUMBER(5,2),
    priority_level NUMBER
);

-- 4. Table des temps d'arrêt
CREATE TABLE downtime_log (
    downtime_id NUMBER PRIMARY KEY,
    machine_id VARCHAR2(50),
    machine_name VARCHAR2(100),
    downtime_reason VARCHAR2(200),
    downtime_category VARCHAR2(50),
    downtime_start TIMESTAMP,
    downtime_end TIMESTAMP,
    duration_minutes NUMBER,
    reported_by VARCHAR2(50)
);

-- 5. Table des opérations (pour statistiques)
CREATE TABLE operations_log (
    operation_id NUMBER PRIMARY KEY,
    machine_id VARCHAR2(50),
    machine_name VARCHAR2(100),
    operation_date DATE,
    processing_time NUMBER,
    status VARCHAR2(20)
);

-- 6. Table de contrôle qualité
CREATE TABLE quality_control (
    inspection_id NUMBER PRIMARY KEY,
    product_id VARCHAR2(50),
    product_name VARCHAR2(100),
    inspection_date DATE,
    quality_status VARCHAR2(20),
    quality_score NUMBER(5,2)
);

-- 7. Table d'inventaire
CREATE TABLE inventory (
    material_id VARCHAR2(50) PRIMARY KEY,
    material_name VARCHAR2(100),
    material_type VARCHAR2(50),
    current_quantity NUMBER,
    min_quantity NUMBER,
    max_quantity NUMBER,
    unit_of_measure VARCHAR2(20),
    last_updated TIMESTAMP,
    warehouse_location VARCHAR2(50)
);

-- 8. Table des activités utilisateur
CREATE TABLE user_activity_log (
    activity_id NUMBER PRIMARY KEY,
    user_id VARCHAR2(50),
    username VARCHAR2(50),
    action_type VARCHAR2(50),
    action_description VARCHAR2(200),
    action_timestamp TIMESTAMP,
    affected_entity VARCHAR2(100),
    ip_address VARCHAR2(50)
);

-- 9. Table des indicateurs de santé système
CREATE TABLE system_health_indicators (
    indicator_id NUMBER PRIMARY KEY,
    indicator_name VARCHAR2(100),
    indicator_value NUMBER,
    indicator_unit VARCHAR2(20),
    status VARCHAR2(20),
    measurement_time TIMESTAMP,
    threshold_warning NUMBER,
    threshold_critical NUMBER
);

-- 10. Table de production par batch
CREATE TABLE batch_production (
    batch_id NUMBER PRIMARY KEY,
    batch_number VARCHAR2(50),
    product_id VARCHAR2(50),
    product_name VARCHAR2(100),
    batch_start_time TIMESTAMP,
    batch_end_time TIMESTAMP,
    total_quantity NUMBER,
    good_quantity NUMBER,
    rejected_quantity NUMBER,
    batch_status VARCHAR2(20),
    yield_percentage NUMBER(5,2)
);

-- ========================================
-- INSERTION DE DONNÉES DE TEST
-- ========================================

-- Ordres de production (10 exemples)
INSERT INTO production_orders VALUES (1, 'PROD-001', 'Widget A', 1000, 987, TIMESTAMP '2025-01-08 08:00:00', TIMESTAMP '2025-01-08 16:30:00', 'COMPLETED', 98.7);
INSERT INTO production_orders VALUES (2, 'PROD-002', 'Widget B', 500, 495, TIMESTAMP '2025-01-07 09:00:00', TIMESTAMP '2025-01-07 15:00:00', 'COMPLETED', 99.0);
INSERT INTO production_orders VALUES (3, 'PROD-003', 'Gadget X', 2000, 1850, TIMESTAMP '2025-01-06 07:00:00', TIMESTAMP '2025-01-06 18:00:00', 'COMPLETED', 92.5);
INSERT INTO production_orders VALUES (4, 'PROD-004', 'Component Y', 1500, 1500, TIMESTAMP '2025-01-05 08:30:00', TIMESTAMP '2025-01-05 17:00:00', 'COMPLETED', 100.0);
INSERT INTO production_orders VALUES (5, 'PROD-001', 'Widget A', 1200, 1050, TIMESTAMP '2025-01-04 08:00:00', TIMESTAMP '2025-01-04 19:30:00', 'COMPLETED', 87.5);

-- Logs d'erreurs (5 exemples)
INSERT INTO error_log VALUES (1, 'ERR-500', 'Database connection timeout', TIMESTAMP '2025-01-08 14:23:00', 'DatabaseModule', 'CRITICAL', 'OracleException at line 245');
INSERT INTO error_log VALUES (2, 'ERR-404', 'Resource not found', TIMESTAMP '2025-01-08 10:15:00', 'FileHandler', 'ERROR', 'FileNotFoundException at line 102');
INSERT INTO error_log VALUES (3, 'ERR-301', 'Machine communication failed', TIMESTAMP '2025-01-07 16:40:00', 'PLCInterface', 'CRITICAL', 'TimeoutException at line 89');
INSERT INTO error_log VALUES (4, 'ERR-200', 'Invalid parameter', TIMESTAMP '2025-01-07 11:30:00', 'ValidationModule', 'ERROR', 'ArgumentException at line 56');
INSERT INTO error_log VALUES (5, 'ERR-101', 'Low memory warning', TIMESTAMP '2025-01-06 09:20:00', 'SystemMonitor', 'WARNING', 'MemoryException at line 334');

-- Tâches MES (3 exemples)
INSERT INTO mes_tasks VALUES (1, 'Quality Inspection Batch-001', 'INSPECTION', 'RUNNING', 'QC-STATION-01', 'john.doe', TIMESTAMP '2025-01-09 08:00:00', TIMESTAMP '2025-01-09 12:00:00', 45.5, 1);
INSERT INTO mes_tasks VALUES (2, 'Machine Calibration', 'MAINTENANCE', 'PENDING', 'MACHINE-05', 'tech.support', TIMESTAMP '2025-01-09 14:00:00', TIMESTAMP '2025-01-09 16:00:00', 0, 3);
INSERT INTO mes_tasks VALUES (3, 'Production Line Cleanup', 'CLEANING', 'ERROR', 'LINE-A', 'maintenance.crew', TIMESTAMP '2025-01-09 06:00:00', TIMESTAMP '2025-01-09 08:00:00', 75.0, 2);

-- Temps d'arrêt (4 exemples)
INSERT INTO downtime_log VALUES (1, 'MACHINE-01', 'CNC Milling Machine', 'Tool breakage', 'MECHANICAL', TIMESTAMP '2025-01-08 10:30:00', TIMESTAMP '2025-01-08 11:15:00', 45, 'operator.smith');
INSERT INTO downtime_log VALUES (2, 'MACHINE-03', 'Assembly Robot', 'Software error', 'ELECTRICAL', TIMESTAMP '2025-01-07 14:00:00', TIMESTAMP '2025-01-07 14:20:00', 20, 'tech.jones');
INSERT INTO downtime_log VALUES (3, 'LINE-B', 'Packaging Line', 'Material shortage', 'MATERIAL', TIMESTAMP '2025-01-06 09:00:00', TIMESTAMP '2025-01-06 10:30:00', 90, 'supervisor.brown');
INSERT INTO downtime_log VALUES (4, 'MACHINE-02', 'Welding Station', 'Planned maintenance', 'PLANNED', TIMESTAMP '2025-01-05 07:00:00', TIMESTAMP '2025-01-05 09:00:00', 120, 'maintenance.lead');

-- Opérations (pour statistiques - 20 exemples)
BEGIN
    FOR i IN 1..20 LOOP
        INSERT INTO operations_log VALUES (
            i, 
            'MACHINE-' || MOD(i, 5), 
            'Machine ' || MOD(i, 5), 
            CURRENT_DATE - MOD(i, 7),
            50 + MOD(i * 13, 100),
            CASE WHEN MOD(i, 4) = 0 THEN 'FAILED' WHEN MOD(i, 7) = 0 THEN 'WARNING' ELSE 'SUCCESS' END
        );
    END LOOP;
END;
/

-- Contrôle qualité (10 exemples)
BEGIN
    FOR i IN 1..10 LOOP
        INSERT INTO quality_control VALUES (
            i,
            'PROD-' || LPAD(TO_CHAR(MOD(i, 5) + 1), 3, '0'),
            'Product ' || (MOD(i, 5) + 1),
            CURRENT_DATE - MOD(i, 30),
            CASE WHEN MOD(i, 5) = 0 THEN 'FAILED' WHEN MOD(i, 8) = 0 THEN 'REWORK' ELSE 'PASSED' END,
            85 + MOD(i * 7, 15)
        );
    END LOOP;
END;
/

-- Inventaire (5 exemples - avec niveaux bas)
INSERT INTO inventory VALUES ('MAT-001', 'Steel Sheet 2mm', 'RAW_MATERIAL', 50, 100, 500, 'KG', CURRENT_TIMESTAMP, 'WAREHOUSE-A');
INSERT INTO inventory VALUES ('MAT-002', 'Aluminum Rod 10mm', 'RAW_MATERIAL', 80, 150, 600, 'METERS', CURRENT_TIMESTAMP, 'WAREHOUSE-B');
INSERT INTO inventory VALUES ('MAT-003', 'Plastic Pellets', 'RAW_MATERIAL', 120, 100, 400, 'KG', CURRENT_TIMESTAMP, 'WAREHOUSE-A');
INSERT INTO inventory VALUES ('MAT-004', 'Fasteners M6', 'COMPONENT', 2000, 5000, 20000, 'PIECES', CURRENT_TIMESTAMP, 'WAREHOUSE-C');
INSERT INTO inventory VALUES ('MAT-005', 'Packaging Material', 'CONSUMABLE', 500, 1000, 3000, 'UNITS', CURRENT_TIMESTAMP, 'WAREHOUSE-D');

-- Activités utilisateur (8 exemples)
BEGIN
    FOR i IN 1..8 LOOP
        INSERT INTO user_activity_log VALUES (
            i,
            'USER-' || LPAD(TO_CHAR(MOD(i, 3) + 1), 3, '0'),
            CASE MOD(i, 3) WHEN 0 THEN 'admin.user' WHEN 1 THEN 'operator.smith' ELSE 'supervisor.jones' END,
            CASE MOD(i, 4) WHEN 0 THEN 'LOGIN' WHEN 1 THEN 'DATA_EDIT' WHEN 2 THEN 'REPORT_VIEW' ELSE 'CONFIG_CHANGE' END,
            'User action description ' || i,
            CURRENT_TIMESTAMP - (i / 24),
            'ENTITY-' || i,
            '192.168.1.' || (100 + i)
        );
    END LOOP;
END;
/

-- Indicateurs de santé (6 exemples)
INSERT INTO system_health_indicators VALUES (1, 'CPU Usage', 45.5, '%', 'NORMAL', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 70, 90);
INSERT INTO system_health_indicators VALUES (2, 'Memory Usage', 62.3, '%', 'NORMAL', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 75, 90);
INSERT INTO system_health_indicators VALUES (3, 'Disk Space', 78.9, '%', 'WARNING', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 75, 90);
INSERT INTO system_health_indicators VALUES (4, 'Network Latency', 15, 'ms', 'NORMAL', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 50, 100);
INSERT INTO system_health_indicators VALUES (5, 'Database Connections', 35, 'count', 'NORMAL', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 80, 100);
INSERT INTO system_health_indicators VALUES (6, 'Temperature Sensor-1', 42, '°C', 'NORMAL', CURRENT_TIMESTAMP - INTERVAL '30' MINUTE, 60, 75);

-- Production par batch (5 exemples)
INSERT INTO batch_production VALUES (1, 'BATCH-2025-001', 'PROD-001', 'Widget A', TIMESTAMP '2025-01-08 06:00:00', TIMESTAMP '2025-01-08 18:00:00', 1000, 987, 13, 'COMPLETED', 98.7);
INSERT INTO batch_production VALUES (2, 'BATCH-2025-002', 'PROD-002', 'Widget B', TIMESTAMP '2025-01-07 07:00:00', TIMESTAMP '2025-01-07 15:00:00', 500, 495, 5, 'COMPLETED', 99.0);
INSERT INTO batch_production VALUES (3, 'BATCH-2025-003', 'PROD-003', 'Gadget X', TIMESTAMP '2025-01-06 06:00:00', TIMESTAMP '2025-01-06 19:00:00', 2000, 1850, 150, 'COMPLETED', 92.5);
INSERT INTO batch_production VALUES (4, 'BATCH-2025-004', 'PROD-004', 'Component Y', TIMESTAMP '2025-01-05 08:00:00', TIMESTAMP '2025-01-05 17:00:00', 1500, 1500, 0, 'COMPLETED', 100.0);
INSERT INTO batch_production VALUES (5, 'BATCH-2025-005', 'PROD-001', 'Widget A', TIMESTAMP '2025-01-04 06:00:00', TIMESTAMP '2025-01-04 20:00:00', 1200, 1050, 150, 'COMPLETED', 87.5);

COMMIT;

-- Vérification des données insérées
SELECT 'production_orders' as table_name, COUNT(*) as row_count FROM production_orders
UNION ALL
SELECT 'error_log', COUNT(*) FROM error_log
UNION ALL
SELECT 'mes_tasks', COUNT(*) FROM mes_tasks
UNION ALL
SELECT 'downtime_log', COUNT(*) FROM downtime_log
UNION ALL
SELECT 'operations_log', COUNT(*) FROM operations_log
UNION ALL
SELECT 'quality_control', COUNT(*) FROM quality_control
UNION ALL
SELECT 'inventory', COUNT(*) FROM inventory
UNION ALL
SELECT 'user_activity_log', COUNT(*) FROM user_activity_log
UNION ALL
SELECT 'system_health_indicators', COUNT(*) FROM system_health_indicators
UNION ALL
SELECT 'batch_production', COUNT(*) FROM batch_production;

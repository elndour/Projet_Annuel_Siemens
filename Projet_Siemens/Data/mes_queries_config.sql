-- ========================================
-- FICHIER DE CONFIGURATION DES REQUÊTES MES
-- Format: ANSI SQL (compatible Oracle/PostgreSQL)
-- ========================================

-- QUERY_NAME: Production Orders - Last 30 Days
-- OUTPUT_FILE: production_orders.json
SELECT 
    order_id,
    product_id,
    product_name,
    quantity_planned,
    quantity_produced,
    start_time,
    end_time,
    status,
    efficiency_rate
FROM production_orders
WHERE start_time >= CURRENT_DATE - 30
ORDER BY start_time DESC;

-- QUERY_NAME: Critical Error Logs
-- OUTPUT_FILE: error_logs.json
SELECT 
    error_id,
    error_code,
    error_message,
    error_timestamp,
    module_name,
    severity_level,
    stack_trace
FROM error_log
WHERE severity_level IN ('CRITICAL', 'ERROR')
  AND error_timestamp >= CURRENT_DATE - 30
ORDER BY error_timestamp DESC
FETCH FIRST 1000 ROWS ONLY;

-- QUERY_NAME: Active MES Tasks
-- OUTPUT_FILE: active_tasks.json
SELECT 
    task_id,
    task_name,
    task_type,
    task_status,
    assigned_machine,
    assigned_user,
    start_time,
    estimated_end_time,
    completion_percentage,
    priority_level
FROM mes_tasks
WHERE task_status IN ('RUNNING', 'ERROR', 'PENDING', 'PAUSED')
ORDER BY priority_level DESC, start_time ASC;

-- QUERY_NAME: Downtime Events
-- OUTPUT_FILE: downtime_events.json
SELECT 
    downtime_id,
    machine_id,
    machine_name,
    downtime_reason,
    downtime_category,
    downtime_start,
    downtime_end,
    duration_minutes,
    reported_by
FROM downtime_log
WHERE downtime_start >= CURRENT_DATE - 7
ORDER BY downtime_start DESC;

-- QUERY_NAME: Machine Performance Statistics
-- OUTPUT_FILE: machine_statistics.json
SELECT 
    machine_id,
    machine_name,
    COUNT(*) as total_operations,
    AVG(processing_time) as avg_processing_time,
    MIN(processing_time) as min_processing_time,
    MAX(processing_time) as max_processing_time,
    SUM(CASE WHEN status = 'SUCCESS' THEN 1 ELSE 0 END) as success_count,
    SUM(CASE WHEN status = 'FAILED' THEN 1 ELSE 0 END) as failed_count,
    SUM(CASE WHEN status = 'WARNING' THEN 1 ELSE 0 END) as warning_count
FROM operations_log
WHERE operation_date >= CURRENT_DATE - 7
GROUP BY machine_id, machine_name
ORDER BY failed_count DESC, machine_id;

-- QUERY_NAME: Product Quality Metrics
-- OUTPUT_FILE: quality_metrics.json
SELECT 
    product_id,
    product_name,
    COUNT(*) as total_produced,
    SUM(CASE WHEN quality_status = 'PASSED' THEN 1 ELSE 0 END) as quality_passed,
    SUM(CASE WHEN quality_status = 'FAILED' THEN 1 ELSE 0 END) as quality_failed,
    SUM(CASE WHEN quality_status = 'REWORK' THEN 1 ELSE 0 END) as quality_rework,
    AVG(quality_score) as avg_quality_score
FROM quality_control
WHERE inspection_date >= CURRENT_DATE - 30
GROUP BY product_id, product_name
ORDER BY quality_failed DESC;

-- QUERY_NAME: Inventory Levels
-- OUTPUT_FILE: inventory_levels.json
SELECT 
    material_id,
    material_name,
    material_type,
    current_quantity,
    min_quantity,
    max_quantity,
    unit_of_measure,
    last_updated,
    warehouse_location
FROM inventory
WHERE current_quantity <= min_quantity * 1.2
ORDER BY (current_quantity / NULLIF(min_quantity, 0)) ASC;

-- QUERY_NAME: User Activity Log
-- OUTPUT_FILE: user_activity.json
SELECT 
    user_id,
    username,
    action_type,
    action_description,
    action_timestamp,
    affected_entity,
    ip_address
FROM user_activity_log
WHERE action_timestamp >= CURRENT_DATE - 7
ORDER BY action_timestamp DESC
FETCH FIRST 500 ROWS ONLY;

-- QUERY_NAME: System Health Indicators
-- OUTPUT_FILE: system_health.json
SELECT 
    indicator_name,
    indicator_value,
    indicator_unit,
    status,
    measurement_time,
    threshold_warning,
    threshold_critical
FROM system_health_indicators
WHERE measurement_time >= CURRENT_DATE - 1
ORDER BY measurement_time DESC;

-- QUERY_NAME: Batch Production Summary
-- OUTPUT_FILE: batch_summary.json
SELECT 
    batch_id,
    batch_number,
    product_id,
    product_name,
    batch_start_time,
    batch_end_time,
    total_quantity,
    good_quantity,
    rejected_quantity,
    batch_status,
    yield_percentage
FROM batch_production
WHERE batch_start_time >= CURRENT_DATE - 30
ORDER BY batch_start_time DESC;

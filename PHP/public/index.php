<?php

declare(strict_types=1);

header('Content-Type: application/json; charset=utf-8');

$method = $_SERVER['REQUEST_METHOD'] ?? 'GET';
$path = parse_url($_SERVER['REQUEST_URI'] ?? '/', PHP_URL_PATH) ?: '/';

if ($method === 'GET' && $path === '/') {
    http_response_code(200);
    echo json_encode([
        'name' => 'Empty PHP API',
        'status' => 'ok',
    ], JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES);
    exit;
}

http_response_code(404);
echo json_encode([
    'error' => 'Not Found',
], JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES);

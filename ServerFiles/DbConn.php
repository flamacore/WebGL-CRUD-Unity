<?php
global $conn;
try {
    $conn = new PDO('mysql:host=localhost;dbname=******;charset=utf8', '******', '*******', array(PDO::ATTR_EMULATE_PREPARES => false, PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
    $conn->exec("SET time_zone='+00:00';");
} catch (Exception $ex) {
    echo $ex;
}
ini_set('display_errors', 1);
error_reporting(E_ALL  & ~E_NOTICE);
header('Content-type: text/html; charset=utf-8');
?>
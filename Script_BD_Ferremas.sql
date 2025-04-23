CREATE DATABASE  IF NOT EXISTS `ferremas_ecommerce` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ferremas_ecommerce`;
-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ferremas_ecommerce
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `carrito`
--

DROP TABLE IF EXISTS `carrito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carrito` (
  `id` int NOT NULL AUTO_INCREMENT,
  `cliente_id` int NOT NULL,
  `producto_id` int NOT NULL,
  `cantidad` int NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `fecha_agregado` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `cliente_id` (`cliente_id`),
  KEY `producto_id` (`producto_id`),
  CONSTRAINT `carrito_ibfk_1` FOREIGN KEY (`cliente_id`) REFERENCES `clientes` (`id`) ON DELETE CASCADE,
  CONSTRAINT `carrito_ibfk_2` FOREIGN KEY (`producto_id`) REFERENCES `productos` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carrito`
--

LOCK TABLES `carrito` WRITE;
/*!40000 ALTER TABLE `carrito` DISABLE KEYS */;
/*!40000 ALTER TABLE `carrito` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categorias`
--

DROP TABLE IF EXISTS `categorias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categorias` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text,
  `categoria_padre_id` int DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `categoria_padre_id` (`categoria_padre_id`),
  CONSTRAINT `categorias_ibfk_1` FOREIGN KEY (`categoria_padre_id`) REFERENCES `categorias` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categorias`
--

LOCK TABLES `categorias` WRITE;
/*!40000 ALTER TABLE `categorias` DISABLE KEYS */;
INSERT INTO `categorias` VALUES (1,'Herramientas','Todo tipo de herramientas para construcción y ferretería',NULL,1),(2,'Materiales de Construcción','Materiales básicos para la construcción',NULL,1),(3,'Equipos de Seguridad','Equipamiento para protección personal',NULL,1),(4,'Accesorios Varios','Accesorios diversos para ferretería',NULL,1),(5,'Herramientas Manuales','Herramientas que no requieren electricidad',1,1),(6,'Herramientas Eléctricas','Herramientas que funcionan con electricidad',1,1),(7,'Materiales Básicos','Materiales fundamentales para la construcción',2,1),(8,'Acabados','Materiales para acabados y terminaciones',2,1),(9,'Tornillos y Anclajes','Todo tipo de tornillos y sistemas de anclaje',4,1),(10,'Fijaciones y Adhesivos','Productos para fijar y pegar',4,1),(11,'Equipos de Medición','Herramientas para medición y precisión',4,1);
/*!40000 ALTER TABLE `categorias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `rut` varchar(12) NOT NULL,
  `correo_electronico` varchar(100) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `fecha_registro` datetime DEFAULT CURRENT_TIMESTAMP,
  `tipo_cliente` enum('particular','empresa','mayorista') DEFAULT 'particular',
  `estado` enum('activo','inactivo') DEFAULT 'activo',
  `newsletter` tinyint(1) DEFAULT '0',
  `ultima_compra` datetime DEFAULT NULL,
  `total_compras` decimal(10,2) DEFAULT '0.00',
  `numero_compras` int DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `rut` (`rut`),
  UNIQUE KEY `correo_electronico` (`correo_electronico`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `conversion_divisas`
--

DROP TABLE IF EXISTS `conversion_divisas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `conversion_divisas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `moneda_origen` varchar(3) NOT NULL,
  `moneda_destino` varchar(3) NOT NULL,
  `tasa_cambio` decimal(10,4) NOT NULL,
  `fecha_actualizacion` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conversion_divisas`
--

LOCK TABLES `conversion_divisas` WRITE;
/*!40000 ALTER TABLE `conversion_divisas` DISABLE KEYS */;
INSERT INTO `conversion_divisas` VALUES (1,'CLP','USD',0.0012,'2025-04-23 13:00:27'),(2,'USD','CLP',850.2500,'2025-04-23 13:00:27'),(3,'CLP','EUR',0.0011,'2025-04-23 13:00:27'),(4,'EUR','CLP',920.1500,'2025-04-23 13:00:27'),(5,'USD','EUR',0.9200,'2025-04-23 13:00:27'),(6,'EUR','USD',1.0900,'2025-04-23 13:00:27');
/*!40000 ALTER TABLE `conversion_divisas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `detalles_pedido`
--

DROP TABLE IF EXISTS `detalles_pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detalles_pedido` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pedido_id` int NOT NULL,
  `producto_id` int NOT NULL,
  `cantidad` int NOT NULL,
  `precio_unitario` decimal(10,2) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pedido_id` (`pedido_id`),
  KEY `producto_id` (`producto_id`),
  CONSTRAINT `detalles_pedido_ibfk_1` FOREIGN KEY (`pedido_id`) REFERENCES `pedidos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `detalles_pedido_ibfk_2` FOREIGN KEY (`producto_id`) REFERENCES `productos` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `detalles_pedido`
--

LOCK TABLES `detalles_pedido` WRITE;
/*!40000 ALTER TABLE `detalles_pedido` DISABLE KEYS */;
/*!40000 ALTER TABLE `detalles_pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `direcciones`
--

DROP TABLE IF EXISTS `direcciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `direcciones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int NOT NULL,
  `calle` varchar(150) NOT NULL,
  `numero` varchar(20) NOT NULL,
  `departamento` varchar(50) DEFAULT NULL,
  `comuna` varchar(100) NOT NULL,
  `region` varchar(100) NOT NULL,
  `codigo_postal` varchar(20) DEFAULT NULL,
  `es_principal` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `direcciones_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `direcciones`
--

LOCK TABLES `direcciones` WRITE;
/*!40000 ALTER TABLE `direcciones` DISABLE KEYS */;
/*!40000 ALTER TABLE `direcciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `historial_compras`
--

DROP TABLE IF EXISTS `historial_compras`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historial_compras` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pedido_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `usuario_id` int DEFAULT NULL,
  `fecha_compra` datetime DEFAULT CURRENT_TIMESTAMP,
  `monto_total` decimal(10,2) NOT NULL,
  `metodo_pago` varchar(50) NOT NULL,
  `estado_final` enum('completado','entregado','cancelado') NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pedido_id` (`pedido_id`),
  KEY `cliente_id` (`cliente_id`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `historial_compras_ibfk_1` FOREIGN KEY (`pedido_id`) REFERENCES `pedidos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `historial_compras_ibfk_2` FOREIGN KEY (`cliente_id`) REFERENCES `clientes` (`id`) ON DELETE CASCADE,
  CONSTRAINT `historial_compras_ibfk_3` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `historial_compras`
--

LOCK TABLES `historial_compras` WRITE;
/*!40000 ALTER TABLE `historial_compras` DISABLE KEYS */;
/*!40000 ALTER TABLE `historial_compras` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `intentos_login`
--

DROP TABLE IF EXISTS `intentos_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `intentos_login` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int DEFAULT NULL,
  `correo_electronico` varchar(100) DEFAULT NULL,
  `ip_address` varchar(45) DEFAULT NULL,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `exitoso` tinyint(1) DEFAULT '0',
  `bloqueado` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `intentos_login`
--

LOCK TABLES `intentos_login` WRITE;
/*!40000 ALTER TABLE `intentos_login` DISABLE KEYS */;
/*!40000 ALTER TABLE `intentos_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventario`
--

DROP TABLE IF EXISTS `inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventario` (
  `id` int NOT NULL AUTO_INCREMENT,
  `producto_id` int NOT NULL,
  `sucursal_id` int NOT NULL,
  `stock` int NOT NULL DEFAULT '0',
  `stock_minimo` int DEFAULT '5',
  `ultimo_ingreso` datetime DEFAULT NULL,
  `ultima_salida` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `producto_id` (`producto_id`,`sucursal_id`),
  KEY `sucursal_id` (`sucursal_id`),
  CONSTRAINT `inventario_ibfk_1` FOREIGN KEY (`producto_id`) REFERENCES `productos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `inventario_ibfk_2` FOREIGN KEY (`sucursal_id`) REFERENCES `sucursales` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=143 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventario`
--

LOCK TABLES `inventario` WRITE;
/*!40000 ALTER TABLE `inventario` DISABLE KEYS */;
INSERT INTO `inventario` VALUES (1,1,1,50,10,'2025-04-23 13:00:27',NULL),(2,2,1,30,5,'2025-04-23 13:00:27',NULL),(3,3,1,20,5,'2025-04-23 13:00:27',NULL),(4,4,1,15,3,'2025-04-23 13:00:27',NULL),(5,5,1,10,2,'2025-04-23 13:00:27',NULL),(6,6,1,25,5,'2025-04-23 13:00:27',NULL),(7,7,1,100,20,'2025-04-23 13:00:27',NULL),(8,8,1,200,50,'2025-04-23 13:00:27',NULL),(9,9,1,5,1,'2025-04-23 13:00:27',NULL),(10,10,1,40,10,'2025-04-23 13:00:27',NULL),(11,11,1,30,5,'2025-04-23 13:00:27',NULL),(12,12,1,80,20,'2025-04-23 13:00:27',NULL),(13,13,1,50,10,'2025-04-23 13:00:27',NULL),(14,14,1,40,10,'2025-04-23 13:00:27',NULL),(15,15,1,60,15,'2025-04-23 13:00:27',NULL),(16,13,7,52,5,'2025-04-23 13:00:27',NULL),(17,13,6,46,5,'2025-04-23 13:00:27',NULL),(18,13,5,16,5,'2025-04-23 13:00:27',NULL),(19,13,4,30,5,'2025-04-23 13:00:27',NULL),(20,13,3,43,5,'2025-04-23 13:00:27',NULL),(21,13,2,15,5,'2025-04-23 13:00:27',NULL),(22,14,7,36,5,'2025-04-23 13:00:27',NULL),(23,14,6,25,5,'2025-04-23 13:00:27',NULL),(24,14,5,10,5,'2025-04-23 13:00:27',NULL),(25,14,4,13,5,'2025-04-23 13:00:27',NULL),(26,14,3,27,5,'2025-04-23 13:00:27',NULL),(27,14,2,35,5,'2025-04-23 13:00:27',NULL),(28,15,7,34,5,'2025-04-23 13:00:27',NULL),(29,15,6,57,5,'2025-04-23 13:00:27',NULL),(30,15,5,21,5,'2025-04-23 13:00:27',NULL),(31,15,4,28,5,'2025-04-23 13:00:27',NULL),(32,15,3,16,5,'2025-04-23 13:00:27',NULL),(33,15,2,38,5,'2025-04-23 13:00:27',NULL),(34,1,7,30,5,'2025-04-23 13:00:27',NULL),(35,1,6,29,5,'2025-04-23 13:00:27',NULL),(36,1,5,43,5,'2025-04-23 13:00:27',NULL),(37,1,4,22,5,'2025-04-23 13:00:27',NULL),(38,1,3,18,5,'2025-04-23 13:00:27',NULL),(39,1,2,16,5,'2025-04-23 13:00:27',NULL),(40,2,7,15,5,'2025-04-23 13:00:27',NULL),(41,2,6,18,5,'2025-04-23 13:00:27',NULL),(42,2,5,36,5,'2025-04-23 13:00:27',NULL),(43,2,4,15,5,'2025-04-23 13:00:27',NULL),(44,2,3,10,5,'2025-04-23 13:00:27',NULL),(45,2,2,46,5,'2025-04-23 13:00:27',NULL),(46,3,7,41,5,'2025-04-23 13:00:27',NULL),(47,3,6,57,5,'2025-04-23 13:00:27',NULL),(48,3,5,53,5,'2025-04-23 13:00:27',NULL),(49,3,4,34,5,'2025-04-23 13:00:27',NULL),(50,3,3,51,5,'2025-04-23 13:00:27',NULL),(51,3,2,45,5,'2025-04-23 13:00:27',NULL),(52,4,7,13,5,'2025-04-23 13:00:27',NULL),(53,4,6,20,5,'2025-04-23 13:00:27',NULL),(54,4,5,53,5,'2025-04-23 13:00:27',NULL),(55,4,4,43,5,'2025-04-23 13:00:27',NULL),(56,4,3,48,5,'2025-04-23 13:00:27',NULL),(57,4,2,53,5,'2025-04-23 13:00:27',NULL),(58,5,7,58,5,'2025-04-23 13:00:27',NULL),(59,5,6,24,5,'2025-04-23 13:00:27',NULL),(60,5,5,34,5,'2025-04-23 13:00:27',NULL),(61,5,4,39,5,'2025-04-23 13:00:27',NULL),(62,5,3,33,5,'2025-04-23 13:00:27',NULL),(63,5,2,40,5,'2025-04-23 13:00:27',NULL),(64,6,7,42,5,'2025-04-23 13:00:27',NULL),(65,6,6,30,5,'2025-04-23 13:00:27',NULL),(66,6,5,13,5,'2025-04-23 13:00:27',NULL),(67,6,4,16,5,'2025-04-23 13:00:27',NULL),(68,6,3,30,5,'2025-04-23 13:00:27',NULL),(69,6,2,42,5,'2025-04-23 13:00:27',NULL),(70,7,7,13,5,'2025-04-23 13:00:27',NULL),(71,7,6,28,5,'2025-04-23 13:00:27',NULL),(72,7,5,42,5,'2025-04-23 13:00:27',NULL),(73,7,4,16,5,'2025-04-23 13:00:27',NULL),(74,7,3,44,5,'2025-04-23 13:00:27',NULL),(75,7,2,12,5,'2025-04-23 13:00:27',NULL),(76,8,7,18,5,'2025-04-23 13:00:27',NULL),(77,8,6,46,5,'2025-04-23 13:00:27',NULL),(78,8,5,16,5,'2025-04-23 13:00:27',NULL),(79,8,4,34,5,'2025-04-23 13:00:27',NULL),(80,8,3,10,5,'2025-04-23 13:00:27',NULL),(81,8,2,40,5,'2025-04-23 13:00:27',NULL),(82,9,7,11,5,'2025-04-23 13:00:27',NULL),(83,9,6,25,5,'2025-04-23 13:00:27',NULL),(84,9,5,31,5,'2025-04-23 13:00:27',NULL),(85,9,4,19,5,'2025-04-23 13:00:27',NULL),(86,9,3,46,5,'2025-04-23 13:00:27',NULL),(87,9,2,12,5,'2025-04-23 13:00:27',NULL),(88,10,7,14,5,'2025-04-23 13:00:27',NULL),(89,10,6,25,5,'2025-04-23 13:00:27',NULL),(90,10,5,23,5,'2025-04-23 13:00:27',NULL),(91,10,4,31,5,'2025-04-23 13:00:27',NULL),(92,10,3,27,5,'2025-04-23 13:00:27',NULL),(93,10,2,31,5,'2025-04-23 13:00:27',NULL),(94,11,7,13,5,'2025-04-23 13:00:27',NULL),(95,11,6,15,5,'2025-04-23 13:00:27',NULL),(96,11,5,28,5,'2025-04-23 13:00:27',NULL),(97,11,4,33,5,'2025-04-23 13:00:27',NULL),(98,11,3,22,5,'2025-04-23 13:00:27',NULL),(99,11,2,51,5,'2025-04-23 13:00:27',NULL),(100,12,7,29,5,'2025-04-23 13:00:27',NULL),(101,12,6,32,5,'2025-04-23 13:00:27',NULL),(102,12,5,16,5,'2025-04-23 13:00:27',NULL),(103,12,4,22,5,'2025-04-23 13:00:27',NULL),(104,12,3,55,5,'2025-04-23 13:00:27',NULL),(105,12,2,49,5,'2025-04-23 13:00:27',NULL);
/*!40000 ALTER TABLE `inventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logs_actividad`
--

DROP TABLE IF EXISTS `logs_actividad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logs_actividad` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int DEFAULT NULL,
  `tipo_actividad` enum('login','compra','actualizacion_perfil','cambio_contrasena') NOT NULL,
  `descripcion` text,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  `ip_address` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `logs_actividad_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logs_actividad`
--

LOCK TABLES `logs_actividad` WRITE;
/*!40000 ALTER TABLE `logs_actividad` DISABLE KEYS */;
/*!40000 ALTER TABLE `logs_actividad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `marcas`
--

DROP TABLE IF EXISTS `marcas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `marcas` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text,
  `logo_url` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `marcas`
--

LOCK TABLES `marcas` WRITE;
/*!40000 ALTER TABLE `marcas` DISABLE KEYS */;
INSERT INTO `marcas` VALUES (1,'Bosch','Empresa alemana líder en herramientas eléctricas y manuales',NULL,1),(2,'Makita','Fabricante japonés de herramientas eléctricas profesionales',NULL,1),(3,'Stanley','Marca estadounidense especializada en herramientas manuales',NULL,1),(4,'Sika','Empresa suiza especializada en productos químicos para la construcción',NULL,1),(5,'3M','Compañía multinacional especializada en seguridad y materiales',NULL,1),(6,'Hilti','Empresa liechtensteiniana especializada en sistemas de anclaje',NULL,1),(7,'DeWalt','Fabricante americano de herramientas eléctricas',NULL,1),(8,'Kärcher','Fabricante alemán de equipos de limpieza',NULL,1);
/*!40000 ALTER TABLE `marcas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notificaciones`
--

DROP TABLE IF EXISTS `notificaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notificaciones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int NOT NULL,
  `tipo` enum('oferta','pedido','stock','general') NOT NULL,
  `mensaje` text NOT NULL,
  `leida` tinyint(1) DEFAULT '0',
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `notificaciones_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notificaciones`
--

LOCK TABLES `notificaciones` WRITE;
/*!40000 ALTER TABLE `notificaciones` DISABLE KEYS */;
/*!40000 ALTER TABLE `notificaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pagos`
--

DROP TABLE IF EXISTS `pagos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pagos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pedido_id` int NOT NULL,
  `metodo` enum('tarjeta_debito','tarjeta_credito','transferencia','webpay') NOT NULL,
  `monto` decimal(10,2) NOT NULL,
  `estado` enum('pendiente','completado','fallido','reembolsado') DEFAULT 'pendiente',
  `fecha_pago` datetime DEFAULT NULL,
  `webpay_token` varchar(255) DEFAULT NULL,
  `webpay_orden_compra` varchar(100) DEFAULT NULL,
  `webpay_transaction_id` varchar(100) DEFAULT NULL,
  `webpay_response_code` varchar(20) DEFAULT NULL,
  `webpay_authorization_code` varchar(100) DEFAULT NULL,
  `webpay_payment_type_code` varchar(50) DEFAULT NULL,
  `webpay_installments_number` int DEFAULT NULL,
  `webpay_card_number` varchar(20) DEFAULT NULL,
  `referencia_transaccion` varchar(255) DEFAULT NULL,
  `notas` text,
  `contador_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `pedido_id` (`pedido_id`),
  KEY `contador_id` (`contador_id`),
  CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`pedido_id`) REFERENCES `pedidos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `pagos_ibfk_2` FOREIGN KEY (`contador_id`) REFERENCES `usuarios` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pagos`
--

LOCK TABLES `pagos` WRITE;
/*!40000 ALTER TABLE `pagos` DISABLE KEYS */;
/*!40000 ALTER TABLE `pagos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int NOT NULL,
  `fecha_pedido` datetime DEFAULT CURRENT_TIMESTAMP,
  `estado` enum('pendiente','aprobado','preparando','enviado','entregado','cancelado') DEFAULT 'pendiente',
  `tipo_entrega` enum('retiro_tienda','despacho_domicilio') NOT NULL,
  `sucursal_id` int DEFAULT NULL,
  `direccion_id` int DEFAULT NULL,
  `subtotal` decimal(10,2) NOT NULL,
  `costo_envio` decimal(10,2) DEFAULT '0.00',
  `impuestos` decimal(10,2) DEFAULT '0.00',
  `total` decimal(10,2) NOT NULL,
  `notas` text,
  `vendedor_id` int DEFAULT NULL,
  `bodeguero_id` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `usuario_id` (`usuario_id`),
  KEY `sucursal_id` (`sucursal_id`),
  KEY `direccion_id` (`direccion_id`),
  KEY `vendedor_id` (`vendedor_id`),
  KEY `bodeguero_id` (`bodeguero_id`),
  CONSTRAINT `pedidos_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE,
  CONSTRAINT `pedidos_ibfk_2` FOREIGN KEY (`sucursal_id`) REFERENCES `sucursales` (`id`) ON DELETE SET NULL,
  CONSTRAINT `pedidos_ibfk_3` FOREIGN KEY (`direccion_id`) REFERENCES `direcciones` (`id`) ON DELETE SET NULL,
  CONSTRAINT `pedidos_ibfk_4` FOREIGN KEY (`vendedor_id`) REFERENCES `usuarios` (`id`) ON DELETE SET NULL,
  CONSTRAINT `pedidos_ibfk_5` FOREIGN KEY (`bodeguero_id`) REFERENCES `usuarios` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos`
--

LOCK TABLES `pedidos` WRITE;
/*!40000 ALTER TABLE `pedidos` DISABLE KEYS */;
/*!40000 ALTER TABLE `pedidos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `codigo` varchar(50) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `descripcion` text,
  `precio` decimal(10,2) NOT NULL,
  `categoria_id` int DEFAULT NULL,
  `marca_id` int DEFAULT NULL,
  `imagen_url` varchar(255) DEFAULT NULL,
  `especificaciones` text,
  `fecha_creacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `fecha_modificacion` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `codigo` (`codigo`),
  KEY `categoria_id` (`categoria_id`),
  KEY `marca_id` (`marca_id`),
  CONSTRAINT `productos_ibfk_1` FOREIGN KEY (`categoria_id`) REFERENCES `categorias` (`id`) ON DELETE SET NULL,
  CONSTRAINT `productos_ibfk_2` FOREIGN KEY (`marca_id`) REFERENCES `marcas` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (1,'MARTB001','Martillo Profesional','Martillo de acero forjado con mango ergonómico',15990.00,5,3,'martillo.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(2,'DESTB002','Set Destornilladores 6 piezas','Set de destornilladores Phillips y planos',12990.00,5,3,'destornilladores.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(3,'LLAVB003','Juego de Llaves Combinadas','Set de 10 llaves combinadas de 8 a 19mm',24990.00,5,1,'llaves.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(4,'TALM001','Taladro Percutor Bosch','Taladro percutor profesional 800W con maleta',89990.00,6,1,'taladro_bosch.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(5,'SIERM002','Sierra Circular Makita','Sierra circular 7-1/4\" 1800W',119990.00,6,2,'sierra_makita.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(6,'LIJAM003','Lijadora Orbital DeWalt','Lijadora orbital con colector de polvo',69990.00,6,7,'lijadora_dewalt.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(7,'CEMS001','Cemento Portland 25kg','Saco de cemento de alta resistencia',6990.00,7,NULL,'cemento.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(8,'AREN002','Arena Fina 40kg','Saco de arena fina para mezclas',4990.00,7,NULL,'arena.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(9,'LADR003','Ladrillos Prensados','Pallet de ladrillos prensados (500 unidades)',189990.00,7,NULL,'ladrillos.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(10,'PINTS001','Pintura Látex Blanco 1 Galón','Pintura látex para interiores',15990.00,8,NULL,'pintura.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(11,'BARNS002','Barniz Marino 1L','Barniz protector resistente al agua',12990.00,8,NULL,'barniz.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(12,'CERS003','Cerámica Blanca 30x30cm','Caja cerámica blanca (2m²)',9990.00,8,NULL,'ceramica.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(13,'CASE001','Casco de Seguridad','Casco certificado con ajuste tipo ratchet',8990.00,3,5,'casco.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(14,'GUAN002','Guantes de Trabajo','Par de guantes de seguridad resistentes',5990.00,3,5,'guantes.jpg',NULL,'2025-04-23 13:00:27',NULL,1),(15,'LENT003','Lentes de Seguridad','Lentes protectores anti-impacto',4990.00,3,5,'lentes.jpg',NULL,'2025-04-23 13:00:27',NULL,1);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos_detalles`
--

DROP TABLE IF EXISTS `productos_detalles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos_detalles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `producto_id` int NOT NULL,
  `descripcion_larga` text,
  `especificaciones` json DEFAULT NULL,
  `marca_id` int DEFAULT NULL,
  `peso` decimal(10,2) DEFAULT NULL,
  `dimensiones` varchar(100) DEFAULT NULL,
  `garantia` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `producto_id` (`producto_id`),
  KEY `marca_id` (`marca_id`),
  CONSTRAINT `productos_detalles_ibfk_1` FOREIGN KEY (`producto_id`) REFERENCES `productos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `productos_detalles_ibfk_2` FOREIGN KEY (`marca_id`) REFERENCES `marcas` (`id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos_detalles`
--

LOCK TABLES `productos_detalles` WRITE;
/*!40000 ALTER TABLE `productos_detalles` DISABLE KEYS */;
/*!40000 ALTER TABLE `productos_detalles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promociones`
--

DROP TABLE IF EXISTS `promociones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promociones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `descripcion` text,
  `tipo` enum('porcentaje','monto_fijo') NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime NOT NULL,
  `activa` tinyint(1) DEFAULT '1',
  `aplicable_a` enum('producto','categoria','total_compra') NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promociones`
--

LOCK TABLES `promociones` WRITE;
/*!40000 ALTER TABLE `promociones` DISABLE KEYS */;
/*!40000 ALTER TABLE `promociones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recuperacion_contrasena`
--

DROP TABLE IF EXISTS `recuperacion_contrasena`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recuperacion_contrasena` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int NOT NULL,
  `token` varchar(255) NOT NULL,
  `fecha_creacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `fecha_expiracion` datetime NOT NULL,
  `utilizado` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `token` (`token`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `recuperacion_contrasena_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recuperacion_contrasena`
--

LOCK TABLES `recuperacion_contrasena` WRITE;
/*!40000 ALTER TABLE `recuperacion_contrasena` DISABLE KEYS */;
/*!40000 ALTER TABLE `recuperacion_contrasena` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reviews`
--

DROP TABLE IF EXISTS `reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reviews` (
  `id` int NOT NULL AUTO_INCREMENT,
  `producto_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `calificacion` int DEFAULT NULL,
  `comentario` text,
  `fecha` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `producto_id` (`producto_id`),
  KEY `cliente_id` (`cliente_id`),
  CONSTRAINT `reviews_ibfk_1` FOREIGN KEY (`producto_id`) REFERENCES `productos` (`id`) ON DELETE CASCADE,
  CONSTRAINT `reviews_ibfk_2` FOREIGN KEY (`cliente_id`) REFERENCES `clientes` (`id`) ON DELETE CASCADE,
  CONSTRAINT `reviews_chk_1` CHECK ((`calificacion` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reviews`
--

LOCK TABLES `reviews` WRITE;
/*!40000 ALTER TABLE `reviews` DISABLE KEYS */;
/*!40000 ALTER TABLE `reviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sesiones`
--

DROP TABLE IF EXISTS `sesiones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sesiones` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuario_id` int NOT NULL,
  `token` varchar(255) NOT NULL,
  `ip_address` varchar(45) DEFAULT NULL,
  `dispositivo` varchar(100) DEFAULT NULL,
  `fecha_creacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `fecha_expiracion` datetime NOT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `token` (`token`),
  KEY `usuario_id` (`usuario_id`),
  CONSTRAINT `sesiones_ibfk_1` FOREIGN KEY (`usuario_id`) REFERENCES `usuarios` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sesiones`
--

LOCK TABLES `sesiones` WRITE;
/*!40000 ALTER TABLE `sesiones` DISABLE KEYS */;
/*!40000 ALTER TABLE `sesiones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sucursales`
--

DROP TABLE IF EXISTS `sucursales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sucursales` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `comuna` varchar(100) NOT NULL,
  `region` varchar(100) NOT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `es_principal` tinyint(1) DEFAULT '0',
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sucursales`
--

LOCK TABLES `sucursales` WRITE;
/*!40000 ALTER TABLE `sucursales` DISABLE KEYS */;
INSERT INTO `sucursales` VALUES (1,'FERREMAS Santiago Centro','Alameda 1234','Santiago','Metropolitana','+5622222222',1,1),(2,'FERREMAS Providencia','Providencia 567','Providencia','Metropolitana','+5622222223',0,1),(3,'FERREMAS Las Condes','Apoquindo 9876','Las Condes','Metropolitana','+5622222224',0,1),(4,'FERREMAS Maipú','Pajaritos 4321','Maipú','Metropolitana','+5622222225',0,1),(5,'FERREMAS Concepción','O\'Higgins 123','Concepción','Biobío','+5641111111',0,1),(6,'FERREMAS Viña del Mar','Valparaíso 456','Viña del Mar','Valparaíso','+5632222222',0,1),(7,'FERREMAS Puerto Montt','Diego Portales 789','Puerto Montt','Los Lagos','+5665555555',0,1);
/*!40000 ALTER TABLE `sucursales` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tracking_pedidos`
--

DROP TABLE IF EXISTS `tracking_pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tracking_pedidos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pedido_id` int NOT NULL,
  `estado` enum('procesando','en_camino','entregado','cancelado') NOT NULL,
  `ubicacion` varchar(255) DEFAULT NULL,
  `fecha_actualizacion` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `pedido_id` (`pedido_id`),
  CONSTRAINT `tracking_pedidos_ibfk_1` FOREIGN KEY (`pedido_id`) REFERENCES `pedidos` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tracking_pedidos`
--

LOCK TABLES `tracking_pedidos` WRITE;
/*!40000 ALTER TABLE `tracking_pedidos` DISABLE KEYS */;
/*!40000 ALTER TABLE `tracking_pedidos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `rut` varchar(20) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `rol` enum('cliente','administrador','vendedor','bodeguero','contador') NOT NULL,
  `fecha_registro` datetime DEFAULT CURRENT_TIMESTAMP,
  `ultimo_acceso` datetime DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `rut` (`rut`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Juan','Pérez','jperez@ferremas.cl','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','12345678-9','+56912345678','administrador','2025-04-23 13:00:27',NULL,1),(2,'María','González','mgonzalez@ferremas.cl','240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9','98765432-1','+56998765432','administrador','2025-04-23 13:00:27',NULL,1),(3,'Carlos','López','clopez@ferremas.cl','56976bf24998ca63e35fe4f1e2469b5751d1856003e8d16fef0aafef496ed044','11111111-1','+56911111111','vendedor','2025-04-23 13:00:27',NULL,1),(4,'Ana','Martínez','amartinez@ferremas.cl','56976bf24998ca63e35fe4f1e2469b5751d1856003e8d16fef0aafef496ed044','22222222-2','+56922222222','vendedor','2025-04-23 13:00:27',NULL,1),(5,'Pedro','Sánchez','psanchez@ferremas.cl','698e5db82a790c81d494f7942ba848c54cbef422f82e2ba17938c30ce315d271','33333333-3','+56933333333','bodeguero','2025-04-23 13:00:27',NULL,1),(6,'Laura','Rodríguez','lrodriguez@ferremas.cl','698e5db82a790c81d494f7942ba848c54cbef422f82e2ba17938c30ce315d271','44444444-4','+56944444444','bodeguero','2025-04-23 13:00:27',NULL,1),(7,'Jorge','Muñoz','jmunoz@ferremas.cl','7d0fa94701416c46f0833330336f33071f32191a67d4c50257ee022dc05d4815','55555555-5','+56955555555','contador','2025-04-23 13:00:27',NULL,1),(8,'Sofía','Castro','scastro@ferremas.cl','7d0fa94701416c46f0833330336f33071f32191a67d4c50257ee022dc05d4815','66666666-6','+56966666666','contador','2025-04-23 13:00:27',NULL,1);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'ferremas_ecommerce'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_actualizar_categoria` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_actualizar_categoria`(
    IN p_categoria_id INT,
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT
)
BEGIN
    UPDATE categorias
    SET 
        nombre = p_nombre,
        descripcion = p_descripcion
    WHERE id = p_categoria_id;
    
    SELECT ROW_COUNT() AS filas_afectadas;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_actualizar_estado_pedido` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_actualizar_estado_pedido`(
    IN p_pedido_id INT,
    IN p_nuevo_estado VARCHAR(50)
)
BEGIN
    UPDATE pedidos
    SET 
        estado = p_nuevo_estado,
        fecha_actualizacion = NOW()
    WHERE id = p_pedido_id;
    
    SELECT 
        id,
        estado AS nuevo_estado,
        fecha_actualizacion
    FROM pedidos
    WHERE id = p_pedido_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_actualizar_marca` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_actualizar_marca`(
    IN p_marca_id INT,
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT
)
BEGIN
    UPDATE marcas
    SET 
        nombre = p_nombre,
        descripcion = p_descripcion
    WHERE id = p_marca_id;
    
    SELECT ROW_COUNT() AS filas_afectadas;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_actualizar_stock` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_actualizar_stock`(
    IN p_producto_id INT,
    IN p_cantidad INT
)
BEGIN
    DECLARE stock_actual INT;
    
    -- Obtener stock actual
    SELECT stock INTO stock_actual
    FROM productos
    WHERE id = p_producto_id;
    
    -- Actualizar stock
    UPDATE productos
    SET 
        stock = stock + p_cantidad,
        fecha_actualizacion = NOW()
    WHERE id = p_producto_id;
    
    -- Devolver nuevo stock
    SELECT 
        p.id AS producto_id,
        p.nombre AS nombre_producto,
        p.stock AS stock_nuevo,
        stock_actual AS stock_anterior
    FROM productos p
    WHERE p.id = p_producto_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_agregar_detalle_pedido` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_agregar_detalle_pedido`(
    IN p_pedido_id INT,
    IN p_producto_id INT,
    IN p_cantidad INT,
    IN p_precio_unitario DECIMAL(10,2)
)
BEGIN
    DECLARE stock_disponible INT;
    
    -- Verificar stock disponible
    SELECT stock INTO stock_disponible
    FROM productos
    WHERE id = p_producto_id;
    
    IF stock_disponible >= p_cantidad THEN
        -- Insertar detalle del pedido
        INSERT INTO pedidos_detalles (
            pedido_id,
            producto_id,
            cantidad,
            precio_unitario,
            subtotal
        ) VALUES (
            p_pedido_id,
            p_producto_id,
            p_cantidad,
            p_precio_unitario,
            p_cantidad * p_precio_unitario
        );
        
        -- Actualizar stock del producto
        UPDATE productos
        SET stock = stock - p_cantidad
        WHERE id = p_producto_id;
        
        SELECT 'Detalle de pedido agregado exitosamente' AS mensaje;
    ELSE
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Stock insuficiente para el producto';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_agregar_direccion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_agregar_direccion`(
    IN p_usuario_id INT,
    IN p_direccion VARCHAR(255),
    IN p_ciudad VARCHAR(100),
    IN p_region VARCHAR(100),
    IN p_codigo_postal VARCHAR(20),
    IN p_es_principal BOOLEAN
)
BEGIN
    DECLARE nueva_direccion_id INT;
    
    -- Si es dirección principal, desmarcar otras direcciones
    IF p_es_principal THEN
        UPDATE direcciones
        SET es_principal = FALSE
        WHERE usuario_id = p_usuario_id;
    END IF;
    
    -- Insertar nueva dirección
    INSERT INTO direcciones (
        usuario_id,
        direccion,
        ciudad,
        region,
        codigo_postal,
        es_principal,
        fecha_creacion
    ) VALUES (
        p_usuario_id,
        p_direccion,
        p_ciudad,
        p_region,
        p_codigo_postal,
        p_es_principal,
        NOW()
    );
    
    SET nueva_direccion_id = LAST_INSERT_ID();
    
    -- Devolver detalles de la dirección
    SELECT 
        id,
        direccion,
        ciudad,
        region,
        codigo_postal,
        es_principal
    FROM direcciones
    WHERE id = nueva_direccion_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_agregar_tracking_pedido` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_agregar_tracking_pedido`(
    IN p_pedido_id INT,
    IN p_estado VARCHAR(50),
    IN p_ubicacion VARCHAR(255),
    IN p_observaciones TEXT
)
BEGIN
    INSERT INTO tracking_pedidos (
        pedido_id,
        estado,
        ubicacion,
        observaciones,
        fecha_registro
    ) VALUES (
        p_pedido_id,
        p_estado,
        p_ubicacion,
        p_observaciones,
        NOW()
    );
    
    SELECT LAST_INSERT_ID() AS tracking_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_buscar_productos` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_buscar_productos`(
    IN p_termino_busqueda VARCHAR(200),
    IN p_categoria_id INT,
    IN p_precio_min DECIMAL(10,2),
    IN p_precio_max DECIMAL(10,2)
)
BEGIN
    SELECT p.id, 
           p.nombre, 
           p.descripcion, 
           p.precio, 
           p.stock, 
           c.nombre AS categoria,
           m.nombre AS marca
    FROM productos p
    JOIN categorias c ON p.categoria_id = c.id
    JOIN marcas m ON p.marca_id = m.id
    WHERE (p_termino_busqueda IS NULL OR 
           p.nombre LIKE CONCAT('%', p_termino_busqueda, '%') OR 
           p.descripcion LIKE CONCAT('%', p_termino_busqueda, '%'))
    AND (p_categoria_id IS NULL OR p.categoria_id = p_categoria_id)
    AND (p_precio_min IS NULL OR p.precio >= p_precio_min)
    AND (p_precio_max IS NULL OR p.precio <= p_precio_max)
    AND p.estado = 'activo';
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_cambiar_contrasena` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_cambiar_contrasena`(
    IN p_token VARCHAR(100),
    IN p_nueva_contrasena VARCHAR(255)
)
BEGIN
    DECLARE email_usuario VARCHAR(100);
    DECLARE solicitud_valida INT;
    
    -- Validar token de recuperación
    SELECT COUNT(*), email INTO solicitud_valida, email_usuario
    FROM recuperacion_contrasena
    WHERE token = p_token 
    AND estado = 'pendiente' 
    AND TIMESTAMPDIFF(HOUR, fecha_solicitud, NOW()) <= 24;
    
    IF solicitud_valida > 0 THEN
        -- Actualizar contraseña del usuario
        UPDATE usuarios
        SET contrasena = SHA2(p_nueva_contrasena, 256)
        WHERE email = email_usuario;
        
        -- Marcar solicitud de recuperación como completada
        UPDATE recuperacion_contrasena
        SET estado = 'completado'
        WHERE token = p_token;
        
        SELECT 'Contraseña actualizada exitosamente' AS mensaje;
    ELSE
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Token de recuperación inválido o expirado';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_crear_categoria` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_crear_categoria`(
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT
)
BEGIN
    INSERT INTO categorias (
        nombre, 
        descripcion, 
        fecha_creacion,
        estado
    ) VALUES (
        p_nombre, 
        p_descripcion, 
        NOW(),
        'activo'
    );
    
    SELECT LAST_INSERT_ID() AS categoria_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_crear_marca` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_crear_marca`(
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT
)
BEGIN
    INSERT INTO marcas (
        nombre, 
        descripcion, 
        fecha_creacion,
        estado
    ) VALUES (
        p_nombre, 
        p_descripcion, 
        NOW(),
        'activo'
    );
    
    SELECT LAST_INSERT_ID() AS marca_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_crear_pedido` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_crear_pedido`(
    IN p_usuario_id INT,
    IN p_total DECIMAL(10,2),
    IN p_estado VARCHAR(50)
)
BEGIN
    DECLARE nuevo_pedido_id INT;
    
    -- Insertar encabezado del pedido
    INSERT INTO pedidos (
        usuario_id,
        fecha_pedido,
        total,
        estado
    ) VALUES (
        p_usuario_id,
        NOW(),
        p_total,
        p_estado
    );
    
    -- Obtener ID del pedido recién creado
    SET nuevo_pedido_id = LAST_INSERT_ID();
    
    SELECT nuevo_pedido_id AS pedido_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_crear_producto` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_crear_producto`(
    IN p_nombre VARCHAR(200),
    IN p_descripcion TEXT,
    IN p_precio DECIMAL(10,2),
    IN p_stock INT,
    IN p_categoria_id INT,
    IN p_marca_id INT
)
BEGIN
    INSERT INTO productos (
        nombre,
        descripcion,
        precio,
        stock,
        categoria_id,
        marca_id,
        fecha_creacion,
        estado
    ) VALUES (
        p_nombre,
        p_descripcion,
        p_precio,
        p_stock,
        p_categoria_id,
        p_marca_id,
        NOW(),
        'activo'
    );
    
    SELECT LAST_INSERT_ID() AS producto_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_generar_cupon` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_generar_cupon`(
    IN p_codigo VARCHAR(50),
    IN p_descuento DECIMAL(5,2),
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE,
    IN p_tipo_descuento VARCHAR(20),
    IN p_monto_minimo DECIMAL(10,2)
)
BEGIN
    INSERT INTO cupones (
        codigo,
        descuento,
        fecha_inicio,
        fecha_fin,
        tipo_descuento,
        monto_minimo,
        estado
    ) VALUES (
        p_codigo,
        p_descuento,
        p_fecha_inicio,
        p_fecha_fin,
        p_tipo_descuento,
        p_monto_minimo,
        'activo'
    );
    
    SELECT LAST_INSERT_ID() AS cupon_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_historial_compras_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_historial_compras_usuario`(
    IN p_usuario_id INT
)
BEGIN
    SELECT 
        p.id AS pedido_id,
        p.fecha_pedido,
        p.total,
        p.estado,
        pd.producto_id,
        pr.nombre AS nombre_producto,
        pd.cantidad,
        pd.precio_unitario
    FROM pedidos p
    JOIN pedidos_detalles pd ON p.id = pd.pedido_id
    JOIN productos pr ON pd.producto_id = pr.id
    WHERE p.usuario_id = p_usuario_id
    ORDER BY p.fecha_pedido DESC;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_informe_ventas` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_informe_ventas`(
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE
)
BEGIN
    SELECT 
        DATE(p.fecha_pedido) AS fecha,
        COUNT(DISTINCT p.id) AS total_pedidos,
        SUM(p.total) AS total_ventas,
        COUNT(DISTINCT pd.producto_id) AS productos_vendidos
    FROM pedidos p
    JOIN pedidos_detalles pd ON p.id = pd.pedido_id
    WHERE p.fecha_pedido BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY DATE(p.fecha_pedido)
    ORDER BY fecha;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_iniciar_sesion` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_iniciar_sesion`(
    IN p_email VARCHAR(100),
    IN p_contrasena VARCHAR(255)
)
BEGIN
    DECLARE usuario_id INT;
    DECLARE usuario_estado VARCHAR(20);
    
    -- Buscar usuario con credenciales proporcionadas
    SELECT id, estado INTO usuario_id, usuario_estado
    FROM usuarios
    WHERE email = p_email 
    AND contrasena = SHA2(p_contrasena, 256);
    
    IF usuario_id IS NOT NULL THEN
        -- Verificar estado del usuario
        IF usuario_estado = 'activo' THEN
            -- Registrar inicio de sesión
            INSERT INTO sesiones (
                usuario_id, 
                fecha_inicio, 
                estado
            ) VALUES (
                usuario_id, 
                NOW(), 
                'activo'
            );
            
            -- Devolver información del usuario
            SELECT 
                id, 
                email, 
                nombre, 
                apellido, 
                'Inicio de sesión exitoso' AS mensaje
            FROM usuarios
            WHERE id = usuario_id;
        ELSE
            SIGNAL SQLSTATE '45000' 
            SET MESSAGE_TEXT = 'Cuenta inactiva o bloqueada';
        END IF;
    ELSE
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Credenciales incorrectas';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_obtener_historial_tracking` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_obtener_historial_tracking`(
    IN p_pedido_id INT
)
BEGIN
    SELECT 
        id,
        estado,
        ubicacion,
        observaciones,
        fecha_registro
    FROM tracking_pedidos
    WHERE pedido_id = p_pedido_id
    ORDER BY fecha_registro ASC;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_productos_mas_vendidos` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_productos_mas_vendidos`(
    IN p_fecha_inicio DATE,
    IN p_fecha_fin DATE,
    IN p_limite INT
)
BEGIN
    SELECT 
        pr.id AS producto_id,
        pr.nombre AS nombre_producto,
        SUM(pd.cantidad) AS cantidad_vendida,
        SUM(pd.subtotal) AS total_ventas
    FROM pedidos_detalles pd
    JOIN productos pr ON pd.producto_id = pr.id
    JOIN pedidos p ON pd.pedido_id = p.id
    WHERE p.fecha_pedido BETWEEN p_fecha_inicio AND p_fecha_fin
    GROUP BY pr.id, pr.nombre
    ORDER BY cantidad_vendida DESC
    LIMIT p_limite;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_recuperar_contrasena` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_recuperar_contrasena`(
    IN p_email VARCHAR(100)
)
BEGIN
    DECLARE usuario_existente INT;
    DECLARE token_recuperacion VARCHAR(100);
    
    -- Verificar existencia del usuario
    SELECT COUNT(*) INTO usuario_existente
    FROM usuarios
    WHERE email = p_email;
    
    IF usuario_existente > 0 THEN
        -- Generar token de recuperación
        SET token_recuperacion = CONCAT(
            MD5(CONCAT(p_email, NOW())), 
            FLOOR(RAND() * 10000)
        );
        
        -- Insertar solicitud de recuperación
        INSERT INTO recuperacion_contrasena (
            email, 
            token, 
            fecha_solicitud, 
            estado
        ) VALUES (
            p_email, 
            token_recuperacion, 
            NOW(), 
            'pendiente'
        );
        
        -- Devolver token de recuperación
        SELECT token_recuperacion AS token_recuperacion;
    ELSE
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'Correo electrónico no registrado';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_registrar_usuario` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_registrar_usuario`(
    IN p_email VARCHAR(100),
    IN p_contrasena VARCHAR(255),
    IN p_nombre VARCHAR(100),
    IN p_apellido VARCHAR(100),
    IN p_telefono VARCHAR(20)
)
BEGIN
    DECLARE usuario_existente INT;
    
    -- Verificar si el usuario ya existe
    SELECT COUNT(*) INTO usuario_existente 
    FROM usuarios 
    WHERE email = p_email;
    
    IF usuario_existente > 0 THEN
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'El correo electrónico ya está registrado';
    ELSE
        -- Insertar nuevo usuario
        INSERT INTO usuarios (
            email, 
            contrasena, 
            nombre, 
            apellido, 
            telefono, 
            fecha_registro, 
            estado
        ) VALUES (
            p_email, 
            SHA2(p_contrasena, 256), 
            p_nombre, 
            p_apellido, 
            p_telefono, 
            NOW(), 
            'activo'
        );
        
        -- Devolver el ID del usuario recién creado
        SELECT LAST_INSERT_ID() AS usuario_id;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_validar_cupon` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_validar_cupon`(
    IN p_codigo VARCHAR(50),
    IN p_monto_compra DECIMAL(10,2)
)
BEGIN
    DECLARE cupon_valido BOOLEAN DEFAULT FALSE;
    DECLARE mensaje VARCHAR(255);
    DECLARE descuento_aplicable DECIMAL(10,2);
    
    -- Verificar validez del cupón
    SELECT 
        (NOW() BETWEEN fecha_inicio AND fecha_fin) 
        AND estado = 'activo' 
        AND p_monto_compra >= monto_minimo
    INTO cupon_valido
    FROM cupones
    WHERE codigo = p_codigo;
    
    IF cupon_valido THEN
        -- Obtener detalles del cupón
        SELECT 
            codigo,
            descuento,
            tipo_descuento,
            IF(tipo_descuento = 'porcentaje', 
               p_monto_compra * (descuento / 100), 
               descuento) AS descuento_aplicable,
            'Cupón válido' AS mensaje
        FROM cupones
        WHERE codigo = p_codigo;
    ELSE
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cupón inválido o no aplicable';
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-23 13:45:46

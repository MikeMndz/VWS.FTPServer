-- MySQL dump 10.13  Distrib 8.0.25, for Win64 (x86_64)
--
-- Host: localhost    Database: vws_ftpserver
-- ------------------------------------------------------
-- Server version	8.0.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Permission`
--

DROP TABLE IF EXISTS `Permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Permission` (
  `Id` int NOT NULL,
  `Name` varchar(200) COLLATE utf8mb4_bin NOT NULL,
  `Enabled` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Permission`
--

LOCK TABLES `Permission` WRITE;
/*!40000 ALTER TABLE `Permission` DISABLE KEYS */;
/*!40000 ALTER TABLE `Permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Role`
--

DROP TABLE IF EXISTS `Role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Role` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) COLLATE utf8mb4_bin NOT NULL,
  `Enabled` tinyint(1) NOT NULL DEFAULT '1',
  `IsStatic` tinyint(1) NOT NULL DEFAULT '0',
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_Id` (`Id`),
  KEY `IDX_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role`
--

LOCK TABLES `Role` WRITE;
/*!40000 ALTER TABLE `Role` DISABLE KEYS */;
INSERT INTO `Role` VALUES (1,'Administrador',1,1,'2022-04-27 12:00:00',NULL),(2,'Operador',1,1,'2022-04-27 12:00:00',NULL);
/*!40000 ALTER TABLE `Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Role_Permission`
--

DROP TABLE IF EXISTS `Role_Permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Role_Permission` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FkRole` int NOT NULL,
  `FkPermission` int NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Role_Permission_Role` (`FkRole`),
  KEY `FK_Role_Permission_Permission` (`FkPermission`),
  CONSTRAINT `FK_Role_Permission_Permission` FOREIGN KEY (`FkPermission`) REFERENCES `Permission` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Role_Permission_Role` FOREIGN KEY (`FkRole`) REFERENCES `Role` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Role_Permission`
--

LOCK TABLES `Role_Permission` WRITE;
/*!40000 ALTER TABLE `Role_Permission` DISABLE KEYS */;
/*!40000 ALTER TABLE `Role_Permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Uid` varchar(36) COLLATE utf8mb4_bin NOT NULL,
  `Account` varchar(150) COLLATE utf8mb4_bin DEFAULT NULL,
  `Password` text COLLATE utf8mb4_bin,
  `Name` varchar(150) COLLATE utf8mb4_bin DEFAULT NULL,
  `LastName` varchar(100) COLLATE utf8mb4_bin DEFAULT NULL,
  `MothersLastName` varchar(100) COLLATE utf8mb4_bin DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL DEFAULT '1',
  `Deleted` tinyint(1) NOT NULL DEFAULT '0',
  `Token` text COLLATE utf8mb4_bin,
  `LastAccess` datetime DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_Id` (`Id`),
  KEY `IDX_Uid` (`Uid`),
  KEY `IDX_Account` (`Account`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User_Role`
--

DROP TABLE IF EXISTS `User_Role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User_Role` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FkUser` int NOT NULL,
  `FkRole` int NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_FkUser` (`FkUser`),
  KEY `IDX_FkRole` (`FkRole`),
  CONSTRAINT `FK_User_Role_Role` FOREIGN KEY (`FkRole`) REFERENCES `Role` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_User_Role_User` FOREIGN KEY (`FkUser`) REFERENCES `User` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User_Role`
--

LOCK TABLES `User_Role` WRITE;
/*!40000 ALTER TABLE `User_Role` DISABLE KEYS */;
/*!40000 ALTER TABLE `User_Role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'vws_ftpserver'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-05-03 13:29:26

-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 27-09-2025 a las 00:42:07
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria_net`
--
CREATE DATABASE IF NOT EXISTS `inmobiliaria_net` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `inmobiliaria_net`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `monto` decimal(11,0) NOT NULL,
  `fechaDesde` date NOT NULL,
  `fechaHasta` date NOT NULL,
  `idUsuarioAlta` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL,
  `idUsuarioBaja` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `monto`, `fechaDesde`, `fechaHasta`, `idUsuarioAlta`, `estado`, `idUsuarioBaja`) VALUES
(16, 3, 8, 100000, '2025-09-10', '2025-09-28', 17, 0, NULL),
(17, 3, 9, 110, '2025-09-13', '2025-09-28', 17, 0, 17),
(18, 3, 9, 110, '2025-09-11', '2025-09-28', 17, 0, 17),
(19, 2, 9, 100000, '2025-09-26', '2025-09-30', 17, 0, 17),
(20, 3, 8, 110, '2025-09-01', '2025-09-02', 17, 0, 17),
(21, 3, 9, 110, '2025-09-01', '2025-09-10', 17, 0, 17),
(22, 2, 9, 110, '2025-09-13', '2025-09-28', 17, 0, 17),
(23, 2, 8, 100000, '2025-09-01', '2025-09-29', 17, 1, NULL),
(24, 3, 8, 100000, '2025-09-01', '2025-08-05', 17, 0, 17),
(25, 2, 8, 341312, '2025-09-30', '2025-10-10', 17, 0, 17),
(26, 3, 9, 111, '2025-09-11', '2025-09-12', 17, 0, 17),
(27, 2, 10, 100000, '2025-09-02', '2025-09-26', 17, 0, 17),
(28, 2, 10, 100000, '2025-09-02', '2025-09-26', 17, 1, NULL),
(29, 2, 10, 111111, '2025-09-27', '2025-09-28', 17, 0, 17),
(30, 2, 9, 3132131, '2025-09-04', '2025-09-27', 17, 0, 17);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `uso` tinyint(50) NOT NULL,
  `tipo` tinyint(50) NOT NULL,
  `cantidadAmb` int(11) NOT NULL,
  `coordenadas` varchar(100) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `direccion`, `uso`, `tipo`, `cantidadAmb`, `coordenadas`, `precio`, `idPropietario`, `estado`) VALUES
(8, 'Ayacucho 1424', 1, 2, 4, '-14.254,-74.25', 120000, 5, 1),
(9, 'francia', 1, 1, 2, '45012,7805', 650000, 8, 1),
(10, 'mitre 14', 2, 4, 7, '-14.254,-74.25', 600000, 7, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `celular` varchar(50) NOT NULL,
  `estado` tinyint(1) NOT NULL,
  `idInquilino` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`nombre`, `apellido`, `dni`, `email`, `celular`, `estado`, `idInquilino`) VALUES
('Candela', 'Godoy', '399903021', 'candegg13@gmail.com', '265256678', 0, 2),
('Roberto', 'Gomez', '24856987', 'robertito@gmail.com', '2665874125', 1, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `descripcion` varchar(100) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` double NOT NULL,
  `estado` tinyint(1) NOT NULL,
  `numPago` int(11) NOT NULL,
  `idUsuarioAlta` int(11) NOT NULL,
  `idUsuarioBaja` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `descripcion`, `idContrato`, `fechaPago`, `importe`, `estado`, `numPago`, `idUsuarioAlta`, `idUsuarioBaja`) VALUES
(10, 'noseeee', 18, '2025-09-18', 100000, 0, 1, 17, 17),
(11, 'nose', 18, '2025-09-18', 100000, 0, 2, 17, 17),
(12, 'noseeeee', 21, '2025-09-18', 100000, 1, 1, 17, NULL),
(13, 'jajaja', 21, '2025-09-27', 100000, 0, 2, 17, 17),
(14, 'jajaja', 21, '2025-09-27', 100000, 0, 2, 17, 17),
(15, 'QSYO', 23, '2025-09-27', 100000, 0, 1, 18, 17),
(16, '313131', 28, '2025-09-28', 3123131, 0, 1, 17, 17);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `celular` varchar(50) NOT NULL,
  `estado` tinyint(1) NOT NULL,
  `idPropietario` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`nombre`, `apellido`, `dni`, `direccion`, `celular`, `estado`, `idPropietario`) VALUES
('Fabricio', 'Zalazar', '43028349', 'Cerro de la cruz c11 111', '2664830242', 1, 5),
('Pedro', 'Perez', '12345678', '', '12345678', 1, 7),
('Candela Guadalupe', 'Godoy', '3698596', 'ayauvus 14', '6558965', 1, 8),
('Nando', 'Aguero', '36173621', 'Rawson 126', '2664158354', 1, 11);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(250) NOT NULL,
  `email` varchar(250) NOT NULL,
  `clave` varchar(250) NOT NULL,
  `avatar` varchar(255) NOT NULL,
  `rol` tinyint(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `nombre`, `apellido`, `email`, `clave`, `avatar`, `rol`) VALUES
(17, 'Lucas', 'Gomez', 'luquitas@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_17_7adf3a3615ca4a25b5b22dd85bda7680.png', 1),
(18, 'Fabricio', 'Zalazar', 'zalazar05fabricio@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_18_83298d5500af440c9dbb677cd7c3297f.png', 2),
(20, 'Ejemplo', 'Ejemplo', 'ejemplo@gmail.com', 'vriFzN0hoKZUEM8VYWQOPSvGnqYir8cQyVMLkaNHoEQ=', '/Uploads/avatar_20_218ca9eb728f4b96bb280c280ac428e1.png', 2);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `idInmueble` (`idInmueble`),
  ADD KEY `idInquilino` (`idInquilino`),
  ADD KEY `idUsuario` (`idUsuarioAlta`),
  ADD KEY `idUsuarioBaja` (`idUsuarioBaja`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idPropietario` (`idPropietario`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idContrato` (`idContrato`),
  ADD KEY `idUsuario` (`idUsuarioAlta`),
  ADD KEY `idUsuarioBaja` (`idUsuarioBaja`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`idInquilino`) REFERENCES `inquilinos` (`idInquilino`),
  ADD CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`idUsuarioAlta`) REFERENCES `usuario` (`idUsuario`),
  ADD CONSTRAINT `contrato_ibfk_4` FOREIGN KEY (`idUsuarioBaja`) REFERENCES `usuario` (`idUsuario`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietarios` (`idPropietario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`),
  ADD CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`idUsuarioAlta`) REFERENCES `usuario` (`idUsuario`),
  ADD CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`idUsuarioBaja`) REFERENCES `usuario` (`idUsuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

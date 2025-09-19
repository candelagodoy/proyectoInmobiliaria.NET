-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-09-2025 a las 04:57:54
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
  `monto` decimal(10,0) NOT NULL,
  `fechaDesde` date NOT NULL,
  `fechaHasta` date NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `monto`, `fechaDesde`, `fechaHasta`, `idUsuario`, `estado`) VALUES
(6, 2, 1, 1110, '2025-09-11', '2025-09-27', 9, 1),
(9, 3, 1, 110, '2025-09-27', '2025-09-04', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `uso` varchar(50) NOT NULL,
  `tipo` varchar(50) NOT NULL,
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
(1, 'Villa larca 1772', 'Rotiseria', 'NOse', 2, '1dasd13', 600000, 5, 1),
(3, 'Cerro de la cruz c22', 'Casa', 'CASA', 2, '123X Y 321', 4020000, 5, 1);

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
('Candela Guadalupe', 'Godoy', '39990302', 'candegg13@gmail.com', '265256625', 0, 2),
('Fabricio', 'ZalazarR', '43072929', 'zalazar5fabricio@gmail.com', '1111111', 1, 3);

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
  `numPago` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
('pepe', 'perez', '12345678', '', '12345678', 1, 2),
('Nano', 'Moise', '6429214', 'Loco Amato', '266593023', 0, 3),
('Fabri', 'Zalazar', '43028349', 'Cerro de la cruz c11 ', '2664830242', 1, 5),
('Candela Guadalupe', 'Godoy', '3698596', 'ayauvus 14', '6558965', 1, 8),
('Pepe', 'Varas', '36173621', 'Villa Larca 1772', '21111', 1, 11);

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
(1, 'Gabri', 'Zalazar', 'Gabri@gmail.com', '1234', 'no hay', 2),
(9, 'Ejemplo', 'Ejemplo', 'nose@nose.com', '1234', 'https://www.w3schools.com/howto/img_avatar.png', 1),
(12, 'Ejemplo', 'Ejemplo', 'nose@nose.com', '1234', 'https://www.w3schools.com/howto/img_avatar.png', 1);

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
  ADD KEY `idUsuario` (`idUsuario`);

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
  ADD KEY `idContrato` (`idContrato`);

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
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`idInquilino`) REFERENCES `inquilinos` (`idInquilino`),
  ADD CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`idUsuario`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietarios` (`idPropietario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

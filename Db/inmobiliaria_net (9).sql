-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 28-09-2025 a las 03:02:45
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
(1, 1, 1, 250000, '2025-07-01', '2026-06-30', 3, 1, NULL),
(2, 2, 2, 180000, '2025-08-01', '2026-07-31', 4, 1, NULL),
(3, 3, 3, 350000, '2025-06-01', '2026-05-31', 3, 1, NULL),
(4, 4, 4, 220000, '2024-09-01', '2025-08-31', 5, 0, 2),
(5, 5, 5, 190000, '2025-03-01', '2026-02-28', 3, 1, NULL),
(6, 6, 6, 300000, '2024-05-01', '2025-04-30', 4, 0, 1),
(7, 7, 7, 270000, '2025-01-15', '2026-01-14', 5, 1, NULL),
(8, 8, 8, 320000, '2025-09-01', '2026-08-31', 3, 1, NULL),
(9, 9, 9, 170000, '2025-02-01', '2026-01-31', 4, 1, NULL),
(10, 10, 10, 280000, '2024-10-01', '2025-09-30', 5, 0, 2),
(11, 2, 4, 100001110, '2025-09-06', '2025-09-19', 2, 1, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `uso` tinyint(50) NOT NULL,
  `idTipoInmueble` int(11) NOT NULL,
  `cantidadAmb` int(11) NOT NULL,
  `coordenadas` varchar(100) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `direccion`, `uso`, `idTipoInmueble`, `cantidadAmb`, `coordenadas`, `precio`, `idPropietario`, `estado`) VALUES
(1, 'Ayacucho 1424, San Luis', 1, 2, 4, '-33.297,-66.337', 250000, 1, 1),
(2, 'Belgrano 580, San Luis', 1, 1, 2, '-33.300,-66.335', 180000, 2, 1),
(3, 'Mitre 742, Villa Mercedes', 2, 3, 1, '-33.676,-65.458', 350000, 3, 1),
(4, 'Chile 345, La Punta', 1, 2, 3, '-33.183,-66.313', 220000, 4, 1),
(5, 'Ituzaingó 910, San Luis', 1, 1, 3, '-33.293,-66.334', 190000, 5, 1),
(6, 'España 1550, San Luis', 2, 4, 2, '-33.295,-66.340', 300000, 6, 1),
(7, 'Lavalle 300, La Punta', 1, 2, 5, '-33.186,-66.313', 270000, 7, 1),
(8, 'Francia 221, San Luis', 2, 3, 1, '-33.296,-66.333', 320000, 8, 1),
(9, 'Rawson 126, San Luis', 1, 1, 2, '-33.300,-66.331', 170000, 9, 1),
(10, 'Av. Illia 1250, San Luis', 2, 10, 1, '-33.295,-66.335', 280000, 10, 0);

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
('Rodrigo', 'Moreno', '36111222', 'rodrigo.moreno@gmail.com', '2665101001', 1, 1),
('Lucía', 'Paz', '38222333', 'lucia.paz@gmail.com', '2665101002', 1, 2),
('Valentina', 'Suárez', '35777999', 'valentina.suarez@gmail.com', '2665101003', 1, 3),
('Agustín', 'Cabrera', '32999111', 'agustin.cabrera@gmail.com', '2665101004', 1, 4),
('Santiago', 'Costa', '27888999', 'santiago.costa@gmail.com', '2665101005', 1, 5),
('Marina', 'Ferreyra', '30111222', 'marina.ferreyra@gmail.com', '2665101006', 1, 6),
('Ignacio', 'Herrera', '27123999', 'ignacio.herrera@gmail.com', '2665101007', 1, 7),
('Camila', 'Molina', '29888111', 'camila.molina@gmail.com', '2665101008', 1, 8),
('Bruno', 'Funes', '33222666', 'bruno.funes@gmail.com', '2665101009', 1, 9),
('Emilia', 'Navarro', '30999666', 'emilia.navarro@gmail.com', '2665101010', 1, 10);

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
(1, 'Alquiler Julio 2025', 1, '2025-07-05', 250000, 1, 1, 3, NULL),
(2, 'Alquiler Agosto 2025', 1, '2025-08-05', 250000, 1, 2, 3, NULL),
(3, 'Alquiler Septiembre 2025', 1, '2025-09-05', 250000, 1, 3, 3, NULL),
(4, 'Alquiler Agosto 2025', 2, '2025-08-03', 180000, 1, 1, 4, NULL),
(5, 'Alquiler Septiembre 2025', 2, '2025-09-03', 180000, 1, 2, 4, NULL),
(6, 'Alquiler Junio 2025', 3, '2025-06-02', 350000, 1, 1, 3, NULL),
(7, 'Alquiler Julio 2025', 3, '2025-07-02', 350000, 1, 2, 3, NULL),
(8, 'Alquiler Agosto 2025', 3, '2025-08-02', 350000, 1, 3, 3, NULL),
(9, 'Alquiler Septiembre 2025', 3, '2025-09-02', 350000, 1, 4, 3, NULL),
(10, 'Alquiler Agosto 2025', 4, '2025-08-05', 220000, 0, 12, 5, 2),
(11, 'Multa rescisión (1 mes)', 4, '2025-08-10', 220000, 1, 13, 5, NULL),
(12, 'Alquiler Marzo 2025', 5, '2025-03-03', 190000, 1, 1, 3, NULL),
(13, 'Alquiler Abril 2025', 5, '2025-04-03', 190000, 1, 2, 3, NULL),
(14, 'Alquiler Septiembre 2025', 5, '2025-09-20', 190000, 1, 7, 3, NULL),
(15, 'Alquiler Abril 2025', 6, '2025-04-05', 300000, 1, 12, 4, NULL),
(16, 'Alquiler Julio 2025', 7, '2025-07-18', 270000, 0, 6, 5, 2),
(17, 'Alquiler Agosto 2025', 7, '2025-08-18', 270000, 1, 7, 5, NULL),
(18, 'Alquiler Septiembre 2025', 8, '2025-09-07', 320000, 1, 1, 3, NULL),
(19, 'Alquiler Agosto 2025', 9, '2025-08-05', 170000, 1, 7, 4, NULL),
(20, 'Alquiler Septiembre 2025', 9, '2025-09-05', 170000, 1, 8, 4, NULL),
(21, 'Alquiler Septiembre 2025', 10, '2025-09-20', 280000, 1, 12, 5, NULL),
(22, 'Multa rescisión (2 meses)', 10, '2025-09-27', 560000, 1, 13, 5, NULL),
(23, 'Pago por anulacion', 7, '2025-09-27', 100000, 1, 8, 2, NULL),
(24, 'Pago por anulacionnnn', 2, '2025-09-27', 100000, 0, 3, 2, 2);

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
('Marta', 'Quiroga', '20345678', 'Av. Illia 1250, San Luis', '2664001001', 1, 1),
('Jorge', 'Pereyra', '17890123', 'Mitre 742, Villa Mercedes', '2657001002', 1, 2),
('Graciela', 'Romero', '25111222', 'Belgrano 580, San Luis', '2664200003', 1, 3),
('Ricardo', 'Sosa', '23333444', 'Chile 345, La Punta', '2664300004', 1, 4),
('Paula', 'Díaz', '27999888', 'Ituzaingó 910, San Luis', '2664400005', 1, 5),
('Héctor', 'Mansilla', '20999111', 'Salta 1220, Juana Koslay', '2664500006', 1, 6),
('Carolina', 'López', '31888999', 'España 1550, San Luis', '2664600007', 1, 7),
('Nicolás', 'Vega', '30123456', 'Francia 221, San Luis', '2664700008', 1, 8),
('Elena', 'Rivas', '28777001', 'Rawson 126, San Luis', '2664800009', 1, 9),
('Gustavo', 'Benítez', '24999123', 'Lavalle 300, La Punta', '2664900010', 1, 10);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoinmueble`
--

CREATE TABLE `tipoinmueble` (
  `idTipoInmueble` int(11) NOT NULL,
  `nombre` varchar(200) NOT NULL,
  `descripción` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipoinmueble`
--

INSERT INTO `tipoinmueble` (`idTipoInmueble`, `nombre`, `descripción`) VALUES
(1, 'Departamento', 'Unidad funcional en edificio'),
(2, 'Casa', 'Vivienda familiar'),
(3, 'Local Comercial', 'Espacio para comercio a la calle'),
(4, 'Oficina', 'Espacio administrativo'),
(5, 'Depósito', 'Almacenamiento / logística'),
(6, 'PH', 'Propiedad horizontal'),
(7, 'Dúplex', 'Vivienda en dos plantas'),
(8, 'Cabaña', 'Vivienda tipo cabaña'),
(9, 'Galpón', 'Espacio amplio cubierto'),
(10, 'Terreno', 'Lote sin edificar');

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
(1, 'Sofía', 'Álvarez', 'sofia.alvarez@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_1_84cba0554dbc4f88987b2eab475fe5cc.jpg', 1),
(2, 'Lucas', 'Gómez', 'luquitas@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_2_ee4f7601bc73412a8f141bc4d9465bd7.png', 1),
(3, 'Fabricio', 'Zalazar', 'zalazar05fabricio@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_3_7a0beea24b404ad9be296bead2f156ee.png', 2),
(4, 'Candela', 'Godoy', 'candela.godoy@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_4.png', 2),
(5, 'Fernando', 'Agüero', 'fernando.aguero@gmail.com', 'DlkcU79nZBoNsU1Cv1q0hwnftlUNMcqvV/TYViY2BDk=', '/Uploads/avatar_5.png', 2);

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
  ADD KEY `idPropietario` (`idPropietario`),
  ADD KEY `idTipoInmueble` (`idTipoInmueble`);

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
-- Indices de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  ADD PRIMARY KEY (`idTipoInmueble`);

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
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `tipoinmueble`
--
ALTER TABLE `tipoinmueble`
  MODIFY `idTipoInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

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
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idPropietario`) REFERENCES `propietarios` (`idPropietario`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`idTipoInmueble`) REFERENCES `tipoinmueble` (`idTipoInmueble`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`idUsuarioAlta`) REFERENCES `usuario` (`idUsuario`),
  ADD CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`idUsuarioBaja`) REFERENCES `usuario` (`idUsuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

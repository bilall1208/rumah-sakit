-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 11, 2026 at 10:17 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_rumah_sakit`
--

-- --------------------------------------------------------

--
-- Table structure for table `detailresep`
--

CREATE TABLE `detailresep` (
  `ID_DetailResep` int(11) NOT NULL,
  `ID_Periksa` int(11) NOT NULL,
  `ID_Obat` int(11) NOT NULL,
  `JumlahObat` int(11) NOT NULL,
  `Subtotal` decimal(10,2) NOT NULL,
  `StatusResep` varchar(20) DEFAULT 'Menunggu'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kunjungan`
--

CREATE TABLE `kunjungan` (
  `ID_Kunjungan` int(11) NOT NULL,
  `No_RM` varchar(20) NOT NULL,
  `Poli` varchar(50) DEFAULT NULL,
  `NamaDokter` varchar(100) DEFAULT NULL,
  `TglKunjungan` datetime DEFAULT current_timestamp(),
  `Status` varchar(20) DEFAULT 'Menunggu'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `kunjungan`
--

INSERT INTO `kunjungan` (`ID_Kunjungan`, `No_RM`, `Poli`, `NamaDokter`, `TglKunjungan`, `Status`) VALUES
(1, 'RM0001', 'Poli Gigi', 'drg. Maya Putri', '2026-09-04 19:47:46', 'Menunggu'),
(2, 'RM0001', 'Poli Umum', 'dr. Andi Wijaya', '2026-09-10 09:20:15', 'Menunggu'),
(3, 'RM0001', 'Poli Kandungan', 'drg. Maya Putri', '2026-09-11 07:10:24', 'Menunggu'),
(4, 'RM0002', 'Poli Kandungan', 'drg. Maya Putri', '2026-09-11 07:46:08', 'Menunggu');

-- --------------------------------------------------------

--
-- Table structure for table `obat`
--

CREATE TABLE `obat` (
  `ID_Obat` int(11) NOT NULL,
  `NamaObat` varchar(100) NOT NULL,
  `Satuan` varchar(20) DEFAULT NULL,
  `HargaSatuan` decimal(10,2) NOT NULL,
  `Stok` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `obat`
--

INSERT INTO `obat` (`ID_Obat`, `NamaObat`, `Satuan`, `HargaSatuan`, `Stok`) VALUES
(1, 'Paracetamol 500mg', 'Kaplet', 12000.00, 120),
(2, 'Vitamin K', 'Tablet', 5000.00, 180);

-- --------------------------------------------------------

--
-- Table structure for table `pasien`
--

CREATE TABLE `pasien` (
  `No_RM` varchar(20) NOT NULL,
  `Nama` varchar(100) NOT NULL,
  `JenisKelamin` enum('L','P') DEFAULT NULL,
  `TglLahir` date DEFAULT NULL,
  `Alamat` varchar(255) DEFAULT NULL,
  `NIK` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pasien`
--

INSERT INTO `pasien` (`No_RM`, `Nama`, `JenisKelamin`, `TglLahir`, `Alamat`, `NIK`) VALUES
('RM0001', 'Zayyan', 'L', '2009-01-14', 'Ngamprahhhh RT99/90, Kel. Lagadar', '899789086'),
('RM0002', 'Faiz Fadhilah', 'L', '2008-12-25', 'Cibeber FuriFjar', '');

-- --------------------------------------------------------

--
-- Table structure for table `pembayaran`
--

CREATE TABLE `pembayaran` (
  `ID_Bayar` int(11) NOT NULL,
  `ID_Kunjungan` int(11) NOT NULL,
  `Total` decimal(10,2) NOT NULL,
  `MetodeBayar` varchar(20) DEFAULT NULL,
  `Bayar` decimal(10,2) DEFAULT NULL,
  `Kembalian` decimal(10,2) DEFAULT NULL,
  `TglBayar` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `pemeriksaan`
--

CREATE TABLE `pemeriksaan` (
  `ID_Periksa` int(11) NOT NULL,
  `ID_Kunjungan` int(11) NOT NULL,
  `Keluhan` varchar(255) DEFAULT NULL,
  `Diagnosa` varchar(255) DEFAULT NULL,
  `BiayaKonsultasi` decimal(10,2) DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pemeriksaan`
--

INSERT INTO `pemeriksaan` (`ID_Periksa`, `ID_Kunjungan`, `Keluhan`, `Diagnosa`, `BiayaKonsultasi`) VALUES
(1, 4, 'Sakit peyut', 'Kuman di perut', 50000.00),
(2, 4, 'Sakit jiwa', 'stressssss', 5000000.00),
(3, 4, 'Sakit perut', 'Kuman di perut', 5000000.00);

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `role_id` int(11) NOT NULL,
  `nama_role` varchar(50) NOT NULL,
  `keterangan` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`role_id`, `nama_role`, `keterangan`) VALUES
(1, 'Admin', 'untuk admin'),
(2, 'Dokter', 'untuk dokter'),
(3, 'Pendaftaran', 'untuk pendaftaran'),
(4, 'Farmasi', 'untuk farmasi'),
(5, 'Kasir', 'untuk kasir');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `ID_User` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `NamaLengkap` varchar(100) DEFAULT NULL,
  `role_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID_User`, `Username`, `Password`, `NamaLengkap`, `role_id`) VALUES
(1, 'admin', 'admin1', 'Administrator', 1),
(2, 'rina', 'daftar123', 'Rina ', 3),
(3, 'drandi', 'dokter123', 'Andi Soebardjo', 2),
(4, 'sari', 'farmasi123', 'Sari ', 4),
(5, 'dedi', 'kasir1', 'Dedi ', 5),
(9, '123456', '12345', 'Jayan', 9),
(10, 'bilal12', '1218', 'Bilal Fahrezi', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `detailresep`
--
ALTER TABLE `detailresep`
  ADD PRIMARY KEY (`ID_DetailResep`),
  ADD KEY `ID_Periksa` (`ID_Periksa`),
  ADD KEY `ID_Obat` (`ID_Obat`);

--
-- Indexes for table `kunjungan`
--
ALTER TABLE `kunjungan`
  ADD PRIMARY KEY (`ID_Kunjungan`),
  ADD KEY `No_RM` (`No_RM`);

--
-- Indexes for table `obat`
--
ALTER TABLE `obat`
  ADD PRIMARY KEY (`ID_Obat`);

--
-- Indexes for table `pasien`
--
ALTER TABLE `pasien`
  ADD PRIMARY KEY (`No_RM`),
  ADD UNIQUE KEY `NIK` (`NIK`);

--
-- Indexes for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD PRIMARY KEY (`ID_Bayar`),
  ADD KEY `ID_Kunjungan` (`ID_Kunjungan`);

--
-- Indexes for table `pemeriksaan`
--
ALTER TABLE `pemeriksaan`
  ADD PRIMARY KEY (`ID_Periksa`),
  ADD KEY `ID_Kunjungan` (`ID_Kunjungan`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`role_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`ID_User`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `detailresep`
--
ALTER TABLE `detailresep`
  MODIFY `ID_DetailResep` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `kunjungan`
--
ALTER TABLE `kunjungan`
  MODIFY `ID_Kunjungan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `obat`
--
ALTER TABLE `obat`
  MODIFY `ID_Obat` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `pembayaran`
--
ALTER TABLE `pembayaran`
  MODIFY `ID_Bayar` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pemeriksaan`
--
ALTER TABLE `pemeriksaan`
  MODIFY `ID_Periksa` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `ID_User` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `detailresep`
--
ALTER TABLE `detailresep`
  ADD CONSTRAINT `detailresep_ibfk_1` FOREIGN KEY (`ID_Periksa`) REFERENCES `pemeriksaan` (`ID_Periksa`),
  ADD CONSTRAINT `detailresep_ibfk_2` FOREIGN KEY (`ID_Obat`) REFERENCES `obat` (`ID_Obat`);

--
-- Constraints for table `kunjungan`
--
ALTER TABLE `kunjungan`
  ADD CONSTRAINT `kunjungan_ibfk_1` FOREIGN KEY (`No_RM`) REFERENCES `pasien` (`No_RM`);

--
-- Constraints for table `pembayaran`
--
ALTER TABLE `pembayaran`
  ADD CONSTRAINT `pembayaran_ibfk_1` FOREIGN KEY (`ID_Kunjungan`) REFERENCES `kunjungan` (`ID_Kunjungan`);

--
-- Constraints for table `pemeriksaan`
--
ALTER TABLE `pemeriksaan`
  ADD CONSTRAINT `pemeriksaan_ibfk_1` FOREIGN KEY (`ID_Kunjungan`) REFERENCES `kunjungan` (`ID_Kunjungan`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 21, 2026 at 01:45 PM
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

--
-- Dumping data for table `detailresep`
--

INSERT INTO `detailresep` (`ID_DetailResep`, `ID_Periksa`, `ID_Obat`, `JumlahObat`, `Subtotal`, `StatusResep`) VALUES
(11, 12, 6, 1, 12000.00, 'Diserahkan'),
(12, 12, 4, 4, 40000.00, 'Diserahkan');

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
(10, 'RM0006', 'Poli Umum', 'dr. Andi Wijaya', '2026-11-13 11:03:26', 'Selesai');

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
(1, 'Paracetamol 500mg', 'Kaplet', 12000.00, 118),
(2, 'Vitamin K', 'Tablet', 5000.00, 174),
(4, 'Vitamin C', 'Tablet', 10000.00, 121),
(5, 'Panadol', 'Tablet', 2500.00, 150),
(6, 'Ibuprofen', 'Kapsul', 12000.00, 108);

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
('RM0001', 'Zayyan', 'P', '2009-01-14', 'Ngamprah RT09/15, Kel. Paku Haji', '899789086'),
('RM0002', 'Faiz Fadhilah', 'L', '2008-12-25', 'Cibeber, Puri  Fajar', '13542674'),
('RM0003', 'Bilal Fahrezi', 'L', '2008-01-29', 'Cikuya, Bumiwangi RT08/15', '137702931121'),
('RM0004', 'Mufti Subhan', 'L', '2006-07-19', 'Cibabat', '13290288492'),
('RM0005', 'Gaza Palestina', 'L', '2026-09-18', 'Melong', '8540670435'),
('RM0006', 'Nazran Arkan', 'L', '2009-03-13', 'Pemda II, Cibeber', '081425367');

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

--
-- Dumping data for table `pembayaran`
--

INSERT INTO `pembayaran` (`ID_Bayar`, `ID_Kunjungan`, `Total`, `MetodeBayar`, `Bayar`, `Kembalian`, `TglBayar`) VALUES
(2, 10, 77000.00, 'Tunai', 77000.00, 0.00, '2026-11-14 15:21:37');

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
(12, 10, 'Sakit perut', 'Diare', 25000.00);

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
  `password` varchar(255) NOT NULL,
  `NamaLengkap` varchar(100) DEFAULT NULL,
  `role_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID_User`, `Username`, `password`, `NamaLengkap`, `role_id`) VALUES
(11, 'admin18', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Bilal', 1),
(12, 'kasir18', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Rina', 5),
(13, 'dokter18', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Andi', 2),
(14, 'daftar18', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Kendra', 3),
(15, 'farmasi18', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Sintya', 4);

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
  MODIFY `ID_DetailResep` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `kunjungan`
--
ALTER TABLE `kunjungan`
  MODIFY `ID_Kunjungan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `obat`
--
ALTER TABLE `obat`
  MODIFY `ID_Obat` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `pembayaran`
--
ALTER TABLE `pembayaran`
  MODIFY `ID_Bayar` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `pemeriksaan`
--
ALTER TABLE `pemeriksaan`
  MODIFY `ID_Periksa` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `ID_User` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

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

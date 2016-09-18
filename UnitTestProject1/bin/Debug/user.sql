/*
Navicat MySQL Data Transfer

Source Server         : 168.168.1.106
Source Server Version : 50711
Source Host           : 168.168.1.106:3306
Source Database       : user1

Target Server Type    : MYSQL
Target Server Version : 50711
File Encoding         : 65001

Date: 2016-09-14 11:10:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `account`
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `Id` int(11) NOT NULL,
  `Name` varchar(128) DEFAULT NULL,
  `Email` varchar(128) DEFAULT NULL,
  `Password` char(32) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `Salt` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of account1
-- ----------------------------

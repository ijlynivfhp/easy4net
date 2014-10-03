/*
Navicat MySQL Data Transfer

Source Server         : Easy4netTest
Source Server Version : 50518
Source Host           : chenghong.mysql.rds.aliyuncs.com:3306
Source Database       : test_db

Target Server Type    : MYSQL
Target Server Version : 50518
File Encoding         : 65001

Date: 2014-10-03 23:15:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `company`
-- ----------------------------
DROP TABLE IF EXISTS `company`;
CREATE TABLE `company` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `company_name` varchar(255) DEFAULT NULL,
  `industry` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of company
-- ----------------------------
INSERT INTO `company` VALUES ('1', '深圳天游网络有限公司', '游戏/互联网', '深圳市');
INSERT INTO `company` VALUES ('2', '上海巨人网络信息科技', '游戏/互联网', '上海市徐汇区');
INSERT INTO `company` VALUES ('3', '上海澄泓信息科技有限公司', '移动互联网', '上海市普陀区枣阳路185弄');
INSERT INTO `company` VALUES ('4', '测试公司1001', '互联网/软件开发', '上海市浦东区');
INSERT INTO `company` VALUES ('5', '测试公司1002', '互联网/软件开发', '上海市徐汇区');
INSERT INTO `company` VALUES ('6', '测试公司1003', '互联网/软件开发', '上海市浦东新区张江高科');
INSERT INTO `company` VALUES ('7', '测试公司1004', '互联网/软件开发', '上海市普陀区枣阳路');
INSERT INTO `company` VALUES ('8', '测试公司1005', '互联网/软件开发', '上海市曹安国际鞋城');
INSERT INTO `company` VALUES ('9', '测试公司1006', '互联网/软件开发', '上海市嘉定区');
INSERT INTO `company` VALUES ('10', '测试公司1007', '互联网/软件开发', '武汉市');
INSERT INTO `company` VALUES ('11', '测试公司1008', '互联网/软件开发', '深圳市');
INSERT INTO `company` VALUES ('12', '测试公司1009', '互联网/软件开发', '北京市西直门');
INSERT INTO `company` VALUES ('13', '测试公司1010', '互联网/软件开发', '北京市西单');
INSERT INTO `company` VALUES ('14', '测试公司1010', '互联网/软件开发', '北京市上地软件园');

-- ----------------------------
-- Table structure for `employee`
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `created` datetime DEFAULT NULL,
  `company_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES ('1', '李想', '100', '山东市', '2014-10-03 22:16:30', '1');
INSERT INTO `employee` VALUES ('2', '王威', '500', '上海市普陀区中山北路510弄', '2014-10-03 22:19:19', '3');
INSERT INTO `employee` VALUES ('3', '张三', '28', '北京市中山路115弄', '2014-10-03 22:59:53', '12');
INSERT INTO `employee` VALUES ('4', '李四', '26', '上海市徐汇区宜山路279号', '2014-10-03 23:00:31', '2');
INSERT INTO `employee` VALUES ('5', '刘兰', '22', '上海市徐汇区宜山路279号', '2014-10-03 23:00:49', '2');
INSERT INTO `employee` VALUES ('6', '张辉', '25', '上海市浦东区浦电路305弄', '2014-10-03 23:01:14', '2');
INSERT INTO `employee` VALUES ('7', '刘星', '26', '上海市普陀区枣阳路115弄', '2014-10-03 23:01:44', '3');
INSERT INTO `employee` VALUES ('8', '李洋', '25', '上海市松江158号', '2014-10-03 23:02:15', '4');
INSERT INTO `employee` VALUES ('9', '张磊', '33', '上海市嘉定区', '2014-10-03 23:02:48', '7');
INSERT INTO `employee` VALUES ('10', '蒋洋', '42', '上海市普陀区太子路188号', '2014-10-03 23:03:22', '9');
INSERT INTO `employee` VALUES ('11', '李红', '29', '上海市黄浦区330号', '2014-10-03 23:04:08', '3');

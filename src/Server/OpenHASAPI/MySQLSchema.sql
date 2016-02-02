CREATE TABLE `accounts` (
  `account_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(128) COLLATE utf8_bin NOT NULL,
  `password_hash` varchar(128) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`account_id`),
  UNIQUE KEY `user_id_UNIQUE` (`account_id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE `homes` (
  `home_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL,
  `name` varchar(128) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`home_id`),
  UNIQUE KEY `home_id_UNIQUE` (`home_id`),
  KEY `account_id_idx` (`account_id`),
  CONSTRAINT `account_home` FOREIGN KEY (`account_id`) REFERENCES `accounts` (`account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE `hubs` (
  `hub_id` int(11) NOT NULL AUTO_INCREMENT,
  `account_id` int(11) NOT NULL,
  `home_id` int(11) NOT NULL,
  `encode_key` varchar(1024) COLLATE utf8_bin NOT NULL,
  `decode_key` varchar(1024) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`hub_id`),
  UNIQUE KEY `hub_id_UNIQUE` (`hub_id`),
  KEY `account_hub_idx` (`account_id`),
  KEY `home_hub_idx` (`home_id`),
  CONSTRAINT `account_hub` FOREIGN KEY (`account_id`) REFERENCES `accounts` (`account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `home_hub` FOREIGN KEY (`home_id`) REFERENCES `homes` (`home_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE `devices` (
  `device_id` varchar(128) COLLATE utf8_bin NOT NULL,
  `hub_id` int(11) NOT NULL,
  `account_id` int(11) NOT NULL,
  `device_type` varchar(128) COLLATE utf8_bin NOT NULL,
  `fw_version` int(11) NOT NULL,
  PRIMARY KEY (`device_id`,`hub_id`),
  KEY `account_device_idx` (`account_id`),
  KEY `hub_device_idx` (`hub_id`),
  CONSTRAINT `account_device` FOREIGN KEY (`account_id`) REFERENCES `accounts` (`account_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `hub_device` FOREIGN KEY (`hub_id`) REFERENCES `hubs` (`hub_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

CREATE TABLE `sensor_values` (
  `hub_id` int(11) NOT NULL,
  `device_id` varchar(128) COLLATE utf8_bin NOT NULL,
  `sensor_index` int(11) NOT NULL,
  `value` varchar(10) COLLATE utf8_bin NOT NULL,
  `timestamp` datetime NOT NULL,
  PRIMARY KEY (`hub_id`,`device_id`,`sensor_index`),
  CONSTRAINT `device_sensor_value` FOREIGN KEY (`hub_id`, `device_id`) REFERENCES `devices` (`hub_id`, `device_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

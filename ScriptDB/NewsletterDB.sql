DROP TABLE IF EXISTS `topics`;

CREATE TABLE
  `topics` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(255) NOT NULL,
    `description` text,
    `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
    `is_active` tinyint (1) DEFAULT '0',
    PRIMARY KEY (`id`),
    UNIQUE KEY `name` (`name`)
  ) ENGINE = InnoDB AUTO_INCREMENT = 11 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

LOCK TABLES `topics` WRITE;

INSERT INTO
  `topics`
VALUES
  (
    4,
    'Thời sự',
    'Tin tức thời sự trong nước và quốc tế',
    '2025-04-06 00:14:15',
    1
  ),
  (
    5,
    'Kinh tế',
    'Tin tức về kinh tế, thị trường và doanh nghiệp',
    '2025-04-06 00:14:15',
    1
  ),
  (
    6,
    'Sức khỏe',
    'Tin tức, kiến thức về sức khỏe và y tế',
    '2025-04-06 00:14:15',
    1
  ),
  (
    7,
    'Giáo dục',
    'Tin tức và thông tin về giáo dục, đào tạo',
    '2025-04-06 00:14:15',
    1
  ),
  (
    8,
    'Công nghệ',
    'Tin tức về công nghệ, khoa học và đổi mới',
    '2025-04-06 00:14:15',
    1
  ),
  (
    9,
    'Thể thao',
    'Tin tức về các môn thể thao trong nước và quốc tế',
    '2025-04-06 00:14:15',
    1
  );

UNLOCK TABLES;

DROP TABLE IF EXISTS `subscriptions`;

CREATE TABLE
  `subscriptions` (
    `id` int NOT NULL AUTO_INCREMENT,
    `user_id` int NOT NULL,
    `newsletter_id` int NOT NULL,
    `frequency` enum ('daily', 'weekly') DEFAULT 'daily',
    `last_sent_at` datetime DEFAULT NULL,
    `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `user_id` (`user_id`, `newsletter_id`),
    KEY `newsletter_id` (`newsletter_id`),
    CONSTRAINT `subscriptions_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
    CONSTRAINT `subscriptions_ibfk_2` FOREIGN KEY (`newsletter_id`) REFERENCES `newsletters` (`id`) ON DELETE CASCADE
  ) ENGINE = InnoDB AUTO_INCREMENT = 13 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

LOCK TABLES `subscriptions` WRITE;

INSERT INTO
  `subscriptions`
VALUES
  (7, 1, 1, 'daily', NULL, '2025-04-06 00:18:18'),
  (8, 1, 3, 'weekly', NULL, '2025-04-06 00:18:18'),
  (9, 1, 2, 'daily', NULL, '2025-04-06 00:18:18'),
  (10, 2, 5, 'weekly', NULL, '2025-04-06 00:18:18'),
  (11, 2, 3, 'daily', NULL, '2025-04-06 00:18:18'),
  (12, 3, 6, 'weekly', NULL, '2025-04-06 00:18:18');

UNLOCK TABLES;

DROP TABLE IF EXISTS `email_logs`;

CREATE TABLE
  `email_logs` (
    `id` int NOT NULL AUTO_INCREMENT,
    `user_id` int NOT NULL,
    `newsletter_id` int NOT NULL,
    `sent_at` datetime DEFAULT CURRENT_TIMESTAMP,
    `subject` varchar(255) DEFAULT NULL,
    `status` enum ('success', 'failed') DEFAULT 'success',
    PRIMARY KEY (`id`),
    KEY `user_id` (`user_id`),
    KEY `newsletter_id` (`newsletter_id`),
    CONSTRAINT `email_logs_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
    CONSTRAINT `email_logs_ibfk_2` FOREIGN KEY (`newsletter_id`) REFERENCES `newsletters` (`id`) ON DELETE CASCADE
  ) ENGINE = InnoDB AUTO_INCREMENT = 5 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

LOCK TABLES `email_logs` WRITE;

INSERT INTO
  `email_logs`
VALUES
  (
    1,
    2,
    1,
    '2025-04-05 00:20:15',
    'Bản tin thời sự VnExpress ngày 05/04/2025',
    'success'
  ),
  (
    2,
    2,
    3,
    '2025-03-30 00:20:15',
    'Bản tin sức khỏe VnExpress tuần qua',
    'success'
  ),
  (
    3,
    3,
    2,
    '2025-04-05 00:20:15',
    'Bản tin kinh doanh VnExpress ngày 05/04/2025',
    'success'
  ),
  (
    4,
    3,
    5,
    '2025-03-30 00:20:15',
    'Bản tin công nghệ VnExpress tuần qua',
    'failed'
  );

UNLOCK TABLES;

DROP TABLE IF EXISTS `newsletters`;

CREATE TABLE
  `newsletters` (
    `id` int NOT NULL AUTO_INCREMENT,
    `topic_id` int NOT NULL,
    `name` varchar(255) NOT NULL,
    `description` text,
    `rss_url` varchar(512) NOT NULL,
    `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    KEY `topic_id` (`topic_id`),
    CONSTRAINT `newsletters_ibfk_1` FOREIGN KEY (`topic_id`) REFERENCES `topics` (`id`) ON DELETE CASCADE
  ) ENGINE = InnoDB AUTO_INCREMENT = 7 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

LOCK TABLES `newsletters` WRITE;

INSERT INTO
  `newsletters`
VALUES
  (
    1,
    4,
    'VnExpress Thời sự',
    'Tin tức thời sự nóng hổi từ VnExpress',
    'https://vnexpress.net/rss/thoi-su.rss',
    '2025-04-06 00:17:03'
  ),
  (
    2,
    5,
    'VnExpress Kinh doanh',
    'Tin tức kinh tế, tài chính từ VnExpress',
    'https://vnexpress.net/rss/kinh-doanh.rss',
    '2025-04-06 00:17:03'
  ),
  (
    3,
    6,
    'VnExpress Sức khỏe',
    'Thông tin y tế, sức khỏe từ VnExpress',
    'https://vnexpress.net/rss/suc-khoe.rss',
    '2025-04-06 00:17:03'
  ),
  (
    4,
    7,
    'VnExpress Giáo dục',
    'Tin tức giáo dục, đào tạo từ VnExpress',
    'https://vnexpress.net/rss/giao-duc.rss',
    '2025-04-06 00:17:03'
  ),
  (
    5,
    8,
    'VnExpress Số hóa',
    'Tin tức công nghệ từ VnExpress',
    'https://vnexpress.net/rss/so-hoa.rss',
    '2025-04-06 00:17:03'
  ),
  (
    6,
    9,
    'VnExpress Thể thao',
    'Tin thể thao từ VnExpress',
    'https://vnexpress.net/rss/the-thao.rss',
    '2025-04-06 00:17:03'
  );

UNLOCK TABLES;

DROP TABLE IF EXISTS `users`;

CREATE TABLE
  `users` (
    `id` int NOT NULL AUTO_INCREMENT,
    `email` varchar(255) NOT NULL,
    `name` varchar(255) DEFAULT NULL,
    `pass_word` varchar(255) NOT NULL,
    `role_id` int NOT NULL,
    `is_email_confirmed` tinyint (1) NOT NULL DEFAULT '0',
    `preferred_time` time DEFAULT '08:00:00',
    `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    UNIQUE KEY `email` (`email`)
  ) ENGINE = InnoDB AUTO_INCREMENT = 8 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

LOCK TABLES `users` WRITE;

INSERT INTO
  `users`
VALUES
  (
    1,
    'thocodeanhquan@gmail.com',
    'Trần Anh Quân',
    '$2a$11$YKjECsjm.lLiP6BrF.2tJ.mnxQIaXT.ZebwLUp7GT1vH20nAdZ0y6',
    0,
    1,
    '08:00:00',
    '2025-04-05 19:10:42'
  ),
  (
    2,
    'ban@gmail.com',
    'Ban',
    '$2a$11$YKjECsjm.lLiP6BrF.2tJ.mnxQIaXT.ZebwLUp7GT1vH20nAdZ0y6',
    2,
    1,
    '08:00:00',
    '2025-04-05 19:10:42'
  ),
  (
    3,
    'quan@gmail.com',
    'Quân',
    '$2a$11$YKjECsjm.lLiP6BrF.2tJ.mnxQIaXT.ZebwLUp7GT1vH20nAdZ0y6',
    1,
    1,
    '08:00:00',
    '2025-04-05 19:10:42'
  ),
  (
    4,
    'cdat@gmail.com',
    'Công Đạt',
    '111111',
    1,
    0,
    '12:22:00',
    '2025-04-06 23:23:43'
  ),
  (
    7,
    'tulan246135@gmail.com',
    'Tú Lan',
    '$2a$11$E68/Yw5j.T5ejlFqM/uYpOthoBtngN6GY0kuEk6ojn4U/LY0aOIrK',
    0,
    1,
    '08:00:00',
    '2025-04-07 14:40:03'
  );

UNLOCK TABLES;

DROP TABLE IF EXISTS `articles`;

CREATE TABLE
  `articles` (
    `id` int NOT NULL AUTO_INCREMENT,
    `newsletter_id` int NOT NULL,
    `title` varchar(500) DEFAULT NULL,
    `link` text,
    `published_at` datetime DEFAULT NULL,
    `summary` text,
    `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
    `ImageUrl` text CHARACTER
    SET
      utf8mb4 COLLATE utf8mb4_0900_ai_ci,
      PRIMARY KEY (`id`),
      KEY `newsletter_id` (`newsletter_id`),
      CONSTRAINT `articles_ibfk_1` FOREIGN KEY (`newsletter_id`) REFERENCES `newsletters` (`id`) ON DELETE CASCADE
  ) ENGINE = InnoDB AUTO_INCREMENT = 1039 DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

UNLOCK TABLES;
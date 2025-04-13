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
        7,
        'tulan246135@gmail.com',
        'Tú Lan',
        '$2a$11$E68/Yw5j.T5ejlFqM/uYpOthoBtngN6GY0kuEk6ojn4U/LY0aOIrK',
        0,
        1,
        '08:00:00',
        '2025-04-07 14:40:03'
    ),
    (
        10,
        'oyy321305@gmail.com',
        'Phạm Công Đạt',
        '$2a$11$/Tr9Sozd76w7CFmma2H5rem4W/Fqa3i5U0rLOMtPcff/dYhbEK6xC',
        1,
        1,
        '04:56:00',
        '2025-04-10 03:56:32'
    ),
    (
        11,
        'test@gmail.com',
        'Tester',
        '$2a$11$0YhJTD7k/9e2EfltuwP7j.9cnmBhEiPAb6kXz1q2MRjH3JKgSXh3.',
        1,
        0,
        '08:00:00',
        '2025-04-10 15:34:00'
    ),
    (
        13,
        'quantitan2004@gmail.com',
        'titan ngu ngok',
        '$2a$11$NHGafeSVDPzHJ8ufUt6KVOd6dTnSnCOCryjuFn1xim6f0ycDwVTX.',
        1,
        1,
        '08:00:00',
        '2025-04-10 16:11:08'
    ),
    (
        15,
        'demo@gmail.com',
        'Trần Anh Quân',
        '$2a$11$MbR7M2OV.jq65LaDBGGNVOwdozP0aGhziZ7ABkOIbdLvpmOQO35/2',
        1,
        0,
        '08:00:00',
        '2025-04-10 16:35:45'
    ),
    (
        17,
        'okemen@gmail.com',
        'Anh Em Chúng Ta',
        '$2a$11$U1q4jX/4xz8XkAVjaM3fBO5N4YrYnHFdXAcRdgcVJbOoCYQvBv/c.',
        1,
        0,
        '08:00:00',
        '2025-04-11 01:55:46'
    );

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
    );

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
        'Thể thao 247',
        'Tin tức về các môn thể thao trong nước và quốc tế',
        '2025-04-06 00:14:15',
        0
    ),
    (44, '12', '12', '2025-04-11 09:04:36', 1),
    (46, 'fdfdfd', '12', '2025-04-11 09:04:54', 1),
    (
        51,
        'fdfdfdfdf',
        'fdfdfd',
        '2025-04-11 10:51:23',
        1
    );

INSERT INTO
    `newsletters`
VALUES
    (
        1,
        4,
        'Thời sự',
        'Tin tức thời sự nóng hổi từ VnExpress',
        'https://vnexpress.net/rss/thoi-su.rss',
        '2025-04-06 00:17:03'
    ),
    (
        2,
        5,
        'Kinh doanh',
        'Tin tức kinh tế, tài chính từ VnExpress',
        'https://vnexpress.net/rss/kinh-doanh.rss',
        '2025-04-06 00:17:03'
    ),
    (
        3,
        6,
        'Sức khỏe',
        'Thông tin y tế, sức khỏe từ VnExpress',
        'https://vnexpress.net/rss/suc-khoe.rss',
        '2025-04-06 00:17:03'
    ),
    (
        4,
        7,
        'Giáo dục',
        'Tin tức giáo dục, đào tạo từ VnExpress',
        'https://vnexpress.net/rss/giao-duc.rss',
        '2025-04-06 00:17:03'
    ),
    (
        5,
        8,
        'Số hóa',
        'Tin tức công nghệ từ VnExpress',
        'https://vnexpress.net/rss/so-hoa.rss',
        '2025-04-06 00:17:03'
    ),
    (
        6,
        9,
        'Thể thao',
        'Tin thể thao từ VnExpress',
        'https://vnexpress.net/rss/the-thao.rss',
        '2025-04-06 00:17:03'
    );

INSERT INTO
    `subscriptions`
VALUES
    (7, 1, 1, 'weekly', NULL, NULL),
    (8, 1, 3, 'weekly', NULL, '2025-04-06 00:18:18'),
    (9, 1, 2, 'daily', NULL, '2025-04-06 00:18:18'),
    (10, 2, 5, 'weekly', NULL, '2025-04-06 00:18:18'),
    (11, 2, 3, 'daily', NULL, '2025-04-06 00:18:18'),
    (16, 2, 1, 'daily', NULL, '2025-04-09 16:49:07'),
    (20, 7, 3, 'daily', NULL, NULL),
    (29, 10, 6, 'daily', NULL, '2025-04-10 14:13:34');
INSERT INTO Articles (Title, Content) VALUES 
('Giới thiệu về SQL', 'SQL là ngôn ngữ tiêu chuẩn để quản lý dữ liệu trong hệ thống quản lý cơ sở dữ liệu quan hệ.'),
('Các kỹ thuật SQL nâng cao', 'Tìm hiểu về các kỹ thuật SQL nâng cao như hàm cửa sổ và biểu thức bảng chung.');

INSERT INTO Services (ServiceName) VALUES 
('Tin tức công nghệ hàng tuần'),
('Mẹo sức khỏe hàng tháng');

INSERT INTO Topics (TopicName) VALUES 
('Công nghệ'),
('Sức khỏe'),
('Giáo dục');

INSERT INTO Users (FullName, Email, PasswordHash) VALUES 
('Nguyễn Văn A', 'nguyenvana@example.com', 'hashed_password_1'),
('Trần Thị B', 'tranthib@example.com', 'hashed_password_2');

INSERT INTO ArticleTopics (ArticleId, TopicId) VALUES 
(1, 1),  -- Bài viết 1 thuộc chủ đề Công nghệ
(2, 1);  -- Bài viết 2 thuộc chủ đề Công nghệ

INSERT INTO Newsletters (ServiceId, Title, Content) VALUES 
(1, 'Tin tức công nghệ tuần này', 'Cập nhật những tin tức mới nhất về công nghệ trong tuần.'),
(2, 'Mẹo sức khỏe tháng này', 'Những mẹo sức khỏe hữu ích cho tháng này.');


INSERT INTO SentMails (UserId, NewsletterId) VALUES 
(1, 1),  -- Người dùng 1 nhận bản tin 1
(2, 2);  -- Người dùng 2 nhận bản tin 2


INSERT INTO Subscriptions (UserId, ServiceId) VALUES 
(1, 1),  -- Người dùng 1 đăng ký dịch vụ 1
(2, 2);  -- Người dùng 2 đăng ký dịch vụ 2

INSERT INTO UserTopicSubscriptions (UserId, TopicId) VALUES 
(1, 1),  -- Người dùng 1 đăng ký chủ đề Công nghệ
(2, 2);  -- Người dùng 2 đăng ký chủ đề Sức khỏe



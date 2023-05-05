CREATE DATABASE BookStoreSchema
GO 
USE BookStoreSchema
GO

CREATE TABLE TYPEOFBOOK
(
    tobID CHAR(10),
    tobName NVARCHAR(100)

	PRIMARY KEY(tobID)
)

CREATE TABLE BOOK
(
    bookID CHAR(5),
    bookName NVARCHAR(100),
    bookAuthor NVARCHAR(100),
    tobID CHAR(10),
    bookPrice MONEY DEFAULT 0,--giá gốc
    bookQuantity int,
    bookPublishedYear int

	PRIMARY KEY(bookID)
)

CREATE TABLE PROMOTION
(
    promoID CHAR(15),
    promoName NVARCHAR(100),
    promoDiscount FLOAT(2),
    promoDescription NVARCHAR(500),
    promoStartTime DATE,
    promoEndTime DATE,
	promoStatus BIT,

	PRIMARY KEY(promoID)
)

CREATE TABLE PROMOTIONDETAIL
(
    promoID CHAR(15),
    tobID CHAR(10)

	PRIMARY KEY(promoID, tobID)
)

CREATE TABLE ORDERS
(
    ordersID CHAR(10),
    cusPhoneNumber CHAR(10),
    accUsername VARCHAR(25),
	ordersPrice MONEY DEFAULT 0,
    ordersTime DATETIME

	PRIMARY KEY(ordersID)
)

CREATE TABLE ORDERSDETAIL
(
    ordersID CHAR(10),
    bookID CHAR(5),
    odCurrentPrice MONEY DEFAULT 0,
    odQuantity INT,
    odDiscountedPrice MONEY DEFAULT 0,
    odTempPrice MONEY DEFAULT 0

	PRIMARY KEY(ordersID, bookID)
)

CREATE TABLE CUSTOMER
(
    cusPhoneNumber CHAR(10),
    cusName NVARCHAR(100)

	PRIMARY KEY(cusPhoneNumber)
)

CREATE TABLE ACCOUNT
(
    accUsername VARCHAR(25),
    accPassword VARCHAR(250)

	PRIMARY KEY(accUsername)
)

ALTER TABLE BOOK ADD 
	CONSTRAINT FK_BOOK_TYPEOFBOOK FOREIGN KEY (tobID) REFERENCES TYPEOFBOOK(tobID)
GO

ALTER TABLE PROMOTIONDETAIL ADD 
	CONSTRAINT FK_PROMOTIONDETAIL_TYPEOFBOOK FOREIGN KEY (tobID) REFERENCES TYPEOFBOOK(tobID)
GO
ALTER TABLE PROMOTIONDETAIL ADD 
	CONSTRAINT FK_PROMOTIONDETAIL_PROMOTION FOREIGN KEY (promoID) REFERENCES PROMOTION(promoID)
GO

ALTER TABLE ORDERS ADD 
	CONSTRAINT FK_ORDERS_CUSTOMER FOREIGN KEY (cusPhoneNumber) REFERENCES CUSTOMER(cusPhoneNumber)
GO
ALTER TABLE ORDERS ADD 
	CONSTRAINT FK_ORDERS_ACCOUNT FOREIGN KEY (accUsername) REFERENCES ACCOUNT(accUsername)
GO

ALTER TABLE ORDERSDETAIL ADD 
	CONSTRAINT FK_ORDERSDETAIL_BOOK FOREIGN KEY (bookID) REFERENCES BOOK(bookID)
GO
ALTER TABLE ORDERSDETAIL ADD 
	CONSTRAINT FK_ORDERSDETAIL_ORDERS FOREIGN KEY (ordersID) REFERENCES ORDERS(ordersID)
GO

------------------------------------------------------------------------------------------------
GO
INSERT INTO ACCOUNT(accUsername, accPassword)
    VALUES  ('admin', '$2a$12$pcXbwYin8TXBt0kE1iVCyOFCp4XC7WMdONsNm6AGvp2JEeEU3pdpK'),
            ('quanly', '$2a$12$ZrAaz1kvCfV33DfB.0bSneB5aGUfj7N0fu57kAfBNqakDqIRc04lC'),
            ('nhanvien', '$2a$12$ZrAaz1kvCfV33DfB.0bSneB5aGUfj7N0fu57kAfBNqakDqIRc04lC')

GO
INSERT INTO CUSTOMER(cusPhoneNumber, cusName)
    VALUES  ('0912001222', N'Trương Mạnh'),
            ('0912712723', N'Khải My'),
            ('0798181991', N'Trần Lê Hồng Nhi'),
            ('0866901801', N'Trần Thái'),
            ('0783450303', N'Trần Văn Đức'),
            ('0328373723', N'Trần Thanh Như'),
            ('0911111222', N'Nguyễn Minh Bảo'),
            ('0769696982', N'Lê Hoàng Bảo Nam'),
            ('0369634889', N'Hoàng Gia Bảo'),
            ('0866923401', N'Nguyễn Ngọc Quỳnh'),
            ('0375812786', N'Phạm Lê Thùy Dung')

GO
INSERT INTO TYPEOFBOOK(tobID, tobName)
    VALUES  ('TB001', N'Văn học Nước ngoài'),
            ('TB002', N'Sách Thiếu nhi'),
            ('TB003', N'Sách Kỹ năng sống'),
            ('TB004', N'Truyện ngắn'),
            ('TB005', N'Tiểu thuyết'),
            ('TB006', N'Sách Nghệ thuật sống đẹp'),
            ('TB007', N'Sách Tôn giáo - Tâm linh')

GO
INSERT INTO BOOK(bookID, tobID, bookName, bookAuthor, bookPrice, bookPublishedYear, bookQuantity)
    VALUES  ('A0001', 'TB001', N'Chiến Thắng Con Quỷ Trong Bạn', N'Napoleon Hill', 90000, 2011, 12),
            ('A0002', 'TB003', N'Nghệ Thuật Tư Duy Phản Biện', N'Albert Rutherford',90000, 2021, 11),
            ('A0003', 'TB005', N'Không gia đình (Tái Bản 2019)', N'Hector Malot', 90000, 2021, 23),
            ('A0004', 'TB001', N'Nghìn Lẻ Một Đêm (Bìa cứng)', N'Antoine Galland', 500000, 2021, 33),
            ('A0005', 'TB005', N'Rừng Na Uy (Tái Bản Lần 4)', N'Haruki Murakami', 100000, 2018, 21),
            ('A0006', 'TB001', N'Ông Già Và Biển Cả', N'Ernest Hemingway', 50400, 2018, 25),
            ('A0007', 'TB005', N'Giết Con Chim Nhại (Bìa cứng)', N'Harper Lee', 90000, 2021, 34),
            ('A0008', 'TB007', N'Nhà Giả Kim (Tái Bản 2020)', N'Paulo Coelho', 40200, 2020, 4),
            ('A0009', 'TB001', N'Ba Người Lính Ngự Lâm', N'Alexandre Dumas', 100000, 2019, 28),
            ('A0010', 'TB005', N'Không Gia Đình (Phiên Bản Mới)', N'Hector Malot', 100000, 2021, 32),
            ('A0011', 'TB002', N'Hoàng Tử Bé (Tái Bản 2019)', N'Antoine De Saint-Exupéry', 60000, 2019, 55),
            ('A0012', 'TB002', N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'Nguyễn Nhật Ánh', 90500, 2018, 56),
            ('A0013', 'TB006', N'Nói Chuyện Là Bản Năng, Giữ Miệng Là Tu Dưỡng, Im Lặng Là Trí Tuệ', N'Trương Tiếu Hằng', 90000, 2019, 52),
            ('A0014', 'TB006', N'Thông Điệp Của Nước', N'Masaru Emoto', 50000, 2020, 21),
            ('A0015', 'TB003', N'Thông Điệp Từ Những Biểu Cảm Và Ngôn Ngữ Cơ Thể', N'Kỷ Vũ', 40000, 2019, 31),
            ('A0016', 'TB003', N'Người Nhạy Cảm Trong Thế Giới Vô Cảm', N'Ilse Sand', 40000, 2021, 35),
            ('A0017', 'TB003', N'Gửi bạn, người có trái tim vô cùng nhạy cảm', N'Jeon Hong Jin', 40640, 2021, 36),
            ('A0018', 'TB003', N'Xé vài trang thanh xuân, đổi lấy một bản thân nỗ lực', N'Văn Cát Nhi', 40000, 2021, 42),
            ('A0019', 'TB006', N'Omoiyari - Nghệ Thuật Đối Nhân Xử Thế Của Người Nhật', N'Erin Niimi Longhurst', 90000, 2020, 51),
            ('A0020', 'TB005', N'Nơi em quay về có tôi đứng đợi', N'Ichikawa Takuji', 50000, 2020, 11),
            ('A0021', 'TB005', N'Em sẽ đến cùng cơn mưa', N'Ichikawa Takuji', 50000, 2020, 11),
            ('A0022', 'TB005', N'Tấm ảnh tình yêu, và một câu chuyện khác', N'Ichikawa Takuji', 50000, 2020, 11),
            ('A0023', 'TB005', N'Nếu gặp người ấy cho tôi gửi lời chào', N'Ichikawa Takuji', 50600, 2020, 11),
            ('A0024', 'TB005', N'Tôi vẫn nghe tiếng em thầm gọi', N'Ichikawa Takuji', 50000, 2020, 11),
            ('A0025', 'TB005', N'Thế giới kết thúc dịu dàng đến thế', N'Ichikawa Takuji', 90000, 2020, 11),
            ('A0026', 'TB005', N'Bàn tay cho em', N'Ichikawa Takuji', 50000, 2020, 11),
            ('A0027', 'TB005', N'MM, chuyện về cô gái ấy', N'Ichikawa Takuji', 90000, 2020, 11),
            ('A0028', 'TB005', N'Anh Sẽ Đi Tìm Em Trên Chiếc Xe Đạp Hỏng', N'Ichikawa Takuji', 90000, 2020, 11),
            ('A0029', 'TB006', N'Cân Bằng Cảm Xúc, Cả Lúc Bão Giông', N'Richard Nicholls', 90000, 2020, 19),
            ('A0030', 'TB006', N'Quên Một Người Là Chuyện Của Thời Gian', N'A Tòn', 50000, 2020, 11),
            ('A0031', 'TB007', N'Dấu Chân Trên Cát', N'Mika Waltari', 100000, 2020, 11),
            ('A0032', 'TB007', N'Bên Rặng Tuyết Sơn', N'Swami Amar Jyoti', 88000, 2020, 16),
            ('A0033', 'TB003', N'Tự Thương Mình Sau Những Năm Tháng Thương Người', N'Trí', 78000, 2020, 28),
            ('A0034', 'TB005', N'Một Lít Nước Mắt', N'Kito Aya', 80000, 2020, 34),
            ('A0035', 'TB003', N'Như Bây Giờ Vẫn Ổn', N'Đại sư Pomnyun Sunim', 106000, 2020, 53),
            ('A0036', 'TB003', N'Chuyến Tàu Một Chiều Không Trở Lại', N'Kiên Trần', 120000, 2020, 47),
            ('A0037', 'TB003', N'Nóng Giận Là Bản Năng , Tĩnh Lặng Là Bản Lĩnh', N'Tống Mặc', 89000, 2020, 47),
            ('A0038', 'TB007', N'Tâm Buông Bỏ, Đời Bình An - Bí Kíp Sống Hạnh Phúc Của Người Nhật', N'Natori Hougen', 78000, 2020, 45),
            ('A0039', 'TB007', N'Từ Bi - Trên Cả Trắc Ẩn Và Yêu Thương', N'Osho', 183000, 2020, 44)

GO
INSERT INTO PROMOTION(promoID, promoName,promoDiscount, promoDescription, promoStartTime, promoEndTime, promoStatus)
    VALUES  ('KM-GIAM15%', N'KM Giảm 15%', 0.15, N'Khuyến mãi Giảm 15% tổng giá trị hóa đơn', '2022/04/11', '2022/05/07',1),
			('KM-GIAM50K', N'KM Giảm 50K', 0.15, N'Khuyến mãi Giảm 15% tổng giá trị hóa đơn tối đa 50K', '2022/04/08', '2022/04/18',1),
            ('KM-GIAM30%', N'KM Giảm 30%', 0.3, N'Khuyến mãi Giảm 30% cho những sách thuộc danh mục Tiểu thuyết, Truyện ngắn', '2022/02/08', '2022/03/08',1),
            ('KM-DACBIET', N'KM Đặc biệt', 0.75, N'Khuyến mãi Giảm 75% tổng giá trị hóa đơn tối đa 500K', '2021/12/31', '2021/12/27',1),
            ('KM-GIAM100K', N'KM Giảm 100K', 0.3, N'Khuyến mãi Giảm 30% tối đa 100K cho những sách thuộc danh mục Sách Nghệ thuật sống đẹp, Sách Kỹ năng sống', '2021/08/08', '2021/09/09',1),
            ('KM-GIAM70%', N'KM Giảm 70%', 0.7, N'Khuyến mãi Giảm 70% cho những sách thuộc danh mục Sách Thiếu nhi', '2021/06/06', '2021/05/27',1),
            ('KM-GIAM50%', N'KM Giảm 50%', 0.5, N'Khuyến mãi Giảm 50% cho những sách thuộc danh mục Sách Tôn giáo - Tâm linh', '2021/04/08', '2021/04/18',1)

GO
INSERT INTO PROMOTIONDETAIL(promoID, tobID)
    VALUES  ('KM-GIAM50K','TB001'),
            ('KM-GIAM50K','TB002'),
            ('KM-GIAM50K','TB003'),
            ('KM-GIAM50K','TB004'),
            ('KM-GIAM50K','TB005'),
            ('KM-GIAM50K','TB006'),
            ('KM-GIAM50K','TB007'),
            ('KM-GIAM30%','TB004'),
            ('KM-GIAM30%','TB005'),
            ('KM-DACBIET','TB001'),
            ('KM-DACBIET','TB002'),
            ('KM-DACBIET','TB003'),
            ('KM-DACBIET','TB004'),
            ('KM-DACBIET','TB005'),
            ('KM-DACBIET','TB006'),
            ('KM-DACBIET','TB007'),
            ('KM-GIAM100K','TB003'),
            ('KM-GIAM100K','TB006'),
            ('KM-GIAM70%','TB002'),
            ('KM-GIAM50%','TB007')

GO
INSERT INTO ORDERS(ordersID, ordersTime)
    VALUES  ('220421DFS', '2023-05-01 07:21:35'),
            ('220421FDV', '2023-05-01 10:20:34'),
            ('220421FDS', '2023-05-01 11:19:33'),
            ('220420RTR', '2023-05-01 12:18:32'),
            ('220420KTH', '2023-05-01 13:17:31'),
            ('220420TJJ', '2023-05-01 15:16:30'),
            ('220419LIE', '2023-05-01 17:15:29'),
            ('220418FGT', '2023-05-01 18:14:28'),
            ('220417WWW', '2023-05-02 19:13:27'),
            ('220416RGH', '2023-05-02 20:12:26'),
            ('220416YUY', '2023-05-02 11:11:11'),
			('220415LEH', '2023-04-15 07:21:35'),
			('220415PQK', '2023-05-02 07:21:35'),
            ('220414GKR', '2023-05-02 10:20:34'),
            ('220414KKH', '2023-05-02 11:19:33'),
            ('220414JEK', '2023-04-14 12:18:32'),
            ('220414RIJ', '2023-04-14 13:17:31'),
            ('220413WKR', '2023-04-13 15:16:30'),
            ('220413KAS', '2023-04-13 17:15:29'),
            ('220412EWQ', '2023-04-12 18:14:28'),
            ('220412DDD', '2023-04-12 19:13:27'),
            ('220412SDF', '2023-04-12 20:12:26'),
            ('220411KAS', '2023-04-11 11:11:11'),
			('220411KSD', '2023-04-11 07:21:35'),
            ('220411MNI', '2023-04-11 10:20:34'),
            ('220411SLL', '2023-04-11 11:19:33'),
            ('220411QWE', '2023-04-11 12:18:32'),
            ('220411IBM', '2023-04-11 13:17:31'),
            ('220411EGN', '2023-04-11 15:16:30'),
            ('220411IGH', '2023-04-11 17:15:29'),
            ('220411LKH', '2023-04-11 18:14:28'),
            ('220409JQK', '2023-04-09 18:14:28'),
            ('220409MNI', '2023-04-09 18:14:28'),
            ('220408SLL', '2023-04-08 18:14:28'),
            ('220408QWE', '2023-04-08 18:14:28'),
            ('220408IBM', '2023-04-08 18:14:28'),
            ('220407EGN', '2023-04-07 18:14:28'),
            ('220406IGH', '2023-04-06 18:14:28'),
            ('220405LKH', '2023-04-05 18:14:28'),
            ('220404HYY', '2023-04-04 19:13:27'),
            ('220404UYU', '2023-04-04 20:12:26'),
            ('220404UYD', '2023-04-04 11:11:11')

GO
INSERT INTO ORDERSDETAIL(ordersID, bookID, odCurrentPrice, odQuantity, odTempPrice)
    VALUES  ('220421DFS', 'A0011', 75000 , 1, 75000 ),
            ('220421DFS', 'A0029', 105000, 1, 105000),
            ('220421DFS', 'A0015', 94000 , 1, 94000 ),
            ('220421FDV', 'A0015', 94000 , 1, 94000 ),
            ('220421FDV', 'A0013', 129000, 1, 129000),
            ('220421FDV', 'A0012', 113500, 1, 113500),
            ('220421FDS', 'A0036', 120000, 1, 120000),
            ('220421FDS', 'A0013', 129000, 1, 129000),
            ('220421FDS', 'A0019', 142000, 1, 142000),
            ('220421FDS', 'A0015', 94000 , 1, 94000 ),
            ('220420RTR', 'A0011', 75000 , 1, 75000 ),
            ('220420RTR', 'A0021', 90000 , 1, 90000 ),
            ('220420RTR', 'A0031', 148000, 1, 148000),
            ('220420RTR', 'A0014', 89000 , 1, 89000 ),
            ('220418FGT', 'A0015', 94000 , 1, 94000 ),
            ('220420KTH', 'A0021', 90000 , 1, 90000 ),
            ('220420KTH', 'A0016', 85000 , 1, 85000 ),
            ('220420KTH', 'A0026', 90000 , 1, 90000 ),
            ('220420KTH', 'A0017', 89640 , 1, 89640 ),
            ('220420KTH', 'A0015', 94000 , 1, 94000 ),
            ('220420RTR', 'A0001', 115000, 1, 115000),
            ('220420TJJ', 'A0002', 129000, 1, 129000),
            ('220420TJJ', 'A0001', 115000, 1, 115000),
			('220420TJJ', 'A0003', 104000, 1, 104000),
            ('220420TJJ', 'A0018', 96000 , 1, 96000 ),
            ('220420TJJ', 'A0013', 129000, 1, 129000),
            ('220420TJJ', 'A0019', 142000, 1, 142000),
            ('220420TJJ', 'A0020', 80000 , 1, 80000 ),
            ('220412DDD', 'A0013', 129000, 1, 129000),
            ('220420TJJ', 'A0021', 90000 , 1, 90000 ),
            ('220419LIE', 'A0015', 94000 , 1, 94000 ),
			('220419LIE', 'A0022', 86000 , 1, 86000 ),
			('220419LIE', 'A0023', 98600 , 1, 98600 ),
            ('220419LIE', 'A0024', 62000 , 1, 62000 ),
            ('220419LIE', 'A0019', 142000, 1, 142000),
			('220419LIE', 'A0025', 120000, 1, 120000),
            ('220418FGT', 'A0026', 90000 , 1, 90000 ),
            ('220417WWW', 'A0027', 108000, 1, 108000),
            ('220417WWW', 'A0031', 148000, 1, 148000),
            ('220417WWW', 'A0028', 128000, 1, 128000),
            ('220416RGH', 'A0029', 105000, 1, 105000),
            ('220416RGH', 'A0015', 94000 , 1, 94000 ),
            ('220416RGH', 'A0030', 89000 , 1, 89000 ),
            ('220416YUY', 'A0031', 148000, 1, 148000),
            ('220416YUY', 'A0032', 88000 , 1, 88000 ),
            ('220416YUY', 'A0037', 183000, 1, 183000),
            ('220416YUY', 'A0002', 129000, 1, 129000),
            ('220415LEH', 'A0033', 78000 , 1, 78000 ),
			('220415LEH', 'A0034', 80000 , 1, 80000 ),
            ('220415LEH', 'A0005', 145000, 5, 145000),
            ('220415LEH', 'A0039', 183000, 1, 183000),
            ('220415LEH', 'A0035', 106000, 4, 106000),
            ('220411QWE', 'A0035', 106000, 1, 106000),
            ('220415LEH', 'A0036', 120000, 1, 120000),
            ('220415LEH', 'A0024', 62000 , 1, 62000 ),
            ('220411QWE', 'A0034', 80000 , 1, 80000 ),
            ('220415PQK', 'A0001', 115000, 1, 115000),
            ('220415PQK', 'A0011', 75000 , 1, 75000 ),
            ('220415PQK', 'A0021', 90000 , 1, 90000 ),
            ('220414GKR', 'A0038', 89000 , 1, 89000 ),
            ('220414GKR', 'A0039', 78000 , 1, 78000 ),
            ('220414KKH', 'A0037', 183000, 1, 183000),
            ('220412EWQ', 'A0002', 129000, 1, 129000),
            ('220414KKH', 'A0001', 115000, 1, 115000),
			('220414JEK', 'A0003', 104000, 1, 104000),
            ('220414JEK', 'A0026', 90000 , 1, 90000 ),
            ('220412DDD', 'A0029', 105000, 1, 105000),
            ('220414RIJ', 'A0005', 145000, 1, 145000),
            ('220414RIJ', 'A0009', 156000, 1, 156000),
            ('220414RIJ', 'A0015', 94000 , 1, 94000 ),
            ('220412SDF', 'A0025', 120000, 1, 120000),
            ('220413WKR', 'A0032', 88000 , 1, 88000 ),
            ('220413WKR', 'A0039', 183000, 2, 183000),
            ('220413WKR', 'A0001', 115000, 1, 115000),
            ('220413KAS', 'A0011', 75000 , 1, 75000 ),
            ('220413KAS', 'A0013', 129000, 1, 129000),
            ('220412EWQ', 'A0014', 89000 , 2, 89000 ),
            ('220412DDD', 'A0033', 78000 , 1, 78000 ),
            ('220412DDD', 'A0005', 145000, 5, 145000),
            ('220411LKH', 'A0039', 183000, 1, 183000),
			('220411LKH', 'A0013', 129000, 1, 129000),
            ('220411LKH', 'A0021', 90000 , 1, 90000 ),
            ('220411LKH', 'A0004', 990000, 1, 990000),
			('220411LKH', 'A0028', 128000, 1, 128000),
            ('220411KSD', 'A0029', 105000, 1, 105000),
            ('220411KAS', 'A0035', 106000, 4, 106000),
			('220411IGH', 'A0003', 104000, 1, 104000),
            ('220411IGH', 'A0018', 96000 , 1, 96000 ),
            ('220411IGH', 'A0013', 129000, 1, 129000),
            ('220411IGH', 'A0019', 142000, 1, 142000),
            ('220411SLL', 'A0020', 80000 , 1, 80000 ),
            ('220411QWE', 'A0013', 129000, 1, 129000),
            ('220409MNI', 'A0021', 90000 , 1, 90000 ),
            ('220409MNI', 'A0004', 990000, 1, 990000),
			('220409MNI', 'A0028', 128000, 1, 128000),
            ('220409MNI', 'A0029', 105000, 1, 105000),
            ('220409MNI', 'A0015', 94000 , 1, 94000 ),
            ('220404HYY', 'A0014', 89000 , 1, 89000 ),
            ('220404HYY', 'A0024', 62000 , 1, 62000 ),
            ('220404HYY', 'A0034', 80000 , 1, 80000 ),
            ('220404UYU', 'A0001', 115000, 1, 115000),
            ('220404UYU', 'A0011', 75000 , 1, 75000 ),
            ('220404UYU', 'A0021', 90000 , 1, 90000 ),
            ('220404UYU', 'A0031', 148000, 1, 148000),
            ('220404UYD', 'A0006', 70400 , 1, 70400 ),
            ('220404UYD', 'A0016', 85000 , 1, 85000 ),
            ('220404UYD', 'A0026', 90000 , 1, 90000 ),
            ('220404UYD', 'A0036', 120000, 1, 120000),
			('220405LKH', 'A0024', 62000 , 1, 62000 ),
            ('220406IGH', 'A0034', 80000 , 1, 80000 ),
            ('220407EGN', 'A0001', 115000, 1, 115000),
            ('220408IBM', 'A0011', 75000 , 1, 75000 ),
            ('220408QWE', 'A0021', 90000 , 1, 90000 ),
            ('220408SLL', 'A0031', 148000, 1, 148000),
            ('220409JQK', 'A0006', 70400 , 1, 70400 ),
            ('220411EGN', 'A0016', 85000 , 1, 85000 ),
            ('220411IBM', 'A0026', 90000 , 1, 90000 ),
            ('220411MNI', 'A0036', 120000, 1, 120000)

------------------------------------------------------------------------------------------------
GO
UPDATE ORDERS SET cusPhoneNumber = '0912001222', accUsername = 'quanly'	WHERE ordersID = '220421DFS'
UPDATE ORDERS SET cusPhoneNumber = '0912712723', accUsername = 'quanly'	WHERE ordersID = '220421FDV'
UPDATE ORDERS SET cusPhoneNumber = '0798181991', accUsername = 'quanly'	WHERE ordersID = '220421FDS'
UPDATE ORDERS SET cusPhoneNumber = '0866901801', accUsername = 'quanly'	WHERE ordersID = '220420RTR'
UPDATE ORDERS SET cusPhoneNumber = '0783450303', accUsername = 'quanly'	WHERE ordersID = '220420KTH'
UPDATE ORDERS SET cusPhoneNumber = '0328373723', accUsername = 'quanly'	WHERE ordersID = '220420TJJ'
UPDATE ORDERS SET cusPhoneNumber = '0911111222', accUsername = 'quanly'	WHERE ordersID = '220419LIE'
UPDATE ORDERS SET cusPhoneNumber = '0769696982', accUsername = 'quanly'	WHERE ordersID = '220418FGT'
UPDATE ORDERS SET cusPhoneNumber = '0369634889', accUsername = 'quanly'	WHERE ordersID = '220417WWW'
UPDATE ORDERS SET cusPhoneNumber = '0866923401', accUsername = 'quanly'	WHERE ordersID = '220416RGH'
UPDATE ORDERS SET cusPhoneNumber = '0375812786', accUsername = 'quanly'	WHERE ordersID = '220416YUY'
UPDATE ORDERS SET cusPhoneNumber = '0912001222', accUsername = 'quanly'	WHERE ordersID = '220415LEH'
UPDATE ORDERS SET cusPhoneNumber = '0912712723', accUsername = 'quanly'	WHERE ordersID = '220415PQK'
UPDATE ORDERS SET cusPhoneNumber = '0798181991', accUsername = 'admin'		WHERE ordersID = '220414GKR'
UPDATE ORDERS SET cusPhoneNumber = '0866901801', accUsername = 'admin'		WHERE ordersID = '220414KKH'
UPDATE ORDERS SET cusPhoneNumber = '0783450303', accUsername = 'admin'		WHERE ordersID = '220414JEK'
UPDATE ORDERS SET cusPhoneNumber = '0328373723', accUsername = 'admin'		WHERE ordersID = '220414RIJ'
UPDATE ORDERS SET cusPhoneNumber = '0911111222', accUsername = 'admin'		WHERE ordersID = '220413WKR'
UPDATE ORDERS SET cusPhoneNumber = '0769696982', accUsername = 'admin'		WHERE ordersID = '220413KAS'
UPDATE ORDERS SET cusPhoneNumber = '0369634889', accUsername = 'admin'		WHERE ordersID = '220412EWQ'
UPDATE ORDERS SET cusPhoneNumber = '0866923401', accUsername = 'admin'		WHERE ordersID = '220412DDD'
UPDATE ORDERS SET cusPhoneNumber = '0912001222', accUsername = 'admin'		WHERE ordersID = '220412SDF'
UPDATE ORDERS SET cusPhoneNumber = '0912712723', accUsername = 'admin'		WHERE ordersID = '220411KAS'
UPDATE ORDERS SET cusPhoneNumber = '0798181991', accUsername = 'admin'		WHERE ordersID = '220411KSD'
UPDATE ORDERS SET cusPhoneNumber = '0866901801', accUsername = 'nhanvien' WHERE ordersID = '220411MNI'
UPDATE ORDERS SET cusPhoneNumber = '0783450303', accUsername = 'nhanvien' WHERE ordersID = '220411SLL'
UPDATE ORDERS SET cusPhoneNumber = '0328373723', accUsername = 'nhanvien' WHERE ordersID = '220411QWE'
UPDATE ORDERS SET cusPhoneNumber = '0911111222', accUsername = 'nhanvien' WHERE ordersID = '220411IBM'
UPDATE ORDERS SET cusPhoneNumber = '0769696982', accUsername = 'nhanvien' WHERE ordersID = '220411EGN'
UPDATE ORDERS SET cusPhoneNumber = '0369634889', accUsername = 'nhanvien' WHERE ordersID = '220411IGH'
UPDATE ORDERS SET cusPhoneNumber = '0866923401', accUsername = 'nhanvien' WHERE ordersID = '220411LKH'
UPDATE ORDERS SET cusPhoneNumber = '0375812786', accUsername = 'nhanvien' WHERE ordersID = '220409JQK'
UPDATE ORDERS SET cusPhoneNumber = '0783450303', accUsername = 'nhanvien' WHERE ordersID = '220409MNI'
UPDATE ORDERS SET cusPhoneNumber = '0375812786', accUsername = 'nhanvien' WHERE ordersID = '220408SLL'
UPDATE ORDERS SET cusPhoneNumber = '0866901801', accUsername = 'nhanvien' WHERE ordersID = '220408QWE'
UPDATE ORDERS SET cusPhoneNumber = '0783450303', accUsername = 'nhanvien' WHERE ordersID = '220408IBM'
UPDATE ORDERS SET cusPhoneNumber = '0328373723', accUsername = 'nhanvien' WHERE ordersID = '220407EGN'
UPDATE ORDERS SET cusPhoneNumber = '0911111222', accUsername = 'nhanvien' WHERE ordersID = '220406IGH'
UPDATE ORDERS SET cusPhoneNumber = '0769696982', accUsername = 'nhanvien' WHERE ordersID = '220405LKH'
UPDATE ORDERS SET cusPhoneNumber = '0369634889', accUsername = 'nhanvien' WHERE ordersID = '220404HYY'
UPDATE ORDERS SET cusPhoneNumber = '0866923401', accUsername = 'nhanvien' WHERE ordersID = '220404UYU'
UPDATE ORDERS SET cusPhoneNumber = '0912001222', accUsername = 'nhanvien' WHERE ordersID = '220404UYD'

------------------------------------------------------------------------------------------------

GO
CREATE PROC CALCULATOR_TOTAL
    @ORDERID CHAR(10)
AS
BEGIN TRAN
	UPDATE ORDERSDETAIL
		SET odTempPrice = (odCurrentPrice - odDiscountedPrice)* odQuantity
		WHERE ordersID = @ORDERID
    UPDATE ORDERS
		SET ordersPrice = (SELECT SUM(odCurrentPrice) FROM ORDERSDETAIL WHERE ordersID = @ORDERID)
		WHERE ordersID = @ORDERID
COMMIT
GO

EXEC CALCULATOR_TOTAL'220421DFS'
EXEC CALCULATOR_TOTAL'220421FDV'
EXEC CALCULATOR_TOTAL'220421FDS'
EXEC CALCULATOR_TOTAL'220420RTR'
EXEC CALCULATOR_TOTAL'220420KTH'
EXEC CALCULATOR_TOTAL '220420TJJ'
EXEC CALCULATOR_TOTAL '220419LIE'
EXEC CALCULATOR_TOTAL '220418FGT'
EXEC CALCULATOR_TOTAL '220417WWW'
EXEC CALCULATOR_TOTAL '220416RGH'
EXEC CALCULATOR_TOTAL '220416YUY'
EXEC CALCULATOR_TOTAL '220415LEH'
EXEC CALCULATOR_TOTAL '220415PQK'
EXEC CALCULATOR_TOTAL '220414GKR'
EXEC CALCULATOR_TOTAL '220414KKH'
EXEC CALCULATOR_TOTAL '220414JEK'
EXEC CALCULATOR_TOTAL '220414RIJ'
EXEC CALCULATOR_TOTAL '220413WKR'
EXEC CALCULATOR_TOTAL '220413KAS'
EXEC CALCULATOR_TOTAL '220412EWQ'
EXEC CALCULATOR_TOTAL '220412DDD'
EXEC CALCULATOR_TOTAL '220412SDF'
EXEC CALCULATOR_TOTAL '220411KAS'
EXEC CALCULATOR_TOTAL '220411KSD'
EXEC CALCULATOR_TOTAL '220411MNI'
EXEC CALCULATOR_TOTAL '220411SLL'
EXEC CALCULATOR_TOTAL '220411QWE'
EXEC CALCULATOR_TOTAL '220411IBM'
EXEC CALCULATOR_TOTAL '220411EGN'
EXEC CALCULATOR_TOTAL '220411IGH'
EXEC CALCULATOR_TOTAL '220411LKH'
EXEC CALCULATOR_TOTAL '220409JQK'
EXEC CALCULATOR_TOTAL '220409MNI'
EXEC CALCULATOR_TOTAL '220408SLL'
EXEC CALCULATOR_TOTAL '220408QWE'
EXEC CALCULATOR_TOTAL '220408IBM'
EXEC CALCULATOR_TOTAL '220407EGN'
EXEC CALCULATOR_TOTAL '220406IGH'
EXEC CALCULATOR_TOTAL '220405LKH'
EXEC CALCULATOR_TOTAL '220404HYY'
EXEC CALCULATOR_TOTAL '220404UYU'
EXEC CALCULATOR_TOTAL '220404UYD'
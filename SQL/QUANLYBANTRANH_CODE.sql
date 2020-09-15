

CREATE DATABASE QuanLyBanTranh
ON PRIMARY
(
	NAME = 'QUANLYBANTRANH_MDF',
	FILENAME = 'F:\QuanLyBanTranh_MDF.mdf',
	SIZE = 5MB,
	MAXSIZE = 10MB,
	FILEGROWTH = 15%
)
LOG ON
(
	NAME = 'QUANLYBANTRANH_LOG',
	FILENAME ='F:\QuanLyBanTranh_LDF.ldf',
	SIZE = 5MB,
	MAXSIZE = 10MB,
	FILEGROWTH = 15%
)
USE QuanLyBanTranh;
GO
--- BẢNG CHỦ ĐỀ---
CREATE TABLE CHUDE
(
	MACD CHAR(4) NOT NULL,
	TENCD NVARCHAR(50),
	CONSTRAINT PK_CHUDE_MACD PRIMARY KEY (MACD)
)
--- BẢNG TRANH ---
CREATE TABLE TRANH
(
	MATRANH CHAR(4) NOT NULL,
	TENTRANH NVARCHAR(50),
	MOTA NVARCHAR(MAX),
	BAOHANH NVARCHAR(30),
	GIABAN decimal(18,0),
	ANHBIA NVARCHAR(MAX),
	TINHTRANG NVARCHAR(20),
	MACD CHAR(4),
	CONSTRAINT PK_MATRANH_TRANH PRIMARY KEY (MATRANH),
	CONSTRAINT FK_TRANH_CHUDE FOREIGN KEY(MACD) REFERENCES CHUDE(MACD)
)
--- BẢNG KHÁCH HÀNG ---
CREATE TABLE KHACHHANG
(
	MAKH CHAR(4) NOT NULL,
	HOTEN NVARCHAR(50),
	NGAYSINH DATETIME,
	GIOITINH NVARCHAR(20),
	DIENTHOAI VARCHAR(50),
	TAIKHOAN NVARCHAR(50),
	MATKHAU NVARCHAR(50),
	EMAIL NVARCHAR(50),
	DIACHI NVARCHAR(50),
	CONSTRAINT PK_MAKH_KHACHHANG PRIMARY KEY (MAKH)
)
--- BẢNG ĐƠN HÀNG ---
CREATE TABLE DONHANG
(
	MADH CHAR(4) NOT NULL,
	NGAYGIAO DATETIME,
	NGAYDAT DATETIME,
	DATHANHTOAN NVARCHAR(30),
	MAKH CHAR(4)
	CONSTRAINT PK_MADH_DONHANG PRIMARY KEY (MADH),
	CONSTRAINT FK_DONHANG_KHACHHANG FOREIGN KEY(MAKH) REFERENCES KHACHHANG(MAKH)
)
--- BẢNG CHI TIẾT ĐƠN HÀNG ---
CREATE TABLE CHITIETDONHANG
(	
	MADH CHAR(4) NOT NULL,
	MATRANH CHAR(4) NOT NULL,
	SOLUONG INT,
	DONGIA DECIMAL(18,0),
	CONSTRAINT PK_MADH_MATRANH PRIMARY KEY (MADH,MATRANH),
	CONSTRAINT FK_CTDH_DONHANG FOREIGN KEY(MADH) REFERENCES DONHANG(MADH),
	CONSTRAINT FK_CTDH_TRANH FOREIGN KEY(MATRANH) REFERENCES TRANH(MATRANH)
)
---BẢNG ADMIN
CREATE TABLE ADMIN
(
	TENDANGNHAP NVARCHAR(30) PRIMARY KEY,
	MATKHAU CHAR(20) NOT NULL,
	HOTEN NVARCHAR(50)
)

---- NHẬP DỮ LIỆU ----
--- CHỦ ĐỀ ---
INSERT INTO CHUDE VALUES('CD01',N'TRANH TREO TƯỜNG 3D NỔI')
INSERT INTO CHUDE VALUES('CD02',N'TRANH TREO TƯỜNG PHONG CẢNH')

-------------- TRANH NỔI 3D--------------
INSERT INTO TRANH VALUES('MT01',N'Tranh Phù Điêu 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Bề mặt nổi là họa tiết nhựa Composite phù điêu nổi. ',
N'3 Năm',1200000,N'phudieu.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT02',N'Tranh Chim Công 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Bề mặt nổi là họa tiết nhựa Composite phù điêu nổi. ',
N'3 Năm',1200000,N'chimcong_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT03',N'Tranh Hoa Mẫu Đơn 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Bề mặt nổi là họa tiết nhựa Composite phù điêu nổi. ',
N'3 Năm',1500000,N'maudon_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT04',N'Tranh Hoa Mai 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Hàng nhập khẩu 100%. Phía sau có sẵn hốc treo vệ sinh bằng chổi lông gà và khăn ẩm dễ dàng. ',
N'3 Năm',1000000,N'hoamai_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT05',N'Tranh Hoa Cúc 3D',
N'Chất liệu: Tranh được làm bằng Da Nỉ công nghệ hiện đại, chất liệu da công nghiệp nhập Ý. Lõi tranh là gỗ hộp được bọc da xung quanh không ẩm mốc, mối mọt.',
N'3 Năm',2000000,N'hoacuc_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT06',N'Tranh Hoa Mộc Lan 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Hàng nhập khẩu 100%. Phía sau có sẵn hốc treo vệ sinh bằng chổi lông gà và khăn ẩm dễ dàng.',
N'3 Năm',1700000,N'moclan_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT07',N'Tranh Lọ Hoa Nổi 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Bề mặt nổi là họa tiết nhựa Composite phù điêu nổi. Hàng nhập khẩu 100%',
N'3 Năm',1600000,N'lohoanoi_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT08',N'Tranh Lọ Hoa Đỏ 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ hộp sơn phủ xung quanh. Bề mặt nổi là họa tiết nhựa Composite phù điêu nổi. Hàng nhập khẩu 100%',
N'3 Năm',1300000,N'hoado_3D.jpg',N'Còn Hàng','CD01')

INSERT INTO TRANH VALUES('MT09',N'Tranh Thuyền Buồm 3D',
N'Chất liệu: Là dòng tranh phù điêu nổi 3D, nền tranh là gỗ sơn phủ xung quanh, bề mặt được tráng lớp thủy tinh bóng. Họa tiết nổi là Sứ phủ nhựa Composite nổi.',
N'3 Năm',2000000,N'thuyenbuom_3D.jpg',N'Còn Hàng','CD01')




------ TRANH PHONG CẢNH ------

INSERT INTO TRANH VALUES('MT10',N'Tranh Phong Cảnh Sơn Dầu',
N'Tranh treo tường thành phố sơn dầu thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1500000,N'sondau.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT11',N'Tranh Phong Cảnh Lá Xanh Nhiệt Đới',
N'Tranh treo tường Lá xanh nhiệt đới thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1600000,N'laxanh.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT12',N'Tranh Phong Cảnh Hà Nội',
N'Tranh treo tường Hà Nội thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1800000,N'hanoi.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT13',N'Tranh Phong Cảnh Cô Gái Và Hoa',
N'Tranh treo tường Cô Gái Và Hoa thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1900000,N'cogaivahoa.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT14',N'Tranh Phong Cảnh Đôi Công Hoàng Kim',
N'Tranh treo tường Đôi Công Hoàng Kim thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1400000,N'doiconghoangkim.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT15',N'Tranh Phong Cảnh Biển',
N'Tranh treo tường Biển thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1000000,N'bien.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT16',N'Tranh Phong Cảnh Biển',
N'Tranh treo tường Biển thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1100000,N'bien.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT17',N'Tranh Phong Cảnh Paris Hoa Lệ',
N'Tranh treo tường Paris Hoa Lệ thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',2000000,N'parishoale.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT18',N'Tranh Phong Cảnh Coffee Always Fresh',
N'Tranh treo tường Coffee Always Fresh thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1700000,N'cafe.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT19',N'Tranh Phong Cảnh Thủy Tinh',
N'Tranh treo tường Thủy Tinh thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1700000,N'thuytinh_3D.jpg',N'Còn Hàng','CD02')

INSERT INTO TRANH VALUES('MT20',N'Tranh Phong Cảnh Phòng Bếp',
N'Tranh treo tường Phòng Bếp thổi luồng gió thiên nhiên vào trong ngôi nhà bạn, đem lại sự tươi mát, thư giãn cho không gian.',
N'3 Năm',1700000,N'phongan_3D.jpg',N'Còn Hàng','CD02')


--------  KHÁCH HÀNG ----------

INSERT INTO KHACHHANG VALUES('KH01',N'Phạm Quốc Nhiên','12/10/1999',N'Nam','012345678','NhienTK','NhienMK',N'Nhienpham@gmail.com',N'Bến Tre')
INSERT INTO KHACHHANG VALUES('KH02',N'Lê Minh Khánh','12/08/1999',N'Nam','012345677','KhanhTK','KhanhMK',N'KhanhLe@gmail.com',N'Tây Ninh')

----------ADMIN-----------------

INSERT INTO ADMIN VALUES('minhkhanh','21051999','Lê Minh Khánh')
INSERT INTO ADMIN VALUES('quocnhien','12101999','Phạm Quốc Nhiên')


------------XEM DỮ LIỆU -----------

SELECT * FROM CHUDE
SELECT * FROM TRANH
SELECT * FROM KHACHHANG
SELECT * FROM DONHANG
SELECT * FROM CHITIETDONHANG
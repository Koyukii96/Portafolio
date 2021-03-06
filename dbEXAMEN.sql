USE MASTER
GO

IF EXISTS (SELECT * FROM SYSDATABASES WHERE NAME='dbEXAMEN')
DROP DATABASE dbEXAMEN
GO

CREATE DATABASE dbEXAMEN
GO

USE dbEXAMEN
GO

/*
DROP TABLE BOLETA;
DROP TABLE BUS;
DROP TABLE CONDUCTOR;
DROP TABLE CLIENTE;
DROP TABLE CIUDAD;
DROP TABLE HORARIO;
DROP TABLE USUARIO;
DROP TABLE VENDEDOR;
*/

CREATE TABLE CONDUCTOR(
	ID_CONDUCTOR NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	NOMBRE_CONDUCTOR VARCHAR(25),
	RUT_CONDUCTOR VARCHAR(10) UNIQUE,
	EDAD_CONDUCTOR NUMERIC(2,0)
);

CREATE TABLE CIUDAD(
	ID_CIUDAD NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	NOMBRE_CIUDAD VARCHAR(25)
);

CREATE TABLE HORARIO(
	COD_HORARIO NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DESCRIPCION VARCHAR(10)
);

CREATE TABLE BUS(
	PATENTE VARCHAR(6) NOT NULL PRIMARY KEY,
	NOMBRE_BUS VARCHAR(20),
	ID_CONDUCTOR NUMERIC REFERENCES CONDUCTOR(ID_CONDUCTOR),
	TIPO_BUS VARCHAR(10),
	COD_HORARIO NUMERIC REFERENCES HORARIO(COD_HORARIO),
	DESTINO_ID NUMERIC REFERENCES CIUDAD(ID_CIUDAD)
);

CREATE TABLE CLIENTE(
	ID_CLIENTE NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RUT_CLIENTE VARCHAR(10) UNIQUE,
	NOMBRE_CLIENTE VARCHAR(25),
	EDAD_CLIENTE NUMERIC(2,0),
	ESTUDIANTE NUMERIC(1,0),
);

CREATE TABLE VENDEDOR(
	ID_VENDEDOR NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	RUT_VENDEDOR VARCHAR(10) UNIQUE,
	NOMBRE_VENDEDOR VARCHAR(25)
);

CREATE TABLE USUARIO(
	NOMBRE_USUARIO VARCHAR(25) NOT NULL PRIMARY KEY,
	PASSWORD_USUARIO VARCHAR(25),
	HABILITADO NUMERIC(1,0),
	TIPO_USUARIO NUMERIC(1,0),
	ID_VENDEDOR NUMERIC REFERENCES VENDEDOR(ID_VENDEDOR)
);

CREATE TABLE BOLETA(
	ID_BOLETA NUMERIC NOT NULL IDENTITY(1,1) PRIMARY KEY,
	ID_BUS VARCHAR(6) REFERENCES BUS(PATENTE),
	ID_CONDUCTOR NUMERIC REFERENCES CONDUCTOR(ID_CONDUCTOR),
	ID_VENDEDOR NUMERIC REFERENCES VENDEDOR(ID_VENDEDOR),
	ID_CLIENTE NUMERIC REFERENCES CLIENTE(ID_CLIENTE),
	FECHA DATE,
	TOTAL NUMERIC(8,0)
);

INSERT INTO CONDUCTOR (NOMBRE_CONDUCTOR, RUT_CONDUCTOR, EDAD_CONDUCTOR) VALUES ('JOSE', '12345678-9', '36');
INSERT INTO CONDUCTOR (NOMBRE_CONDUCTOR, RUT_CONDUCTOR, EDAD_CONDUCTOR) VALUES ('LUCAS', '12365678-9', '32');
INSERT INTO CIUDAD (NOMBRE_CIUDAD) VALUES ('SANTIAGO');
INSERT INTO HORARIO (DESCRIPCION) VALUES ('13:00');
INSERT INTO BUS (PATENTE, NOMBRE_BUS, ID_CONDUCTOR, TIPO_BUS, COD_HORARIO, DESTINO_ID) VALUES ('NK3426', 'SUR LAN', '1', 'NORMAL', '1', '1');
INSERT INTO BUS (PATENTE, NOMBRE_BUS, ID_CONDUCTOR, TIPO_BUS, COD_HORARIO, DESTINO_ID) VALUES ('VJ1801', 'PULLMAN', '2', 'NORMAL', '1', '1');
INSERT INTO CLIENTE (RUT_CLIENTE, NOMBRE_CLIENTE, EDAD_CLIENTE, ESTUDIANTE) VALUES ('98765432-1', 'MARCOS', '24', '1');
INSERT INTO VENDEDOR (RUT_VENDEDOR, NOMBRE_VENDEDOR) VALUES ('34567892-1', 'CAMILO');
INSERT INTO USUARIO (NOMBRE_USUARIO, PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, ID_VENDEDOR) VALUES ('CAMILO_EMP1', 'ClaveSegura', '1', '2', '1');
INSERT INTO BOLETA (ID_BUS, ID_CONDUCTOR, ID_VENDEDOR, ID_CLIENTE, FECHA, TOTAL) VALUES ('NK3426', '1', '1', '1', '2017-12-02', '10000');

INSERT INTO USUARIO (NOMBRE_USUARIO, PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, ID_VENDEDOR) VALUES ('wea', 'wea', '1', '2', '1');
INSERT INTO USUARIO (NOMBRE_USUARIO, PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, ID_VENDEDOR) VALUES ('wea2', 'wea2', '0', '2', '1');

SELECT * FROM CONDUCTOR;
SELECT * FROM CIUDAD;
SELECT * FROM HORARIO;
SELECT * FROM BUS;
SELECT * FROM CLIENTE;
SELECT * FROM VENDEDOR;
SELECT * FROM USUARIO;
SELECT * FROM BOLETA;

SELECT PATENTE, NOMBRE_BUS, B.ID_CONDUCTOR, C.NOMBRE_CONDUCTOR 
FROM BUS B 
JOIN CONDUCTOR C 
ON (B.ID_CONDUCTOR = C.ID_CONDUCTOR);


SELECT NOMBRE_USUARIO,PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, V.NOMBRE_VENDEDOR  
FROM Usuario U
Join VENDEDOR V
ON (U.ID_VENDEDOR = V.ID_VENDEDOR)
where NOMBRE_USUARIO='WEA' and PASSWORD_USUARIO='WEA';


SELECT B.ID_CONDUCTOR, C.NOMBRE_CONDUCTOR FROM BUS B JOIN CONDUCTOR C ON(B.ID_CONDUCTOR = C.ID_CONDUCTOR)
where B.PATENTE = '' ;

SELECT * FROM CLIENTE WHERE ID_CLIENTE=1;

----------jOIN DEL COMPROBANTE
SELECT BO.ID_BOLETA,  BU.PATENTE, CO.NOMBRE_CONDUCTOR, CIU.NOMBRE_CIUDAD, CLI.NOMBRE_CLIENTE, CLI.RUT_CLIENTE, CLI.ESTUDIANTE, BO.FECHA, BO.TOTAL, HO.DESCRIPCION
FROM BOLETA BO 
JOIN BUS BU
ON (BO.ID_BUS = BU.PATENTE)
JOIN CONDUCTOR CO
ON (BO.ID_CONDUCTOR = CO.ID_CONDUCTOR)
JOIN VENDEDOR VE
ON(BO.ID_VENDEDOR = VE.ID_VENDEDOR)
JOIN CLIENTE CLI
ON(BO.ID_CLIENTE = CLI.ID_CLIENTE)
JOIN CIUDAD CIU
ON (BU.DESTINO_ID = CIU.ID_CIUDAD)
Join HORARIO HO
ON (BU.COD_HORARIO = HO.COD_HORARIO)
WHERE ID_BOLETA =1;

SELECT * FROM BOLETA;

INSERT INTO HORARIO (DESCRIPCION) VALUES ('13:00')

SELECT @@IDENTITY as wea;
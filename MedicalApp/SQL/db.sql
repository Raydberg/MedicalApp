CREATE TABLE Especialidad
(
    codesp char(3)     NOT NULL PRIMARY KEY,
    nombre varchar(50) NOT NULL
);

CREATE TABLE Distrito
(
    coddis char(3)     NOT NULL PRIMARY KEY,
    nombre varchar(50) NOT NULL
);

CREATE TABLE Medicos
(
    codmed           char(5) NOT NULL
        CONSTRAINT pk_codmed PRIMARY KEY,
    codesp           char(3) REFERENCES Especialidad (codesp),
    nommed           varchar(40),
    anio_colegiatura int,
    coddis           char(3) REFERENCES Distrito (coddis),
    estado           int DEFAULT 1
);

CREATE TABLE Pacientes
(
    codpac    INT PRIMARY KEY,
    nombre    VARCHAR(60) NOT NULL,
    apellido  VARCHAR(60) NOT NULL,
    direccion VARCHAR(100),
    coddis    char(3) REFERENCES Distrito (coddis),
    telefono  VARCHAR(15),
    estado    INT DEFAULT 1
);

CREATE TABLE Citas
(
    id       INT PRIMARY KEY IDENTITY (1,1),
    nro_cita INT      NOT NULL,
    fecha    DATETIME NOT NULL,
    pago     FLOAT    NOT NULL,
    codpac   INT      NOT NULL,
    codmed   char(5)  NOT NULL,
    estado   INT DEFAULT 1,
    FOREIGN KEY (codpac) REFERENCES Pacientes (codpac),
    FOREIGN KEY (codmed) REFERENCES Medicos (codmed)
);

CREATE PROCEDURE CitasAnio @year INT
AS
BEGIN
SELECT C.nro_cita,
       C.fecha,
       C.pago,
       P.nombre + ' ' + P.apellido AS nompac,
       M.nommed                    AS nommed
FROM Citas C
         INNER JOIN Pacientes P ON C.codpac = P.codpac
         INNER JOIN Medicos M ON C.codmed = M.codmed
WHERE YEAR(C.fecha) = @year
  AND C.estado = 1
ORDER BY C.fecha;
END;
GO

CREATE PROCEDURE ListarMedicos
    AS
BEGIN
SELECT M.codmed,
       M.nommed,
       E.nombre AS especialidad,
       M.anio_colegiatura,
       D.nombre AS distrito,
       M.estado
FROM Medicos M
         INNER JOIN Especialidad E ON M.codesp = E.codesp
         INNER JOIN Distrito D ON M.coddis = D.coddis
WHERE M.estado = 1;
END;
GO

-- Listar especialidades
CREATE PROCEDURE ListarEspecialidades
    AS
BEGIN
SELECT codesp, nombre
FROM Especialidad
ORDER BY nombre;
END;
GO

-- Agregar un nuevo médico
CREATE PROCEDURE AgregarMedico @codmed char(5),
                               @codesp char(3),
                               @nommed varchar(40),
                               @anio_colegiatura int,
                               @coddis char(3)
AS
BEGIN
    IF EXISTS(SELECT 1 FROM Medicos WHERE codmed = @codmed)
BEGIN
RETURN 'El código del médico ya existe';
END

    IF NOT EXISTS(SELECT 1 FROM Especialidad WHERE codesp = @codesp)
BEGIN
RETURN 'No existe la especialidad indicada';
END

    IF NOT EXISTS(SELECT 1 FROM Distrito WHERE coddis = @coddis)
BEGIN
RETURN 'No existe el distrito indicado';
END

INSERT INTO Medicos(codmed, codesp, nommed, anio_colegiatura, coddis, estado)
VALUES (@codmed, @codesp, @nommed, @anio_colegiatura, @coddis, 1);

RETURN 'Médico registrado correctamente';
END;
GO




INSERT INTO Especialidad (codesp, nombre)
VALUES ('E01', 'Cardiología'),
       ('E02', 'Pediatría'),
       ('E03', 'Neurología'),
       ('E04', 'Dermatología'),
       ('E05', 'Oftalmología');

INSERT INTO Distrito (coddis, nombre)
VALUES ('D01', 'San Borja'),
       ('D02', 'Miraflores'),
       ('D03', 'San Isidro'),
       ('D04', 'Surco'),
       ('D05', 'La Molina');

INSERT INTO Medicos (codmed, codesp, nommed, anio_colegiatura, coddis)
VALUES ('M0001', 'E01', 'Dr. Carlos Pérez', 2010, 'D01'),
       ('M0002', 'E02', 'Dra. Ana Gómez', 2015, 'D02'),
       ('M0003', 'E03', 'Dr. Luis Torres', 2008, 'D03'),
       ('M0004', 'E04', 'Dra. María López', 2012, 'D04'),
       ('M0005', 'E05', 'Dr. Juan Silva', 2009, 'D05');

INSERT INTO Pacientes (codpac, nombre, apellido, direccion, coddis, telefono)
VALUES (1, 'Roberto', 'Martínez', 'Av. Principal 123', 'D01', '987654321'),
       (2, 'Laura', 'Sánchez', 'Jr. Los Olivos 456', 'D02', '987123456'),
       (3, 'Pedro', 'García', 'Calle Las Flores 789', 'D03', '987789456'),
       (4, 'Carmen', 'Rodríguez', 'Av. Los Pinos 234', 'D04', '987456123'),
       (5, 'Miguel', 'Díaz', 'Jr. Las Palmeras 567', 'D05', '987321654');

INSERT INTO Citas (nro_cita, fecha, pago, codpac, codmed)
VALUES (1001, '2022-05-15 10:00:00', 150.00, 1, 'M0001'),
       (1002, '2022-06-20 11:30:00', 200.00, 2, 'M0002'),
       (1003, '2023-03-10 09:15:00', 180.00, 3, 'M0003'),
       (1004, '2023-04-25 14:00:00', 220.00, 4, 'M0004'),
       (1005, '2023-07-05 16:30:00', 190.00, 5, 'M0005'),
       (1006, '2024-01-18 10:30:00', 230.00, 1, 'M0003'),
       (1007, '2024-02-22 11:00:00', 170.00, 2, 'M0004'),
       (1008, '2024-03-30 15:45:00', 210.00, 3, 'M0005');

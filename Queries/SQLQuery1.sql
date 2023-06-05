--CREATE TABLE EmployeeResources(
--employeeID int NULL,
--prn int  NOT NULL PRIMARY KEY,
--laptop varchar(30) NOT NULL,
--mouse varchar(30) NOT NULL
--CONSTRAINT FK_Employee_ID FOREIGN KEY (employeeID)
--REFERENCES clients (employeeID)
--ON DELETE CASCADE
--ON UPDATE CASCADE,
--);


--INSERT INTO EmployeeResources (employeeID, prn, laptop, mouse)
--VALUES
--(101, 1, 'DELL LATITUDE 4340', 'Logitech g102'),
--(102, 2, 'DELL LATITUDE 4340', 'Logitech g102'),
--(103, 3, 'DELL LATITUDE 4110', 'Viper mini'),
--(104, 4, 'DELL LATITUDE 4010', 'HP 650'),
--(105, 5, 'DELL LATITUDE 4300', 'Cosmic Byte'),
--(109, 6, 'HP 403', 'HP 650'),
--(113, 7, 'Chromebook 1', 'Lenovo Legion M200');

--drop table EmployeeResources;

--employeeID int NULL,

--SELECT c.name, c.email, c.salary, c.salary, e.prn, e.laptop, e.mouse
--FROM clients c 
-- FULL JOIN EmployeeResources e on c.employeeID = e.employeeID;

--SELECT StudentCourse.Course_ID, Student.Name, Student.Age FROM Student
--FULL JOIN StudentCourse
--ON Student.Roll_No = StudentCourse.Roll_No;

SELECT c.name, c.phone, c.address, e.prn, e.mouse 
from clients c
INNER JOIN EmployeeResources e
ON c.employeeID = e.employeeID
WHERE mouse = 'HP 650';

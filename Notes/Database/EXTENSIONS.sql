use THH_IdentityServer
GO
--Alter Table AspNetUsers ADD CONSTRAINT  CHK_UserIdentityNumberLen CHECK (LEN(IdentityNumber)=11); 

--ALTER TABLE AspNetUsers add  IVote BIT default 'FALSE';

--CREATE TRIGGER handle_voted_change ON AspNetUsers
--AFTER UPDATE
--AS
--BEGIN
--set nocount on;
--declare @oldValue bit;
--declare @newValue bit;
--Select @oldValue = IVoted from deleted;
--Select @newValue = IVoted from inserted;
--if(@oldValue=1 and @newValue=0)
--begin 
--	UPDATE U set IVoted=1 
--	FROM AspNetUsers U
--	inner join inserted I on U.Id=I.Id
	
--end

--END





--CREATE VIEW GetUserCountByRole AS
--select AspNetRoles.Name as [Role Name], Count(AspNetUsers.Id) as [User Count] from AspNetUsers
--inner join AspNetUserRoles on AspNetUserRoles.UserId=AspNetUsers.Id
--inner join AspNetRoles on AspNetRoles.Id=AspNetUserRoles.RoleId
--group by AspNetRoles.Name

--select * from GetUserCountByRole


--CREATE VIEW GetUsersByVoter AS
--select AspNetUsers.* from AspNetUsers
--inner join AspNetUserRoles on AspNetUserRoles.UserId=AspNetUsers.Id
--inner join AspNetRoles on AspNetRoles.Id=AspNetUserRoles.RoleId
--where AspNetRoles.Name='User'


--select * from GetUsersByVoter


--Create PROCEDURE Vote
--@userId nvarchar(450)
--AS
--Update AspNetUsers Set IVoted=1 where AspNetUsers.Id=@userId

--exec Vote '8d0f4316-6613-4227-896e-19d1775e0d98'


--create function fun_get_age  
--(  
--   @birthday datetime2 
--)  
--returns int  
--as  
--begin 
--return FLOOR(DATEDIFF(DAY, @birthday, getdate()) / 365.25)
--end  

--print dbo.fun_get_age('2000-06-13'); --21

--create function fun_Age_Check  
--(  
--   @birthday datetime2 
--)  
--returns bit  
--as  
--begin 
--if(dbo.fun_get_age(@birthday) >18) return 1;
--return 0;
--end  

--print dbo.fun_Age_Check('2015-01-24'); --0
--print dbo.fun_Age_Check('2000-06-13'); --1


--CREATE FUNCTION fun_voting_rate
--( 
--)
--RETURNS float
--AS
--begin 
--declare @voters float;
--declare @total float;
--select @voters = COUNT(*) from AspNetUsers where IVoted=1
--select @total = COUNT(*) from AspNetUsers
--return @voters/@total;
--end


--SELECT dbo.fun_voting_rate()


--CREATE RULE sc_rule  
--AS   
--@value like '%[^a-Z0-9]%'

--SP_BINDRULE  sc_rule,'AspNetUsers.FirstName' 
--SP_BINDRULE  sc_rule,'AspNetUsers.LastName' 


USE THH_MainApi
GO

--Alter Table Cities Add Constraint CHK_PlateRange Check (Plate >0 and Plate <82);
--CREATE RULE rule_plate_range AS @value >0 and @value <82;

--ALTER TABLE Cities ADD UNIQUE (Plate);

--ALTER TABLE [Cities] ADD CONSTRAINT DF_Cities_CT DEFAULT GETDATE() FOR CreatedTime
--ALTER TABLE Districts ADD CONSTRAINT DF_Districts_CT DEFAULT GETDATE() FOR CreatedTime
--ALTER TABLE Elections ADD CONSTRAINT DF_Elections_CT DEFAULT GETDATE() FOR CreatedTime
--ALTER TABLE Nodes ADD CONSTRAINT DF_Nodes_CT DEFAULT GETDATE() FOR CreatedTime
--ALTER TABLE PollingStations ADD CONSTRAINT DF_PollingStations_CT DEFAULT GETDATE() FOR CreatedTime


--Create PROCEDURE GetDistrictsByCityName
--@cityName varchar(Max)
--AS
--SELECT * FROM Districts 
--inner join Cities on Cities.Id =Districts.CityId
--where Cities.Name like @cityName


--Create PROCEDURE GetDistrictsByCityPlate
--@plate varchar(Max)
--AS
--SELECT * FROM Districts 
--inner join Cities on Cities.Id =Districts.CityId
--where Cities.Plate = @plate


--Create PROCEDURE GetNodesByCityName
--@cityName varchar(Max)
--AS
--SELECT * FROM Nodes 
--inner join Cities on Cities.Id =Nodes.CityId
--where Cities.Name like @cityName


--Exec GetDistrictsByCityName 'Adana'
--Exec GetDistrictsByCityPlate 45
--Exec GetNodesByCityName 'Adana'


--CREATE VIEW GetPollingStationsGroupByCityName AS
--select Cities.Name as [City Name] ,COUNT(PollingStations.Id) as [Polling Station Count] from Cities
--inner join PollingStations on PollingStations.CityId=Cities.Id
--group by Cities.Name


--select * from GetPollingStationsGroupByCityName


--CREATE RULE plate_rule  
--AS   
--@range> 0 AND @range <82;  

--SP_BINDRULE  plate_rule,'Cities.Plate' 



--CREATE RULE len_rule  
--AS   
--Len(@value)>0 and Len(@value)<15


--SP_BINDRULE  len_rule,'Cities.Name' 
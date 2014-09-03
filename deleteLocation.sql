USE [WP11_cu222ai_Weather]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_createLocation]


@LocationID int,
@Country varchar(50),
@County varchar(70),
@Name varchar(70),
@NextUpdate datetime2(0)


AS
BEGIN
DELETE FROM Geoname

WHERE
@LocationID = LocationID

END
GO
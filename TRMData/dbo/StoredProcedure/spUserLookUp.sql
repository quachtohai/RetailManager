CREATE PROCEDURE [dbo].[spUserLookUp]
	@Id nvarchar(128)	
AS
Begin
	Set Nocount on
	select FirstName,LastName,EmailAddress,CreatedDate from [dbo].[User]
	where Id =@Id
	End

RETURN 

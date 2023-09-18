CREATE PROCEDURE TransferFunds
    @senderAccountNumber INT,
    @receiverAccountNumber INT,
    @amount FLOAT,
@result INT OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Check if the sender has sufficient balance
        DECLARE @senderBalance FLOAT;
        SELECT @senderBalance = balance FROM Account WHERE account_number = @senderAccountNumber;
DECLARE @transaction_number INT;
DECLARE @ErrorMessage VARCHAR(2000);
DECLARE @ErrorSeverity TINYINT
DECLARE @ErrorState TINYINT

        IF @senderBalance >= @amount
        BEGIN
            -- Update sender's balance with withdrawal
            UPDATE Account SET balance = balance - @amount WHERE account_number = @senderAccountNumber;

            -- Update receiver's balance with deposit
            UPDATE Account SET balance = balance + @amount WHERE account_number = @receiverAccountNumber;

            -- Insert a record into the transaction table for the transfer
            INSERT INTO Transactions (account_number, transaction_type, transaction_amount)
            VALUES (@senderAccountNumber, 'WITHDRAWAL', @amount);

SET @transaction_number = SCOPE_IDENTITY();
SET IDENTITY_INSERT dbo.Transactions ON

            INSERT INTO Transactions (transaction_number,account_number, transaction_type, transaction_amount)
            VALUES (@transaction_number, @receiverAccountNumber, 'DEPOSIT', @amount);

            -- Commit the transaction
            COMMIT;
SET @result = 0;
        END
        ELSE
        BEGIN
           
            ROLLBACK;
            --THROW 51000, 'Insufficient balance in sender''s account', 1;
SET @result = -1;
RETURN -1;
        END
    END TRY
    BEGIN CATCH
       
SET @ErrorMessage  = ERROR_MESSAGE()
    SET @ErrorSeverity = ERROR_SEVERITY()
    SET @ErrorState    = ERROR_STATE()
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState)

        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK;
        END
       
SET @result = -2;
RETURN -2;
    END CATCH;
END;

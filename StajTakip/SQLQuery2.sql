INSERT INTO dbo.Kullanici
(
    uName,
    Pass,
    Actor,
    eMail,
    ID
)
VALUES
(   N'istanbul', -- uName - nchar(10)
    N'asd', -- Pass - nchar(10)
    2,   -- Actor - int
    N'iu@mail.com', -- eMail - nvarchar(50)
    2    -- ID - int
    )
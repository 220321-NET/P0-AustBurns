CREATE TABLE NashvilleStore
(
    NashStoreID INT PRIMARY KEY IDENTITY(1, 1),
    City VARCHAR(20),
    InventoryID int FOREIGN KEY REFERENCES Inventory(InventoryID)

)
CREATE TABLE RichmondStore
(
    RichStoreID INT PRIMARY KEY IDENTITY(1, 1),
    City VARCHAR(20),
    InventoryID int FOREIGN KEY REFERENCES Inventory(InventoryID)

)
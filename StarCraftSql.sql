-- Terran buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Command Center', 0, 400, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\61.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Barracks', 0, 150, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\60.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Academy', 0, 150, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\183.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Factory', 0, 200, 100, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\191.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Starport', 0, 150, 100, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\201.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Armory', 0, 100, 50, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\184.png', Single_Blob) as image


-- Zerg buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Hatchery', 1, 300, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\280.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Spawning Pool', 1, 200, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\300.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Hydralisk Den', 1, 100, 50, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\295.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Spire', 1, 200, 150, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\290.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Queen''s Nest', 1, 150, 100, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\293.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Ultralisk Cavern', 1, 150, 200, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\303.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Defiler Mound', 1, 100, 100, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\279.png', Single_Blob) as image

-- Protoss buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Nexus', 2, 400, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\506.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Gateway', 2, 150, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\504.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Cybernetics Core', 2, 200, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\501.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Robotics Facility', 2, 200, 200, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\512.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Stargate', 2, 150, 150, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\515.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Robotics Support Bay', 2, 150, 100, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\513.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Fleet Beacon', 2, 300, 200, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\502.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, Image)
SELECT 'Templar Archives', 2, 150, 200, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\516.png', Single_Blob) as image
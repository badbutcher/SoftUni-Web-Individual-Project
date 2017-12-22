-- Terran buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Command Center', 0, 400, 0, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\61.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Barracks', 0, 150, 0, 2, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\60.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Academy', 0, 150, 0, 3, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\183.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Factory', 0, 200, 100, 4, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\191.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Starport', 0, 150, 100, 6, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\201.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Armory', 0, 100, 50, 8, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\184.png', Single_Blob) as image


-- Zerg buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Hatchery', 1, 300, 0, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\280.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Spawning Pool', 1, 200, 0, 2, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\300.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Hydralisk Den', 1, 100, 50, 3, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\295.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Spire', 1, 200, 150, 4, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\290.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Queen''s Nest', 1, 150, 100, 5, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\293.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Ultralisk Cavern', 1, 150, 200, 6, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\303.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Defiler Mound', 1, 100, 100, 8, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\279.png', Single_Blob) as image

-- Protoss buildings
INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Nexus', 2, 400, 0, 0, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\506.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Gateway', 2, 150, 0, 2, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\504.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Cybernetics Core', 2, 200, 0, 3, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\501.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Robotics Facility', 2, 200, 200, 4, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\512.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Stargate', 2, 150, 150, 5, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\515.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Robotics Support Bay', 2, 150, 6, 9, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\513.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Fleet Beacon', 2, 300, 200, 7, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\502.png', Single_Blob) as image

INSERT INTO Buildings(Name, Race, MineralCost, GasCost, UnlockLevel, Image)
SELECT 'Templar Archives', 2, 150, 200, 8, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\516.png', Single_Blob) as image

-- Terran units
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'SCV', 0, 50, 0, 60, 5, 0, 4, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\7.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Marine', 0, 50, 0, 40, 7, 0, 3, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\32.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Firebat', 0, 50, 25, 60, 9, 1, 5, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\94.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Ghost', 0, 25, 75, 45, 11, 2, 7, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\57.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Vulture', 0, 75, 0, 80, 22, 3, 8, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\77.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Siege Tank', 0, 150, 100, 170, 40, 4, 10, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\Tank.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Goliath', 0, 100, 50, 145, 26, 4, 10, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\Goliath.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Wraith', 0, 150, 100, 120, 32, 5, 11, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\116.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Battlecruiser', 0, 400, 300, 590, 260, 7, 14, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\172.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Valkyrie', 0, 250, 125, 260, 58, 7, 15, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\147.png', Single_Blob) as image

INSERT BuildingUnit(BuildingId, UnitId) VALUES (1, 1)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (2, 2)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (3, 3)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (3, 4)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 5)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 6)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 7)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 8)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 9)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 10)

-- Zerg units
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Drone', 1, 50, 0, 40, 5, 0, 4, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Drone.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Zergling', 1, 25, 0, 35, 6, 0, 5, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\210.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Hydralisk', 1, 75, 25, 80, 11, 2, 6, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\203.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Ultralisk', 1, 200, 200, 450, 23, 6, 10, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\230.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Mutalisk', 1, 100, 100, 120, 16, 4, 12, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\225.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Scourge', 1, 25, 75, 25, 50, 5, 12, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Scourge.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Guardian', 1, 150, 200, 200, 25, 5, 14, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\242.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Devourer', 1, 250, 150, 280, 30, 7, 16, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\269.png', Single_Blob) as image

INSERT BuildingUnit(BuildingId, UnitId) VALUES (7, 11)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (8, 12)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (9, 13)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (10, 15)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (10, 16)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (10, 17)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (10, 18)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (12, 14)

-- Protoss units
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Probe', 2, 50, 0, 40, 5, 0, 4, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\381.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Zealot', 2, 100, 0, 170, 18, 0, 4, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\358.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Dragoon', 2, 125, 50, 190, 32, 2, 5, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\306.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Dark Templar', 2, 125, 100, 100, 50, 3, 7, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Dark Templar.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Reaver', 2, 300, 150, 170, 75, 3, 8, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\413.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Archon', 2, 100, 400, 450, 35, 6, 9, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Archon.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Scout', 2, 275, 125, 250, 30, 5, 11, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Scout.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, UnlockLevel, ExpWorth, Image)
SELECT 'Carrier', 2, 550, 250, 600, 12, 7, 12, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\429.png', Single_Blob) as image

INSERT BuildingUnit(BuildingId, UnitId) VALUES (14, 19)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (15, 20)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (16, 21)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (17, 25)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (18, 26)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (19, 23)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (20, 22)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (21, 24)

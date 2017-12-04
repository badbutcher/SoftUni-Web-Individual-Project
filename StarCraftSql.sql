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

-- Terran units
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'SCV', 0, 50, 0, 60, 5, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\7.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Marine', 0, 50, 0, 40, 7, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\32.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Firebat', 0, 50, 25, 60, 9, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\94.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Ghost', 0, 25, 75, 45, 11, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\57.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Vulture', 0, 75, 0, 80, 22, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\77.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Siege Tank', 0, 150, 100, 170, 40, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\Tank.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Goliath', 0, 100, 50, 145, 26, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\Goliath.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Wraith', 0, 150, 100, 120, 32, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\116.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Battlecruiser', 0, 400, 300, 590, 260, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\172.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Valkyrie', 0, 250, 125, 260, 58, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\147.png', Single_Blob) as image

INSERT BuildingUnit(BuildingId, UnitId) VALUES (1, 1)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (2, 2)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (2, 3)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (2, 4)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 5)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 6)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (4, 7)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 8)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 9)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 10)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 1)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (5, 1)

-- Zerg units
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Drone', 1, 50, 0, 40, 5, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Drone.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Zergling', 1, 25, 0, 35, 6, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\210.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Hydralisk', 1, 75, 25, 80, 11, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\203.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Ultralisk', 1, 200, 200, 450, 23, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\230.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Mutalisk', 1, 100, 100, 120, 16, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\225.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Scourge', 1, 25, 75, 25, 50, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Scourge.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Guardian', 1, 150, 200, 200, 25, BulkColumn 
FROM Openrowset( Bulk 'C:\StarCraftSprites\242.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Devourer', 1, 250, 150, 280, 30, BulkColumn 
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
INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Probe', 2, 50, 0, 40, 5, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\381.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Zealot', 2, 100, 0, 170, 18, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\358.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Dragoon', 2, 125, 50, 190, 32, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\306.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Dark Templar', 2, 125, 100, 100, 50, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Dark Templar.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Reaver', 2, 300, 150, 170, 75, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\413.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Archon', 2, 100, 400, 450, 35, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Archon.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Scout', 2, 275, 125, 250, 30, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\Scout.png', Single_Blob) as image

INSERT INTO Units(Name, Race, MineralCost, GasCost, Health, Damage, Image)
SELECT 'Carrier', 2, 550, 250, 600, 12, BulkColumn
FROM Openrowset( Bulk 'C:\StarCraftSprites\429.png', Single_Blob) as image

INSERT BuildingUnit(BuildingId, UnitId) VALUES (14, 19)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (15, 20)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (16, 21)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (18, 25)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (18, 26)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (19, 23)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (21, 22)
INSERT BuildingUnit(BuildingId, UnitId) VALUES (21, 24)
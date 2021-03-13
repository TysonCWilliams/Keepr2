-- USE tdub08;

-- CREATE TABLE profiles (
--     id VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id)
-- )

-- CREATE TABLE keeps (
--   id INT NOT NULL AUTO_INCREMENT,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(500) NOT NULL,
--   img VARCHAR(255) NOT NULL,
--   views INT NOT NULL,
--   shares INT NOT NULL,
--   keeps INT NOT NULL,
--   creatorId VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id),
--   FOREIGN KEY (creatorId)
--       REFERENCES profiles(id)
--       ON DELETE CASCADE
-- )

-- CREATE TABLE vaults (
--   id INT NOT NULL AUTO_INCREMENT,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(500) NOT NULL,
--   isPrivate TINYINT NOT NULL,
--   creatorId VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id),
--   FOREIGN KEY (creatorId)
--       REFERENCES profiles(id)
--       ON DELETE CASCADE
-- )

-- CREATE TABLE vaultkeeps(
--   id INT NOT NULL AUTO_INCREMENT,   
--   vaultId INT,
--   keepId INT,
--   creatorId VARCHAR(255) NOT NULL,

--   PRIMARY KEY (id),

--   FOREIGN KEY (vaultId)
--   REFERENCES vaults (id)
--   ON DELETE CASCADE,

--   FOREIGN KEY (keepId)
--   REFERENCES keeps (id)
--   ON DELETE CASCADE,

--    FOREIGN KEY (creatorId)
--         REFERENCES profiles(id)
--         ON DELETE CASCADE

-- )

-- DROP TABLE vaultkeeps;
-- DROP TABLE vaults;
-- DROP TABLE keeps;
-- DROP TABLE profiles;
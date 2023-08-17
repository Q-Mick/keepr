CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
    ) default charset utf8 COMMENT '';

ALTER TABLE accounts
ADD
    COLUMN coverImg VARCHAR(255) DEFAULT "https://images.unsplash.com/photo-1595599512948-b9831e5fc11c?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1171&q=80";

ALTER TABLE accounts DROP COLUMN coverImg;
ALTER TABLE keeps
ADD
    COLUMN Kept INT NOT NULL DEFAULT 0;

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500),
        img VARCHAR(255),
        views INT NOT NULL DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    keeps(
        name,
        creatorId,
        description,
        img
    )
VALUES (
        "Gaming Rig",
        "646d205e9bfe771cd1820821",
        "RGB gaming setup",
        "https://images.unsplash.com/photo-1597872200969-2b65d56bd16b?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=735&q=80"
    );

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500),
        img VARCHAR(255) NOT NULL,
        isPrivate BOOLEAN DEFAULT true,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    vaults(
        creatorId,
        name,
        description,
        img
    )
VALUES (
        "646d205e9bfe771cd1820821",
        "a set of a bunch of awesome gaming PCs",
        "Gaming PCs",
        "https://images.unsplash.com/photo-1616588589676-62b3bd4ff6d2?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1632&q=80"
    );

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DELETE FROM vaultkeeps WHERE id = 27 LIMIT 1;

SELECT vk.* FROM vaultkeeps vk WHERE vk.id = 20;

SELECT vks.*, keep.*, act.*
FROM vaultkeeps vks
    JOIN keeps keep ON keep.id = vks.KeepId
    JOIN accounts act ON act.id = keep.CreatorId
WHERE vks.VaultId = 124;

SELECT keep.*, act.*
FROM keeps keep
    JOIN accounts act ON act.id = keep.CreatorId
WHERE
    keep.CreatorId = '648b6d84e289b1b37944ac30';

SELECT
    vault.*,
    act.*
FROM
    vaults vault
JOIN
    accounts act ON act.id = vault.CreatorId
WHERE
    vault.CreatorId = '648cfae3f4c3b27d6717acfe'
AND NOT vault.IsPrivate;

    --  SELECT
    --   vks.*,
    --   keep.*,
    --   act.*
    --   FROM vaultkeeps vks
    --   JOIN keeps keep ON keep.id = vks.KeepId
    --   JOIN accounts act ON act.id = keep.creatorId
    --   WHERE vks.VaultId = '248'

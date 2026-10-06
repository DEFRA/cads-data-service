-- liquibase formatted sql

-- changeset schema:0000-010-cla-core-tables splitStatements:false

-- Core CLA destination tables. trans_type is B for a source bulk row and S for a migration stub.

CREATE TABLE IF NOT EXISTS cla.aspnetroles (
    "Id" uuid NOT NULL,
    "Name" varchar(256) NOT NULL,
    "NormalizedName" varchar(256) NOT NULL,
    "ConcurrencyStamp" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Id")
);
COMMENT ON COLUMN cla.aspnetroles."Id" IS 'The unique identifier for roles in the system, used for permissions management.';
COMMENT ON COLUMN cla.aspnetroles."Name" IS 'The name of the role (e.g., ''Admin'', ''User'').';
COMMENT ON COLUMN cla.aspnetroles."NormalizedName" IS 'A normalized version of the role name for consistent lookup and comparison.';
COMMENT ON COLUMN cla.aspnetroles."ConcurrencyStamp" IS 'A concurrency token used to ensure data integrity during updates.';

CREATE TABLE IF NOT EXISTS cla.aspnetuserroles (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("UserId", "RoleId")
);
COMMENT ON COLUMN cla.aspnetuserroles."UserId" IS 'Foreign key referencing the unique identifier in the AspNetUsers table.';
COMMENT ON COLUMN cla.aspnetuserroles."RoleId" IS 'Foreign key referencing the unique identifier in the AspNetRoles table.';

CREATE TABLE IF NOT EXISTS cla.tbl_batchmovement (
    "BatchMovement_ID" int NOT NULL,
    "Movement_ID" int NOT NULL,
    "MovementGroup_ID" int,
    "BatchNumber_ID" int NOT NULL,
    "StockType_ID" smallint,
    "AnimalTotal" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "IDMark" varchar(50),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("BatchMovement_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_batchmovement."BatchMovement_ID" IS 'Primary key uniquely identifying a batch movement record.';
COMMENT ON COLUMN cla.tbl_batchmovement."Movement_ID" IS 'Foreign key referencing the Tbl_Movement table for associated movement data.';
COMMENT ON COLUMN cla.tbl_batchmovement."MovementGroup_ID" IS 'Identifier grouping related batch movements.';
COMMENT ON COLUMN cla.tbl_batchmovement."BatchNumber_ID" IS 'Reference to the batch number associated with the movement.';
COMMENT ON COLUMN cla.tbl_batchmovement."StockType_ID" IS 'Identifier for the type of stock (e.g., sheep, goats).';
COMMENT ON COLUMN cla.tbl_batchmovement."AnimalTotal" IS 'The total number of animals in the batch.';
COMMENT ON COLUMN cla.tbl_batchmovement."YearPartition" IS 'Logical year-based partitioning for efficient data management.';
COMMENT ON COLUMN cla.tbl_batchmovement."IDMark" IS 'Identification mark associated with the batch (e.g., ''Mixed'').';
COMMENT ON COLUMN cla.tbl_batchmovement."CreatedDateTime" IS 'The timestamp when the batch movement record was created.';
COMMENT ON COLUMN cla.tbl_batchmovement."UpdatedDateTime" IS 'The timestamp for the last update to the batch movement record.';

CREATE TABLE IF NOT EXISTS cla.tbl_batchmovement_undo (
    "BatchMovement_ID" int NOT NULL,
    "Movement_ID" int NOT NULL,
    "MovementGroup_ID" int,
    "BatchNumber_ID" int NOT NULL,
    "StockType_ID" smallint,
    "AnimalTotal" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "IDMark" varchar(50),
    "RequestUndo_ID" int NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("BatchMovement_ID", "YearPartition", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_batchmovement_undo."BatchMovement_ID" IS 'Identifier linking to the original batch movement record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."Movement_ID" IS 'Foreign key referencing the movement ID of the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."MovementGroup_ID" IS 'Grouping identifier from the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."BatchNumber_ID" IS 'Reference to the batch number from the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."StockType_ID" IS 'Identifier for the type of stock from the original batch.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."AnimalTotal" IS 'Total number of animals in the batch from the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."YearPartition" IS 'Year partition information from the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."IDMark" IS 'Identification mark from the original record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."RequestUndo_ID" IS 'Reference to the request that triggered the undo operation.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."CreatedDateTime" IS 'The timestamp for the creation of the undo record.';
COMMENT ON COLUMN cla.tbl_batchmovement_undo."UpdatedDateTime" IS 'The timestamp for the most recent update to the undo record.';

CREATE TABLE IF NOT EXISTS cla.tbl_batchnumber (
    "BatchNumber_ID" int NOT NULL,
    "Species_ID" smallint NOT NULL,
    "BatchNumber" varchar(9) NOT NULL,
    "Request_ID" int NOT NULL,
    "Enterprise_ID" int NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("BatchNumber_ID")
);
COMMENT ON COLUMN cla.tbl_batchnumber."BatchNumber_ID" IS 'Primary key identifying a batch number entry.';
COMMENT ON COLUMN cla.tbl_batchnumber."Species_ID" IS 'Foreign key referencing the species associated with the batch.';
COMMENT ON COLUMN cla.tbl_batchnumber."BatchNumber" IS 'The identifier for the batch within the system.';
COMMENT ON COLUMN cla.tbl_batchnumber."Request_ID" IS 'Foreign key referencing the request associated with the batch creation or update.';
COMMENT ON COLUMN cla.tbl_batchnumber."Enterprise_ID" IS 'Identifier linking the batch to the enterprise or business unit.';
COMMENT ON COLUMN cla.tbl_batchnumber."CreatedDateTime" IS 'Timestamp for when the batch number record was created.';
COMMENT ON COLUMN cla.tbl_batchnumber."UpdatedDateTime" IS 'Timestamp for the most recent update to the batch number record.';

CREATE TABLE IF NOT EXISTS cla.tbl_breed (
    "Breed_ID" smallint NOT NULL,
    "Species_ID" smallint NOT NULL,
    "BreedName" varchar(50) NOT NULL,
    "IsCrossBreed" boolean NOT NULL,
    "IsPrimary" boolean NOT NULL,
    "BCMSBreedCode" varchar(10),
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Breed_ID")
);
COMMENT ON COLUMN cla.tbl_breed."Breed_ID" IS 'Primary key identifying a breed entry.';
COMMENT ON COLUMN cla.tbl_breed."Species_ID" IS 'Foreign key linking the breed to the corresponding species.';
COMMENT ON COLUMN cla.tbl_breed."BreedName" IS 'The name of the breed, e.g., ''Aberblack'', ''Galway''.';
COMMENT ON COLUMN cla.tbl_breed."IsCrossBreed" IS 'Boolean field indicating whether the breed is a crossbreed (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_breed."IsPrimary" IS 'Flag indicating if the breed is a primary breed for the species.';
COMMENT ON COLUMN cla.tbl_breed."BCMSBreedCode" IS 'Code assigned by the British Cattle Movement Service (BCMS) for the breed.';

CREATE TABLE IF NOT EXISTS cla.tbl_contact (
    "LTATUser_ID" int NOT NULL,
    "aspnetUserID" uuid NOT NULL,
    "Title_ID" smallint,
    "FirstName" varchar(32),
    "LastName" varchar(255),
    "MiddleName" varchar(32),
    "Mobile" varchar(35),
    "Telephone" varchar(35),
    "PropertyName" varchar(255),
    "Address1" varchar(128),
    "Address2" varchar(128),
    "Town" varchar(50),
    "County_ID" smallint,
    "Postcode" varchar(10),
    "PartyId" varchar(8),
    "SAMEmail" varchar(256),
    "SingleBusinessIdentifier" varchar(80),
    "ParentLTATUser_ID" int,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("LTATUser_ID")
);
COMMENT ON COLUMN cla.tbl_contact."LTATUser_ID" IS 'Foreign key linking to the user responsible for the contact.';
COMMENT ON COLUMN cla.tbl_contact."aspnetUserID" IS 'Foreign key referencing the user ID in the ASP.NET identity framework.';
COMMENT ON COLUMN cla.tbl_contact."Title_ID" IS 'Foreign key linking to the title of the contact (e.g., Mr., Mrs., Dr.).';
COMMENT ON COLUMN cla.tbl_contact."FirstName" IS 'The first name of the contact person.';
COMMENT ON COLUMN cla.tbl_contact."LastName" IS 'The last name of the contact person.';
COMMENT ON COLUMN cla.tbl_contact."MiddleName" IS 'The middle name of the contact person, if applicable.';
COMMENT ON COLUMN cla.tbl_contact."Mobile" IS 'Mobile phone number of the contact person.';
COMMENT ON COLUMN cla.tbl_contact."Telephone" IS 'Landline or secondary contact number.';
COMMENT ON COLUMN cla.tbl_contact."PropertyName" IS 'Name of the property associated with the contact, if applicable.';
COMMENT ON COLUMN cla.tbl_contact."Address1" IS 'The first line of the contact''s postal address, typically including house number and street name.';
COMMENT ON COLUMN cla.tbl_contact."Address2" IS 'The second line of the postal address, for additional location details such as neighborhood or estate.';
COMMENT ON COLUMN cla.tbl_contact."Town" IS 'The town or city where the contact is located.';
COMMENT ON COLUMN cla.tbl_contact."County_ID" IS 'Identifier referencing the county or administrative region of the contact''s address.';
COMMENT ON COLUMN cla.tbl_contact."Postcode" IS 'The postal code associated with the contact''s address.';
COMMENT ON COLUMN cla.tbl_contact."PartyId" IS 'Unique identifier for the party or entity associated with the contact.';
COMMENT ON COLUMN cla.tbl_contact."SAMEmail" IS 'The email address for Secure Access Management (SAM) associated with the contact.';
COMMENT ON COLUMN cla.tbl_contact."SingleBusinessIdentifier" IS 'Unique identifier for the business entity linked to the contact, used for business-specific reporting and compliance.';
COMMENT ON COLUMN cla.tbl_contact."ParentLTATUser_ID" IS 'Foreign key referencing the parent LTAT user (if applicable) associated with this contact.';
COMMENT ON COLUMN cla.tbl_contact."CreatedDateTime" IS 'Timestamp for when the contact record was created.';
COMMENT ON COLUMN cla.tbl_contact."UpdatedDateTime" IS 'Timestamp for the most recent update to the contact record.';

CREATE TABLE IF NOT EXISTS cla.tbl_county (
    "County_ID" smallint NOT NULL,
    "CountyName" varchar(50) NOT NULL,
    "DevolvedArea_ID" smallint,
    "Country_ID" smallint,
    "Location" text,
    "Region_ID" smallint,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("County_ID")
);


CREATE TABLE IF NOT EXISTS cla.tbl_device (
    "Device_ID" int NOT NULL,
    "LTAT_ID" varchar(16),
    "RFID" char(16),
    "BatchNumber_ID" int,
    "Freezebrand" int,
    "IsLTATDevice" boolean NOT NULL,
    "IsActive" boolean NOT NULL,
    "Recovered" boolean NOT NULL,
    "Killed" boolean NOT NULL,
    "LT" boolean NOT NULL,
    "StatusSet" boolean NOT NULL,
    "Replaced" boolean NOT NULL,
    "Replacing" boolean NOT NULL,
    "DeviceAttributesYearPartition" smallint NOT NULL,
    "Species_ID" smallint NOT NULL,
    "Enterprise_ID" int NOT NULL,
    "WithdrawnDate" timestamp without time zone,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Device_ID")
);
COMMENT ON COLUMN cla.tbl_device."Device_ID" IS 'Primary key uniquely identifying a device, such as an ear tag or electronic identifier.';
COMMENT ON COLUMN cla.tbl_device."LTAT_ID" IS 'Identifier for the Livestock Track and Trace (LTAT) system associated with the device.';
COMMENT ON COLUMN cla.tbl_device."RFID" IS 'Radio Frequency Identification (RFID) code linked to the device, enabling electronic tracking.';
COMMENT ON COLUMN cla.tbl_device."BatchNumber_ID" IS 'Foreign key referencing the batch number the device is associated with.';
COMMENT ON COLUMN cla.tbl_device."Freezebrand" IS 'Any freeze branding information associated with the device or animal.';
COMMENT ON COLUMN cla.tbl_device."IsLTATDevice" IS 'Boolean field indicating if the device is registered with the LTAT system (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_device."IsActive" IS 'Boolean field indicating if the device is currently active and in use (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_device."Recovered" IS 'Boolean field indicating if the device has been recovered after being lost or misplaced.';
COMMENT ON COLUMN cla.tbl_device."Killed" IS 'Boolean field indicating if the device has been deactivated or marked as no longer in use.';
COMMENT ON COLUMN cla.tbl_device."LT" IS 'Additional livestock tracking (LT) data or metadata linked to the device.';
COMMENT ON COLUMN cla.tbl_device."StatusSet" IS 'Set of status indicators for the device, such as active, inactive, or pending verification.';
COMMENT ON COLUMN cla.tbl_device."Replaced" IS 'Boolean field indicating if the device has been replaced by a newer one.';
COMMENT ON COLUMN cla.tbl_device."Replacing" IS 'Identifier referencing the device replacing this one, if applicable.';
COMMENT ON COLUMN cla.tbl_device."DeviceAttributesYearPartition" IS 'Logical year-based partitioning for the device attributes data.';
COMMENT ON COLUMN cla.tbl_device."Species_ID" IS 'Foreign key linking the device to the species it is used for (e.g., sheep, goats).';
COMMENT ON COLUMN cla.tbl_device."Enterprise_ID" IS 'Identifier linking the device to the enterprise or business responsible for its use.';
COMMENT ON COLUMN cla.tbl_device."WithdrawnDate" IS 'Date the device was withdrawn from use.';
COMMENT ON COLUMN cla.tbl_device."CreatedDateTime" IS 'Timestamp for when the device record was created.';
COMMENT ON COLUMN cla.tbl_device."UpdatedDateTime" IS 'Timestamp for the most recent update to the device record.';

CREATE TABLE IF NOT EXISTS cla.tbl_deviceattributes (
    "Device_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "IssuedPic_ID" int NOT NULL,
    "DeviceManufacturer_ID" smallint,
    "DeviceType_ID" smallint NOT NULL,
    "DeviceColour_ID" smallint NOT NULL,
    "DespatchDate" timestamp without time zone NOT NULL,
    "ReplacementDate" timestamp without time zone NOT NULL,
    "ReplacementDevice_ID" int,
    "ReplacementRequest_ID" int,
    "ReplacementReason_ID" smallint,
    "ReplacingDate" timestamp without time zone NOT NULL,
    "ReplacingDevice_ID" int,
    "ReplacingRequest_ID" int,
    "YearPartition" smallint NOT NULL,
    "TagManufacture_ID" int,
    "PurgeRequest_ID" int,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Device_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_deviceattributes."Device_ID" IS 'Foreign key referencing the unique identifier of the associated device in the Tbl_Device table.';
COMMENT ON COLUMN cla.tbl_deviceattributes."Request_ID" IS 'Foreign key referencing the request that initiated the device attribute creation or update.';
COMMENT ON COLUMN cla.tbl_deviceattributes."IssuedPic_ID" IS 'Identifier for the holding (Pic_ID) where the device was issued.';
COMMENT ON COLUMN cla.tbl_deviceattributes."DeviceManufacturer_ID" IS 'Identifier for the manufacturer of the device.';
COMMENT ON COLUMN cla.tbl_deviceattributes."DeviceType_ID" IS 'Identifier for the type of device (e.g., ear tag, RFID).';
COMMENT ON COLUMN cla.tbl_deviceattributes."DeviceColour_ID" IS 'Identifier for the color of the device, if applicable (e.g., color-coded tags).';
COMMENT ON COLUMN cla.tbl_deviceattributes."DespatchDate" IS 'Date the device was dispatched to the associated holding or user.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacementDate" IS 'Date the device was replaced with another one.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacementDevice_ID" IS 'Identifier for the device that replaced this one.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacementRequest_ID" IS 'Identifier for the request associated with replacing this device.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacementReason_ID" IS 'Identifier for the reason associated with replacing this device.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacingDate" IS 'Date when this device was used to replace another.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacingDevice_ID" IS 'Identifier for the device being replaced by this one.';
COMMENT ON COLUMN cla.tbl_deviceattributes."ReplacingRequest_ID" IS 'Request ID associated with the replacing action for this device.';
COMMENT ON COLUMN cla.tbl_deviceattributes."YearPartition" IS 'Logical partitioning by year for efficient data management and reporting.';
COMMENT ON COLUMN cla.tbl_deviceattributes."TagManufacture_ID" IS 'Identifier for the manufacturer responsible for creating the tag or device.';
COMMENT ON COLUMN cla.tbl_deviceattributes."PurgeRequest_ID" IS 'Identifier for the request that marked this device for removal or purging from the system.';
COMMENT ON COLUMN cla.tbl_deviceattributes."CreatedDateTime" IS 'Timestamp indicating when the device attributes record was created.';
COMMENT ON COLUMN cla.tbl_deviceattributes."UpdatedDateTime" IS 'Timestamp for the most recent update to the device attributes record.';

CREATE TABLE IF NOT EXISTS cla.tbl_devicelocation (
    "DeviceLocation_ID" bigint NOT NULL,
    "Titem_ID" int,
    "Device_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "Movement_ID" int,
    "Pic_ID" int NOT NULL,
    "FromDate" timestamp without time zone NOT NULL,
    "DeviceLocationTo_ID" bigint,
    "ToDate" timestamp without time zone NOT NULL,
    "PicTo_ID" int,
    "MovementTo_ID" int,
    "YearPartition" smallint NOT NULL,
    "CrDate" timestamp without time zone NOT NULL,
    "MovementYearPartition" smallint,
    "MovementToYearPartition" smallint,
    "ActualFromDate" timestamp without time zone,
    "ActualToDate" timestamp without time zone,
    "Species_ID" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("DeviceLocation_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_devicelocation."DeviceLocation_ID" IS 'Primary key uniquely identifying a device location entry.';
COMMENT ON COLUMN cla.tbl_devicelocation."Titem_ID" IS 'Foreign key linking to the livestock item associated with the device location.';
COMMENT ON COLUMN cla.tbl_devicelocation."Device_ID" IS 'Foreign key linking to the device associated with the location.';
COMMENT ON COLUMN cla.tbl_devicelocation."Request_ID" IS 'Identifier for the request that created or updated the device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation."Movement_ID" IS 'Foreign key linking to the movement associated with this device location.';
COMMENT ON COLUMN cla.tbl_devicelocation."Pic_ID" IS 'Identifier for the premises (Pic_ID) associated with the device location.';
COMMENT ON COLUMN cla.tbl_devicelocation."FromDate" IS 'The start date of the device''s presence at this location.';
COMMENT ON COLUMN cla.tbl_devicelocation."DeviceLocationTo_ID" IS 'Identifier for the next device location if this is part of a sequence.';
COMMENT ON COLUMN cla.tbl_devicelocation."ToDate" IS 'The end date of the device''s presence at this location.';
COMMENT ON COLUMN cla.tbl_devicelocation."PicTo_ID" IS 'Identifier for the destination premises (Pic_ID) for this device location.';
COMMENT ON COLUMN cla.tbl_devicelocation."MovementTo_ID" IS 'Identifier for the movement record associated with the next device location.';
COMMENT ON COLUMN cla.tbl_devicelocation."YearPartition" IS 'Logical partitioning of the location data by year for performance optimization.';
COMMENT ON COLUMN cla.tbl_devicelocation."CrDate" IS 'Creation date of the device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation."MovementYearPartition" IS 'Year partition associated with the movement data for this location.';
COMMENT ON COLUMN cla.tbl_devicelocation."MovementToYearPartition" IS 'Year partition associated with the next movement data for this location.';
COMMENT ON COLUMN cla.tbl_devicelocation."ActualFromDate" IS 'Actual date the device was recorded as being at the location.';
COMMENT ON COLUMN cla.tbl_devicelocation."ActualToDate" IS 'Actual date the device was recorded as leaving the location.';
COMMENT ON COLUMN cla.tbl_devicelocation."Species_ID" IS 'Identifier for the species associated with the device at this location.';
COMMENT ON COLUMN cla.tbl_devicelocation."CreatedDateTime" IS 'Timestamp for when the device location record was created.';
COMMENT ON COLUMN cla.tbl_devicelocation."UpdatedDateTime" IS 'Timestamp for the most recent update to the device location record.';

CREATE TABLE IF NOT EXISTS cla.tbl_devicelocationduplicate (
    "DeviceLocation_ID" bigint NOT NULL,
    "Movement_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "CrDate" timestamp without time zone NOT NULL,
    "MovementYearPartition" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("DeviceLocation_ID", "Movement_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."DeviceLocation_ID" IS 'Primary key identifying a duplicate entry of a device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."Movement_ID" IS 'Foreign key linking to the movement associated with the duplicate device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."YearPartition" IS 'Year partition for efficient querying of duplicate device location records.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."CrDate" IS 'The creation date of the duplicate device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."MovementYearPartition" IS 'Year partition for the movement associated with the duplicate device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."CreatedDateTime" IS 'Timestamp for when the duplicate device location record was created.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate."UpdatedDateTime" IS 'Timestamp for the most recent update to the duplicate device location record.';

CREATE TABLE IF NOT EXISTS cla.tbl_devicelocationduplicate_undo (
    "DeviceLocation_ID" bigint NOT NULL,
    "Movement_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "CrDate" timestamp without time zone NOT NULL,
    "RequestUndo_ID" int NOT NULL,
    "MovementYearPartition" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("DeviceLocation_ID", "Movement_ID", "YearPartition", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."DeviceLocation_ID" IS 'Identifier linking to the original duplicate device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."Movement_ID" IS 'Identifier for the movement associated with the duplicate device location undo record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."YearPartition" IS 'Year partition for the original duplicate device location undo record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."CrDate" IS 'Creation date of the original duplicate device location record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation for the duplicate record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."MovementYearPartition" IS 'Year partition for the movement in the original duplicate device location undo record.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."CreatedDateTime" IS 'Timestamp for when the duplicate device location undo record was created.';
COMMENT ON COLUMN cla.tbl_devicelocationduplicate_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the duplicate device location undo record.';

CREATE TABLE IF NOT EXISTS cla.tbl_devicelocation_undo (
    "DeviceLocation_ID" bigint NOT NULL,
    "Titem_ID" int,
    "Device_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "Movement_ID" int,
    "Pic_ID" int NOT NULL,
    "FromDate" timestamp without time zone NOT NULL,
    "DeviceLocationTo_ID" bigint,
    "ToDate" timestamp without time zone NOT NULL,
    "PicTo_ID" int,
    "MovementTo_ID" int,
    "YearPartition" smallint NOT NULL,
    "CrDate" timestamp without time zone NOT NULL,
    "RequestUndo_ID" int NOT NULL,
    "UndoIsDelete" boolean NOT NULL,
    "MovementYearPartition" smallint,
    "MovementToYearPartition" smallint,
    "ActualFromDate" timestamp without time zone,
    "ActualToDate" timestamp without time zone,
    "Species_ID" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("DeviceLocation_ID", "Request_ID", "YearPartition", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_devicelocation_undo."DeviceLocation_ID" IS 'Identifier linking to the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Titem_ID" IS 'Identifier for the livestock item from the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Device_ID" IS 'Identifier for the device from the original location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Request_ID" IS 'Identifier for the request associated with the undo operation.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Movement_ID" IS 'Identifier for the movement record from the original device location entry.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Pic_ID" IS 'Identifier for the premises (Pic_ID) from the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."FromDate" IS 'Start date of the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."DeviceLocationTo_ID" IS 'Identifier for the next location in the sequence from the original record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."ToDate" IS 'End date of the device’s presence at the location from the original record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."PicTo_ID" IS 'Identifier for the destination premises from the original record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."MovementTo_ID" IS 'Identifier for the movement to the next location from the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."YearPartition" IS 'Year-based partitioning for the original device location record for efficient data management.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."CrDate" IS 'The creation date of the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."UndoIsDelete" IS 'Boolean field indicating if the undo operation involved a deletion (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."MovementYearPartition" IS 'Year partition associated with the original movement data.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."MovementToYearPartition" IS 'Year partition for the movement to the next location in the original record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."ActualFromDate" IS 'Actual start date of the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."ActualToDate" IS 'Actual end date of the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."Species_ID" IS 'Identifier for the species associated with the original device location record.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."CreatedDateTime" IS 'Timestamp for when the undo record was created.';
COMMENT ON COLUMN cla.tbl_devicelocation_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';

CREATE TABLE IF NOT EXISTS cla.tbl_device_undo (
    "Device_ID" int NOT NULL,
    "LTAT_ID" varchar(16),
    "RFID" char(16),
    "BatchNumber_ID" int,
    "Freezebrand" int,
    "IsLTATDevice" boolean NOT NULL,
    "IsActive" boolean NOT NULL,
    "Recovered" boolean NOT NULL,
    "Killed" boolean NOT NULL,
    "LT" boolean NOT NULL,
    "StatusSet" boolean NOT NULL,
    "Replaced" boolean NOT NULL,
    "Replacing" boolean NOT NULL,
    "DeviceAttributesYearPartition" smallint NOT NULL,
    "RequestUndo_ID" int NOT NULL,
    "UndoIsDelete" boolean NOT NULL,
    "Species_ID" smallint,
    "Enterprise_ID" int NOT NULL,
    "WithdrawnDate" timestamp without time zone,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Device_ID", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_device_undo."Device_ID" IS 'Identifier referencing the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."LTAT_ID" IS 'Livestock Track and Trace (LTAT) identifier from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."RFID" IS 'RFID associated with the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."BatchNumber_ID" IS 'Batch number ID from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."Freezebrand" IS 'Freeze branding details from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."IsLTATDevice" IS 'Indicates if the original device was part of the LTAT system.';
COMMENT ON COLUMN cla.tbl_device_undo."IsActive" IS 'Indicates if the original device was active at the time of the undo operation.';
COMMENT ON COLUMN cla.tbl_device_undo."Recovered" IS 'Indicates if the original device was marked as recovered before the undo operation.';
COMMENT ON COLUMN cla.tbl_device_undo."Killed" IS 'Indicates if the original device was marked as killed before the undo operation.';
COMMENT ON COLUMN cla.tbl_device_undo."LT" IS 'Livestock tracking data from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."StatusSet" IS 'Set of statuses from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."Replaced" IS 'Indicates if the original device was marked as replaced.';
COMMENT ON COLUMN cla.tbl_device_undo."Replacing" IS 'Reference to the replacing device from the original record.';
COMMENT ON COLUMN cla.tbl_device_undo."DeviceAttributesYearPartition" IS 'Year partition data from the original device attributes record.';
COMMENT ON COLUMN cla.tbl_device_undo."RequestUndo_ID" IS 'Identifier referencing the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_device_undo."UndoIsDelete" IS 'Boolean field indicating whether the undo operation involved a deletion (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_device_undo."Species_ID" IS 'Species identifier from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."Enterprise_ID" IS 'Enterprise identifier from the original device record.';
COMMENT ON COLUMN cla.tbl_device_undo."WithdrawnDate" IS 'Date the original device was withdrawn from use.';
COMMENT ON COLUMN cla.tbl_device_undo."CreatedDateTime" IS 'Timestamp for when the undo record was created.';
COMMENT ON COLUMN cla.tbl_device_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';

CREATE TABLE IF NOT EXISTS cla.tbl_devolvedarea (
    "DevolvedArea_ID" smallint NOT NULL,
    "AreaName" varchar(20) NOT NULL,
    "Country_ID" smallint NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("DevolvedArea_ID")
);


CREATE TABLE IF NOT EXISTS cla.tbl_gender (
    "Gender_ID" smallint NOT NULL,
    "GenderName" varchar(30) NOT NULL,
    "GenderCode" varchar(1) NOT NULL,
    "GenderTypeCode" varchar(10) NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Gender_ID")
);
COMMENT ON COLUMN cla.tbl_gender."Gender_ID" IS 'Primary key uniquely identifying a gender entry.';
COMMENT ON COLUMN cla.tbl_gender."GenderName" IS 'The name of the gender, e.g., ''Male'', ''Female''.';
COMMENT ON COLUMN cla.tbl_gender."GenderCode" IS 'A code representing the gender, used for classification and data management.';
COMMENT ON COLUMN cla.tbl_gender."GenderTypeCode" IS 'An additional code used to classify the type of gender for specific use cases.';

CREATE TABLE IF NOT EXISTS cla.tbl_ltaterror (
    "LTATError_ID" smallint NOT NULL,
    "LTATErrorType_ID" smallint NOT NULL,
    "LTATError" varchar(256) NOT NULL,
    "LTATErrorDescription" varchar(256) NOT NULL,
    "UserReturn" boolean NOT NULL,
    "OtherReturn" boolean NOT NULL,
    "BusinessRule_ID" smallint,
    "IsException" boolean NOT NULL,
    "OverrideWithMessage" boolean NOT NULL,
    "OverrideMessage" varchar(256),
    "OverrideProcessingFlag_ID" smallint,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("LTATError_ID")
);
COMMENT ON COLUMN cla.tbl_ltaterror."LTATError_ID" IS 'Primary key identifying a Livestock Track and Trace (LTAT) error record.';
COMMENT ON COLUMN cla.tbl_ltaterror."LTATErrorType_ID" IS 'Identifier for the type of error associated with the LTAT record.';
COMMENT ON COLUMN cla.tbl_ltaterror."LTATError" IS 'Description or name of the error encountered in the LTAT system.';
COMMENT ON COLUMN cla.tbl_ltaterror."LTATErrorDescription" IS 'Detailed description of the LTAT error, providing context and guidance for resolution.';
COMMENT ON COLUMN cla.tbl_ltaterror."UserReturn" IS 'Indicator of whether the error was returned to the user for resolution (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_ltaterror."OtherReturn" IS 'Indicator of whether the error was routed to other systems or stakeholders for resolution.';
COMMENT ON COLUMN cla.tbl_ltaterror."BusinessRule_ID" IS 'Identifier linking the error to a specific business rule violated.';
COMMENT ON COLUMN cla.tbl_ltaterror."IsException" IS 'Boolean field indicating if the error is classified as an exception (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_ltaterror."OverrideWithMessage" IS 'Boolean field indicating if the error can be overridden with a specific message (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_ltaterror."OverrideMessage" IS 'Custom message used to override the error when applicable.';
COMMENT ON COLUMN cla.tbl_ltaterror."OverrideProcessingFlag_ID" IS 'Identifier for a processing flag linked to overriding the error.';

CREATE TABLE IF NOT EXISTS cla.tbl_ltaterrorinfo (
    "LTATErrorInfo_ID" int NOT NULL,
    "LTATError_ID" smallint NOT NULL,
    "Request_ID" int NOT NULL,
    "DataRow" int,
    "DataInfo" varchar(4000),
    "ErrorTarget" varchar(500),
    "YearPartition" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("LTATErrorInfo_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."LTATErrorInfo_ID" IS 'Primary key uniquely identifying an LTAT error information record.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."LTATError_ID" IS 'Foreign key linking to the associated LTAT error record.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."Request_ID" IS 'Foreign key linking to the request associated with the LTAT error.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."DataRow" IS 'The row of data that caused or is linked to the error.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."DataInfo" IS 'Additional information about the data causing the error.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."ErrorTarget" IS 'Target entity or field affected by the error.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."YearPartition" IS 'Logical partitioning of the error information record by year for efficient data management.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."CreatedDateTime" IS 'Timestamp for when the LTAT error information record was created.';
COMMENT ON COLUMN cla.tbl_ltaterrorinfo."UpdatedDateTime" IS 'Timestamp for the most recent update to the LTAT error information record.';

CREATE TABLE IF NOT EXISTS cla.tbl_ltaterrortype (
    "LTATErrorType_ID" smallint NOT NULL,
    "LTATErrorType" varchar(50) NOT NULL,
    "LTATErrorLevel" smallint NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("LTATErrorType_ID")
);
COMMENT ON COLUMN cla.tbl_ltaterrortype."LTATErrorType_ID" IS 'Primary key uniquely identifying an LTAT error type entry.';
COMMENT ON COLUMN cla.tbl_ltaterrortype."LTATErrorType" IS 'Name or description of the error type within the LTAT system.';
COMMENT ON COLUMN cla.tbl_ltaterrortype."LTATErrorLevel" IS 'Severity level of the error, indicating its potential impact on system operations or compliance.';

CREATE TABLE IF NOT EXISTS cla.tbl_movement (
    "Movement_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "DepartureDate" timestamp without time zone NOT NULL,
    "ArrivalDate" timestamp without time zone NOT NULL,
    "FromPic_ID" int NOT NULL,
    "UserPic_ID" int,
    "ToPic_ID" int NOT NULL,
    "Species_ID" smallint NOT NULL,
    "NumberOfAnimals" int NOT NULL,
    "IsSystem" boolean NOT NULL,
    "MovementQuality_ID" int,
    "YearPartition" smallint NOT NULL,
    "TransferDate" timestamp without time zone NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Movement_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_movement."Movement_ID" IS 'Primary key uniquely identifying a livestock movement record.';
COMMENT ON COLUMN cla.tbl_movement."Request_ID" IS 'Foreign key referencing the request that initiated the movement record.';
COMMENT ON COLUMN cla.tbl_movement."DepartureDate" IS 'Date when the livestock departed the source holding or premises.';
COMMENT ON COLUMN cla.tbl_movement."ArrivalDate" IS 'Date when the livestock arrived at the destination holding or premises.';
COMMENT ON COLUMN cla.tbl_movement."FromPic_ID" IS 'Identifier for the premises (Pic_ID) from which the livestock departed.';
COMMENT ON COLUMN cla.tbl_movement."UserPic_ID" IS 'Identifier for the premises associated with the user initiating the movement.';
COMMENT ON COLUMN cla.tbl_movement."ToPic_ID" IS 'Identifier for the premises (Pic_ID) to which the livestock was moved.';
COMMENT ON COLUMN cla.tbl_movement."Species_ID" IS 'Identifier for the species involved in the movement (e.g., sheep, goats).';
COMMENT ON COLUMN cla.tbl_movement."NumberOfAnimals" IS 'The total number of animals involved in the movement.';
COMMENT ON COLUMN cla.tbl_movement."IsSystem" IS 'Boolean field indicating whether the movement record was system-generated (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movement."MovementQuality_ID" IS 'Identifier for the quality or status of the movement record (e.g., approved, pending).';
COMMENT ON COLUMN cla.tbl_movement."YearPartition" IS 'Logical year-based partitioning for efficient data storage and retrieval.';
COMMENT ON COLUMN cla.tbl_movement."TransferDate" IS 'Date when ownership or keepership of the livestock was transferred as part of the movement.';
COMMENT ON COLUMN cla.tbl_movement."CreatedDateTime" IS 'Timestamp for when the movement record was created.';
COMMENT ON COLUMN cla.tbl_movement."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementattributes (
    "Movement_ID" int NOT NULL,
    "DocumentType_ID" smallint,
    "DocumentID" varchar(32),
    "DeviceCount" int NOT NULL,
    "SaleDate" timestamp without time zone,
    "SaleID" varchar(32),
    "BuyingAgentName" varchar(128),
    "BuyingAgentPIC" varchar(14),
    "SellingAgentName" varchar(128),
    "SellingAgentPIC" varchar(14),
    "TransportHaulierName" varchar(50),
    "TransportVehicleRegistrationNo" varchar(16),
    "UserLinkedFromPic" boolean NOT NULL,
    "UserLinkedToPic" boolean NOT NULL,
    "MovementDocumentRef_ID" int,
    "MovementDocumentYearPartition" smallint,
    "YearPartition" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "Comment" varchar(125),
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Movement_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_movementattributes."Movement_ID" IS 'Foreign key linking to the movement record this attributes entry is associated with.';
COMMENT ON COLUMN cla.tbl_movementattributes."DocumentType_ID" IS 'Identifier for the type of document associated with the movement (e.g., movement certificate).';
COMMENT ON COLUMN cla.tbl_movementattributes."DocumentID" IS 'Identifier for the document linked to the movement.';
COMMENT ON COLUMN cla.tbl_movementattributes."DeviceCount" IS 'Total number of devices (e.g., tags) associated with the movement.';
COMMENT ON COLUMN cla.tbl_movementattributes."SaleDate" IS 'Date of sale associated with the movement, if applicable.';
COMMENT ON COLUMN cla.tbl_movementattributes."SaleID" IS 'Identifier for the sale linked to the movement.';
COMMENT ON COLUMN cla.tbl_movementattributes."BuyingAgentName" IS 'Name of the buying agent involved in the movement.';
COMMENT ON COLUMN cla.tbl_movementattributes."BuyingAgentPIC" IS 'Premises (Pic_ID) associated with the buying agent.';
COMMENT ON COLUMN cla.tbl_movementattributes."SellingAgentName" IS 'Name of the selling agent involved in the movement.';
COMMENT ON COLUMN cla.tbl_movementattributes."SellingAgentPIC" IS 'Premises (Pic_ID) associated with the selling agent.';
COMMENT ON COLUMN cla.tbl_movementattributes."TransportHaulierName" IS 'Name of the haulier transporting the livestock.';
COMMENT ON COLUMN cla.tbl_movementattributes."TransportVehicleRegistrationNo" IS 'Vehicle registration number of the transporter.';
COMMENT ON COLUMN cla.tbl_movementattributes."UserLinkedFromPic" IS 'User-associated premises (Pic_ID) from which the livestock originated.';
COMMENT ON COLUMN cla.tbl_movementattributes."UserLinkedToPic" IS 'User-associated premises (Pic_ID) to which the livestock is being transported.';
COMMENT ON COLUMN cla.tbl_movementattributes."MovementDocumentRef_ID" IS 'Reference identifier for the movement document associated with the attributes record.';
COMMENT ON COLUMN cla.tbl_movementattributes."MovementDocumentYearPartition" IS 'Logical year-based partitioning for the associated movement document for efficient data management.';
COMMENT ON COLUMN cla.tbl_movementattributes."YearPartition" IS 'Logical partitioning of the movement attributes record by year.';
COMMENT ON COLUMN cla.tbl_movementattributes."CreatedDateTime" IS 'Timestamp for when the movement attributes record was created.';
COMMENT ON COLUMN cla.tbl_movementattributes."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement attributes record.';
COMMENT ON COLUMN cla.tbl_movementattributes."Comment" IS 'Additional comments or notes associated with the movement attributes record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementdocumentcontact (
    "MovementDocumentContact_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "Title_ID" smallint,
    "FirstName" varchar(32),
    "LastName" varchar(32),
    "PropertyName" varchar(32),
    "Address1" varchar(128),
    "Address2" varchar(128),
    "Town" varchar(50),
    "County_ID" smallint,
    "Postcode" varchar(10),
    "TelephoneNumber" varchar(13),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("MovementDocumentContact_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."MovementDocumentContact_ID" IS 'Primary key uniquely identifying a movement document contact record.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."YearPartition" IS 'Logical partitioning of the contact data by year for performance optimization.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."Title_ID" IS 'Foreign key linking to the title of the contact (e.g., Mr., Mrs., Dr.).';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."FirstName" IS 'The first name of the contact person associated with the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."LastName" IS 'The last name of the contact person associated with the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."PropertyName" IS 'The property name associated with the contact person, if applicable.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."Address1" IS 'The first line of the postal address for the contact person.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."Address2" IS 'The second line of the postal address, providing additional address details.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."Town" IS 'The town or city where the contact person is located.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."County_ID" IS 'Identifier referencing the county or administrative region of the contact person’s address.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."Postcode" IS 'The postal code associated with the contact person’s address.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."TelephoneNumber" IS 'Contact telephone number for the individual or entity associated with the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."CreatedDateTime" IS 'Timestamp for when the movement document contact record was created.';
COMMENT ON COLUMN cla.tbl_movementdocumentcontact."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement document contact record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementdocumentnoncompliantdevice (
    "MovementDocumentNonCompliantDevice_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "MovementDocumentVersion_ID" int NOT NULL,
    "Device_ID" int,
    "LTAT_ID" varchar(16),
    "RFID" varchar(16),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("MovementDocumentNonCompliantDevice_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."MovementDocumentNonCompliantDevice_ID" IS 'Primary key uniquely identifying a non-compliant device entry associated with a movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."YearPartition" IS 'Year partitioning for efficient querying of non-compliant device data.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."MovementDocumentVersion_ID" IS 'Identifier for the version of the movement document associated with the non-compliant device.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."Device_ID" IS 'Foreign key referencing the associated device that is marked as non-compliant.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."LTAT_ID" IS 'Identifier for the Livestock Track and Trace (LTAT) system associated with the non-compliant device.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."RFID" IS 'RFID tag associated with the non-compliant device.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."CreatedDateTime" IS 'Timestamp for when the non-compliant device record was created.';
COMMENT ON COLUMN cla.tbl_movementdocumentnoncompliantdevice."UpdatedDateTime" IS 'Timestamp for the most recent update to the non-compliant device record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementdocumenttransportertype (
    "MovementDocumentTransporterType_ID" smallint NOT NULL,
    "TransporterType" varchar(50) NOT NULL,
    "IsActive" boolean NOT NULL,
    "TransporterTypeCode" varchar(20) NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("MovementDocumentTransporterType_ID")
);
COMMENT ON COLUMN cla.tbl_movementdocumenttransportertype."MovementDocumentTransporterType_ID" IS 'Primary key uniquely identifying a transporter type record associated with movement documents.';
COMMENT ON COLUMN cla.tbl_movementdocumenttransportertype."TransporterType" IS 'Description of the type of transporter (e.g., lorry, trailer).';
COMMENT ON COLUMN cla.tbl_movementdocumenttransportertype."IsActive" IS 'Boolean field indicating whether the transporter type is currently active in the system (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumenttransportertype."TransporterTypeCode" IS 'Code representing the transporter type for classification and data management.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementdocumentversion (
    "MovementDocumentVersion_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "MovementDocumentRef_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "VersionNumber" int NOT NULL,
    "IsActive" boolean NOT NULL,
    "DepartureKeeperContact_ID" int,
    "DepartureOwnerContact_ID" int,
    "DestinationKeeperContact_ID" int,
    "HasKeeperChanged" boolean,
    "AnimalTotal" int,
    "DepartureDateTime" timestamp without time zone NOT NULL,
    "LoadingDateTime" timestamp without time zone NOT NULL,
    "ExpectedDurationOfJourney" timestamp without time zone NOT NULL,
    "TransporterType_ID" smallint,
    "TransporterAuthNumber" varchar(50),
    "TransporterContact_ID" int,
    "TransporterHaulierName" varchar(50),
    "TransporterVehicleRegistrationNo" varchar(16),
    "TotalAnimalsReceived" int,
    "ArrivalDateTime" timestamp without time zone NOT NULL,
    "UnloadingDateTime" timestamp without time zone NOT NULL,
    "FCICompliant" boolean,
    "FCINonCompliantText" varchar(255),
    "FCIHoldingRestrictions" varchar(255),
    "FCIWithdrawalPeriodObservedText" varchar(255),
    "DeparturePicType_ID" smallint,
    "DestinationPicType_ID" smallint,
    "FCIWithdrawalPeriodsMet_ID" int,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "DepartureDateNULL" timestamp without time zone,
    "ArrivalDateNULL" timestamp without time zone,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("MovementDocumentVersion_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_movementdocumentversion."MovementDocumentVersion_ID" IS 'Primary key uniquely identifying a version of the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."YearPartition" IS 'Logical partitioning of the movement document version data by year for optimized performance.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."MovementDocumentRef_ID" IS 'Reference identifier linking the version to the associated movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."Request_ID" IS 'Foreign key linking to the request that initiated the movement document version.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."VersionNumber" IS 'Numeric indicator of the version of the movement document (e.g., 1 for the first version).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."IsActive" IS 'Boolean field indicating if this version of the movement document is active (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DepartureKeeperContact_ID" IS 'Identifier for the contact person responsible for the livestock at the point of departure.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DepartureOwnerContact_ID" IS 'Identifier for the owner of the livestock at the point of departure.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DestinationKeeperContact_ID" IS 'Identifier for the contact person responsible for the livestock at the destination.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."HasKeeperChanged" IS 'Boolean field indicating if the keeper of the livestock changed during the movement (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."AnimalTotal" IS 'Total number of animals recorded in this version of the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DepartureDateTime" IS 'Date and time when the livestock departed from the source premises.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."LoadingDateTime" IS 'Date and time when the livestock was loaded for transport.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."ExpectedDurationOfJourney" IS 'Estimated duration of the journey for the livestock movement.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TransporterType_ID" IS 'Identifier linking to the type of transporter used for the livestock movement.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TransporterAuthNumber" IS 'Authorization number of the transporter.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TransporterContact_ID" IS 'Identifier for the contact person or entity associated with the transporter.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TransporterHaulierName" IS 'Name of the haulier responsible for the livestock transport.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TransporterVehicleRegistrationNo" IS 'Registration number of the vehicle used for transporting the livestock.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."TotalAnimalsReceived" IS 'Total number of animals received at the destination premises as per the movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."ArrivalDateTime" IS 'Date and time when the livestock arrived at the destination premises.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."UnloadingDateTime" IS 'Date and time when the livestock was unloaded at the destination premises.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."FCICompliant" IS 'Boolean field indicating whether the movement complies with the Food Chain Information (FCI) requirements (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."FCINonCompliantText" IS 'Additional details or notes if the movement does not comply with FCI requirements.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."FCIHoldingRestrictions" IS 'Any holding-specific restrictions noted in the Food Chain Information.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."FCIWithdrawalPeriodObservedText" IS 'Notes regarding the withdrawal period observed for animals as part of the FCI compliance.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DeparturePicType_ID" IS 'Identifier for the type of premises at the point of departure.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DestinationPicType_ID" IS 'Identifier for the type of premises at the destination.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."FCIWithdrawalPeriodsMet_ID" IS 'Identifier indicating whether the withdrawal periods were met for FCI compliance.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."CreatedDateTime" IS 'Timestamp for when the movement document version record was created.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement document version record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."DepartureDateNULL" IS 'Boolean field indicating whether the departure date is null (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion."ArrivalDateNULL" IS 'Boolean field indicating whether the arrival date is null (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_movementdocumentversion_undo (
    "MovementDocumentVersion_ID" text,
    "YearPartition" text,
    "MovementDocumentRef_ID" text,
    "Request_ID" text,
    "VersionNumber" text,
    "IsActive" text,
    "DepartureKeeperContact_ID" text,
    "DepartureOwnerContact_ID" text,
    "DestinationKeeperContact_ID" text,
    "HasKeeperChanged" text,
    "AnimalTotal" text,
    "DepartureDateTime" text,
    "LoadingDateTime" text,
    "ExpectedDurationOfJourney" text,
    "TransporterType_ID" text,
    "TransporterAuthNumber" text,
    "TransporterContact_ID" text,
    "TransporterHaulierName" text,
    "TransporterVehicleRegistrationNo" text,
    "TotalAnimalsReceived" text,
    "ArrivalDateTime" text,
    "UnloadingDateTime" text,
    "FCICompliant" text,
    "FCINonCompliantText" text,
    "FCIHoldingRestrictions" text,
    "RequestUndo_ID" text,
    "UndoIsDelete" text,
    "FCIWithdrawalPeriodObservedText" text,
    "DeparturePicType_ID" text,
    "DestinationPicType_ID" text,
    "FCIWithdrawalPeriodsMet_ID" text,
    "CreatedDateTime" text,
    "UpdatedDateTime" text,
    "DepartureDateNULL" text,
    "ArrivalDateNULL" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B'
);
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."MovementDocumentVersion_ID" IS 'Identifier linking to the original movement document version record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."YearPartition" IS 'Logical partitioning of the undo record by year for efficient data management.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."MovementDocumentRef_ID" IS 'Reference identifier for the original movement document.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."Request_ID" IS 'Identifier for the request associated with undoing the movement document version.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."VersionNumber" IS 'Numeric identifier for the version of the movement document being undone.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."IsActive" IS 'Indicates whether the undo record is currently active (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DepartureKeeperContact_ID" IS 'Identifier for the contact responsible for the livestock at departure from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DepartureOwnerContact_ID" IS 'Identifier for the owner of the livestock at departure from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DestinationKeeperContact_ID" IS 'Identifier for the contact responsible for the livestock at the destination from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."HasKeeperChanged" IS 'Indicates if the keeper was changed in the original record (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."AnimalTotal" IS 'Total number of animals recorded in the original version.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DepartureDateTime" IS 'Departure date and time from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."LoadingDateTime" IS 'Loading date and time from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."ExpectedDurationOfJourney" IS 'Expected journey duration from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TransporterType_ID" IS 'Identifier for the transporter type in the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TransporterAuthNumber" IS 'Transporter authorization number from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TransporterContact_ID" IS 'Identifier for the transporter contact from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TransporterHaulierName" IS 'Name of the haulier from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TransporterVehicleRegistrationNo" IS 'Vehicle registration number of the transporter from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."TotalAnimalsReceived" IS 'Total number of animals received from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."ArrivalDateTime" IS 'Arrival date and time from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."UnloadingDateTime" IS 'Unloading date and time from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."FCICompliant" IS 'Indicates FCI compliance from the original record (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."FCINonCompliantText" IS 'Notes regarding FCI non-compliance from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."FCIHoldingRestrictions" IS 'Holding-specific restrictions from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."UndoIsDelete" IS 'Indicates if the undo operation involved deletion (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."FCIWithdrawalPeriodObservedText" IS 'Notes on withdrawal period observed from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DeparturePicType_ID" IS 'Identifier for the departure premises type from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DestinationPicType_ID" IS 'Identifier for the destination premises type from the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."FCIWithdrawalPeriodsMet_ID" IS 'Identifier indicating withdrawal periods met for FCI compliance in the original record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."CreatedDateTime" IS 'Timestamp for when the undo record was created.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."DepartureDateNULL" IS 'Indicates if the departure date is null in the original record (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementdocumentversion_undo."ArrivalDateNULL" IS 'Indicates if the arrival date is null in the original record (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_movementreviewstatus (
    "Movement_ID" int NOT NULL,
    "MovementYearPartition" smallint NOT NULL,
    "Pic_ID" int NOT NULL,
    "ReviewRequest_ID" int NOT NULL,
    "Accepted" boolean NOT NULL,
    "ArrivalDateTime" timestamp without time zone,
    "TotalAnimalsReceived" int,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "DepartureAnimalTotal" int,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Movement_ID", "MovementYearPartition", "Pic_ID")
);
COMMENT ON COLUMN cla.tbl_movementreviewstatus."Movement_ID" IS 'Identifier for the movement being reviewed.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."MovementYearPartition" IS 'Year partition for the movement data under review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."Pic_ID" IS 'Identifier for the premises (Pic_ID) involved in the movement review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."ReviewRequest_ID" IS 'Identifier for the request associated with reviewing the movement record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."Accepted" IS 'Boolean field indicating whether the movement review was accepted (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."ArrivalDateTime" IS 'Date and time when the livestock was confirmed to have arrived.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."TotalAnimalsReceived" IS 'Total number of animals confirmed as received during the movement review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."CreatedDateTime" IS 'Timestamp for when the movement review status record was created.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement review status record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus."DepartureAnimalTotal" IS 'Total number of animals recorded at the point of departure.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementreviewstatus_undo (
    "Movement_ID" int NOT NULL,
    "MovementYearPartition" smallint NOT NULL,
    "Pic_ID" int NOT NULL,
    "ReviewRequest_ID" int NOT NULL,
    "Accepted" boolean NOT NULL,
    "RequestUndo_ID" int NOT NULL,
    "ArrivalDateTime" timestamp without time zone,
    "TotalAnimalsReceived" int,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "DepartureAnimalTotal" int,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Movement_ID", "MovementYearPartition", "Pic_ID", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."Movement_ID" IS 'Identifier linking to the original movement record under review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."MovementYearPartition" IS 'Year partition data from the original movement record under review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."Pic_ID" IS 'Identifier for the premises (Pic_ID) in the original movement review record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."ReviewRequest_ID" IS 'Identifier for the request that initiated the undo operation for the movement review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."Accepted" IS 'Indicates if the original review was accepted (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."RequestUndo_ID" IS 'Identifier for the undo request associated with the original review.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."ArrivalDateTime" IS 'Arrival date and time from the original review record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."TotalAnimalsReceived" IS 'Total number of animals received from the original review record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."CreatedDateTime" IS 'Timestamp for when the undo record was created.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';
COMMENT ON COLUMN cla.tbl_movementreviewstatus_undo."DepartureAnimalTotal" IS 'Total number of animals at departure in the original review record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movement_undo (
    "Movement_ID" int NOT NULL,
    "Request_ID" int NOT NULL,
    "DepartureDate" timestamp without time zone NOT NULL,
    "ArrivalDate" timestamp without time zone NOT NULL,
    "FromPic_ID" int NOT NULL,
    "UserPic_ID" int,
    "ToPic_ID" int NOT NULL,
    "Species_ID" smallint NOT NULL,
    "NumberOfAnimals" int NOT NULL,
    "IsSystem" boolean NOT NULL,
    "MovementQuality_ID" int,
    "YearPartition" smallint NOT NULL,
    "RequestUndo_ID" int NOT NULL,
    "UndoIsDelete" boolean NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Movement_ID", "YearPartition", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_movement_undo."Movement_ID" IS 'Identifier linking to the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."Request_ID" IS 'Identifier for the request associated with undoing the movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."DepartureDate" IS 'Departure date from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."ArrivalDate" IS 'Arrival date from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."FromPic_ID" IS 'Source premises (Pic_ID) from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."UserPic_ID" IS 'User-associated premises from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."ToPic_ID" IS 'Destination premises (Pic_ID) from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."Species_ID" IS 'Identifier for the species in the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."NumberOfAnimals" IS 'Total number of animals from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."IsSystem" IS 'Indicates if the original movement was system-generated (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movement_undo."MovementQuality_ID" IS 'Quality or status identifier from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."YearPartition" IS 'Year partition data from the original movement record.';
COMMENT ON COLUMN cla.tbl_movement_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_movement_undo."UndoIsDelete" IS 'Boolean field indicating if the undo operation resulted in deletion (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_movement_undo."CreatedDateTime" IS 'Timestamp for when the undo record was created.';
COMMENT ON COLUMN cla.tbl_movement_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovement (
    "PaperMovement_ID" int NOT NULL,
    "ScannedDocument_ID" varchar(15) NOT NULL,
    "CreateRequest_ID" int NOT NULL,
    "LastModifyRequest_ID" int NOT NULL,
    "PaperMovementStatus_ID" smallint NOT NULL,
    "DateReceived" timestamp without time zone NOT NULL,
    "DocumentSource" varchar(50),
    "DocumentSourceFormat" varchar(50),
    "Species_ID" smallint,
    "DepartureHolding" varchar(50),
    "DepartureHoldingManualEntryAddress" text,
    "DepartureDate" timestamp without time zone,
    "DepartureDay" varchar(10),
    "DepartureMonth" varchar(10),
    "DepartureYear" varchar(10),
    "TransporterType_ID" smallint,
    "TransporterType" varchar(50),
    "TransporterAuthNumber" varchar(50),
    "TransporterHaulierName" varchar(50),
    "TransporterVehicleRegistrationNo" varchar(50),
    "TotalAnimalsReceived" varchar(10),
    "DestinationHolding" varchar(50),
    "DestinationHoldingManualEntryAddress" text,
    "ArrivalDate" timestamp without time zone,
    "ArrivalDay" varchar(10),
    "ArrivalMonth" varchar(10),
    "ArrivalYear" varchar(10),
    "Movement_ID" int,
    "MovementYearPartition" smallint,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "Comment" varchar(125),
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovement_ID")
);
COMMENT ON COLUMN cla.tbl_papermovement."PaperMovement_ID" IS 'Primary key uniquely identifying a paper-based movement record.';
COMMENT ON COLUMN cla.tbl_papermovement."ScannedDocument_ID" IS 'Identifier for the scanned document associated with the paper-based movement.';
COMMENT ON COLUMN cla.tbl_papermovement."CreateRequest_ID" IS 'Identifier for the request that created the paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement."LastModifyRequest_ID" IS 'Identifier for the request that last modified the paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement."PaperMovementStatus_ID" IS 'Identifier for the status of the paper movement (e.g., pending, completed).';
COMMENT ON COLUMN cla.tbl_papermovement."DateReceived" IS 'Date when the paper movement document was received.';
COMMENT ON COLUMN cla.tbl_papermovement."DocumentSource" IS 'Source of the paper movement document (e.g., farm, market).';
COMMENT ON COLUMN cla.tbl_papermovement."DocumentSourceFormat" IS 'Format of the document source (e.g., scanned, handwritten).';
COMMENT ON COLUMN cla.tbl_papermovement."Species_ID" IS 'Identifier for the species involved in the paper-based movement (e.g., sheep, goats).';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureHolding" IS 'Identifier for the premises (holding) from which the animals departed.';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureHoldingManualEntryAddress" IS 'Manually entered address of the departure premises if the standardized holding ID is unavailable.';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureDate" IS 'Date when the animals departed from the source premises.';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureDay" IS 'Day of the departure date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureMonth" IS 'Month of the departure date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."DepartureYear" IS 'Year of the departure date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."TransporterType_ID" IS 'Identifier for the type of transporter used in the paper-based movement.';
COMMENT ON COLUMN cla.tbl_papermovement."TransporterType" IS 'Description of the transporter type (e.g., truck, trailer).';
COMMENT ON COLUMN cla.tbl_papermovement."TransporterAuthNumber" IS 'Authorization number of the transporter responsible for moving the livestock.';
COMMENT ON COLUMN cla.tbl_papermovement."TransporterHaulierName" IS 'Name of the haulier or transporter handling the movement.';
COMMENT ON COLUMN cla.tbl_papermovement."TransporterVehicleRegistrationNo" IS 'Registration number of the vehicle used for transporting the livestock.';
COMMENT ON COLUMN cla.tbl_papermovement."TotalAnimalsReceived" IS 'Total number of animals received at the destination premises.';
COMMENT ON COLUMN cla.tbl_papermovement."DestinationHolding" IS 'Identifier for the premises (holding) to which the animals are transported.';
COMMENT ON COLUMN cla.tbl_papermovement."DestinationHoldingManualEntryAddress" IS 'Manually entered address of the destination premises if the standardized holding ID is unavailable.';
COMMENT ON COLUMN cla.tbl_papermovement."ArrivalDate" IS 'Date when the animals arrived at the destination premises.';
COMMENT ON COLUMN cla.tbl_papermovement."ArrivalDay" IS 'Day of the arrival date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."ArrivalMonth" IS 'Month of the arrival date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."ArrivalYear" IS 'Year of the arrival date recorded separately for granular querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."Movement_ID" IS 'Foreign key linking to the associated movement record.';
COMMENT ON COLUMN cla.tbl_papermovement."MovementYearPartition" IS 'Logical partitioning of the paper movement data by year for efficient querying and reporting.';
COMMENT ON COLUMN cla.tbl_papermovement."CreatedDateTime" IS 'Timestamp for when the paper movement record was created.';
COMMENT ON COLUMN cla.tbl_papermovement."UpdatedDateTime" IS 'Timestamp for the most recent update to the paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement."Comment" IS 'Additional comments or notes associated with the paper movement record.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovementbatch (
    "PaperMovementBatch_ID" bigint NOT NULL,
    "PaperMovement_ID" int NOT NULL,
    "CollectionIndex" int NOT NULL,
    "BatchNumber" varchar(20),
    "StockType_ID" smallint,
    "StockType" varchar(50),
    "IDMark" varchar(50),
    "AnimalTotal" varchar(10),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovementBatch_ID")
);
COMMENT ON COLUMN cla.tbl_papermovementbatch."PaperMovementBatch_ID" IS 'Primary key uniquely identifying a batch within the paper movement.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."PaperMovement_ID" IS 'Identifier linking to the associated paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."CollectionIndex" IS 'Index used for organizing and retrieving batch details within the paper movement.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."BatchNumber" IS 'Number or identifier assigned to the batch within the paper movement.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."StockType_ID" IS 'Identifier for the type of stock in the batch (e.g., lamb, ewe).';
COMMENT ON COLUMN cla.tbl_papermovementbatch."StockType" IS 'Description of the stock type in the batch.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."IDMark" IS 'Identification mark or tag assigned to the batch (e.g., tag number, identifier).';
COMMENT ON COLUMN cla.tbl_papermovementbatch."AnimalTotal" IS 'Total number of animals in the batch.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."CreatedDateTime" IS 'Timestamp for when the batch record was created.';
COMMENT ON COLUMN cla.tbl_papermovementbatch."UpdatedDateTime" IS 'Timestamp for the most recent update to the batch record.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovementbatch_undo (
    "PaperMovementBatch_ID" bigint NOT NULL,
    "PaperMovement_ID" int NOT NULL,
    "CollectionIndex" int NOT NULL,
    "BatchNumber" varchar(20),
    "StockType_ID" smallint,
    "StockType" varchar(50),
    "IDMark" varchar(50),
    "AnimalTotal" varchar(10),
    "RequestUndo_ID" int NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovementBatch_ID", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."PaperMovementBatch_ID" IS 'Identifier linking to the original paper movement batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."PaperMovement_ID" IS 'Identifier linking to the associated paper movement record from the original batch.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."CollectionIndex" IS 'Index of the batch in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."BatchNumber" IS 'Batch number or identifier from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."StockType_ID" IS 'Identifier for the stock type in the original batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."StockType" IS 'Description of the stock type from the original batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."IDMark" IS 'Identification mark or tag from the original batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."AnimalTotal" IS 'Total number of animals in the original batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation for the batch record.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."CreatedDateTime" IS 'Timestamp for when the undo record for the batch was created.';
COMMENT ON COLUMN cla.tbl_papermovementbatch_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record for the batch.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovementdevice (
    "PaperMovementDevice_ID" bigint NOT NULL,
    "PaperMovement_ID" int NOT NULL,
    "CollectionIndex" int NOT NULL,
    "LTAT_ID" varchar(50),
    "RF_ID" varchar(50),
    "Freezebrand" varchar(50),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovementDevice_ID")
);
COMMENT ON COLUMN cla.tbl_papermovementdevice."PaperMovementDevice_ID" IS 'Primary key uniquely identifying a device associated with a paper movement.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."PaperMovement_ID" IS 'Identifier linking to the associated paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."CollectionIndex" IS 'Index of the device within the paper movement.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."LTAT_ID" IS 'Identifier for the Livestock Track and Trace (LTAT) system associated with the device.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."RF_ID" IS 'RFID tag number associated with the device.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."Freezebrand" IS 'Freezebrand identifier, if applicable, for the device.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."CreatedDateTime" IS 'Timestamp for when the device record was created.';
COMMENT ON COLUMN cla.tbl_papermovementdevice."UpdatedDateTime" IS 'Timestamp for the most recent update to the device record.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovementdevice_undo (
    "PaperMovementDevice_ID" bigint NOT NULL,
    "PaperMovement_ID" int NOT NULL,
    "CollectionIndex" int NOT NULL,
    "LTAT_ID" varchar(50),
    "RF_ID" varchar(50),
    "Freezebrand" varchar(50),
    "RequestUndo_ID" int NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovementDevice_ID", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."PaperMovementDevice_ID" IS 'Identifier linking to the original paper movement device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."PaperMovement_ID" IS 'Identifier linking to the associated paper movement record from the original device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."CollectionIndex" IS 'Index of the device within the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."LTAT_ID" IS 'LTAT identifier from the original device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."RF_ID" IS 'RFID tag number from the original device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."Freezebrand" IS 'Freezebrand identifier from the original device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation for the device record.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."CreatedDateTime" IS 'Timestamp for when the undo record for the device was created.';
COMMENT ON COLUMN cla.tbl_papermovementdevice_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record for the device.';

CREATE TABLE IF NOT EXISTS cla.tbl_papermovement_undo (
    "PaperMovement_ID" int NOT NULL,
    "ScannedDocument_ID" varchar(15) NOT NULL,
    "CreateRequest_ID" int NOT NULL,
    "LastModifyRequest_ID" int NOT NULL,
    "PaperMovementStatus_ID" smallint NOT NULL,
    "DateReceived" timestamp without time zone NOT NULL,
    "DocumentSource" varchar(50),
    "DocumentSourceFormat" varchar(50),
    "Species_ID" smallint,
    "DepartureHolding" varchar(50),
    "DepartureHoldingManualEntryAddress" text,
    "DepartureDate" timestamp without time zone,
    "DepartureDay" varchar(10),
    "DepartureMonth" varchar(10),
    "DepartureYear" varchar(10),
    "TransporterType_ID" smallint,
    "TransporterType" varchar(50),
    "TransporterAuthNumber" varchar(50),
    "TransporterHaulierName" varchar(50),
    "TransporterVehicleRegistrationNo" varchar(50),
    "TotalAnimalsReceived" varchar(10),
    "DestinationHolding" varchar(50),
    "DestinationHoldingManualEntryAddress" text,
    "ArrivalDate" timestamp without time zone,
    "ArrivalDay" varchar(10),
    "ArrivalMonth" varchar(10),
    "ArrivalYear" varchar(10),
    "Movement_ID" int,
    "MovementYearPartition" smallint,
    "RequestUndo_ID" int NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "Comment" varchar(125),
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PaperMovement_ID", "RequestUndo_ID")
);
COMMENT ON COLUMN cla.tbl_papermovement_undo."PaperMovement_ID" IS 'Identifier linking to the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."ScannedDocument_ID" IS 'Identifier for the scanned document from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."CreateRequest_ID" IS 'Identifier for the request that created the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."LastModifyRequest_ID" IS 'Identifier for the request that last modified the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."PaperMovementStatus_ID" IS 'Identifier for the status of the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DateReceived" IS 'Date the original paper movement document was received.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DocumentSource" IS 'Source of the original paper movement document (e.g., market, farm).';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DocumentSourceFormat" IS 'Format of the original document source (e.g., scanned, handwritten).';
COMMENT ON COLUMN cla.tbl_papermovement_undo."Species_ID" IS 'Identifier for the species involved in the original paper-based movement.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureHolding" IS 'Identifier for the departure premises in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureHoldingManualEntryAddress" IS 'Manually entered address of the departure premises from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureDate" IS 'Departure date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureDay" IS 'Day of the departure date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureMonth" IS 'Month of the departure date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DepartureYear" IS 'Year of the departure date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TransporterType_ID" IS 'Identifier for the transporter type in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TransporterType" IS 'Description of the transporter type from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TransporterAuthNumber" IS 'Authorization number of the transporter in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TransporterHaulierName" IS 'Name of the haulier from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TransporterVehicleRegistrationNo" IS 'Vehicle registration number of the transporter from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."TotalAnimalsReceived" IS 'Total number of animals received at the destination in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DestinationHolding" IS 'Identifier for the destination premises in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."DestinationHoldingManualEntryAddress" IS 'Manually entered address of the destination premises from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."ArrivalDate" IS 'Arrival date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."ArrivalDay" IS 'Day of the arrival date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."ArrivalMonth" IS 'Month of the arrival date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."ArrivalYear" IS 'Year of the arrival date from the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."Movement_ID" IS 'Identifier linking to the associated movement record from the original paper movement.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."MovementYearPartition" IS 'Year partition for the movement data in the original paper movement record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."CreatedDateTime" IS 'Timestamp for when the undo record for the paper movement was created.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';
COMMENT ON COLUMN cla.tbl_papermovement_undo."Comment" IS 'Additional comments or notes associated with the undo record for the paper movement.';

CREATE TABLE IF NOT EXISTS cla.tbl_pic (
    "Pic_ID" int NOT NULL,
    "Pic" varchar(14) NOT NULL,
    "PicType_ID" smallint NOT NULL,
    "StatusSet" boolean NOT NULL,
    "Enterprise_ID" int NOT NULL,
    "VetNetCoreID" varchar(80),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Pic_ID")
);
COMMENT ON COLUMN cla.tbl_pic."Pic_ID" IS 'Primary key uniquely identifying a premises (holding).';
COMMENT ON COLUMN cla.tbl_pic."Pic" IS 'Code or identifier for the premises.';
COMMENT ON COLUMN cla.tbl_pic."PicType_ID" IS 'Identifier for the type of premises (e.g., farm, market, abattoir).';
COMMENT ON COLUMN cla.tbl_pic."StatusSet" IS 'Status of the premises (e.g., active, inactive).';
COMMENT ON COLUMN cla.tbl_pic."Enterprise_ID" IS 'Identifier for the enterprise associated with the premises.';
COMMENT ON COLUMN cla.tbl_pic."VetNetCoreID" IS 'Identifier for the veterinary network core system associated with the premises.';
COMMENT ON COLUMN cla.tbl_pic."CreatedDateTime" IS 'Timestamp for when the premises record was created.';
COMMENT ON COLUMN cla.tbl_pic."UpdatedDateTime" IS 'Timestamp for the most recent update to the premises record.';

CREATE TABLE IF NOT EXISTS cla.tbl_picattributes (
    "Pic_ID" int NOT NULL,
    "Address1" varchar(128),
    "Address2" varchar(128),
    "PicStatus_ID" smallint NOT NULL,
    "Request_ID" int NOT NULL,
    "PropertyName" varchar(255),
    "Town" varchar(50),
    "County_ID" smallint,
    "Postcode" varchar(10),
    "Location" text,
    "LocationAccuracy_ID" smallint,
    "KeeperLTATUser_ID" int,
    "KeeperTitle_ID" smallint,
    "KeeperFirstName" varchar(32),
    "KeeperLastName" varchar(255),
    "KeeperPropertyName" varchar(255),
    "KeeperAddress1" varchar(128),
    "KeeperAddress2" varchar(128),
    "KeeperTown" varchar(50),
    "KeeperCounty_ID" smallint,
    "KeeperPostcode" varchar(10),
    "KeeperCustomerRefNo" varchar(10),
    "KeeperTelephone" varchar(35),
    "KeeperMobilePhone" varchar(35),
    "KeeperEmail" varchar(255),
    "OsMapReference" varchar(12),
    "Easting" varchar(12),
    "Northing" varchar(12),
    "KeeperAddressPK" varchar(38),
    "FeaturePK" varchar(38),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "DevolvedArea_ID" smallint,
    "IsSystemCreated" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Pic_ID")
);
COMMENT ON COLUMN cla.tbl_picattributes."Pic_ID" IS 'Foreign key linking to the associated premises (Pic_ID).';
COMMENT ON COLUMN cla.tbl_picattributes."Address1" IS 'First line of the address for the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Address2" IS 'Second line of the address for the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."PicStatus_ID" IS 'Identifier for the status of the premises (e.g., active, inactive).';
COMMENT ON COLUMN cla.tbl_picattributes."Request_ID" IS 'Identifier for the request associated with creating or updating the premises record.';
COMMENT ON COLUMN cla.tbl_picattributes."PropertyName" IS 'Name of the property associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Town" IS 'Town or city where the premises is located.';
COMMENT ON COLUMN cla.tbl_picattributes."County_ID" IS 'Identifier for the county or administrative region of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Postcode" IS 'Postal code for the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Location" IS 'Geographical location or coordinates of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."LocationAccuracy_ID" IS 'Identifier for the accuracy level of the location data.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperLTATUser_ID" IS 'Identifier for the Livestock Track and Trace (LTAT) user associated as the keeper of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperTitle_ID" IS 'Identifier for the title of the keeper (e.g., Mr., Mrs., Dr.).';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperFirstName" IS 'First name of the keeper associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperLastName" IS 'Last name of the keeper associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperPropertyName" IS 'Name of the keeper’s property.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperAddress1" IS 'First line of the keeper’s address.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperAddress2" IS 'Second line of the keeper’s address.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperTown" IS 'Town or city where the keeper resides.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperCounty_ID" IS 'Identifier for the county or administrative region of the keeper’s address.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperPostcode" IS 'Postal code for the keeper’s address.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperCustomerRefNo" IS 'Customer reference number for the keeper associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperTelephone" IS 'Telephone number for the keeper.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperMobilePhone" IS 'Mobile phone number for the keeper.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperEmail" IS 'Email address for the keeper.';
COMMENT ON COLUMN cla.tbl_picattributes."OsMapReference" IS 'Ordnance Survey map reference for the location of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Easting" IS 'Easting coordinate for the geographical location of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."Northing" IS 'Northing coordinate for the geographical location of the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."KeeperAddressPK" IS 'Primary key for the keeper’s address record.';
COMMENT ON COLUMN cla.tbl_picattributes."FeaturePK" IS 'Primary key for additional geographical features associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."CreatedDateTime" IS 'Timestamp for when the premises attribute record was created.';
COMMENT ON COLUMN cla.tbl_picattributes."UpdatedDateTime" IS 'Timestamp for the most recent update to the premises attribute record.';
COMMENT ON COLUMN cla.tbl_picattributes."DevolvedArea_ID" IS 'Identifier for the devolved area (e.g., Wales, Scotland) associated with the premises.';
COMMENT ON COLUMN cla.tbl_picattributes."IsSystemCreated" IS 'Boolean field indicating whether the record was created by the system (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_pictoabattoirmhs (
    "PicToAbattoirMHS_ID" int NOT NULL,
    "Pic_ID" int NOT NULL,
    "MHSNumber" varchar(4) NOT NULL,
    "Status_ID" smallint NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PicToAbattoirMHS_ID")
);
COMMENT ON COLUMN cla.tbl_pictoabattoirmhs."PicToAbattoirMHS_ID" IS 'Primary key uniquely identifying the abattoir record associated with a premises.';
COMMENT ON COLUMN cla.tbl_pictoabattoirmhs."Pic_ID" IS 'Identifier linking to the associated premises.';
COMMENT ON COLUMN cla.tbl_pictoabattoirmhs."MHSNumber" IS 'Identifier for the Meat Hygiene Service (MHS) associated with the premises.';
COMMENT ON COLUMN cla.tbl_pictoabattoirmhs."Status_ID" IS 'Identifier for the status of the premises in relation to the abattoir (e.g., approved, pending).';

CREATE TABLE IF NOT EXISTS cla.tbl_pictoabattoirmhsheader (
    "PicToAbattoirMHSHeader_ID" int NOT NULL,
    "Pic_ID" int NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PicToAbattoirMHSHeader_ID")
);
COMMENT ON COLUMN cla.tbl_pictoabattoirmhsheader."PicToAbattoirMHSHeader_ID" IS 'Primary key uniquely identifying the header record for abattoir data associated with a premises.';
COMMENT ON COLUMN cla.tbl_pictoabattoirmhsheader."Pic_ID" IS 'Identifier linking to the associated premises.';

CREATE TABLE IF NOT EXISTS cla.tbl_pictype (
    "PicType_ID" smallint NOT NULL,
    "PicType" varchar(32) NOT NULL,
    "ShowDeadMovements" boolean NOT NULL,
    "PicTypeFullName" varchar(32),
    "IsEnabled" boolean NOT NULL,
    "IsSystem" boolean NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("PicType_ID")
);
COMMENT ON COLUMN cla.tbl_pictype."PicType_ID" IS 'Primary key uniquely identifying the type of premises.';
COMMENT ON COLUMN cla.tbl_pictype."PicType" IS 'Description of the premises type (e.g., farm, market, abattoir).';
COMMENT ON COLUMN cla.tbl_pictype."ShowDeadMovements" IS 'Boolean field indicating whether dead movements are shown for this premises type (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_pictype."PicTypeFullName" IS 'Full name or detailed description of the premises type.';
COMMENT ON COLUMN cla.tbl_pictype."IsEnabled" IS 'Boolean field indicating whether the premises type is currently enabled in the system (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_pictype."IsSystem" IS 'Boolean field indicating whether the premises type was system-defined (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_processingflag (
    "ProcessingFlag_ID" smallint NOT NULL,
    "FlagName" varchar(30) NOT NULL,
    "FlagDescription" varchar(70) NOT NULL,
    "IsSystem" boolean NOT NULL,
    "UserQuestion" varchar(200),
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("ProcessingFlag_ID")
);
COMMENT ON COLUMN cla.tbl_processingflag."ProcessingFlag_ID" IS 'Primary key uniquely identifying a processing flag.';
COMMENT ON COLUMN cla.tbl_processingflag."FlagName" IS 'Name of the processing flag (e.g., Approved, Pending, Rejected).';
COMMENT ON COLUMN cla.tbl_processingflag."FlagDescription" IS 'Detailed description of the processing flag, indicating its purpose or status.';
COMMENT ON COLUMN cla.tbl_processingflag."IsSystem" IS 'Boolean field indicating if the processing flag is system-generated (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_processingflag."UserQuestion" IS 'User-facing question or prompt associated with the processing flag, if applicable.';

CREATE TABLE IF NOT EXISTS cla.tbl_region (
    "Region_ID" smallint NOT NULL,
    "RegionName" varchar(50) NOT NULL,
    "Country_ID" smallint NOT NULL,
    "DevolvedArea_ID" smallint,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Region_ID")
);


CREATE TABLE IF NOT EXISTS cla.tbl_request (
    "Request_ID" int NOT NULL,
    "LTATUser_ID" int NOT NULL,
    "RequestType_ID" smallint NOT NULL,
    "DateReceived" timestamp without time zone NOT NULL,
    "RequestStatus_ID" smallint NOT NULL,
    "DateUpdated" timestamp without time zone,
    "Version" varchar(16) NOT NULL,
    "RequestUndo_ID" int,
    "YearPartition" smallint NOT NULL,
    "TrackingID" varchar(36),
    "PendingStatusProcessStatus_ID" smallint NOT NULL,
    "Enterprise_ID" int,
    "AsyncEndpoint_ID" varchar(50),
    "ProcessingStartDate" timestamp without time zone,
    "ProcessingEndDate" timestamp without time zone,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    "TransferReqChecksum" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Request_ID")
);
COMMENT ON COLUMN cla.tbl_request."Request_ID" IS 'Primary key uniquely identifying a request.';
COMMENT ON COLUMN cla.tbl_request."LTATUser_ID" IS 'Identifier linking to the user who initiated the request.';
COMMENT ON COLUMN cla.tbl_request."RequestType_ID" IS 'Identifier for the type of request (e.g., movement, update, undo).';
COMMENT ON COLUMN cla.tbl_request."DateReceived" IS 'Date when the request was received in the system.';
COMMENT ON COLUMN cla.tbl_request."RequestStatus_ID" IS 'Identifier for the status of the request (e.g., pending, approved).';
COMMENT ON COLUMN cla.tbl_request."DateUpdated" IS 'Date when the request was last updated.';
COMMENT ON COLUMN cla.tbl_request."Version" IS 'Version number of the request record for tracking updates or changes.';
COMMENT ON COLUMN cla.tbl_request."RequestUndo_ID" IS 'Identifier for the undo request associated with the current request, if applicable.';
COMMENT ON COLUMN cla.tbl_request."YearPartition" IS 'Logical partitioning of the request data by year for efficient querying.';
COMMENT ON COLUMN cla.tbl_request."TrackingID" IS 'Tracking number or identifier for the request, used for reference and status checks.';
COMMENT ON COLUMN cla.tbl_request."PendingStatusProcessStatus_ID" IS 'Identifier linking to the process status of a pending request.';
COMMENT ON COLUMN cla.tbl_request."Enterprise_ID" IS 'Identifier for the enterprise associated with the request.';
COMMENT ON COLUMN cla.tbl_request."AsyncEndpoint_ID" IS 'Identifier for the asynchronous endpoint used to process the request.';
COMMENT ON COLUMN cla.tbl_request."ProcessingStartDate" IS 'Date and time when processing of the request started.';
COMMENT ON COLUMN cla.tbl_request."ProcessingEndDate" IS 'Date and time when processing of the request was completed.';
COMMENT ON COLUMN cla.tbl_request."CreatedDateTime" IS 'Timestamp for when the request record was created.';
COMMENT ON COLUMN cla.tbl_request."UpdatedDateTime" IS 'Timestamp for the most recent update to the request record.';
COMMENT ON COLUMN cla.tbl_request."TransferReqChecksum" IS 'Checksum for validating the integrity of the request data during transfers.';

CREATE TABLE IF NOT EXISTS cla.tbl_requestprocessingflag (
    "Request_ID" int NOT NULL,
    "ProcessingFlag_ID" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Request_ID", "ProcessingFlag_ID")
);
COMMENT ON COLUMN cla.tbl_requestprocessingflag."Request_ID" IS 'Foreign key linking to the associated request.';
COMMENT ON COLUMN cla.tbl_requestprocessingflag."ProcessingFlag_ID" IS 'Foreign key linking to the associated processing flag.';
COMMENT ON COLUMN cla.tbl_requestprocessingflag."CreatedDateTime" IS 'Timestamp for when the request-processing flag record was created.';
COMMENT ON COLUMN cla.tbl_requestprocessingflag."UpdatedDateTime" IS 'Timestamp for the most recent update to the request-processing flag record.';

CREATE TABLE IF NOT EXISTS cla.tbl_sessioninfo (
    "SessionInfo_ID" bigint NOT NULL,
    "LTATUser_ID" int NOT NULL,
    "LoginTime" timestamp without time zone NOT NULL,
    "LogoutTime" timestamp without time zone NOT NULL,
    "Software_ID" smallint NOT NULL,
    "SoftwareInstallToken" uuid,
    "YearPartition" smallint NOT NULL,
    "LastActivityDate" timestamp without time zone NOT NULL,
    "OAuthClientSession_ID" varchar(32),
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("SessionInfo_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_sessioninfo."SessionInfo_ID" IS 'Primary key uniquely identifying a session record.';
COMMENT ON COLUMN cla.tbl_sessioninfo."LTATUser_ID" IS 'Identifier linking to the user associated with the session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."LoginTime" IS 'Timestamp for when the user logged into the session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."LogoutTime" IS 'Timestamp for when the user logged out of the session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."Software_ID" IS 'Identifier for the software associated with the session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."SoftwareInstallToken" IS 'Token associated with the installation of the software for session tracking.';
COMMENT ON COLUMN cla.tbl_sessioninfo."YearPartition" IS 'Logical partitioning of the session data by year for efficient querying and management.';
COMMENT ON COLUMN cla.tbl_sessioninfo."LastActivityDate" IS 'Date of the last recorded activity in the session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."OAuthClientSession_ID" IS 'Identifier for the OAuth client session associated with the user session.';
COMMENT ON COLUMN cla.tbl_sessioninfo."CreatedDateTime" IS 'Timestamp for when the session record was created.';
COMMENT ON COLUMN cla.tbl_sessioninfo."UpdatedDateTime" IS 'Timestamp for the most recent update to the session record.';

CREATE TABLE IF NOT EXISTS cla.tbl_sessiontorequest (
    "SessionInfo_ID" bigint NOT NULL,
    "Request_ID" int NOT NULL,
    "YearPartition" smallint NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("SessionInfo_ID", "Request_ID", "YearPartition")
);
COMMENT ON COLUMN cla.tbl_sessiontorequest."SessionInfo_ID" IS 'Foreign key linking to the session associated with the request.';
COMMENT ON COLUMN cla.tbl_sessiontorequest."Request_ID" IS 'Foreign key linking to the request associated with the session.';
COMMENT ON COLUMN cla.tbl_sessiontorequest."YearPartition" IS 'Logical partitioning of the session-to-request data by year for efficient querying.';
COMMENT ON COLUMN cla.tbl_sessiontorequest."CreatedDateTime" IS 'Timestamp for when the session-to-request record was created.';
COMMENT ON COLUMN cla.tbl_sessiontorequest."UpdatedDateTime" IS 'Timestamp for the most recent update to the session-to-request record.';

CREATE TABLE IF NOT EXISTS cla.tbl_software (
    "Software_ID" smallint NOT NULL,
    "Request_ID" int,
    "SoftwareName" varchar(120) NOT NULL,
    "SoftwareVersion" varchar(16) NOT NULL,
    "ApprovalStatus_ID" smallint NOT NULL,
    "LTATUser_ID" int,
    "Token" char(128),
    "RequiresLicense" boolean NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Software_ID")
);
COMMENT ON COLUMN cla.tbl_software."Software_ID" IS 'Primary key uniquely identifying a software record.';
COMMENT ON COLUMN cla.tbl_software."Request_ID" IS 'Foreign key linking to the associated request for software management.';
COMMENT ON COLUMN cla.tbl_software."SoftwareName" IS 'Name of the software associated with the request.';
COMMENT ON COLUMN cla.tbl_software."SoftwareVersion" IS 'Version number of the software associated with the request.';
COMMENT ON COLUMN cla.tbl_software."ApprovalStatus_ID" IS 'Identifier for the approval status of the software (e.g., approved, pending).';
COMMENT ON COLUMN cla.tbl_software."LTATUser_ID" IS 'Identifier linking to the user associated with the software request.';
COMMENT ON COLUMN cla.tbl_software."Token" IS 'Token associated with the software for authorization and tracking purposes.';
COMMENT ON COLUMN cla.tbl_software."RequiresLicense" IS 'Boolean field indicating whether the software requires a license (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_species (
    "Species_ID" smallint NOT NULL,
    "SpeciesName" varchar(8) NOT NULL,
    "SpeciesScientificName" varchar(16),
    "SpeciesCode" varchar(8) NOT NULL,
    "BatchIdentificationSupported" boolean NOT NULL,
    "MaxDobInterval" smallint NOT NULL,
    "DobIntervalExactDateRange" boolean NOT NULL,
    "DobIntervalRequired" boolean NOT NULL,
    "EIDSpeciesCode" smallint NOT NULL,
    "DeviceIdentificationSupported" boolean NOT NULL,
    "BatchNumberFriendlyName" varchar(20) NOT NULL,
    "CreateUnknownDeviceWithNoLTATID" boolean NOT NULL,
    "CreateUnknownDeviceWithNoRFID" boolean NOT NULL,
    "AllowDeviceApplicationInEvent" boolean NOT NULL,
    "GenerateDeviceRangeSupported" boolean NOT NULL,
    "CreateUnknownDevice" boolean NOT NULL,
    "SystemTagCreationWarningPattern" varchar(100),
    "AutoSetFinalMove" boolean NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Species_ID")
);
COMMENT ON COLUMN cla.tbl_species."Species_ID" IS 'Primary key uniquely identifying a species.';
COMMENT ON COLUMN cla.tbl_species."SpeciesName" IS 'Common name of the species (e.g., sheep, goat).';
COMMENT ON COLUMN cla.tbl_species."SpeciesScientificName" IS 'Scientific name of the species (e.g., Ovis aries for sheep).';
COMMENT ON COLUMN cla.tbl_species."SpeciesCode" IS 'Unique code assigned to the species for identification purposes.';
COMMENT ON COLUMN cla.tbl_species."BatchIdentificationSupported" IS 'Boolean field indicating whether batch identification is supported for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."MaxDobInterval" IS 'Maximum date of birth interval allowed for the species in movements or registrations.';
COMMENT ON COLUMN cla.tbl_species."DobIntervalExactDateRange" IS 'Exact date range for date of birth interval applicable to the species.';
COMMENT ON COLUMN cla.tbl_species."DobIntervalRequired" IS 'Boolean field indicating if date of birth interval is required for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."EIDSpeciesCode" IS 'Electronic Identification (EID) code specific to the species.';
COMMENT ON COLUMN cla.tbl_species."DeviceIdentificationSupported" IS 'Boolean field indicating whether device identification is supported for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."BatchNumberFriendlyName" IS 'User-friendly name for batch numbers applicable to the species.';
COMMENT ON COLUMN cla.tbl_species."CreateUnknownDeviceWithNoLTATID" IS 'Boolean field indicating if unknown devices can be created without LTAT identifiers for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."CreateUnknownDeviceWithNoRFID" IS 'Boolean field indicating if unknown devices can be created without RFID for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."AllowDeviceApplicationInEvent" IS 'Boolean field indicating if device application is allowed during events for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."GenerateDeviceRangeSupported" IS 'Boolean field indicating if generating a range of devices is supported for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."CreateUnknownDevice" IS 'Boolean field indicating if creating unknown devices is allowed for the species (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_species."SystemTagCreationWarningPattern" IS 'Warning pattern used by the system during tag creation for the species.';
COMMENT ON COLUMN cla.tbl_species."AutoSetFinalMove" IS 'Boolean field indicating if the system should automatically set the final move for the species (0 = No, 1 = Yes).';

CREATE TABLE IF NOT EXISTS cla.tbl_titemattributes (
    "Titem_ID" int NOT NULL,
    "TitemYearPartition" smallint NOT NULL,
    "Request_ID" int NOT NULL,
    "BeginBirthPeriod" timestamp without time zone NOT NULL,
    "EndBirthPeriod" timestamp without time zone NOT NULL,
    "BirthRequest_ID" int,
    "Breed_ID" smallint,
    "Gender_ID" smallint NOT NULL,
    "DamTitem_ID" int,
    "DamIdentifier" varchar(16),
    "GeneticDamTitem_ID" int,
    "GeneticDamIdentifier" varchar(16),
    "SireTitem_ID" int,
    "SireIdentifier" varchar(16),
    "IsBreeding" boolean NOT NULL,
    "WasEmbryoTransfer" boolean,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Titem_ID", "TitemYearPartition")
);
COMMENT ON COLUMN cla.tbl_titemattributes."Titem_ID" IS 'Primary key uniquely identifying an individual or batch of livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."TitemYearPartition" IS 'Logical partitioning of the Titem-attributes data by year for efficient querying.';
COMMENT ON COLUMN cla.tbl_titemattributes."Request_ID" IS 'Identifier for the request associated with the Titem attributes.';
COMMENT ON COLUMN cla.tbl_titemattributes."BeginBirthPeriod" IS 'Beginning of the birth period for the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."EndBirthPeriod" IS 'End of the birth period for the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."BirthRequest_ID" IS 'Identifier for the request associated with recording the birth of the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."Breed_ID" IS 'Identifier for the breed of the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."Gender_ID" IS 'Identifier for the gender of the livestock (e.g., male, female).';
COMMENT ON COLUMN cla.tbl_titemattributes."DamTitem_ID" IS 'Identifier for the dam (mother) of the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."DamIdentifier" IS 'Identifier for the dam (e.g., tag number).';
COMMENT ON COLUMN cla.tbl_titemattributes."GeneticDamTitem_ID" IS 'Identifier for the genetic dam (biological mother) of the livestock, if applicable.';
COMMENT ON COLUMN cla.tbl_titemattributes."GeneticDamIdentifier" IS 'Identifier for the genetic dam (e.g., tag number), if applicable.';
COMMENT ON COLUMN cla.tbl_titemattributes."SireTitem_ID" IS 'Identifier for the sire (father) of the livestock.';
COMMENT ON COLUMN cla.tbl_titemattributes."SireIdentifier" IS 'Identifier for the sire (e.g., tag number).';
COMMENT ON COLUMN cla.tbl_titemattributes."IsBreeding" IS 'Boolean field indicating if the livestock is used for breeding (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_titemattributes."WasEmbryoTransfer" IS 'Boolean field indicating if the livestock was produced through embryo transfer (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_titemattributes."CreatedDateTime" IS 'Timestamp for when the Titem-attributes record was created.';
COMMENT ON COLUMN cla.tbl_titemattributes."UpdatedDateTime" IS 'Timestamp for the most recent update to the Titem-attributes record.';

CREATE TABLE IF NOT EXISTS cla.tbl_titem_device (
    "Device_ID" int NOT NULL,
    "Titem_ID" int NOT NULL,
    "TitemYearPartition" smallint NOT NULL,
    "DateApplied" timestamp without time zone NOT NULL,
    "ApplicationRequest_ID" int,
    "ApplicationPic_ID" int,
    "SystemDateApplied" timestamp without time zone NOT NULL,
    "SystemApplicationRequest_ID" int,
    "SystemApplicationPic_ID" int,
    "IsPreferredDevice" boolean NOT NULL,
    "CreatedDateTime" timestamp without time zone NOT NULL,
    "UpdatedDateTime" timestamp without time zone NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Device_ID", "Titem_ID", "TitemYearPartition")
);
COMMENT ON COLUMN cla.tbl_titem_device."Device_ID" IS 'Primary key uniquely identifying a device.';
COMMENT ON COLUMN cla.tbl_titem_device."Titem_ID" IS 'Foreign key linking to the associated individual or batch of livestock.';
COMMENT ON COLUMN cla.tbl_titem_device."TitemYearPartition" IS 'Logical partitioning of the Titem-device data by year for efficient querying.';
COMMENT ON COLUMN cla.tbl_titem_device."DateApplied" IS 'Date when the device was applied to the livestock.';
COMMENT ON COLUMN cla.tbl_titem_device."ApplicationRequest_ID" IS 'Identifier for the request associated with the application of the device.';
COMMENT ON COLUMN cla.tbl_titem_device."ApplicationPic_ID" IS 'Identifier for the premises (Pic_ID) where the device application occurred.';
COMMENT ON COLUMN cla.tbl_titem_device."SystemDateApplied" IS 'Date when the system recorded the application of the device.';
COMMENT ON COLUMN cla.tbl_titem_device."SystemApplicationRequest_ID" IS 'Identifier for the system-generated request for the device application.';
COMMENT ON COLUMN cla.tbl_titem_device."SystemApplicationPic_ID" IS 'Identifier for the premises associated with the system-generated application of the device.';
COMMENT ON COLUMN cla.tbl_titem_device."IsPreferredDevice" IS 'Boolean field indicating if the device is the preferred device for the associated livestock (0 = No, 1 = Yes).';
COMMENT ON COLUMN cla.tbl_titem_device."CreatedDateTime" IS 'Timestamp for when the Titem-device record was created.';
COMMENT ON COLUMN cla.tbl_titem_device."UpdatedDateTime" IS 'Timestamp for the most recent update to the Titem-device record.';

CREATE TABLE IF NOT EXISTS cla.tbl_title (
    "Title_ID" smallint NOT NULL,
    "Title" varchar(16) NOT NULL,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B',
    PRIMARY KEY ("Title_ID")
);
COMMENT ON COLUMN cla.tbl_title."Title_ID" IS 'Primary key uniquely identifying a title record (e.g., Mr., Mrs., Dr.).';
COMMENT ON COLUMN cla.tbl_title."Title" IS 'Text description of the title.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementgroup (
    "MovementGroup_ID" text,
    "Movement_ID" text,
    "fromSubLocation" text,
    "toSubLocation" text,
    "VendorPIC_ID" text,
    "YearPartition" text,
    "CreatedDateTime" text,
    "UpdatedDateTime" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B'
);
COMMENT ON COLUMN cla.tbl_movementgroup."MovementGroup_ID" IS 'Primary key uniquely identifying a movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup."Movement_ID" IS 'Foreign key linking to the movement associated with this group.';
COMMENT ON COLUMN cla.tbl_movementgroup."fromSubLocation" IS 'Sub-location within the departure premises where the movement originated.';
COMMENT ON COLUMN cla.tbl_movementgroup."toSubLocation" IS 'Sub-location within the destination premises where the movement is directed.';
COMMENT ON COLUMN cla.tbl_movementgroup."VendorPIC_ID" IS 'Identifier for the vendor premises involved in the movement group.';
COMMENT ON COLUMN cla.tbl_movementgroup."YearPartition" IS 'Logical partitioning of the movement group data by year for efficient querying and storage.';
COMMENT ON COLUMN cla.tbl_movementgroup."CreatedDateTime" IS 'Timestamp for when the movement group record was created.';
COMMENT ON COLUMN cla.tbl_movementgroup."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement group record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementgroupdevice (
    "MovementGroup_ID" text,
    "Device_ID" text,
    "YearPartition" text,
    "CreatedDateTime" text,
    "UpdatedDateTime" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B'
);
COMMENT ON COLUMN cla.tbl_movementgroupdevice."MovementGroup_ID" IS 'Foreign key linking to the movement group associated with this device.';
COMMENT ON COLUMN cla.tbl_movementgroupdevice."Device_ID" IS 'Foreign key linking to the device involved in the movement group.';
COMMENT ON COLUMN cla.tbl_movementgroupdevice."YearPartition" IS 'Logical partitioning of the movement group device data by year.';
COMMENT ON COLUMN cla.tbl_movementgroupdevice."CreatedDateTime" IS 'Timestamp for when the movement group device record was created.';
COMMENT ON COLUMN cla.tbl_movementgroupdevice."UpdatedDateTime" IS 'Timestamp for the most recent update to the movement group device record.';

CREATE TABLE IF NOT EXISTS cla.tbl_movementgroup_undo (
    "MovementGroup_ID" text,
    "Movement_ID" text,
    "fromSubLocation" text,
    "toSubLocation" text,
    "VendorPIC_ID" text,
    "YearPartition" text,
    "RequestUndo_ID" text,
    "CreatedDateTime" text,
    "UpdatedDateTime" text,
    trans_id bigint,
    trans_type text NOT NULL DEFAULT 'B'
);
COMMENT ON COLUMN cla.tbl_movementgroup_undo."MovementGroup_ID" IS 'Identifier linking to the original movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."Movement_ID" IS 'Identifier for the movement associated with the original group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."fromSubLocation" IS 'Departure sub-location from the original movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."toSubLocation" IS 'Destination sub-location from the original movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."VendorPIC_ID" IS 'Identifier for the vendor premises in the original movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."YearPartition" IS 'Year partition data from the original movement group record.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."RequestUndo_ID" IS 'Identifier for the request that initiated the undo operation.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."CreatedDateTime" IS 'Timestamp for when the undo record for the movement group was created.';
COMMENT ON COLUMN cla.tbl_movementgroup_undo."UpdatedDateTime" IS 'Timestamp for the most recent update to the undo record.';

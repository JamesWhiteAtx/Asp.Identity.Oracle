﻿create table ASP_IDNTY_USER
(
  id                         VARCHAR2(45) not null,
  username                   VARCHAR2(45),
  passwordhash               VARCHAR2(100),
  securitystamp              VARCHAR2(45),
  email                      VARCHAR2(100),
  email_confirmed_flag       VARCHAR2(1) default 'N' not null,
  phonenumber                VARCHAR2(25),
  phonenumber_confirmed_flag VARCHAR2(1) default 'N' not null,
  twofactorenabled_flag      VARCHAR2(1) default 'N' not null,
  lockoutenddateutc          DATE,
  lockoutenabled_flag        VARCHAR2(1) default 'N' not null,
  accessfailedcount          NUMBER default 0 not null
)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER
ADD CONSTRAINT pk_aspidntyuser_id PRIMARY KEY (id)
/

CREATE TABLE SYSADM.ASP_IDNTY_ROLE
(
    id     VARCHAR2 (45 BYTE) NOT NULL,
    name   VARCHAR2 (45 BYTE)
)
/

ALTER TABLE SYSADM.ASP_IDNTY_ROLE
ADD CONSTRAINT pk_aspidntyrole_id PRIMARY KEY (id);
/

CREATE TABLE SYSADM.ASP_IDNTY_USER_CLAIM
(
    id           NUMBER (*, 0) NOT NULL,
    userid       VARCHAR2 (45 BYTE),
    claimtype    VARCHAR2 (100 BYTE),
    claimvalue   VARCHAR2 (100 BYTE)
)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_CLAIM
ADD CONSTRAINT pk_aspidntyclaim_id PRIMARY KEY (id)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_CLAIM
ADD CONSTRAINT fk_aspidntyclaim_userid FOREIGN KEY (userid)
REFERENCES SYSADM.ASP_IDNTY_USER (id) ON DELETE CASCADE
/

CREATE SEQUENCE SYSADM.seq_aspidntyclaim_id
    INCREMENT BY 1
    START WITH 1
    MINVALUE 1
    MAXVALUE 9999999999
    NOCYCLE
    NOORDER
    CACHE 20
/

CREATE OR REPLACE TRIGGER SYSADM.tr_aspidntyclaim_ins
    BEFORE INSERT
    ON SYSADM.ASP_IDNTY_USER_CLAIM
    REFERENCING NEW AS new OLD AS old
    FOR EACH ROW
BEGIN
    IF INSERTING
    THEN
        IF :new.id IS NULL
        THEN
            SELECT seq_aspidntyclaim_id.NEXTVAL INTO :new.id FROM DUAL;
        END IF;
    END IF;
END;
/

CREATE TABLE SYSADM.ASP_IDNTY_USER_LOGIN
(
    userid          VARCHAR2 (44 BYTE) NOT NULL,
    providerkey     VARCHAR2 (100 BYTE),
    loginprovider   VARCHAR2 (100 BYTE)
)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_LOGIN
ADD CONSTRAINT pk_aspidntylogin PRIMARY KEY (userid, providerkey, loginprovider)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_LOGIN
ADD CONSTRAINT fk_aspidntylogin_userid FOREIGN KEY (userid)
REFERENCES SYSADM.ASP_IDNTY_USER (id) ON DELETE CASCADE
/

CREATE TABLE SYSADM.ASP_IDNTY_USER_ROLE
(
    userid   VARCHAR2 (45 BYTE) NOT NULL,
    roleid   VARCHAR2 (45 BYTE) NOT NULL
)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_ROLE
ADD CONSTRAINT pk_aspidntyusrrol_id PRIMARY KEY (userid, roleid)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_ROLE
ADD CONSTRAINT fk_aspidntysrrol_userid FOREIGN KEY (userid)
REFERENCES SYSADM.ASP_IDNTY_USER (id)
/

ALTER TABLE SYSADM.ASP_IDNTY_USER_ROLE
ADD CONSTRAINT fk_aspidntyusrrol_roleid FOREIGN KEY (roleid)
REFERENCES SYSADM.ASP_IDNTY_ROLE (id)
/
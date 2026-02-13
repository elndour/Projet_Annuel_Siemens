drop table MACHINE cascade constraints;

drop table SAVEDATA cascade constraints;

/*==============================================================*/
/* Table : MACHINE                                              */
/*==============================================================*/
create table MACHINE 
(
   IDM            NUMBER               not null,
   NOM            VARCHAR(20),
   IPV4           NUMBER,
   SSH            NUMBER,
   BUG            VARCHAR2(50) 
);

/*==============================================================*/
/* Table : SAVEDATA                                             */
/*==============================================================*/
create table SAVEDATA 
(
   IDM            NUMBER               not null,
   NOM            VARCHAR(20),
   IPV4           NUMBER,
   SSH            NUMBER,
   BUG            VARCHAR2(50) 
);

/*==============================================================*/
/* Peuplement Machine                                           */
/*==============================================================*/

CREATE OR REPLACE PROCEDURE AJOUTMACHINE IS
    M_ID NUMBER := 1;
    IP NUMBER;
    SH NUMBER;
    TYPE ListeMachine is VARRAY(4) OF VARCHAR2(20);
    noms ListeMachine := ListeMachine('BDD', 'ApplicationServer', 'PresentationServer', 'MachineServer');
    X NUMBER;
    
BEGIN
    FOR i IN 1..10 LOOP
        IP := TRUNC(DBMS_RANDOM.VALUE(1000000,9999999));
        SH := TRUNC(DBMS_RANDOM.VALUE(100,9999));
        X := TRUNC(DBMS_RANDOM.VALUE(1,noms.COUNT+1));
        INSERT INTO MACHINE (IDM, NOM, IPV4, SSH) VALUES (M_ID, noms(X), IP, SH);
        M_ID := M_ID + 1;  
    END LOOP;
END ;
/

BEGIN
AJOUTMACHINE;
END;
/

/*==============================================================*/
/* Affichage Data                                               */
/*==============================================================*/

SELECT * FROM MACHINE;
SELECT IDM FROM MACHINE;
SELECT NOM FROM MACHINE;
SELECT IPV4 FROM MACHINE;
SELECT SSH FROM MACHINE;
SELECT BUG FROM MACHINE;

/*==============================================================*/
/* Procedure INSERT                                             */
/*==============================================================*/

CREATE OR REPLACE PROCEDURE COPIEDATA IS
    CURSOR C_M IS SELECT IDM, NOM, IPV4, SSH, BUG FROM MACHINE;

BEGIN
    FOR i IN C_M LOOP
        INSERT INTO SAVEDATA (IDM, NOM, IPV4, SSH, BUG) VALUES (i.IDM, i.NOM, i.IPV4, i.SSH, i.BUG);
    END LOOP;
END;
/

BEGIN
COPIEDATA;
END;
/

SELECT * FROM SAVEDATA;

COMMIT
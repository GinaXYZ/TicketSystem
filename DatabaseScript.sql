-- Datenbank erstellen
CREATE DATABASE TicketSystem1;
GO

USE TicketSystem1;
GO

-- Tabelle: BENUTZER
CREATE TABLE BENUTZER (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL UNIQUE,
    rolle NVARCHAR(50) NOT NULL
);
GO

-- Tabelle: STATUS
CREATE TABLE STATUS (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    beschreibung NVARCHAR(500)
);
GO

-- Tabelle: PRIORITÄT
CREATE TABLE PRIORITÄT (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    beschreibung NVARCHAR(500)
);
GO

-- Tabelle: KATEGORIE
CREATE TABLE KATEGORIE (
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL
);
GO

-- Tabelle: TICKET
CREATE TABLE TICKET (
    id INT PRIMARY KEY IDENTITY(1,1),
    ersteller_id INT NOT NULL,
    status_id INT NOT NULL,
    priorität_id INT NOT NULL,
    kategorie_id INT NOT NULL,
    titel NVARCHAR(255) NOT NULL,
    beschreibung NVARCHAR(MAX),
    erstellungsdatum DATE NOT NULL DEFAULT GETDATE(),
    fälligkeitsdatum DATE,
    CONSTRAINT FK_Ticket_Ersteller FOREIGN KEY (ersteller_id) REFERENCES BENUTZER(id),
    CONSTRAINT FK_Ticket_Status FOREIGN KEY (status_id) REFERENCES STATUS(id),
    CONSTRAINT FK_Ticket_Priorität FOREIGN KEY (priorität_id) REFERENCES PRIORITÄT(id),
    CONSTRAINT FK_Ticket_Kategorie FOREIGN KEY (kategorie_id) REFERENCES KATEGORIE(id)
);
GO

-- Tabelle: ZUWEISUNG
CREATE TABLE ZUWEISUNG (
    id INT PRIMARY KEY IDENTITY(1,1),
    ticket_id INT NOT NULL,
    benutzer_id INT NOT NULL,
    zuweisungsdatum DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Zuweisung_Ticket FOREIGN KEY (ticket_id) REFERENCES TICKET(id) ON DELETE CASCADE,
    CONSTRAINT FK_Zuweisung_Benutzer FOREIGN KEY (benutzer_id) REFERENCES BENUTZER(id)
);
GO

-- Tabelle: KOMMENTARE
CREATE TABLE KOMMENTARE (
    id INT PRIMARY KEY IDENTITY(1,1),
    ticket_id INT NOT NULL,
    benutzer_id INT NOT NULL,
    kommentar NVARCHAR(MAX) NOT NULL,
    erstellungsdatum DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Kommentare_Ticket FOREIGN KEY (ticket_id) REFERENCES TICKET(id) ON DELETE CASCADE,
    CONSTRAINT FK_Kommentare_Benutzer FOREIGN KEY (benutzer_id) REFERENCES BENUTZER(id)
);
GO

-- Tabelle: HISTORIE
CREATE TABLE HISTORIE (
    id INT PRIMARY KEY IDENTITY(1,1),
    ticket_id INT NOT NULL,
    benutzer_id INT NOT NULL,
    änderung NVARCHAR(MAX) NOT NULL,
    änderungsdatum DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Historie_Ticket FOREIGN KEY (ticket_id) REFERENCES TICKET(id) ON DELETE CASCADE,
    CONSTRAINT FK_Historie_Benutzer FOREIGN KEY (benutzer_id) REFERENCES BENUTZER(id)
);
GO

-- Tabelle: BENACHRICHTIGUNGEN
CREATE TABLE BENACHRICHTIGUNGEN (
    id INT PRIMARY KEY IDENTITY(1,1),
    benutzer_id INT NOT NULL,
    ticket_id INT NOT NULL,
    benachrichtigungstyp NVARCHAR(100) NOT NULL,
    nachrichteninhalt NVARCHAR(MAX),
    zeitpunkt DATETIME NOT NULL DEFAULT GETDATE(),
    gelesen BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Benachrichtigungen_Benutzer FOREIGN KEY (benutzer_id) REFERENCES BENUTZER(id),
    CONSTRAINT FK_Benachrichtigungen_Ticket FOREIGN KEY (ticket_id) REFERENCES TICKET(id) ON DELETE CASCADE
);
GO

-- Tabelle: ANHÄNGE
CREATE TABLE ANHÄNGE (
    id INT PRIMARY KEY IDENTITY(1,1),
    ticket_id INT NOT NULL,
    benutzer_id INT NOT NULL,
    dateipfad NVARCHAR(500) NOT NULL,
    dateiname NVARCHAR(255) NOT NULL,
    dateigröße INT NOT NULL,
    dateitype NVARCHAR(100),
    uploadzeit DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Anhänge_Ticket FOREIGN KEY (ticket_id) REFERENCES TICKET(id) ON DELETE CASCADE,
    CONSTRAINT FK_Anhänge_Benutzer FOREIGN KEY (benutzer_id) REFERENCES BENUTZER(id)
);
GO

-- Indizes für bessere Performance
CREATE INDEX IX_Ticket_Ersteller ON TICKET(ersteller_id);
CREATE INDEX IX_Ticket_Status ON TICKET(status_id);
CREATE INDEX IX_Ticket_Priorität ON TICKET(priorität_id);
CREATE INDEX IX_Ticket_Kategorie ON TICKET(kategorie_id);
CREATE INDEX IX_Zuweisung_Ticket ON ZUWEISUNG(ticket_id);
CREATE INDEX IX_Zuweisung_Benutzer ON ZUWEISUNG(benutzer_id);
CREATE INDEX IX_Kommentare_Ticket ON KOMMENTARE(ticket_id);
CREATE INDEX IX_Historie_Ticket ON HISTORIE(ticket_id);
CREATE INDEX IX_Benachrichtigungen_Benutzer ON BENACHRICHTIGUNGEN(benutzer_id);
CREATE INDEX IX_Anhänge_Ticket ON ANHÄNGE(ticket_id);
GO

-- Beispieldaten für STATUS
INSERT INTO STATUS (name, beschreibung) VALUES 
('Offen', 'Ticket wurde erstellt und wartet auf Bearbeitung'),
('In Bearbeitung', 'Ticket wird aktuell bearbeitet'),
('Gelöst', 'Problem wurde gelöst, wartet auf Bestätigung'),
('Geschlossen', 'Ticket wurde abgeschlossen'),
('Zurückgestellt', 'Ticket wurde temporär zurückgestellt');
GO

-- Beispieldaten für PRIORITÄT
INSERT INTO PRIORITÄT (name, beschreibung) VALUES 
('Niedrig', 'Kann später bearbeitet werden'),
('Normal', 'Standard-Priorität'),
('Hoch', 'Sollte zeitnah bearbeitet werden'),
('Kritisch', 'Erfordert sofortige Aufmerksamkeit');
GO

-- Beispieldaten für KATEGORIE
INSERT INTO KATEGORIE (name) VALUES 
('Technisches Problem'),
('Anfrage'),
('Bug'),
('Feature Request'),
('Support'),
('Dokumentation');
GO

PRINT 'Datenbank TicketSystem1 wurde erfolgreich erstellt!';
GO

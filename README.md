# ExceptionNotifier
---
To usługa Windows, która pozwala na sprawdzanie czy w podanej tabeli zostały zapisane nowe rekordy, jeżeli tak to wysyła je do zdefiniowanych klientów (slack, e-mail). Dodatkowo można w niej ustawiać filtrowanie i agregowanie wiadomości oraz czas życia usługi.

# Requirements
1.	.NET Framework 4.6.1

# How To Use
Projekt posiada pięć plików kofiguracyjnych:

1.	App.config
2.	Config.json
3.	Log4NetLogger.config
4.	MailConfig.json
5.	SlackConfig.json

Do szyfrowania hasła w konfiguracji maila trzeba użyć:

1.	CryptoEncoder.exe

# Configuration

1. App.config
```
  <appSettings>
    <add key="FileName" value="C:\ExceptionNotifier\Config.json"/> <!--Ścieżka do pliku konfiguracyjnego Config.json-->
    <add key="SendNotificationInterval" value="5000" /> <!--Intertwał ponierania i wytsyłania wiadomości. 1000 to jedna sekunda-->
    <add key="ApplicationLifetime" value="2022-01-04 12:00:00" /> <!--Po przekroczeniu tej daty usługa będzie zamykana-->
    <add key="CheckApplicationLifetimeInterval" value="5000" /> <!--Interwał sprawdzania czy przekroczono datę-->
    <add key="Log4NetLogger" value="True" /> <!--Czy używać Log4NetLogger-->
	<add key="Log4NetLoggerDaysToKeepLogs" value="60" /> <!--Trzyma logi z ostatnich 60 dni. Jeżeli usuniemy wpis lub podamy wartość 0 pliki nie będą usuwane-->
  </appSettings>
```
2. Log4NetLogger.config
```
Standardowo jest ustawiony na tworzenie dwóch plików: errorlog oraz diagnostic. Każdy plik w nazwie ma datę. 
W errorlog logowane są błędy usługi natomiast w pliku diagnostic logowane są wszystkie informacje diagnostyczne. 
Tworzony jest tylko jeden plik na dzień dla każdego, gdzie maksymalny rozmiar pliku dla errorlog to 5MB, a dla diagnostic 10MB.
```
3. Config.json
```
{
  "readers": [//Ta sekcja pozwala na tworzenie readerów z których są pobierane wiadomości
    {
      "readerType": "dbReader",//Typ: reader bazy danych
      "readerId": 1,//Id musi być unikalne
      "readerName": "do odczytu danych z bazy: X",//Nazwa Readera
      "logicalName": "X",//Ta nazwa jest wyświetlana podczas wysyłki do klienta
      "columnNames": [ "Id", "MessageType", "ApplicationName", "CreationDate", "CreationUser", "Exception", "Message" ],//Jakie kolumny z tabeli mają być wysyłane do klienta
      "initialCounter": 0,//Licznik z ilością wiadomości w bazie. Podczas dodania usługi trzeba wyczyscić tabelę logów lub ustawić licznik
      "connectionString": "data source=adres bazy;initial catalog=baza danych;persist security info=True;user id=użytkownik;password=hasło;",//połączenie do bazych
      "readerAdditionalParams": {
		"schema": "dbo",//Nazwa schematu bazy danych użytego na tabeli, jeżeli niezostanie podany to domyślnie jest przypisywany schemat: "dbo"
        "tableName": "Logs",//Tabela z jakiej mają być pobierane dane
        "messageTypeColumnName": "MessageType",//Podajemy kolumnę jeżeli mamy rozróżnienie typu wpisu
        "orderByColumnName": "Id"//Nazwa kolumny po której chcemy sortować nasze dane (Użyte w logice tworzenia listy notyfikacji do wysyłki)
      }
    },
    {
      "readerType": "dbReader",
      "readerId": 2,
      "readerName": "do odczytu danych z bazy: X2",
      "logicalName": "X2",
      "columnNames": [ "Id", "MessageType", "ApplicationName", "CreationDate", "CreationUser", "Exception", "Message" ],
      "initialCounter": 0,
      "connectionString": "data source=adres bazy;initial catalog=baza danych;persist security info=True;user id=użytkownik;password=hasło;",
      "readerAdditionalParams": {
        "tableName": "Logs",
        "messageTypeColumnName": "MessageType",
        "orderByColumnName": "CreationDate"
      }
    },
    {
      "readerType": "pingReader",//Typ: readera, który wysyła wiadomości, że usługa żyje
      "readerId": 3,//Id musi być unikalne
      "logicalName": "NN",//Ta nazwa jest wyświetlana podczas wysyłki do klienta. Zbiorcza dla wszystkich readerów
      "readerAdditionalParams": {
        "StartTime": "07:00:00",//O której godzinie mają zostać wysyłane powiadomienia
        "Occurs": "2",//Ile razy powtórzyć powiadomienie
        "Interval": "6:00:00"//Co ile ma być wysyłana powtórka powiadomienia
      }
    },
    {
      "readerType": "exitReader",//Typ: readera, który wysyła wiadomości, że usługa została wyłączona
      "readerId": 4,//Id musi być unikalne
      "logicalName": "NN"//Ta nazwa jest wyświetlana podczas wysyłki do klienta. Zbiorcza dla wszystkich readerów
    }
    //{
    //  "readerType": "fileReader",
    //  "readerId": 3,
    //  "readerName": "do odczytu danych z pliku x",
    //  "logicalName": "",
    //  "columnNames": [ "Id", "MessageType", "ApplicationName", "CreationDate", "CreationUser", "Exception", "Message" ],
    //  "initialCounter": 0,
    //  "readerAdditionalParams": {
    //    "fileName": "abc",
    //    "tableName": "Logs"
    //  }
    //}
  ],
  "clients": [//Ta sekcja pozwala na tworzenie klientów do których są wysyłane wiadomości
    {
      "clientType": "slackClient",//Typ: slack. Wtedy wiadomości są wysyłane na slacka
      "clientId": 1,//Id musi być unikalne
      "clientAdditionalParams": {
        "userName": null,//Można podać nazwę użytkownika
        "channel": null,//Możemy podać kanał
		"extractTheHeader": true,//Pozwala na wyciągnięcie agregacji wiadomości oraz tytuł, czyli LogicalName z którego jest wiadomość nad załącznik
        "url": "https://hooks.slack.com/services/T45HP07HP",//WebHook na który zostanie wysłana wiadomość
        /** Do poprawnego działania trzeba dodać w slacku integracje webhook: https://nazwaslacka.com/apps/new/A0F7XDUAZ-incoming-webhooks **/
        "configPath": "C:\\ExceptionNotifier\\SlackConfig.json" //Ścieżka do pliku z konfiguracją kolorów w slacku
      }
    },
    //{
      //"clientType": "fileClient",
      //"clientId": 2,
      //"clientAdditionalParams": {
      //  "filename": "moj-plik.txt"
      //}
    //},
    {
      "clientType": "slackClient",
      "clientId": 3,
      "clientAdditionalParams": {
        "userName": null,
        "channel": "pinglivingexceptions",
		"extractTheHeader": false,
        "url": "https://hooks.slack.com/services/T45HP07HP",
        "configPath": "C:\\ExceptionNotifier\\SlackConfig.json"
      }
    },
    {
      "clientType": "MailClient",//Typ: mail. Wtedy wiadomości są wysyłane na skrzynkę pocztową
      "clientId": 4,
      "clientAdditionalParams": {
        "configPath": "C:\\ExceptionNotifier\\MailConfig.json"//Ścieżka do pliku konfiguracyjnego
      }
    }
  ],
  "filters": [//Ta sekcja pozwala na filtrowanie wiadomości
    {
      "filterId": 1,//Id musi być unikalne
      "filterParams": [
        {
          "columnName": "Message",//Kolumna która ma być filtrowana
          "patterns": [ "Start DataCollector", "null" ],//Jeżeli zawiera taki ciąg znaków to nie wysyłaj wiadomości do klienta
		  "matchPatternOperator": "=" //Pozwala na wybranie wzorca filtrowania, domyślnie filter działa na zasadzie zawiera. Jeżeli wartość zawiera dane wyrażenie to nie wysyłaj jej. Dodatkowo można wybrać następujące operatory: "=", ">=", "<=", ">", "<" np. dla ">", jeżeli wiadomość ma większą liczbę niż we wzorcu to nie wysyłaj wiadomości. Operatory ">", "<", ">=", "<=" działają tylko dla kolumn z wartościami liczbowymi inaczej zostanie zalogowany błąd
        },
        {
          "columnName": "MessageType",
          "patterns": [ "WARN", "INFO" ]
        }
      ]
    }
  ],
  "aggregators": [//Ta sekcja pozwala na agregowanie wiadomości
    {
      "aggregatorId": 1,//Id musi być unikalne
      "aggregationParams": [
        {
          "messageAggregateColumnNames": [ "ApplicationName", "Message" ]//Jeżeli pobrane wiadomości mają te same wartości dla podanych kolumn dodawane jest pole do wiadomości ile razy wystąpiła wiadomość w przeciągu pobrania, które definiuje interwał w readerze
        }
      ]
    }
  ],
  "rules": [//Ta sekcja pozwala na konfiguracje powiazań klient-reader
    {
      "clientId": 1,
      "readerId": [ 1, 2 ]
    },
    {
      "clientId": 3,
      "readerId": [ 3, 4 ]
    }
  ],
  "filterRules": [//Ta sekcja pozwala na konfiguracje powiazań filter-reader
    {
      "filterId": 1,
      "readerId": [ 1, 2 ]
    }
  ],
  "aggregatorRules": [//Ta sekcja pozwala na konfiguracje powiazań agregator-reader
    {
      "aggregatorId": 1,
      "readerId": [ 1, 2 ]
    }
  ]
}
```
4. MailConfig.json
```
{
  "Host": "owa.poczta.pl",//Adres hosta z którego wysyłamy mail
  "Port": 25,//Port hosta
  "EnableSsl": true,//Włączony certyfikat ssl
  "UserName": "exceptionnotifier",//Nazwa użytkownika dla hosta
  "Password": "M@il$ervice!1",//Hasło dla użytkownika, którego wykorzystujemy podczas wysyłki. Trzeba użyć programu "CryptoEncoder" do zaszyfrowania hasła. Inaczej nie będzie działać
  "From": "ExceptionNotifier@poczta.pl",//Adres wysyłającego
  "To": [ "dbrzeczek@poczta.pl", "serwis@poczta.pl" ],//Adresy odbiorców
  "CC": [ "lista adresów" ],//Kopia do odbiorców
  "Bcc": [ "lista adresów" ],//Ukryta kopia do odbiorców
  "Subject": "ExceptionNotifier",//Temat
  "Body": "Na bazie został zalogowany $Message.Name$: $Message.Value$",//Ciało maila, aby odwołać się do warrtości z kolumny podajemy ciąg $NazwaKolumny.Value$, jeżeli chcemy podać nazwę kolumny podajemy $NazwaKolumny.Name$, aby się odwołać do PingReadera podczas jego definiowania podajemy $Living Message.Value$ analogicznie dla ExitReadera $Exit Message.Value$
  "IsBodyHtml": true//Włączone znaczniki html w treści maila
}
```
5. SlackConfig.json
```
{
  "ERROR": "danger",//Podajemy nazwę i wartość koloru, gdy wskażemy kolumnę "MessageType" i będzie ona miała wartość "ERROR" w wiadomości to wtedy w slacku wiadomość zostanie wysłana z kolorem czerwonym
  "WARN": "warning",
  "FATAL": "#000000",
  "DEFAULT": "#439FE0"//Domyślny kolor wiadomości, gdy powyższe nazwy nie zostaną znalezione dla kolumny "MessageType"
}
```

# Installation

Żeby zainstalować usługę należy uruchomić konsolę, następnie wpisać np.
```
C:/ExceptionNotifier/ExceptionNotifier.exe install
```

# Uninstallation

Żeby odinstalować usługę należy uruchomić konsolę, następnie wpisać np.
```
C:/ExceptionNotifier/ExceptionNotifier.exe uninstall
```

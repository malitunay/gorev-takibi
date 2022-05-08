## Week - 4

## Homework 

İş Yönetim Sistemi


## Homework Description

- Login : E-Mail ve parola ile yapılır. (Veritabanında tutulan parolalar MD5 ile şifrelidir)
- İsterler gereği register işlemi yapılmamıştır. Kullanıcılar "Admin" veya "Yönetici" rolündeki kullanıcılar tarafından eklenir.
- Yeni kullanıcı kaydı yapıldığında; e-mail adresine 6 haneli random üretilmiş bir parola gönderilir ve bu parolanın md5 ile şifrelenmiş hali "Users" tablosuna kaydedilir.
- 3 farklı rol vardır : Admin , Yönetici , Personel. Roles ve Users tabloları arasında bire çok ilişki vardır.
- Kullanıcılar parolalarını güncelleyebilir. Bu işlem sırasında MD5 encrypt ve decrypt işlemleri yapılarak eski şifre kontolünden geçen talep veritabanına kaydedilir.
- Request oluşturma : Request oluşturulurken başlık, departman, öncelik, içerik gibi bilgiler girilir. Request'i oluşturan kullanıcının kimliği "ReporterId", requestte belirtilen talebi yapacak kullanıcı kimliği "AssigneeId" olarak kaydedilir. Request oluşturulduğunda assignee user olmayacağı için, request varsayılan olarak "Unassigned User Id : 4" kullanıcısına assign edilir.
- İş Atama (Take Request) : Login olan kullanıcı, bağlı olduğu departmana açılmış olan requestleri görebilir ve bunlardan istediği requesti yapmak için kendi üzerine alabilir. Bu işlemde sadece unassigned request'ler alınabilir. (Yani request'in AssignedId değeri 4 olanlar)
- İş Listeleme : Login olan personel, bağlı olduğu departmana açılan requestleri listeleyebilir. Admin ve Yönetici rolündeki kullanıcılar ise tüm requestleri, belli bir departmana açılmış requestleri veya belli bir kullanıcıya açılmış requestleri listeleyebilir. Listelemelerde; request ile ilgili olarak konu, içerik, reporter user, assignee user, tarih, departman, öncelik gibi veriler getirilir.
- İş Üzerinden Yazışma : Request'e ait Reporter User ile Assignee User arasında mesajlaşma yapılabilir. Bunların dışındaki kullanıcılar mesajları göremez ve mesajlaşmaya dahil olamaz. Mesaj yazmak veya görmek isteyen kullanıcı ilgili requestte Reporter veya Assignee ise Sender veya Receiver olabilir.
- İş Detaylarının Görüntülenmesi : Admin veya Yönetici bir requestin mesajlaşmalar dahil tüm detaylarını görebilir.

## Database 

![](https://github.com/GelecekVarlik-FullStack-Bootcamp/odev-hafta4-dnet-malitunay/blob/main/diagram.png)

## Methods

#### 1. Oturum Açma (Account/Login)

Varsayılan olarak oluşturulan "Admin" rolündeki kullanıcı ile giriş yapılır.
http://localhost:40183/api/Account/Login

    {
          "email": "at@g.com",
          "password": "123"
    }
  
Başarılı Sonuç Örneği:

	{
    "message": "Token Üretildi.",
    "statusCode": 200,
    "data": {
        "dtoLoginUser": {
            "id": 2,
            "name": "Ali",
            "surname": "Tunay",
            "email": "at@g.com",
            "departmentId": 1,
            "roleId": 1,
            "roleName": "Admin"
        },
        "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhdEBnLmNvbSIsImp0aSI6IjIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIm5iZiI6MTY1MjAwNTA1NSwiZXhwIjoxNjUyMDkxNDU1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQwMTgzIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MDE4MyJ9.4gHAjP0WvO3FXxjaU8sk3_JImNnSSwPyF3nnoJIw0S8"
    }
	}
#### 2. Kullanıcı Ekleme (User/AddUser)

Admin veya Yönetici rollerindeki kullanıcılar kullanıcı ekleyebilir.
http://localhost:40183/api/User/AddUser

	{
  	"id": 0,
  	"name": "string",
  	"surname": "string",
  	"email": "string",
  	"departmentId": int,
  	"roleId": int,
  	"telephone": "string"
	}
  
Başarılı Sonuç Örneği:

	{
    "message": "Success",
    "statusCode": 200,
    "data": {
        "id": 45,
        "name": "Zeynep",
        "surname": "Tunay",
        "email": "malitunay5@gmail.com",
        "password": "ETt7CdWGn3Q=",
        "departmentId": 2,
        "roleId": 3,
        "telephone": "123123123"
   	 }
	}
#### 3. Şifre Yenileme (User/AddUser)

Sisteme eklenen kullanıcı, e-mail adresine gelen parola ile oturum açtıktan sonra şifresini değiştirebilir. Eski şifre kontrolü yapılır.
http://localhost:40183/api/User/ChangePassword

	{
  	"oldPassword": "string",
  	"newPassword": "string"
	}
  
Başarılı Sonuç Örneği:

	{
    "message": "Parolanız başarılı bir şekilde güncellendi.",
    "statusCode": 200,
    "data": {
        "id": 45,
        "email": "malitunay5@gmail.com",
        "oldPassword": "117888",
        "newPassword": "123"
		}
	}

#### 4. Request Oluşturma (Request/CreateRequest)

Sistemdeki tüm kullanıcılar departman id göndererek request açabilir.
http://localhost:40183/api/Request/CreateRequest

	{
  	"id": 0,
  	"requestNo": "string",
  	"subject": "string",
  	"departmentSubject": "string",
  	"startDate": "string",
  	"endDate": "string",
  	"description": "string",
  	"departmentId": int,
  	"priorityId": int
	}
  
Başarılı Sonuç Örneği: (AssigneeId değeri geri dönmez, veritabanında unassigned user olarak yani Id alanı 4 olarak yazılır.)

	{
    "message": "Success",
    "statusCode": 200,
    "data": {
        "id": 0,
        "requestNo": "SD-123",
        "subject": "Webi E-Mail List Güncelleme",
        "departmentSubject": "BO",
        "startDate": "2022-05-08T08:18:40.107Z",
        "endDate": "2022-05-09T08:18:40.107Z",
        "description": "Webi E-Mail List Güncellemenmesini istiyorum",
        "reporterId": 44,
        "departmentId": 1,
        "priorityId": 1,
        "departments": null
    	}
	}

#### 5. Departmana Göre Request Listeleme (Request/GetListByDepartmentOfUserId)

Login olan kullanıcının bağlı olduğu departmana açılmış olan requestleri listeler. Parametre almaz. Login olan kullanıcının bilgilerini otomatik olarak çeker ve listeleme işlemini kullanıcı bilgilerinden giderek onun departmanına göre yapar.
http://localhost:40183/api/Request/GetListByDepartmentOfUserId

Başarılı Sonuç Örneği:

	{
    "message": "Success",
    "statusCode": 200,
    "data": [
        {
            "id": 12,
            "requestNo": "string",
            "subject": "string",
            "departmentSubject": "string",
            "startDate": "2022-05-03T00:00:00",
            "endDate": "2022-05-03T00:00:00",
            "description": "string",
            "assigneeId": 2,
            "reporterId": 3,
            "departmentId": 1,
            "priorityId": 1
        },
        {
            "id": 15,
            "requestNo": "string",
            "subject": "string",
            "departmentSubject": "string",
            "startDate": "2022-05-03T00:00:00",
            "endDate": "2022-05-03T00:00:00",
            "description": "string",
            "assigneeId": 3,
            "reporterId": 3,
            "departmentId": 1,
            "priorityId": 1
        }
    ]
	}

#### 6. Request'i Üzerine Alma (Request/TakeRequest)

Login olan kullanıcı kendi departmanına açılmış olan requesti üzerine alabilir. Parametre olarak ilgili request'in id değeri gönderilmelidir. Login olan kullanıcının id değeri ilgili requestin "AssigneeId" alanına yazılır. Kullanıcı sadece başka bir kullanıcıya assign edilmemiş requestleri kendi üzerine alabilir.
http://localhost:40183/api/Request/TakeRequest?id=REQUESTID

Başarılı Sonuç Örneği: (AssigneeId : 45)

	{
    "message": "Success",
    "statusCode": 200,
    "data": {
        "id": 10,
        "requestNo": "string",
        "subject": "string",
        "departmentSubject": "string",
        "startDate": "2022-05-03T00:00:00",
        "endDate": "2022-05-03T00:00:00",
        "description": "string",
        "assigneeId": 45,
        "reporterId": 3,
        "departmentId": 3,
        "priorityId": 1
    }
	}

#### 7. Belli Bir Kullanıcıya Assign Edilmiş Requestleri Listeleme (Request/CreateRequest)

Admin veya Yönetici rolündeki kullanıcılar belli bir kullanıcıya assign edilmiş requestleri listeleyebilir. Parametre olarak ilgili kullanıcının User Id değeri gönderilir.
http://localhost:40183/api/Request/GetOwnListByAssigneeId?id=USERID

Başarılı Sonuç Örneği: (Id değeri 45 olan kullanıcıya Assign edilmiş requestler)

	{
    "message": "Success",
    "statusCode": 200,
    "data": [
        {
            "id": 10,
            "requestNo": "string",
            "subject": "string",
            "departmentSubject": "string",
            "startDate": "2022-05-03T00:00:00",
            "endDate": "2022-05-03T00:00:00",
            "description": "string",
            "assigneeId": 45,
            "reporterId": 3,
            "departmentId": 3,
            "priorityId": 1
        },
        {
            "id": 33,
            "requestNo": "SD-123",
            "subject": "Webi E-Mail List Güncelleme",
            "departmentSubject": "BO",
            "startDate": "2022-05-08T00:00:00",
            "endDate": "2022-05-09T00:00:00",
            "description": "Webi E-Mail List Güncellemenmesini istiyorum",
            "assigneeId": 45,
            "reporterId": 44,
            "departmentId": 2,
            "priorityId": 1
        }
    ]
	}

#### 8. Request Üzerinden Mesaj Gönderme (Message/SendMessage)

Assign edilmiş bir request üzerinden requst'i açan Reporter User ile Assignee User mesajlaşabilir. İlgili request'in Reporter User ve Assignee User ları Messages tablosunda Sender veya Receiver olarak kaydedilir. 
Sender; o anda login olan kullanıcıdır. Bu kullanıcının ilgili request'te assignee veya reporter olup olmadığı kontrol edilir. Eğer ikisinden birisi ise Messages tablosunda SenderId alanına o kullanıcının id değeri yazılır. 
Receiver; ReceiverId alanına ise SenderId olan kullanıcının reporter veya assignee olmadığı id değeri yazılır. Örneğin; mesaj göndermek isteyen kullanıcı reporter ise ReceiverId alanına ilgili request'in AssigneeId değeri yazılır. 

http://localhost:40183/api/Message/SendMessage

	{
  	"id": 0,
  	"senderId": int,
  	"receiverId": int,
  	"messagge": "string",
  	"requestId": int,
  }
  
  Başarılı Sonuç Örneği: (Id değeri 33 olan requestin Assignee ve Reporter User'ları arasında mesajlaşma)
  
  	{
    "message": "Success",
    "statusCode": 200,
    "data": {
        "id": 0,
        "senderId": 45,
        "receiverId": 44,
        "messagge": "Raporun tam adını yazabilir misiniz",
        "requestId": 33,
        "sendingDate": "2022-05-08T11:55:13.0041917+03:00"
    }
	}
  
#### 9. Request'in Detaylarını Görüntüleme (Request/GetRequestDetail)

Kullanıcılar belli bir request'in tüm detaylarını, mesajlaşmalar ile birlikte görüntüleyebilir. Parametre olarak ilgili request'in Id değeri gönderilmelidir.

http://localhost:40183/api/Request/GetRequestDetail?id=REQUESTID

Başarılı Sonuç Örneği:

	{
    "message": "Success",
    "statusCode": 200,
    "data": {
        "dtoRequest": {
            "id": 33,
            "requestNo": "SD-123",
            "subject": "Webi E-Mail List Güncelleme",
            "departmentSubject": "BO",
            "startDate": "2022-05-08T00:00:00",
            "endDate": "2022-05-09T00:00:00",
            "description": "Webi E-Mail List Güncellemenmesini istiyorum",
            "assigneeId": 45,
            "reporterId": 44,
            "departmentId": 2,
            "priorityId": 1
        },
        "messagesOfRequest": [
            {
                "id": 5,
                "senderId": 45,
                "receiverId": 44,
                "messagge": "Raporun tam adını yazabilir misiniz",
                "requestId": 33,
                "sendingDate": "2022-05-08T11:55:13.0041917"
            },
            {
                "id": 6,
                "senderId": 44,
                "receiverId": 45,
                "messagge": "Konsolide Gelir Raporu",
                "requestId": 33,
                "sendingDate": "2022-05-08T13:33:41.8041543"
            }
        ]
    }
	}

## How to Use

1. Uygulama dosyalarını locale indirin.
2. Veritabanı yedeğini restore edin.
3. appsettings.json dosyasında ConnectionStrings > SqlServer > Server alanına veritabanını restore ettiğiniz local bilginizi yazın.

## Author

- Mehmet Ali TUNAY


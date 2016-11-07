# NotificationService

##Sync mobile apps example
POST api/v1/sync/start
```
{
	"Id" : "Guid in your system",
	"Certificate" : "ios certificate file in base64 string",
	"CertificatePassword": "ios certificate password",
	"AndroidSenderId": "android sender id",
	"AuthToken": "android auth token",
	"Name" : "app name"
}

```

##Queue gcm notification
POST api/v1/send/gcm
Request body
```
{
  "Payload" : "Msg payload",
  "Tokens" : ["android token"],
  "MobileAppId": "Guid in your system"
}
```
Response is expired tokens
##Queue apple notification
POST api/v1/send/apple
Request body
```
{
  "Payload" : "Msg payload",
  "Token" : "apple token",
  "MobileAppId": "Guid in your system"
}

```
Response is expired tokens

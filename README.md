# KlickHealthApp
 Simple CRUD operation for Klick Health Assignment.


### Get user by user id
URL: http://localhost:34781/api/Users/{id}
Example: http://localhost:34781/api/Users/1



### Get user by Status
URL: http://localhost:34781/api/users/status/{true or false}
Example: http://localhost:34781/api/users/status/true


### Insert request
URL: http://localhost:34781/api/Users/CreateUser
Method: POST

```json
{  
  "name": "Rajveer Singh",
  "emailAddress": "rajveersinghsaini@gmail.com",
  "provinceCode": "ON",
  "medicines": [1,3],
  "createdBy": "System"
}
```
### Update request
URL: http://localhost:34781/api/Users/UpdateUser
Method: POST

```json
{
  "Id":1,
  "name": "Rajveer Singh",
  "emailAddress": "rajveersinghsaini@gmail.com",
  "provinceCode": "ON",
  "medicines": [1,3],
  "createdBy": "System"
}
```
### Deactivate User
URL: http://localhost:34781/api/Users/deactivate/{integerValue}
Example: http://localhost:34781/api/Users/deactivate/2

Method: POST

### Deactivate User
URL: http://localhost:34781/api/Users/delete/{integerValue}
Example: http://localhost:34781/api/Users/delete/2
Method: POST

### Filter User based on different criteria
URL: http://localhost:34781/api/Users/FilterUsers
Method: GET
```json
{       
	"provinceCode": "ON",       
	"isActive": true,
	"medicines": [1,2]
}
```
Note: Above properties are optional but atleast one is required.

Developer: Rajveer Singh (www.rajveersinghsaini.com)
# Accra-City-Towns-Api
This api contains information about all the towns in Accra.
# Accra City Api

- [Accra City Api](#accra-city-api)
  - [Create Town](#create-town)
    - [Create Town Request](#create-town-request)
    - [Create Town Response](#create-town-response)
  - [Get Towns](#get-towns)
    - [Get Towns Request](#get-towns-request)
    - [Get Towns Response](#get-towns-response)
  - [Update Town](#update-town)
    - [Update Town Request](#update-town-request)
    - [Update Towns Response](#update-towns-response)
  - [Delete Town](#delete-town)
    - [Delete Town Request](#delete-town-request)
    - [Delete Town Response](#delete-town-response)

## Create Town

### Create Town Request

```js
POST /town
```

```json
{
    "TownName": "Mamprobi",
    "District":"",
    "Category":"",
    "Region":"",
    "Population":"",
    "Latitude": "",
    "Longitude": "",
    "startDateTime": "",
    "NearbyTown": [
        
    ],
    "Notable LandMarks": [
      
    ]
}
```

### Create Town Response

```js
201 Created
```

```yml
Location: {{host}}/town/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
   "TownName": "Vegan Sunshine",
    "District":"",
    "Category":"",
    "Region":"",
    "Population":"",
    "Date of District Establishment":"",
    "Latitude": "Vegan everything! Join us for a healthy Town..",
    "Longitude": "Vegan everything! Join us for a healthy Town..",
    "startDateTime": "2022-04-08T08:00:00",
    "LastModifiedDateTime": "2022-04-08T11:00:00",
    "NearbyTown": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Notable LandMarks": [
        "Cookie"
    ]
}
```

## Get Towns

### Get Towns Request

```js
GET /town/{{id}}
```

### Get Towns Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
 "TownName": "Vegan Sunshine",
    "District":"",
    "Category":"",
    "Region":"",
    "Population":"",
    "Date of District Establishment":"",
    "Latitude": "Vegan everything! Join us for a healthy Town..",
    "Longitude": "Vegan everything! Join us for a healthy Town..",
    "startDateTime": "2022-04-08T08:00:00",
    "LastModifiedDateTime": "2022-04-08T11:00:00",
    "NearbyTown": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Notable LandMarks": [
        "Cookie"
    ]
}
```

## Update Town

### Update Town Request

```js
PUT /town/{{id}}
```

```json
{
   "TownName": "Vegan Sunshine",
    "District":"",
    "Category":"",
    "Region":"",
    "Population":"",
    "Date of District Establishment":"",
    "Latitude": "Vegan everything! Join us for a healthy Town..",
    "Longitude": "Vegan everything! Join us for a healthy Town..",
    "startDateTime": "2022-04-08T08:00:00",

    "NearbyTown": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Notable LandMarks": [
        "Cookie"
    ]
}
```

### Update Towns Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/town/{{id}}
```

## Delete Town

### Delete Town Request

```js
DELETE /town/{{id}}
```

### Delete Town Response

```js
204 No Content
```

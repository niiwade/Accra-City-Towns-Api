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
  "townName": "New Town",
  "category": " ",
  "population": "50",
  "latitude": "334.998",
  "longitude": "223.889",
  "createdAt": "2023-09-29T11:28:35.292Z",
  "nearbyTowns": [
    "nearbyTowns 1",
    "nearbyTowns 2",
    "nearbyTowns 3"
  ],
  "notableLandMarks": [
    "notableLandMarks 1",
    "notableLandMarks 2"
  ],
  "districtId": "b5dcafed-fff1-4326-b3d2-5e53f31c8bee",
  "regionId": "8f531844-4f73-4271-9d40-c98cda9a856f"
}
```

### Create Town Response

```js
201 Created
```

```yml
Location: {{host}}/api/town
```

```json
{
    "statusCode": 201,
    "message": "Town created successfully.",
    "data": {
        "id": "732b8ba7-998b-40aa-bf3e-ff7554c607b6",
        "townName": "The API Town",
        "category": "Premuim",
        "population": 50000,
        "latitude": 334.998,
        "longitude": 223.889,
        "createdAt": "2023-09-29T12:48:01.666856Z",
        "lastModifiedAt": "0001-01-01T00:00:00",
        "nearbyTowns": [
            "nearbyTowns 1",
            "nearbyTowns 2",
            "nearbyTowns 3"
        ],
        "notableLandMarks": [
            "notableLandMarks 1",
            "notableLandMarks 2"
        ],
        "districtId": "b5dcafed-fff1-4326-b3d2-5e53f31c8bee",
        "regionId": "8f531844-4f73-4271-9d40-c98cda9a856f"
    }
}
```

## Get Towns

### Get Towns Request

```js
GET /api/town/town/:id
```

### Get Towns Response

```js
200 Ok
```

```json
{
    "statusCode": 201,
    "message": "Town created successfully.",
    "data": {
        "id": "732b8ba7-998b-40aa-bf3e-ff7554c607b6",
        "townName": "The API Town",
        "category": "Premuim",
        "population": 50000,
        "latitude": 334.998,
        "longitude": 223.889,
        "createdAt": "2023-09-29T12:48:01.666856Z",
        "lastModifiedAt": "0001-01-01T00:00:00",
        "nearbyTowns": [
            "nearbyTowns 1",
            "nearbyTowns 2",
            "nearbyTowns 3"
        ],
        "notableLandMarks": [
            "notableLandMarks 1",
            "notableLandMarks 2"
        ],
        "districtId": "b5dcafed-fff1-4326-b3d2-5e53f31c8bee",
        "regionId": "8f531844-4f73-4271-9d40-c98cda9a856f"
    }
}
```

## Update Town

### Update Town Request

```js
PUT /api/town/:id
```

```json
{
  "townName": "The API Town Updated",
  "category": "Premuim",
  "population": "50000",
  "latitude": "334.998",
  "longitude": "223.889",
  "lastModifiedAt": "2023-09-29T11:46:55.510Z",
  "nearbyTowns": [
    "nearbyTowns 4",
    "nearbyTowns 5",
    "nearbyTowns 6"
  ],
  "notableLandMarks": [
    "notableLandMarks 3",
    "notableLandMarks 4"
  ],
  "districtId": "b5dcafed-fff1-4326-b3d2-5e53f31c8bee",
  "regionId": "8f531844-4f73-4271-9d40-c98cda9a856f"
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
DELETE /api/town/:id
```

### Delete Town Response

```js
204 No Content
```

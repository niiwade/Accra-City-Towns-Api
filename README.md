# Accra-City-Towns-Api
The Accra-City-Towns API is a RESTful web service that offers comprehensive information about towns across various regions. It allows users to retrieve data such as population, geographical coordinates, notable landmarks, demographics, and more for a wide range of towns. Whether you need to research towns for travel planning, business expansion, or demographic analysis, this API provides access to valuable town-specific data to meet your informational needs. Explore and access town details effortlessly with this API to make informed decisions and gain insights into different townships.

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
POST api/town
```

```json
{
  "townName": "API Town",
  "category": "<string>",
  "population": "<integer>",
  "latitude": "<float>",
  "longitude": "<float>",
  "createdAt": "<dateTime>",
  "nearbyTowns": [
    "<string>",
    "<string>"
  ],
  "notableLandMarks": [
    "<string>",
    "<string>"
  ],
  "districtId": "<uuid>",
  "regionId": "<uuid>"
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
  "message": "Town retrieved successfully.",
  "data": {
    "id": "79282fbd-e975-4574-9449-2d5808439754",
    "townName": "Gold Town",
    "category": "",
    "population": 4444444,
    "latitude": 37.421024,
    "longitude": 122.142105,
    "createdAt": "2023-09-27T02:43:16.379Z",
    "lastModifiedAt": "2023-09-27T02:43:16.379Z",
    "nearbyTowns": [
      "nearbyTowns 1",
      "nearbyTowns 2",
      "nearbyTowns 3"
    ],
    "notableLandMarks": [
      "notableLandMarks 1",
      "notableLandMarks 2",
      "notableLandMarks 3"
    ],
    "districtId": "3cc068fd-e22d-4dd2-81fb-8775de3d2fe9",
    "regionId": "7e05c998-d190-4381-bc8a-08d1e41ba624"
  }
}
```

## Get Towns

### Get Towns Request

```js
GET api/town/{{id}}
```

### Get Towns Response

```js
200 Ok
```

```json
{
  "statusCode": 200,
  "message": "Towns retrieved successfully.",
  "data": {
    "towns": [
      {
        "id": "79282fbd-e975-4574-9449-2d5808439754",
        "townName": "Gold Town",
        "category": "",
        "population": 4444444,
        "latitude": 37.421024,
        "longitude": 122.142105,
        "createdAt": "2023-09-27T02:43:16.379Z",
        "lastModifiedAt": "2023-09-27T02:43:16.379Z",
        "nearbyTowns": [
          "nearbyTowns 1",
          "nearbyTowns 2",
          "nearbyTowns 3"
        ],
        "notableLandMarks": [
          "notableLandMarks 1",
          "notableLandMarks 2",
          "notableLandMarks 3"
        ],
        "districtId": "3cc068fd-e22d-4dd2-81fb-8775de3d2fe9",
        "regionId": "7e05c998-d190-4381-bc8a-08d1e41ba624"
      },
      {
        "id": "60cd59d5-b2fe-481b-9bc9-024310a4e7b6",
        "townName": "Sliver-Town",
        "category": "",
        "population": 50000,
        "latitude": 334.998,
        "longitude": 223.889,
        "createdAt": "2023-09-29T11:48:42.919355Z",
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
    ]
  }
}
```

## Update Town

### Update Town Request

```js
PUT /town/{{id}}
```

```json
{
  "townName": "API Town",
  "category": "<string>",
  "population": "<integer>",
  "latitude": "<float>",
  "longitude": "<float>",
  "lastModifiedAt": "<dateTime>",
  "nearbyTowns": [
    "<string>",
    "<string>"
  ],
  "notableLandMarks": [
    "<string>",
    "<string>"
  ],
  "districtId": "<uuid>",
  "regionId": "<uuid>"
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
DELETE api/town/{{id}}
```

### Delete Town Response

```js
204 No Content
```

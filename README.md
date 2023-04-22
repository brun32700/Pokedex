# This is a CRUD REST API for a Pokedex made using .NET 6

## How To Run the REST API?

In terminal clone the repository using the following command: `git clone https://github.com/brun32700/Pokedex` then run the project using: `dotnet run --project Pokedex`.

# Pokedex API Definition

- [Pokedex API](#pokedex-api)
  - [Create Pokemon](#create-pokemon)
    - [Create Pokemon Request](#create-pokemon-request)
    - [Create Pokemon Response](#create-pokemon-response)
  - [Get Pokemon](#get-pokemon)
    - [Get Pokemon Request](#get-pokemon-request)
    - [Get Pokemon Response](#get-pokemon-response)
  - [Update Pokemon](#update-pokemon)
    - [Update Pokemon Request](#update-pokemon-request)
    - [Update Pokemon Response](#update-pokemon-response)
  - [Delete Pokemon](#delete-pokemon)
    - [Delete Pokemon Request](#delete-pokemon-request)
    - [Delete Pokemon Response](#delete-pokemon-response)
  - [Get All Pokemon](#get-all-pokemon)
    - [Get All Pokemon Request](#get-all-pokemon-request)
    - [Get All Pokemon Response](#get-all-pokemon-response)

## Create Pokemon

### Create Pokemon Request

```js
POST /pokedex
```

```json
{
    "name": "Bulbasaur",
    "pokedexId": 1,
    "description": "There is a plant seed on its back right from the day this Pokémon is born. The seed slowly grows larger.",
    "type": [
        "Grass",
        "Poison"
    ],
    "Weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
    ]
}
```

### Create Pokemon Response

```js
201 Created
```

```yml
Location: {{host}}/Pokemons/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Bulbasaur",
    "pokedexId": 1,
    "description": "There is a plant seed on its back right from the day this Pokémon is born. The seed slowly grows larger.",
    "type": [
        "Grass",
        "Poison"
    ],
    "Weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
    ],
    "lastModifiedDateTime": "2023-01-01T12:00:00"
}
```

## Get Pokemon

### Get Pokemon Request

```js
GET /pokedex/{{id}}
```

### Get Pokemon Response

```js
200 OK
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Bulbasaur",
    "pokedexId": 1,
    "description": "There is a plant seed on its back right from the day this Pokémon is born. The seed slowly grows larger.",
    "type": [
        "Grass",
        "Poison"
    ],
    "Weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
    ],
    "lastModifiedDateTime": "2023-01-01T12:00:00"
}
```

## Update Pokemon

### Update Pokemon Request

```js
PUT /pokedex/{{id}}
```

```json
{
    "name": "Bulbasaur",
    "pokedexId": 1,
    "description": "While it is young, it uses the nutrients that are stored in the seed on its back in order to grow.",
    "type": [
        "Grass",
        "Poison"
    ],
    "Weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
    ]
}
```

### Update Pokemon Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Pokemons/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Bulbasaur",
    "pokedexId": 1,
    "description": "While it is young, it uses the nutrients that are stored in the seed on its back in order to grow.",
    "type": [
        "Grass",
        "Poison"
    ],
    "Weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
    ],
    "lastModifiedDateTime": "2023-01-01T12:00:00"
}
```

## Delete Pokemon

### Delete Pokemon Request

```js
DELETE /pokedex/{{id}}
```

### Delete Pokemon Response

```js
204 No Content
```

## Get All Pokemon

### Get All Pokemon Request

```js
GET /pokedex
```

### Get All Pokemon Response

```js
200 OK
```

```json
{
  "pokemon": [
    {
      "id": "5cfcc4c3-cf1b-423f-ae9b-5fa378b8ca34",
      "name": "Bulbasaur",
      "pokedexId": 1,
      "description": "There is a plant seed on its back right from the day this Pokémon is born. The seed slowly grows larger.",
      "type": [
        "Grass",
        "Poison"
      ],
      "weaknesses": [
        "Fire",
        "Psychic",
        "Flying",
        "Ice"
      ],
      "lastModifiedDateTime": "2023-04-20T13:29:41.1347017Z"
    },
    {
      "id": "9e9eec63-c03d-4abb-824f-e4293c57ee79",
      "name": "Charmander",
      "pokedexId": 4,
      "description": "It has a preference for hot things. When it rains, steam is said to spout from the tip of its tail.",
      "type": [
        "Fire"
      ],
      "weaknesses": [
        "Water",
        "Ground",
        "Rock"
      ],
      "lastModifiedDateTime": "2023-04-20T13:29:43.5961377Z"
    }
  ]
}
```


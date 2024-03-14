# ForeignExchangeRate

ForeignExchangeRate is a web app developed in ASP.NET core 8. This web app implements a solution to persist the bid and ask rates values of foreign exchange rates between a given origin and target currencies, retrieved from an external provider.
When a new exchange rate is retrieved, and the Kafka event is published.
The App exposes CRUD operations to manage the foreign exchange rates.

## Project dependencies

This project depends on dependencies such as:
* MongoDB
* Zookepper
* Kafka
* Docker

## Requirements
You must have the docker installed.

## Installation

Run the command on the root directory.

```bash
docker-compose up --build -d
```
The command will create a Docker container with the needed dependencies. 

## Usage

The available options are Get, Delete, and Patch.
For now, doesn't have a post or put operation because only the get makes sense. However, once the rate isn't updated after persisting, we have the patch and delete to manually update or delete to force the update on the next get operation.
```c#
# GetForeignExchangeRate
'GET https://localhost:44318/api/ForeignExchangeRates?fromCurrency=EUR&toCurrency=USD'

# DeleteForeignExchangeRate
'DELETE https://localhost:44318/api/ForeignExchangeRates?fromCurrency=USD&toCurrency=EUR'

# PatchForeignExchangeRate
'PATCH https://localhost:44318/api/ForeignExchangeRates'
  Body 
  '{
    "fromCurrency": "YXC",
    "toCurrency": "XUW",
    "bid": 0,
    "ask": 0
  }'
```
